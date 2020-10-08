﻿Example 9 - ChangeLinkTypesPackage

namespace TFSExt.ChangeLinkTypes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using Microsoft.TeamFoundation.Controls;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.TeamFoundation.WorkItemTracking;
    using Microsoft.VisualStudio.TeamFoundation.WorkItemTracking.Extensibility;

    using tfsprod;

    public static class ChangeLinkTypesPackage
    {
        #region Methods

        public static int ChangeLinkTypes(int[] qdata, WorkItemLinkType fromlink, WorkItemLinkType tolink)
        {
            int changedlinks = 0;

            foreach (int itm in qdata)
            {
                if (itm == 0) continue;

                WorkItem wi = Utilities.wistore.GetWorkItem(itm);
                foreach (var link in wi.Links.OfType<RelatedLink>().Where(x => x.LinkTypeEnd.LinkType.ReferenceName == fromlink.ReferenceName).ToList())
                {
                    WorkItemLinkTypeEnd linkTypeEnd = Utilities.wistore.WorkItemLinkTypes.LinkTypeEnds[(link.LinkTypeEnd.IsForwardLink ? tolink.ForwardEnd.Name : tolink.ReverseEnd.Name)];
                    Utilities.OutputCommandString(string.Format("Updated WorkItemID={0}, OriginalLink={1}, NewLink={2}", wi.Id, link.LinkTypeEnd.Name, linkTypeEnd.Name));

                    if (wi.IsDirty)
                    {
                        try
                        {
                            wi.Save();
                        }
                        catch (Exception ex)
                        {
                            var result = MessageBox.Show(ex.Message, "Failed to save dirty Work Item #" + wi.Id, MessageBoxButtons.AbortRetryIgnore);
                            Utilities.OutputCommandString(ex.ToString());
                            if (result == DialogResult.Abort) return changedlinks;
                            else continue;
                        }
                    }
                    try
                    {
                        wi.Links.Add(new RelatedLink(linkTypeEnd, link.RelatedWorkItemId));
                        wi.Save();
                        changedlinks++;
                    }
                    catch (Exception ex)
                    {
                        var result = MessageBox.Show(ex.Message, "Failed to add new link to Work Item #" + wi.Id, MessageBoxButtons.AbortRetryIgnore);
                        Utilities.OutputCommandString(ex.ToString());
                        if (result == DialogResult.Abort) return changedlinks;
                        else continue;
                    }
                    try
                    {
                        wi.Links.Remove(link);
                        wi.Save();
                    }
                    catch (Exception ex)
                    {
                        var result = MessageBox.Show(ex.Message, "Failed to remove original link from Work Item #" + wi.Id, MessageBoxButtons.AbortRetryIgnore);
                        Utilities.OutputCommandString(ex.ToString());
                        if (result == DialogResult.Abort) return changedlinks;
                        else continue;
                    }
                }
            }

            return changedlinks;
        }

        public static void CopyChangesetComments(object sender, EventArgs e)
        {
            object _lockToken2 = new object();

            if (DialogResult.No == MessageBox.Show("Are you sure you want to copy the Changeset comments?", Utilities.AppTitle, MessageBoxButtons.YesNo))
                return;

            IWorkItemTrackingDocument doc2 = Utilities.docsrv2.FindDocument(Utilities.dte.ActiveDocument.FullName, _lockToken2);
            if (doc2 == null) return;

            try
            {
                foreach (int i in (doc2 as IResultsDocument).SelectedItemIds)
                {
                    var wi = Utilities.wistore.GetWorkItem(i);
                    if (wi.ExternalLinkCount == 0) continue;

                    wi.Open();
                    foreach (var link in wi.Links.OfType<ExternalLink>())
                    {
                        var ch = Utilities.vcsrv.ArtifactProvider.GetChangeset(new Uri(link.LinkedArtifactUri));
                        link.Comment = ch.Comment;
                    }
                    wi.Save();
                }

                MessageBox.Show("Changesets added sucessfully.", Utilities.AppTitle);
            }
            catch (Exception ex)
            {
                Utilities.OutputCommandString(ex.ToString());
                MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message, Utilities.AppTitle);
            }

            doc2.Release(_lockToken2);
        }

        public static bool ExecuteQueryLinkTypes(QueryDefinition qdef, string ProjectName, out int[] qdata)
        {
            Hashtable context = new Hashtable();
            StringBuilder strb = new StringBuilder();
            List<int> lqdata = new List<int>();

            context.Add("project", ProjectName); //@me, @today are filled automatically
            var query = new Query(Utilities.wistore, qdef.QueryText, context);

            if (query.IsLinkQuery)
            {
                foreach (var wilnk in query.RunLinkQuery())
                {
                    lqdata.Add(wilnk.TargetId);
                    lqdata.Add(wilnk.SourceId);

                    Utilities.OutputCommandString(string.Format("ParentID={0}, WorkItemID={1}", wilnk.SourceId, wilnk.TargetId));
                }
                lqdata = lqdata.Distinct().ToList();
            }
            else
            {
                foreach (WorkItem wi in query.RunQuery())
                {
                    lqdata.Add(wi.Id);

                    Utilities.OutputCommandString(string.Format("WorkItemID={0}, Title={1}", wi.Id, wi.Title));
                }
            }

            qdata = lqdata.ToArray();

            return false;
        }

        public static void LinkChangesetsToWICallback(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Are you sure you want to link the Changesets to the WhorkItems?", Utilities.AppTitle, MessageBoxButtons.YesNo))
                return;

            var chlinktype = Utilities.wistore.RegisteredLinkTypes["Fixed in Changeset"];
            var changesets = Utilities.vcext.History.ActiveWindow.SelectedChangesets.Select(x => Utilities.vcsrv.GetChangeset(x.ChangesetId));
            if (changesets == null || changesets.Count() == 0)
            {
                MessageBox.Show("Select one or more Changesets.", Utilities.AppTitle);
                return;
            }

            var workitems = Utilities.ShowWorkItemPickerDialog();
            if (workitems == null || workitems.Count() == 0)
            {
                MessageBox.Show("Select one or more WorkItems.", Utilities.AppTitle);
                return;
            }

            var isFound = false;
            foreach (var wi in workitems)
            {
                var oldlinks = wi.Links
                    .OfType<ExternalLink>()
                    .Select(x => Utilities.vcsrv.ArtifactProvider.GetChangeset(new Uri(x.LinkedArtifactUri)).ChangesetId)
                    .ToList();

                var chs = changesets.Where(x => !oldlinks.Contains(x.ChangesetId));
                if (chs.Count() == 0) continue;

                wi.Open();
                foreach (Changeset ch in chs)
                {
                    isFound = true;
                    wi.Links.Add(new ExternalLink(chlinktype, ch.ArtifactUri.AbsoluteUri)
                    {
                        Comment = ch.Comment
                    });
                }
                wi.Save();
            }

            MessageBox.Show(isFound ? "Links added sucessfully." : "No links are found.", Utilities.AppTitle);
        }

        public static void QueryLinkTypesCallback(object sender, EventArgs e)
        {
            //IntPtr hier;
            //uint itemid;
            //IVsMultiItemSelect dummy;
            //string canonicalName;
            bool bcanceled;
            int icanceled;
            string OperationCaption = "Change Work Items Link Types";
            IVsThreadedWaitDialog2 dlg = null;

            var lkdlg = new EditLinkTypeDialog(Utilities.wistore);
            var dlgResult = lkdlg.ShowDialog();
            if (dlgResult != DialogResult.OK) return;

            var origCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                dlg = Utilities.CreateThreadedWaitDialog(OperationCaption, "Parsing query...", "status", 100);
                dlg.UpdateProgress(OperationCaption, "Parsing query...", "status", 1, 100, true, out bcanceled);

                var WIQueriesPageExt = Utilities.teamExplorer.CurrentPage.GetService<IWorkItemQueriesExt>();
                var qItem = WIQueriesPageExt.SelectedQueryItems.First();

                int[] qdata;
                int changedlinks = 0;
                bcanceled = ExecuteQueryLinkTypes(qItem as QueryDefinition, qItem.Project.Name, out qdata);
                dlg.UpdateProgress(OperationCaption, "Changing Link Types...", "status", 1, 100, false, out bcanceled);

                if (!bcanceled) changedlinks = ChangeLinkTypes(qdata, lkdlg.fromlink, lkdlg.tolink);

                dlg.EndWaitDialog(out icanceled);

                if (!bcanceled) MessageBox.Show(changedlinks + " links were changed.", Utilities.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dlg.EndWaitDialog(out icanceled);
                Cursor.Current = origCursor;
            }
            catch (Exception ex)
            {
                if (dlg != null) dlg.EndWaitDialog(out icanceled);
                Cursor.Current = origCursor;
                Utilities.OutputCommandString(ex.ToString());
                MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message, Utilities.AppTitle, MessageBoxButtons.OK);
            }
        }

        #endregion Methods
    }
}
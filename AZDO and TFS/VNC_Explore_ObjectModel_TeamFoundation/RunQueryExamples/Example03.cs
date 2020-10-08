namespace TFSOnlineBIData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.Discussion.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class CodeReviewComment
    {

        #region Properties

        public string Author
        {
            get; set;
        }

        public string Comment
        {
            get; set;
        }

        public string ItemName
        {
            get; set;
        }

        public string PublishDate
        {
            get; set;
        }

        #endregion Properties
    }

    class Program
    {
        #region Fields

        const string URL_TO_TFS_COLLECTION = "https://decos.visualstudio.com/DefaultCollection";

        #endregion Fields

        #region Methods

        private static void AddTaskToResult(WorkItem workitem, WorkItemStore store, List<WorkItem> result)
        {
            try
            {
                string sTitle = "|" + workitem.Title.ToUpper() + "|";
                if (("|ISSUE|INTERNAL SUPPORT|INTERNALSUPPORT|").IndexOf(sTitle) >= 0)
                {
                    List<WorkItem> lstTasks = GetTasks(store, workitem);
                    foreach (var task in lstTasks)
                        result.Add(task);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Adding Tasks" + ex);
            }
        }

        static void CallCompletedCallback(IAsyncResult result)
        {
            // Handle error conditions here
        }

        private static List<WorkItem> GetChildWorkItems(WorkItemStore store, WorkItem workitem, string queryText)
        {
            //Ref. http://blogs.msdn.com/b/jsocha/archive/2012/02/22/retrieving-tfs-results-from-a-tree-query.aspx and
            //http://blogs.msdn.com/b/team_foundation/archive/2010/07/02/wiql-syntax-for-link-query.aspx
            //https://msdn.microsoft.com/en-us/library/bb130306(v=vs.120).aspx

            List<WorkItem> details = null;

            try
            {
                int id = workitem.Id;
                var treeQuery = new Query(store, queryText);
                WorkItemLinkInfo[] links = treeQuery.RunLinkQuery();

                List<int> except = new List<int>();
                except.Add(id);

                //
                // Build the list of work items for which we want to retrieve more information
                //
                int[] ids = (from WorkItemLinkInfo info in links
                             select info.TargetId).Except(except.AsEnumerable()).ToArray();

                if (ids.Length > 0)
                {
                    //
                    // Next we want to create a new query that will retrieve all the column values from the original query, for
                    // each of the work item IDs returned by the original query.
                    //
                    var detailsWiql = new StringBuilder();
                    detailsWiql.AppendLine("SELECT");
                    bool first = true;

                    foreach (FieldDefinition field in treeQuery.DisplayFieldList)
                    {
                        detailsWiql.Append("    ");
                        if (!first)
                            detailsWiql.Append(",");
                        detailsWiql.AppendLine("[" + field.ReferenceName + "]");
                        first = false;
                    }
                    detailsWiql.AppendLine("FROM WorkItems");

                    //
                    // Get the work item details
                    //
                    var flatQuery = new Query(store, detailsWiql.ToString(), ids);
                    details = flatQuery.RunQuery().OfType<WorkItem>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return details;
        }

        private static List<CodeReviewComment> GetCodeReviewComments(int workItemId)
        {
            List<CodeReviewComment> comments = new List<CodeReviewComment>();

            Uri uri = new Uri(URL_TO_TFS_COLLECTION);
            TeamFoundationDiscussionService service = new TeamFoundationDiscussionService();
            service.Initialize(new Microsoft.TeamFoundation.Client.TfsTeamProjectCollection(uri));
            IDiscussionManager discussionManager = service.CreateDiscussionManager();

            IAsyncResult result = discussionManager.BeginQueryByCodeReviewRequest(workItemId, QueryStoreOptions.ServerAndLocal, new AsyncCallback(CallCompletedCallback), null);
            var output = discussionManager.EndQueryByCodeReviewRequest(result);

            foreach (DiscussionThread thread in output)
            {
                if (thread.RootComment != null)
                {
                    CodeReviewComment comment = new CodeReviewComment();
                    comment.Author = thread.RootComment.Author.DisplayName;
                    comment.Comment = thread.RootComment.Content.Replace(",", " COMMA ").Replace(System.Environment.NewLine, " NEWLINE ");
                    comment.PublishDate = thread.RootComment.PublishedDate.ToShortDateString();
                    comment.ItemName = thread.ItemPath;
                    comments.Add(comment);
                }
            }

            return comments;
        }

        private static List<WorkItem> GetReviewRequests(WorkItemStore store, WorkItem workitem)
        {
            int id = workitem.Id;
            string queryText = 
                "SELECT [System.Id]"
                + " FROM WorkItemLinks"
                + " WHERE ([Target].[System.WorkItemType] = 'Code Review Request')"
                + " And ([System.Links.LinkType] = 'Child')"
                + " And ([Source].[System.Id] = '" + id + "')"
                + " mode(MayContain)";

            return GetChildWorkItems(store, workitem, queryText);
        }

        private static List<WorkItem> GetReviewResponses(WorkItemStore store, WorkItem workitem)
        {
            int id = workitem.Id;
            string queryText = 
                "SELECT [System.Id]"
                + " FROM WorkItemLinks"
                + " WHERE([Target].[System.WorkItemType] = 'Code Review Response')"
                + " And ([System.Links.LinkType] = 'Child')"
                + " And ([Source].[System.Id] = '" + workitem.Id + "')"
                + " mode(MayContain)";
            return GetChildWorkItems(store, workitem, queryText);
        }

        private static List<WorkItem> GetTasks(WorkItemStore store, WorkItem workitem)
        {
            int id = workitem.Id;
            string queryText = 
                "SELECT [System.Id]"
                + " FROM WorkItemLinks"
                + " WHERE ([Target].[System.WorkItemType] = 'Task')"
                + " And ([System.Links.LinkType] = 'Child')"
                + " And ([Source].[System.Id] = '" + id + "')"
                + " mode(MayContain)";
            return GetChildWorkItems(store, workitem, queryText);
        }

        private static WorkItem GetWorkItemFromReviewRequest(WorkItemStore store, WorkItem workitem)
        {
            string queryText = 
                "SELECT [System.Id]"
                + " FROM WorkItemLinks"
                + " WHERE ([System.Links.LinkType] = 'Parent')"
                + " And ([Source].[System.Id] = '" + workitem.Id + "')"
                + " mode(MayContain)";
            return GetChildWorkItems(store, workitem, queryText)[0];
        }

        private static string GetWorkItemLine(string sReviewRequestHeaderFormat, WorkItem workitem, List<WorkItem> wicReviewRequests)
        {
            int iReviewRequestCount = 0;
            if (wicReviewRequests != null) iReviewRequestCount = wicReviewRequests.Count;

            string sAssignedTo = workitem.Fields["Assigned To"].Value as string;
            string sTitle = workitem.Title.Replace(",", " COMMA ").Replace(Environment.NewLine, " NEWLINE ");
            string sTag = workitem.Tags.Replace(",", " COMMA ").Replace(Environment.NewLine, " NEWLINE ");

            string sEffort = string.Empty;
            if (workitem.Fields.Contains("Effort"))
            {
                object oEffort = workitem.Fields["Effort"].Value;
                if (oEffort != null) sEffort = oEffort.ToString();
            }
            object oChangedDate = workitem.Fields["Changed Date"].Value;
            string sChangedDate = string.Empty;
            if (oChangedDate != null) sChangedDate = oChangedDate.ToString();

            object oClosedDate = workitem.Fields["Closed Date"].Value;
            string sClosedDate = string.Empty;
            if (oClosedDate != null) sClosedDate = oClosedDate.ToString();

            string sRemainingWork = string.Empty;
            if (workitem.Fields.Contains("Remaining Work"))
            {
                object oRemainingWork = workitem.Fields["Remaining Work"].Value;
                if (oRemainingWork != null) sRemainingWork = oRemainingWork.ToString();
            }
            ////ID, Work Item Type, Title, Assigned To, State, Effort, Tags, Area Path, Changed Date, Closed Date, Created Date, Iteration Path, Remaining Work, ReviewRequestCount {NewLine}
            string sRequestNewLine = string.Format(sReviewRequestHeaderFormat, workitem.Id, workitem.Type.Name, sTitle, sAssignedTo, workitem.State
                                        , sEffort, sTag, workitem.AreaPath, sChangedDate, sClosedDate, workitem.CreatedDate
                                        , workitem.IterationPath, sRemainingWork, iReviewRequestCount, Environment.NewLine);
            return sRequestNewLine;
        }


        #endregion Methods
    }
}

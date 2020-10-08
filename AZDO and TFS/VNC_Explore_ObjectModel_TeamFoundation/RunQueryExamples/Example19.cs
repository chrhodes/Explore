﻿Example 19

namespace JustAProgrammer.TeamPilgrim.VisualStudio.Business.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using JustAProgrammer.TeamPilgrim.VisualStudio.Common;
    using JustAProgrammer.TeamPilgrim.VisualStudio.Common.Extensions;
    using JustAProgrammer.TeamPilgrim.VisualStudio.Domain.BusinessInterfaces;

    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    using NLog;

    public class TeamPilgrimTfsService : ITeamPilgrimTfsService
    {
        #region Methods

        public QueryFolder AddNewQueryFolder(TfsTeamProjectCollection teamProjectCollection, Project teamProject, Guid parentFolderId)
        {
            this.Logger().Trace("AddNewQueryFolder");

            var workItemStore = GetWorkItemStore(teamProjectCollection);
            var queryHierarchy = workItemStore.GetQueryHierarchy(teamProject);
            var loadedParentFolder = queryHierarchy.Find(parentFolderId) as QueryFolder;

            var queryFolder = new QueryFolder("New Folder");

            Debug.Assert(loadedParentFolder != null, "loadedParentFolder != null");
            loadedParentFolder.Add(queryFolder);

            queryHierarchy.Save();

            return queryFolder;
        }

        public IBuildDefinition CloneBuildDefinition(TfsTeamProjectCollection tfsTeamProjectCollection, string projectName, IBuildDefinition sourceDefinition)
        {
            this.Logger().Trace("CloneBuildDefinition");

            var buildServer = tfsTeamProjectCollection.GetService<IBuildServer>();
            var clonedDefinition = buildServer.CreateBuildDefinition(projectName);
            clonedDefinition.CopyFrom(sourceDefinition);
            clonedDefinition.Name = string.Format("Copy of {0}", clonedDefinition.Name);
            clonedDefinition.Save();
            return clonedDefinition;
        }

        public void DeleteBuildDefinition(IBuildDefinition buildDefinition)
        {
            this.Logger().Trace("DeleteBuildDefinition");

            buildDefinition.Delete();
        }

        public bool DeleteQueryItem(TfsTeamProjectCollection tfsTeamProjectCollection, Project teamProject, Guid queryItemId)
        {
            this.Logger().Trace("DeleteQueryItem");

            var workItemStore = GetWorkItemStore(tfsTeamProjectCollection);
            var queryHierarchy = workItemStore.GetQueryHierarchy(teamProject);
            var queryItem = queryHierarchy.Find(queryItemId);

            if (queryItem == null)
                return false;

            queryItem.Delete();
            queryHierarchy.Save();
            return true;
        }

        public void DeleteShelveset(TfsTeamProjectCollection tfsTeamProjectCollection, string shelvesetName, string shelvesetOwner)
        {
            this.Logger().Trace("DeleteShelveset ShelvesetName: {0} ShelvesetOwner: {1}", shelvesetName, shelvesetOwner);

            tfsTeamProjectCollection.GetVersionControlServer().DeleteShelveset(shelvesetName, shelvesetOwner);
        }

        public CheckinEvaluationResult EvaluateCheckin(Workspace workspace, PendingChange[] changes, string comment, CheckinNote checkinNote, WorkItemCheckinInfo[] workItemChanges)
        {
            this.Logger().Trace("EvaluateCheckin");

            return workspace.EvaluateCheckin(CheckinEvaluationOptions.All, changes, changes, comment, checkinNote, workItemChanges);
        }

        public Conflict[] GetAllConflicts(Workspace workspace)
        {
            this.Logger().Trace("GetAllConflicts");

            return workspace.QueryConflicts(workspace.Folders.Select(folder => folder.ServerItem).ToArray(), true);
        }

        public WorkItemCollection GetFlatQueryDefinitionWorkItemCollection(TfsTeamProjectCollection tfsTeamProjectCollection, QueryDefinition queryDefinition, string projectName)
        {
            this.Logger().Trace("GetQueryDefinitionWorkItemCollection QueryType: {0}", queryDefinition.QueryType);

            if (queryDefinition.QueryType != QueryType.List)
                throw new ArgumentException("Flat Queries only");

            var context = new Dictionary<string, string> { { "project", projectName } };
            var workItemStore = GetWorkItemStore(tfsTeamProjectCollection);

            return new Query(workItemStore, queryDefinition.QueryText, context).RunQuery();
        }

        public WorkItemCollection GetLinkQueryDefinitionWorkItemCollection(TfsTeamProjectCollection tfsTeamProjectCollection, QueryDefinition queryDefinition, string projectName)
        {
            WorkItemStore workItemStore;
            Query oneHopQuery;
            var oneHopQueryDefinitionWorkItemLinkInfo = GetLinkQueryDefinitionWorkItemLinkInfo(tfsTeamProjectCollection, queryDefinition, projectName, out workItemStore, out oneHopQuery);
            var ids = oneHopQueryDefinitionWorkItemLinkInfo.Select(info => info.TargetId).Distinct().ToArray();

            var detailsWiql = new StringBuilder();
            detailsWiql.AppendLine("SELECT");
            bool first = true;

            foreach (FieldDefinition field in oneHopQuery.DisplayFieldList)
            {
                detailsWiql.Append(" ");
                if (!first)
                    detailsWiql.Append(",");
                detailsWiql.AppendLine("[" + field.ReferenceName + "]");
                first = false;
            }
            detailsWiql.AppendLine("FROM WorkItems");

            return new Query(workItemStore, detailsWiql.ToString(), ids).RunQuery();
        }

        public WorkItemLinkInfo[] GetLinkQueryDefinitionWorkItemLinkInfo(TfsTeamProjectCollection tfsTeamProjectCollection, QueryDefinition queryDefinition, string projectName)
        {
            WorkItemStore workItemStore;
            Query oneHopQuery;
            return GetLinkQueryDefinitionWorkItemLinkInfo(tfsTeamProjectCollection, queryDefinition, projectName, out workItemStore, out oneHopQuery);
        }

        public WorkspaceInfo[] GetLocalWorkspaceInfo(Guid? projectCollectionId = null)
        {
            if (projectCollectionId != null)
            {
                this.Logger().Trace("GetLocalWorkspaceInfo ProjectCollectionID: {0}", projectCollectionId.Value.ToString());
            }
            else
            {
                this.Logger().Trace("GetLocalWorkspaceInfo");
            }

            IEnumerable<WorkspaceInfo> allLocalWorkspaceInfo = Workstation.Current.GetAllLocalWorkspaceInfo();

            if (projectCollectionId.HasValue)
            {
                allLocalWorkspaceInfo = allLocalWorkspaceInfo.Where(info => info.ServerGuid.Equals(projectCollectionId));
            }

            return allLocalWorkspaceInfo as WorkspaceInfo[] ?? allLocalWorkspaceInfo.ToArray();
        }

        public TfsTeamProjectCollection GetProjectCollection(Uri uri)
        {
            this.Logger().Trace("GetProjectCollection Uri: {0}", uri);

            if (uri == null)
                return null;

            var tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(uri);
            tfsTeamProjectCollection.Authenticate();

            return tfsTeamProjectCollection;
        }

        public TfsTeamProjectCollection[] GetProjectCollections()
        {
            this.Logger().Trace("GetProjectCollections");

            RegisteredProjectCollection[] registeredProjectCollections = GetRegisteredProjectCollections();

            return registeredProjectCollections.Select(collection => new TfsTeamProjectCollection(collection)).ToArray();
        }

        public Project[] GetProjects(Uri tpcAddress)
        {
            return GetProjects(GetProjectCollection(tpcAddress));
        }

        public Project[] GetProjects(TfsTeamProjectCollection tfsTeamProjectCollection)
        {
            this.Logger().Trace("GetProjects ProjectCollection: {0}", tfsTeamProjectCollection.Name);

            var workItemStore = new WorkItemStore(tfsTeamProjectCollection);

            return workItemStore.Projects.Cast<Project>().ToArray();
        }

        public WorkItemCollection GetQueryDefinitionWorkItemCollection(TfsTeamProjectCollection tfsTeamProjectCollection,
            QueryDefinition queryDefinition, string projectName)
        {
            switch (queryDefinition.QueryType)
            {
                case QueryType.List:
                    return GetFlatQueryDefinitionWorkItemCollection(tfsTeamProjectCollection, queryDefinition, projectName);

                case QueryType.OneHop:
                case QueryType.Tree:
                    return GetLinkQueryDefinitionWorkItemCollection(tfsTeamProjectCollection, queryDefinition, projectName);

                case QueryType.Invalid:
                    throw new ArgumentException("Invalid QueryType");
            }

            return null;
        }

        public RegisteredProjectCollection[] GetRegisteredProjectCollections()
        {
            this.Logger().Trace("GetRegisteredProjectCollections");

            return RegisteredTfsConnections.GetProjectCollections();
        }

        public Workspace GetWorkspace(WorkspaceInfo workspaceInfo, TfsTeamProjectCollection tfsTeamProjectCollection)
        {
            this.Logger().Trace("GetWorkspace  ProjectCollection: {0} Workspace: {1}", tfsTeamProjectCollection.Name, workspaceInfo.Name);

            return workspaceInfo.GetWorkspace(tfsTeamProjectCollection);
        }

        public IBuildDefinition[] QueryBuildDefinitions(TfsTeamProjectCollection tfsTeamProjectCollection, string teamProject)
        {
            this.Logger().Trace("QueryBuildDefinitions ProjectCollection: {0} Project: {1}", tfsTeamProjectCollection.Name, teamProject);

            return tfsTeamProjectCollection.GetService<IBuildServer>()
                .QueryBuildDefinitions(teamProject)
                .ToArray();
        }

        public IBuildDetail[] QueryBuildDetails(TfsTeamProjectCollection tfsTeamProjectCollection, string teamProject)
        {
            this.Logger().Trace("QueryBuildDetails ProjectCollection: {0} Project: {1}", tfsTeamProjectCollection.Name, teamProject);

            return tfsTeamProjectCollection.GetService<IBuildServer>()
                .QueryBuilds(teamProject)
                .ToArray();
        }

        public Shelveset[] QueryShelvesets(TfsTeamProjectCollection tfsTeamProjectCollection, string shelvesetName = null, string shelvesetOwner = null)
        {
            this.Logger().Trace("QueryShelvesets ProjectCollection: {0} ShelvesetName: {1} ShelvesetOwner: {2}", tfsTeamProjectCollection.Name, shelvesetName, shelvesetOwner);

            return tfsTeamProjectCollection.GetVersionControlServer().QueryShelvesets(shelvesetName, shelvesetOwner);
        }

        public void WorkspaceCheckin(Workspace workspace, PendingChange[] changes, string comment, CheckinNote checkinNote, WorkItemCheckinInfo[] workItemChanges, PolicyOverrideInfo policyOverride)
        {
            this.Logger().Trace("WorkspaceCheckin");

            workspace.CheckIn(changes, comment, checkinNote, workItemChanges, policyOverride);
        }

        public PendingSet[] WorkspaceQueryShelvedChanges(Workspace workspace, string shelvesetName, string shelvesetOwner, ItemSpec[] itemSpecs)
        {
            return workspace.QueryShelvedChanges(shelvesetName, shelvesetOwner, itemSpecs);
        }

        public void WorkspaceShelve(Workspace workspace, Shelveset shelveset, PendingChange[] pendingChanges, ShelvingOptions shelvingOptions)
        {
            workspace.Shelve(shelveset, pendingChanges, shelvingOptions);
        }

        public Shelveset WorkspaceUnshelve(Workspace workspace, string shelvesetName, string shelvesetOwner, ItemSpec[] items = null)
        {
            this.Logger().Trace("Unshelve ShelvesetName: {0} ShelvesetOwner: {1}, Item Count: {2}", shelvesetName, shelvesetOwner, items == null ? "[null]" : items.Count().ToString());

            return workspace.Unshelve(shelvesetName, shelvesetOwner, items);
        }

        private WorkItemLinkInfo[] GetLinkQueryDefinitionWorkItemLinkInfo(TfsTeamProjectCollection tfsTeamProjectCollection, QueryDefinition queryDefinition, string projectName, out WorkItemStore workItemStore, out Query oneHopQuery)
        {
            this.Logger().Trace("GetQueryDefinitionWorkItemLinkInfo QueryType: {0}", queryDefinition.QueryType);

            if (queryDefinition.QueryType != QueryType.OneHop && queryDefinition.QueryType != QueryType.Tree)
                throw new ArgumentException("Link Queries only");

            var context = new Dictionary<string, string> { { "project", projectName } };
            workItemStore = GetWorkItemStore(tfsTeamProjectCollection);

            oneHopQuery = new Query(workItemStore, queryDefinition.QueryText, context);
            return oneHopQuery.RunLinkQuery();
        }

        private WorkItemStore GetWorkItemStore(TfsTeamProjectCollection tfsTeamProjectCollection)
        {
            this.Logger().Trace("GetWorkItemStore");

            return tfsTeamProjectCollection.GetService<WorkItemStore>();
        }

        #endregion Methods
    }
}
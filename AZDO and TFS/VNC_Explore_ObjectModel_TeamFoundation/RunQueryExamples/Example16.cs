Example 16

namespace Learning.TFS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class TFSManager
    {
        #region Fields

        private WorkItemStore _workItemStore;

        #endregion Fields

        #region Methods

        public void ConnectToTFS()
        {
            var collectionUri = new Uri("http://TFS:8080/TFS/MyCollection");
            var projectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(collectionUri);
            _workItemStore = projectCollection.GetService<WorkItemStore>();
        }

        public WorkItemViewModel GetWorkItem(int id)
        {
            WorkItemViewModel model = null;

            var workItem = _workItemStore.GetWorkItem(id);

            if (workItem != null)
            {
                model = new WorkItemViewModel()
                {
                    Id = workItem.Id,
                    RequestNo = workItem.Fields["MyCompany.RequestNumber"].Value.ToString(),
                    Title = workItem.Title,
                    WorkItemType = workItem.Fields["MyCompany.WorkItemType"].Value.ToString(),
                    Priority = workItem.Fields["MyCompany.Priority"].Value.ToString()
                };
            }

            return model;
        }

        public List<WorkItemViewModel> GetWorkItems(string query)
        {
            WorkItemCollection workItemCollection = _workItemStore.Query(query);

            var workItemList = new List<WorkItemViewModel>();

            foreach (WorkItem workItem in workItemCollection)
            {

                var model = new WorkItemViewModel()
                {
                    Id = workItem.Id,
                    RequestNo = workItem.Fields["MyCompany.RequestNumber"].Value.ToString(),
                    Title = workItem.Title,
                    WorkItemType = workItem.Fields["MyCompany.WorkItemType"].Value.ToString(),
                    Priority = workItem.Fields["MyCompany.Priority"].Value.ToString()
                };
            }

            return workItemList;
        }

        private IList<WorkItemViewModel> GetWorkItemTree(string query)
        {
            var treeQuery = new Query(_workItemStore, query);
            var links = treeQuery.RunLinkQuery();

            var workItemIds = links.Select(l => l.TargetId).ToArray();

            query = "SELECT * FROM WorkItems";
            var flatQuery = new Query(_workItemStore, query, workItemIds);
            var workItemCollection = flatQuery.RunQuery();

            var workItemList = new List<WorkItemViewModel>();

            for (int i = 0; i < workItemCollection.Count; i++)
            {
                var workItem = workItemCollection[i];

                if (workItem.Type.Name == "Requirement")
                {
                    var model = new WorkItemViewModel()
                    {
                        Id = workItem.Id,
                        RequestNo = workItem.Fields["MyCompany.RequestNumber"].Value.ToString(),
                        Title = workItem.Title,
                        WorkItemType = workItem.Fields["MyCompany.WorkItemType"].Value.ToString(),
                        Priority = workItem.Fields["MyCompany.Priority"].Value.ToString()
                    };

                    workItemList.Add(model);
                }
                else
                {
                    switch (workItem.Type.Name)
                    {
                        case "Task":
                            var task = new TFSTask()
                            {
                                Name = workItem.Title,
                                Activity = workItem.Fields["MyCompany.Activity"].Value.ToString(),
                                Start = (DateTime?)workItem.Fields["MyCompany.ActivityStart"].Value,
                                Due = (DateTime?)workItem.Fields["MyCompany.ActivityFinish"].Value,
                                Status = workItem.State
                            };

                            workItemList.Last().Tasks.Add(task);
                            break;
                        case "Issue":
                            var issue = new TFSIssue()
                            {
                                Name = workItem.Title,
                                Created = workItem.CreatedDate,
                                Due = (DateTime?)workItem.Fields["MyCompany.ActivityDue"].Value,
                                Status = workItem.State
                            };
                            workItemList.Last().Issues.Add(issue);
                            break;
                        default:
                            break;
                    }
                }
            }

            return workItemList;
        }

        #endregion Methods
    }
}
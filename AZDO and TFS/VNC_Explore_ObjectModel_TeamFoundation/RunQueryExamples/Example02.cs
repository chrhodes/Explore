Example 2 - TPCQuery

namespace WiLinq.LinqProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    /// <summary>
    /// This is the result of a WI Linq Query.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class TPCQuery
    {
        #region Fields

        readonly Dictionary<string, object> _defaultParameters;
        private readonly TfsTeamProjectCollection _tpc;
        private readonly QueryType _type;
        private readonly string _wiql;

        #endregion Fields

        #region Constructors

        public TPCQuery(TfsTeamProjectCollection tpc, string wiql, Dictionary<string, object> defaultParameters, QueryType type)
        {
            if (tpc == null)
            {
                throw new ArgumentNullException("tpc");
            }

            _wiql = wiql;
            _tpc = tpc;
            _defaultParameters = defaultParameters;
            _type = type;
        }

        #endregion Constructors

        #region Methods

        public WorkItemLinkInfo[] GetLinks()
        {
            var store = GetWorkItemStore();

            var query = new Query(store, _wiql, _defaultParameters);

            return query.RunLinkQuery();
        }

        public WorkItem[] GetWorkItems()
        {
            if (_type != QueryType.WorkItem)
            {
                throw new InvalidOperationException("This is not a workitem query");
            }

            var store = GetWorkItemStore();

            return store.Query(_wiql, _defaultParameters)
                .Cast<WorkItem>()
                .ToArray();
        }

        private WorkItemStore GetWorkItemStore()
        {
            var store = _tpc.GetService(typeof(WorkItemStore)) as WorkItemStore;
            return store;
        }

        #endregion Methods
    }
}
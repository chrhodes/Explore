Example 6 - QueryRunner

namespace TFSWorkItemQueryService.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class QueryRunner : IQueryRunner
    {
        #region Fields

        private ITfsContext currentContext;
        private IQueryMacroParser macroParser;

        #endregion Fields

        #region Constructors

        public QueryRunner(ITfsContext currentContext, IQueryMacroParser macroParser)
        {
            this.currentContext = currentContext;
            this.macroParser = macroParser;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<WorkItem> RunQuery(QueryDefinition queryDefinition)
        {
            QueryDefinition parsedQueryDefinition = macroParser.Replace(queryDefinition);

            var query = new Query(currentContext.CurrentWorkItemStore, parsedQueryDefinition.QueryText);

            if (query.IsLinkQuery)
            {
                var queryResults = query.RunLinkQuery();

                return from l in queryResults.OfType<WorkItemLinkInfo>()
                       select currentContext.CurrentWorkItemStore.GetWorkItem(l.TargetId);
            }
            else
            {
                var queryResults = query.RunQuery();

                return queryResults.OfType<WorkItem>();
            }
        }

        #endregion Methods
    }
}
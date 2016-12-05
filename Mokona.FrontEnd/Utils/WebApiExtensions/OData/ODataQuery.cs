namespace Mokona.FrontEnd.Utils
{
    using Mokona.Utils.Interfaces;
    using System.Linq;
    using System.Web.Http.OData.Query;
    using System.Data.Entity;
    using Microsoft.Data.OData.Query.SemanticAst;
    using Util.CustomQueries;

    public class ODataQuery<T> : ICustomQuery<T>
        where T : class
    {
        private ODataQueryOptions<T> oDataQueryOptions;

        public ODataQuery(ODataQueryOptions<T> oDataQueryOptions)
        {
            this.oDataQueryOptions = oDataQueryOptions;
        }

        public QueryResultSet<T> ApplyTo(IQueryable<T> query) 
        {
            var result = new QueryResultSet<T>();
            var settings = new ODataQuerySettings();

            IQueryable applied = query;
            if(this.oDataQueryOptions.Filter != null)
                applied = this.oDataQueryOptions.Filter.ApplyTo(applied, settings);

            if (this.oDataQueryOptions.OrderBy != null)
                applied = this.oDataQueryOptions.OrderBy.ApplyTo(applied, settings);

            if (this.oDataQueryOptions.Skip != null)
                applied = this.oDataQueryOptions.Skip.ApplyTo(applied, settings);

            if (this.oDataQueryOptions.Top != null)
                applied = this.oDataQueryOptions.Top.ApplyTo(applied, settings);

            if (this.oDataQueryOptions.SelectExpand != null)
                applied = this.ApplyExpand(this.oDataQueryOptions.SelectExpand, applied);

            result.Values = (applied as IQueryable<T>).ToList();

            if (this.oDataQueryOptions.InlineCount != null)
            {
                var filteredQuery = this.oDataQueryOptions.Filter?.ApplyTo(query, new ODataQuerySettings()) ?? query;
                result.TotalCount = this.oDataQueryOptions.InlineCount?.GetEntityCount(filteredQuery);
            }
            
            return result;
        }

        private IQueryable ApplyExpand(SelectExpandQueryOption options, IQueryable query)
        {
            foreach (var item in options.SelectExpandClause.SelectedItems)
            {
                var expandedItem = item as ExpandedNavigationSelectItem;
                if (expandedItem == null)
                    continue;

                var navigation = expandedItem.PathToNavigationProperty.FirstOrDefault() as NavigationPropertySegment;
                if (navigation == null)
                    continue;

                query = query.Include(navigation.NavigationProperty.Name);
            }

            return query;
        }
    }
}

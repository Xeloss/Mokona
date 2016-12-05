namespace Mokona.Util.CustomQueries
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class QueryResultSet<T> 
        where T : class
    {
        public IEnumerable<T> Values { get; set; }

        public long? TotalCount { get; set; }

        public QueryResultSet<Z> AsQueryResultSetOf<Z>(Func<T, Z> transformation)
            where Z : class
        {
            return new QueryResultSet<Z>()
            {
                TotalCount = this.TotalCount,
                Values = this.Values.Select(transformation).ToList()
            };
        }
    }
}
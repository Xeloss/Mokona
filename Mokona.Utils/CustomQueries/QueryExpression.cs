namespace Mokona.Util.CustomQueries
{
    using CustomQueries;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Utils.Interfaces;

    public class PredicateQueryExpression<T> : ICustomQuery<T>
        where T : class
    {
        private Expression<Func<T, bool>> predicate;

        public PredicateQueryExpression(Expression<Func<T, bool>> predicate)
        {
            this.predicate = predicate;
        }

        public QueryResultSet<T> ApplyTo(IQueryable<T> query)
        {
            var result = query.Where(this.predicate);

            return new QueryResultSet<T>()
            {
                Values = result,
                TotalCount = result.Count()
            };
        }
    }
}
namespace Mokona.Utils.Extensions
{
    using Mokona.Utils.Interfaces;
    using System.Collections.Concurrent;
    using System.Linq;
    using Util.CustomQueries;

    public static class QuereableExtensions
    {
        public static QueryResultSet<T> Applying<T>(this ConcurrentBag<T> items, ICustomQuery<T> query) where T : class
        {
            return items.AsQueryable().Applying(query);
        }

        public static QueryResultSet<T> Applying<T>(this IQueryable<T> items, ICustomQuery<T> query) where T : class
        {
            return query.ApplyTo(items);
        }
    }
}

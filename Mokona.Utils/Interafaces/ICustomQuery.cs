namespace Mokona.Utils.Interfaces
{
    using System.Linq;
    using Util.CustomQueries;

    public interface ICustomQuery<T> where T : class
    {
        QueryResultSet<T> ApplyTo(IQueryable<T> query);
    }
}

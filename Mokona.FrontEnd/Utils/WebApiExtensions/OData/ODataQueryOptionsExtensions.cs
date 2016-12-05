namespace Mokona.FrontEnd.Utils
{
    using Mokona.Utils.Interfaces;
    using System.Web.Http.OData.Query;

    public static class ODataQueryOptionsExtensions
    {
        public static ICustomQuery<T> AsCustomQuery<T>(this ODataQueryOptions<T> oDataQueryOptions) where T : class
        {
            return new ODataQuery<T>(oDataQueryOptions);
        }
    }
}

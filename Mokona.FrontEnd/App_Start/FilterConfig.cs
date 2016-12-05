namespace Mokona.FrontEnd
{
    using Mokona.FrontEnd.Utils;
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}

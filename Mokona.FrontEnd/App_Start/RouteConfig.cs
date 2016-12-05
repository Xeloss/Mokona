namespace Mokona.FrontEnd
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Views",
                url: "View/{area}/{viewName}",
                defaults: new { controller = "View", action = "Index", viewName = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "SinglePageContainer", id = UrlParameter.Optional }
            );
        }
    }
}

namespace Mokona.FrontEnd.Controllers.Mvc
{
    using System.Web.Mvc;

    public class ViewController : BaseController
    {
        public ActionResult Index(string area, string viewName)
        {
            return ViewFor(area, viewName);
        }

        private ViewResult ViewFor(string area, string viewName)
        {
            return View(string.Format("../{0}/{1}", area, viewName));
        }
    }
}

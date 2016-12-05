namespace Mokona.FrontEnd.Controllers.Mvc
{
    using Mokona.FrontEnd.Utils;
    using System;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using Core.Security;

    public abstract class BaseController : Controller
    {
        public UserPrincipal UserPrincipal
        {
            get { return (UserPrincipal)this.User; }
        }
    }
}

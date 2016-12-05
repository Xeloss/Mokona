using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Mokona.FrontEnd.WebApiExtensions;
using System.Web.Security;
using Mokona.Core.Security;
using Mokona.Utils.Extensions;
using Mokona.FrontEnd.Models;
using Mokona.Core.Utils;
using System.Threading;
using Mokona.FrontEnd.Utils;

namespace Mokona.FrontEnd
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        IContainer IoCContainer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            this.IoCContainer = IoCConfig.RegisterDependencies();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FormatterConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthorizationPoliciesConfig.RegisterPolicies();

        }

        public void Application_End()
        {
            // dispose the container when the application is about to shutdown to
            // let it gracefully release all components and clean up after them
            this.IoCContainer.Dispose();
        }

        public void Application_BeginRequest()
        {
            this.SetUpThreadCulture();
        }

        public void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
                this.LoadUserFromCookie(authCookie);
            else
            {
                var token = Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(token))
                    this.LoadUserFromToken(this.CleanToken(token));
            }
        }

        public void LoadUserFromCookie(HttpCookie authCookie)
        {
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var userCookie = authTicket.UserData.DeserealizeAs<UserPrincipalCookie>();

            HttpContext.Current.User = userCookie.AsPrincipal();
        }
        public void LoadUserFromToken(string token)
        {
            var json = EncryptionHelper.Decrypt(token);

            var userData = json.DeserealizeAs<UserPrincipalCookie>();

            HttpContext.Current.User = userData.AsPrincipal();
        }

        private void SetUpThreadCulture()
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else if (Request.UserLanguages != null && Request.UserLanguages.Length > 0)
            {
                cultureName = Request.UserLanguages[0];  // obtain it from HTTP header AcceptLanguages
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        private string CleanToken(string token)
        {
            if (token.StartsWith("bearer ", StringComparison.CurrentCultureIgnoreCase))
                return token.Remove(0, 7);

            return token;
        }
    }
}
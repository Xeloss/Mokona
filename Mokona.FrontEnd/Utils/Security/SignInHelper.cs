namespace Mokona.FrontEnd.Utils
{
    using Core.Security;
    using Models;
    using System;
    using System.Web;
    using System.Web.Security;
    using Mokona.Utils.Extensions;

    public static class SignInHelper
    {
        public static FormsAuthenticationTicket CreateAuthenticationTicketFor(UserPrincipal principal, bool rememberMe)
        {
            var userData = new UserPrincipalCookie(principal).ToJson();
            var ticket = new FormsAuthenticationTicket(1, principal.LoginName, DateTime.UtcNow, DateTime.UtcNow.AddDays(7), rememberMe, userData);

            return ticket;
        }

        public static void SetAuthorizationCookie(FormsAuthenticationTicket ticket)
        {
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}

namespace Mokona.FrontEnd.Controllers.Mvc
{
    using Mokona.Core.Exceptions;
    using Mokona.Core.Services;
    using Mokona.FrontEnd.Models;
    using Mokona.Utils.Extensions;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Core.Security;
    using Utils;

    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private ISecurityService securityService;

        public LoginController(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        public ActionResult Index()
        {
            var user = securityService.GetCurrentUserPrincipal();
            if (user != null)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        public string Index(LogInViewModel login)
        {
            if (!securityService.AreCredentialsValid(login.Name, login.CompanyDomain, login.Password))
                throw new BusinessException("Invalid username or password.");

            var result = this.BuildResult(login);

            return result.ToJson();
        }

        public ActionResult PendingApproval()
        {
            return View();
        }

        public ActionResult VerificateEmail()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        private UserPrincipal CreatePrincipalFor(LogInViewModel aLogin)
        {
            return securityService.CreatePrincipalFor(aLogin.Name, aLogin.CompanyDomain);
        }

        private LogInResult BuildResult(LogInViewModel login)
        {
            var principal = this.CreatePrincipalFor(login);

            var authorizationTicket = SignInHelper.CreateAuthenticationTicketFor(principal, login.RememberMe);
            SignInHelper.SetAuthorizationCookie(authorizationTicket);

            var result = new LogInResult()
            {
                Success = true,
                FirstName = principal.FirstName,
                LastName = principal.LastName,
                CompanyName = principal.CompanyName
            };
            result.TargetUrl = FormsAuthentication.GetRedirectUrl(login.Name, true);

            return result;
        }

        private LogInResult RedirectToNotApproved()
        {
            return new LogInResult()
            {
                Success = false,
                TargetUrl = "/Login/PendingApproval"
            };
        }

        private LogInResult RedirectToPendingVerification()
        {
            return new LogInResult()
            {
                Success = false,
                TargetUrl = "/Login/VerificateEmail"
            };
        }
    }
}

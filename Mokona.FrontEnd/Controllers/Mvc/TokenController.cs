namespace Mokona.FrontEnd.Controllers.Mvc
{
    using Core.Security;
    using Core.Utils;
    using System;
    using System.Net;
    using System.Web.Mvc;
    using Mokona.Core.Services;
    using Mokona.FrontEnd.Models;
    using Mokona.Utils.Extensions;

    [AllowAnonymous]
    public class TokenController : BaseController
    {
        private ISecurityService securityService;
        public TokenController(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        [HttpPost]
        public JsonResult Authenticate(LogInViewModel login)
        {
            if (!securityService.AreCredentialsValid(login.Name, login.CompanyDomain, login.Password))
                return RespondInvalidCredentials();

            var principal = this.LogUserIn(login);

            return Json(new
            {
                Token = CreateAuthenticationTokenFor(principal)
            });
        }

        private UserPrincipal LogUserIn(LogInViewModel aLogin)
        {
            var principal = securityService.CreatePrincipalFor(aLogin.Name, aLogin.CompanyDomain);
            HttpContext.User = principal;

            return principal;
        }

        private string CreateAuthenticationTokenFor(UserPrincipal principal)
        {
            var expirationDate = DateTime.Now.AddDays(1);

            var userData = new UserPrincipalCookie(principal).ToJson();

            return EncryptionHelper.Encrypt(userData);
        }

        private JsonResult RespondInvalidCredentials()
        {
            return Json(new
            {
                User = new
                {
                    LoginName = "",
                    FirstName = "",
                    LastName = "",
                    Email = "",
                    CompanyId = 0,
                    Id = 0,
                },
                Token = ""
            });
        }
    }
}

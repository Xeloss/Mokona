using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web;
using Mokona.Core.Utils;
using Mokona.Entities;

namespace Mokona.Core.Utils.Email
{
    public class VerificationEmailProvider : IEmailDataProvider
    {
        private User user;

        public VerificationEmailProvider(User user)
        {
            this.user = user;
        }

        public string GetBody()
        {
            var request = HttpContext.Current.Request;

            var code = HttpUtility.UrlEncode(EncryptionHelper.Encrypt(this.user.Id.ToString()));
            var verificationLink = $"{request.Url.Scheme}://{request.Url.Authority}{request.ApplicationPath.TrimEnd('/')}/Verification/Verify?code={code}";

            var path = HttpContext.Current.Server.MapPath("/Templates/EmailVerification.cshtml");

            var template = File.ReadAllText(path);

            var model = new {
                VerifiationLink = verificationLink,
                Name = user.FirstName
            };

            var body = Engine.Razor.RunCompile(template, "emailVerification", null, model);

            return body;
        }

        public IEnumerable<MailAddress> GetRecipients()
        {
            return new MailAddress[] {
                new MailAddress(user.Email, user.FullName)
            };
        }

        public string GetSubject()
        {
            return "Verify your Email address";
        }
    }
}

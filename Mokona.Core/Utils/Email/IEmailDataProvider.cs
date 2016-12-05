using System.Collections.Generic;
using System.Net.Mail;

namespace Mokona.Core.Utils.Email
{
    public interface IEmailDataProvider
    {
        IEnumerable<MailAddress> GetRecipients();
        string GetBody();
        string GetSubject();
    }
}

namespace Mokona.Core.Utils.Email
{
    using System.Net.Mail;
    using System.Net.Mime;
    using Mokona.Utils.Extensions;

    public class EmailSender : IEmailSender
    {
        public void Send(IEmailDataProvider dataProvider)
        {
            using (var mailMsg = new MailMessage())
            using (var smtpClient = new SmtpClient())
            {
                mailMsg.To.AddRange(dataProvider.GetRecipients());

                mailMsg.From = new MailAddress("info@Mokona.com", "Mokona");
                
                mailMsg.Subject = "Mokona - " + dataProvider.GetSubject();
                var body = dataProvider.GetBody();

                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
            
                smtpClient.Send(mailMsg);
            }
        }
    }
}

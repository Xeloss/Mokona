namespace Mokona.Core.Utils.Email
{
    public interface IEmailSender
    {
        void Send(IEmailDataProvider dataProvider);
    }
}
namespace Mokona.Core.Services
{
    using Mokona.Entities;

    public interface IEmailService
    {
        void SendVerificationEmailTo(int userId);
        void SendVerificationEmailTo(User anUser);
    }
}

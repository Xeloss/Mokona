namespace Mokona.Core.Services
{
    using Mokona.Core.DataAccess.Repositories;
    using Mokona.Core.IoC;
    using Mokona.Entities;
    using System.Linq;
    using Utils.Email;
    using Exceptions;

    public class EmailService : IEmailService
    {
        private IRepository<User> userRepository;
        private IEmailSender emailSender;

        public EmailService(IRepository<User> userRepository, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.userRepository = userRepository;
        }
        
        public void SendVerificationEmailTo(int userId)
        {
            var user = this.userRepository.AsUntracked()
                                          .GetBy(userId);
            this.SendVerificationEmailTo(user);
        }
        public void SendVerificationEmailTo(User anUser)
        {
            var provider = new VerificationEmailProvider(anUser);
            this.emailSender.Send(provider);
        }
    }
}

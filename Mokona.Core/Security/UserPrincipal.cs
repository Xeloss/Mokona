namespace Mokona.Core.Security
{
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System;
    using System.Security.Principal;

    public class UserPrincipal : IPrincipal
    {
        public UserPrincipal(int userId, string loginName, int companyId)
        {
            if (userId == 0 || loginName.IsNullOrEmpty() || companyId == 0)
            {
                throw new ArgumentException("Invalid UserId, LoginName or CompanyDomain");
            }

            this.UserId = userId;
            this.LoginName = loginName;
            this.CompanyId = companyId;
            this.Identity = new GenericIdentity(loginName);
        }

        public UserPrincipal(User user)
            : this(user.Id, user.LoginName, user.CompanyId)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.CompanyName = user.Company.Name;
        }

        public string LoginName { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "{0} - {1}".ApplyFormat(UserId, LoginName);
        }

        public User AsUser()
        {
            return new User()
            {
                LoginName = this.LoginName,
                Id = this.UserId
            };
        }
    }
}

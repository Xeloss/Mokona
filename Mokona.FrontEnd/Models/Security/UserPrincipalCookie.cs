namespace Mokona.FrontEnd.Models
{
    using Mokona.Core.Security;
    using Mokona.Utils.Extensions;

    public class UserPrincipalCookie
    {
        public UserPrincipalCookie()
        { }

        public UserPrincipalCookie(UserPrincipal userPrincipal)
        {
            this.LoginName = userPrincipal.LoginName;
            this.UserId = userPrincipal.UserId;
            this.CompanyId = userPrincipal.CompanyId;
            this.FirstName = userPrincipal.FirstName;
            this.LastName = userPrincipal.LastName;
            this.CompanyName = userPrincipal.CompanyName;
        }

        public string LoginName { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public UserPrincipal AsPrincipal()
        {
            return new UserPrincipal(UserId, LoginName, this.CompanyId) {
                FirstName = this.FirstName,
                LastName = this.LastName,
                CompanyName = this.CompanyName
            };
        }

        public override string ToString()
        {
            return "{0} - {1}".ApplyFormat(UserId, LoginName);
        }
    }
}

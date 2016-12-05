namespace Mokona.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    public class User : SoftDeletableEntity
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual List<Role> Roles { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public bool IsAdmin
        {
            get { return this.Roles.Any(r => r.IsAdmin); }
        }

        public override string ToString()
        {
            return string.Concat("LoginName: ", this.LoginName,
                               ", FullName: ", this.FullName);
        }
    }
}

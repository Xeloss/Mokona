namespace Mokona.FrontEnd.Models.Resources
{
    using Mokona.Entities;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class UserResource
    {
        public UserResource()
        { }

        public UserResource(User user)
        {
            this.Id = user.Id;
            this.LoginName = user.LoginName;

            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.IsAdmin = user.Roles.Any(r => r.Name == "Admin");
        }

        public int Id { get; set; }

        [Required]
        public string LoginName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public bool EmailWasVerified { get; set; }

        public bool IsAdmin { get; set; }

        public string FullName {
            get
            {
                return this.LastName + ", " + this.FirstName;
            }
        }

        public virtual User AsUser()
        {
            return new User() {
                Id = this.Id,
                LoginName = this.LoginName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email
            };
        }

        public override string ToString()
        {
            return string.Concat(Id, " - ", FullName);
        }
    }
}

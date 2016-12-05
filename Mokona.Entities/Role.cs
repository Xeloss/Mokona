namespace Mokona.Entities
{
    using System.Collections.Generic;

    public class Role : Entity
    {
        public Role()
        {
            Permissions = new List<RolePermission>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsAdmin { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual List<RolePermission> Permissions { get; set; }

        public override string ToString()
        {
            return string.Concat("Role: ", this.Name,
                               ", Description: ", this.Description);
        }
    }
}

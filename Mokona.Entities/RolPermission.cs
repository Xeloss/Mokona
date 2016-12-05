namespace Mokona.Entities
{
    using System;

    public class RolePermission : Entity
    {
        public RolePermission()
        { }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public int? EntityId { get; set; }

        public EntityTypeId EntityType { get; set; }

        public Permissions Permission { get; set; }

        public bool IsDenied { get; set; }

        public override string ToString()
        {
            return EntityType.ToString() + " - " + Enum.GetName(typeof(Permissions), this.Permission);
        }
    }
}

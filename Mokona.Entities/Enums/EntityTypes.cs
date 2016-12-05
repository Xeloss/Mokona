namespace Mokona.Entities
{
    using System;

    public static class EntityTypes
    {
        static EntityTypes()
        {
            LogEntry = EntityType.CreateFor<LogEntry>();
            Role = EntityType.CreateFor<Role>();
            RolePermission = EntityType.CreateFor<RolePermission>();
            User = EntityType.CreateFor<User>();

            Company = EntityType.CreateFor<Company>();
        }
        
        public static readonly EntityType LogEntry;
        public static readonly EntityType Role;
        public static readonly EntityType RolePermission;
        public static readonly EntityType User;

        public static readonly EntityType Company;

        public static EntityType Parse(string name)
        {
            var Class = typeof(EntityTypes);

            var entityType = Class.GetField(name);
            if (entityType == null)
            {
                throw new ArgumentException("'{0}' is not a valid EntityType");
            }

            return (EntityType)entityType.GetValue(null);
        }
    }

    public sealed class EntityType
    {
        private EntityType()
        { }

        private EntityType(EntityTypeId Id, string Name, Type entityType)
        {
            this.Id = Id;
            this.Name = Name;
            this.Type = entityType;
        }

        public string Name { get; set; }

        public EntityTypeId Id { get; set; }

        public Type Type { get; set; }

        public static EntityType CreateFor<T>() where T : Entity
        {
            var type = typeof(T);
            var id = (EntityTypeId)Enum.Parse(typeof(EntityTypeId), type.Name);

            return new EntityType(id, type.Name, type);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

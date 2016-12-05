namespace Mokona.Core.DataAccess
{
    using Mokona.Core.DataAccess.Context;
    using Mokona.Core.DataAccess.GraphDiff;
    using Mokona.Core.Exceptions;
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using RefactorThis.GraphDiff;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class EntityContext : DbContext, IDBContext
    {
        public EntityContext()
            : base("DBConnection")
        {
            Database.SetInitializer<EntityContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany<Role>(u => u.Roles)
                        .WithMany();

            var convention = new AttributeToTableAnnotationConvention<SoftDeletableAttribute, string>(
                "SoftDeleteColumnName",
                (type, attributes) => attributes.Single().ColumnName);

            modelBuilder.Conventions.Add(convention);

            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<Entity> Relation(Type entityType)
        {
            if (!typeof(Entity).IsAssignableFrom(entityType))
                throw new TechnicalException("'{0}' is not a subclass of Entity".ApplyFormat(entityType));

            var method = typeof(EntityContext).GetMethod("Set", new Type[0])
                                              .MakeGenericMethod(entityType);

            var entitySet = method.Invoke(this, new object[0]);

            return entitySet as IQueryable<Entity>;
        }

        public IQueryable<T> Relation<T>() where T : Entity
        {
            return this.Set<T>();
        }

        public T Delete<T>(T anEntity) where T : Entity
        {
            return this.Set<T>()
                       .Remove(anEntity);
        }

        public T Save<T>(T anEntity) where T : Entity, new()
        {
            var action = anEntity.Id == 0 ? GraphDiffAction.Create : GraphDiffAction.Update;

            return this.UpdateGraph(anEntity, GraphDiffMappings.GetOf<T>(action));
        }

        public T Attach<T>(T anEntity) where T : Entity
        {
            var trackedEntity = this.Entry<T>(anEntity);

            if (trackedEntity.State == EntityState.Detached)
            {
                this.Set<T>()
                    .Attach(anEntity);

                trackedEntity = this.Entry<T>(anEntity);
                trackedEntity.State = EntityState.Modified;
            }

            return trackedEntity.Entity;
        }

        public ITransaction BeginTransaction()
        {
            return new EntityContextTransaction(this.Database.BeginTransaction());
        }
        
        public DbSet<LogEntry> LogEntries { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Company> Companies { get; set; }
    }
}

namespace Mokona.Core.DataAccess
{
    using Mokona.Core.DataAccess.Context;
    using Mokona.Entities;
    using System;
    using System.Linq;

    public class SoftDeletableEntityContext : IDBContext
    {
        private IDBContext context;

        public SoftDeletableEntityContext(IDBContext context)
        {
            this.context = context;
        }

        public IQueryable<Entity> Relation(Type entityType)
        {
            var relation = this.context.Relation(entityType) as IQueryable<SoftDeletableEntity>;

            return relation.Where(e => !e.Deleted);
        }

        public IQueryable<T> Relation<T>() where T : Entity
        {
            var relation = this.context.Relation<T>() as IQueryable<SoftDeletableEntity>;

            return relation.Where(e => !e.Deleted)
                           .Cast<T>();
        }

        public T Save<T>(T anEntity) where T : Entity, new()
        {
            return this.context.Save(anEntity);
        }

        public T Delete<T>(T anEntity) where T : Entity
        {
            var deletable = anEntity as SoftDeletableEntity;
            deletable.Deleted = true;
            deletable.DeletedDate = DateTime.UtcNow;

            return anEntity;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public ITransaction BeginTransaction()
        {
            return this.context.BeginTransaction();
        }
    }
}

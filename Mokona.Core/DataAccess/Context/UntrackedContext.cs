namespace Mokona.Core.DataAccess
{
    using Mokona.Core.DataAccess.Context;
    using Mokona.Core.Exceptions;
    using Mokona.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UntrackedContext : IDBContext
    {
        private IDBContext context;

        public UntrackedContext(IDBContext context)
        {
            this.context = context;
        }

        public IQueryable<Entity> Relation(Type entityType)
        {
            return this.context.Relation(entityType)
                               .AsNoTracking();
        }

        public IQueryable<T> Relation<T>() where T : Entity
        {
            return this.context.Relation<T>()
                               .AsNoTracking();
        }

        public T Save<T>(T anEntity) where T : Entity, new()
        {
            throw new TechnicalException("Untracked context should not save entities.");
        }

        public T Delete<T>(T anEntity) where T : Entity
        {
            throw new TechnicalException("Untracked context should not delete entities.");
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

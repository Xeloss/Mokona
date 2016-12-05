namespace Mokona.Core.DataAccess
{
    using Mokona.Core.DataAccess.Context;
    using Mokona.Core.Services;
    using Mokona.Entities;
    using System;
    using System.Linq;

    public class AuditableEntityContext : IDBContext
    {
        private IDBContext context;
        private ISecurityService securityService;

        public AuditableEntityContext(IDBContext context, ISecurityService securityService)
        {
            this.context = context;
            this.securityService = securityService;
        }

        public IQueryable<Entity> Relation(Type entityType)
        {
            return this.context.Relation(entityType);
        }

        public IQueryable<T> Relation<T>() where T : Entity
        {
            return this.context.Relation<T>();
        }

        public T Save<T>(T anEntity) where T : Entity, new()
        {
            var auditable = anEntity as AuditableEntity;
            var currentPrincipal = securityService.GetCurrentUserPrincipal();

            if (auditable.Id == 0)
            {
                auditable.CreatorId = currentPrincipal?.UserId ?? 0;
                auditable.CreationDate = DateTime.UtcNow;
            }

            auditable.LastUpdaterId = currentPrincipal?.UserId ?? 0;
            auditable.LastUpdateDate = DateTime.UtcNow;

            return this.context.Save(anEntity);
        }

        public T Delete<T>(T anEntity) where T : Entity
        {
            return this.context.Delete(anEntity);
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

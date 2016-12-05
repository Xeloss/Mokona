namespace Mokona.Core.DataAccess.Repositories
{
    using Mokona.Core.DataAccess;
    using Mokona.Core.DataAccess.Context;
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class BasicRepository<T> : IRepository<T>
        where T : Entity, new()
    {
        protected IDBContext context { get; set; }

        public BasicRepository()
        {
            this.context = ContextBuilder<T>.AutoBuild();
        }

        protected BasicRepository(IDBContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> ListAll()
        {
            return this.context.Relation<T>();
        }

        public virtual T GetBy(int Id)
        {
            return this.context.Relation<T>()
                               .FirstOrDefault(e => e.Id == Id);
        }

        public virtual T Save(T anEntity)
        {
            return this.context.Save(anEntity);
        }

        public virtual IEnumerable<T> Save(IEnumerable<T> entities)
        {
            return entities.Select(e => this.context.Save(e))
                           .ToList();
        }

        public virtual void Delete(T aEntity)
        {
            this.context.Delete(aEntity);
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            entities.Foreach(e => this.context.Delete(e));
        }

        public virtual void DeleteBy(int Id)
        {
            var entity = this.context.Relation<T>()
                                     .FirstOrDefault(e => e.Id == Id);

            if (entity != null)
                this.Delete(entity);
        }

        public virtual IRepository<T> AsUntracked()
        {
            return new BasicRepository<T>(new UntrackedContext(this.context));
        }

        public virtual IRepository<T> AsUnsecured()
        {
            var context = new ContextBuilder<T>()
                              .WithDeletableEntity()
                              .WithAuditableEntity();

            if (this.context is UntrackedContext)
                context.WithUntracked();                   

            return new BasicRepository<T>(context.Build());
        }
    }
}

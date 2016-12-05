namespace Mokona.Core.Services
{
    using DataAccess.Repositories;
    using Entities;
    using Mokona.Utils.Extensions;
    using Mokona.Utils.Interfaces;
    using IoC;
    using System.Collections.Generic;
    using System.Linq;
    using Util.CustomQueries;

    [AutomaticSaveChanges(true)]
    public class BasicService<T> : IService<T> where T : Entity
    {
        protected IRepository<T> repository;

        public BasicService(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual T Create(T anEntity)
        {
            return this.repository.Save(anEntity);
        }

        public virtual void Update(T anEntity)
        {
            this.repository.Save(anEntity);
        }

        public virtual void Delete(int id)
        {
            this.repository.DeleteBy(id);
        }

        [AutomaticSaveChanges(false)]
        public virtual IEnumerable<T> ListAll()
        {
            return this.repository.AsUntracked()
                                  .ListAll()
                                  .ToList();
        }

        [AutomaticSaveChanges(false)]
        public virtual QueryResultSet<T> ListWhere(ICustomQuery<T> query)
        {
            return this.repository.AsUntracked()
                                  .ListAll()
                                  .Applying(query);
        }

        public virtual T GetBy(int id)
        {
            return this.repository.AsUntracked()
                                  .GetBy(id);
        }
    }
}

namespace Mokona.Core.DataAccess.Repositories
{
    using Mokona.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRepository<T> where T : Entity
    {
        void Delete(IEnumerable<T> entities);

        void Delete(T aEntity);

        void DeleteBy(int Id);

        T GetBy(int Id);

        IQueryable<T> ListAll();

        IEnumerable<T> Save(IEnumerable<T> entities);

        T Save(T anEntity);

        IRepository<T> AsUntracked();

        IRepository<T> AsUnsecured();
    }
}

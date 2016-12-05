namespace Mokona.Core.Services
{
    using Mokona.Entities;
    using Mokona.Utils.Interfaces;
    using System.Collections.Generic;
    using Util.CustomQueries;

    public interface IService<T> where T : Entity
    {
        T Create(T anEntity);

        void Update(T anEntity);

        void Delete(int id);

        IEnumerable<T> ListAll();

        QueryResultSet<T> ListWhere(ICustomQuery<T> query);

        T GetBy(int id);
    }
}

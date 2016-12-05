namespace Mokona.Core.Services
{
    using DataAccess.Repositories;
    using Entities;
    using Mokona.Utils.Extensions;
    using Mokona.Utils.Interfaces;
    using IoC;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Util.CustomQueries;

    [AutomaticSaveChanges(true)]
    public class CacheService<T> : BasicService<T>, ICacheService<T> where T : Entity
    {
        private static object lockKey;
        private static ConcurrentBag<T> cache;

        static CacheService()
        {
            cache = new ConcurrentBag<T>();
            lockKey = new object();
        }

        public CacheService(IRepository<T> repository)
            : base(repository)
        {
            InitializeCache();
        }

        [AutomaticSaveChanges(false)]
        public override IEnumerable<T> ListAll()
        {
            return cache.ToList();
        }

        [AutomaticSaveChanges(false)]
        public override QueryResultSet<T> ListWhere(ICustomQuery<T> query)
        {
            return cache.Applying(query);
        }

        [AutomaticSaveChanges(false)]
        public override T GetBy(int id)
        {
            return cache.FirstOrDefault(e => e.Id == id);
        }

        private void InitializeCache()
        {
            lock (lockKey)
            {
                if (!cache.IsEmpty)
                    return;

                this.repository.AsUntracked()
                               .ListAll()
                               .ToList()
                               .ForEach(cache.Add);
            }
        }
    }
}

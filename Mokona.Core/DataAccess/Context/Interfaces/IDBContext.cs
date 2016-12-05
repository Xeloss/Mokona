namespace Mokona.Core.DataAccess.Context
{
    using Mokona.Entities;
    using System;
    using System.Linq;

    public interface IDBContext
    {
        IQueryable<Entity> Relation(Type entityType);

        IQueryable<T> Relation<T>() where T : Entity;

        T Save<T>(T anEntity) where T : Entity, new();

        T Delete<T>(T anEntity) where T : Entity;

        int SaveChanges();

        ITransaction BeginTransaction();
    }
}

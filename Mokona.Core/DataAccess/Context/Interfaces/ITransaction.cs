namespace Mokona.Core.DataAccess.Context
{
    using System;

    public interface ITransaction : IDisposable
    {
        void Rollback();

        void Commit();
    }
}

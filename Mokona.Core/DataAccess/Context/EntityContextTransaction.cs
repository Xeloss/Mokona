namespace Mokona.Core.DataAccess.Context
{
    using System.Data.Entity;

    public class EntityContextTransaction : ITransaction
    {
        private DbContextTransaction contextTransaction;

        public EntityContextTransaction(DbContextTransaction contextTransaction)
        {
            this.contextTransaction = contextTransaction;
        }

        public void Rollback()
        {
            this.contextTransaction.Rollback();
        }

        public void Commit()
        {
            this.contextTransaction.Commit();
        }

        public void Dispose()
        {
            this.Rollback();
            this.contextTransaction.Dispose();
        }
    }
}

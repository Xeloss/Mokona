namespace Mokona.Core.DataAccess.Context
{
    using Mokona.Core.IoC;

    public static class ContextHelper
    {
        public static ITransaction BeginTransaction()
        {
            var context = ContainerAccessor.Resolve<IDBContext>();

            return context.BeginTransaction();
        }

        public static void SaveChanges()
        {
            var context = ContainerAccessor.Resolve<IDBContext>();
            context.SaveChanges();
        }
    }
}

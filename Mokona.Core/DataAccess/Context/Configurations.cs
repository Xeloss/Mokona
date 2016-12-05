namespace Mokona.Core.DataAccess.Context
{
    using System.Data.Entity;

    /// <summary>
    /// https://channel9.msdn.com/Events/TechEd/NorthAmerica/2014/DEV-B417#fbid=
    /// </summary>
    public class EntityFrameworkConfigurations : DbConfiguration
    {
        public EntityFrameworkConfigurations()
        {
            this.AddInterceptor(new SoftDeleteInterseptor());
        }
    }
}

namespace Mokona.Core.IoC
{
    using Autofac;
    using Entities;
    using Mokona.Core.DataAccess.Repositories;

    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder theBuilder)
        {
            base.Load(theBuilder);

            theBuilder.RegisterGeneric(typeof(BasicRepository<>))
                      .As(typeof(IRepository<>));

            theBuilder.RegisterType<SecurityRepository>()
                   .As<ISecurityRepository>();

        }
    }
}

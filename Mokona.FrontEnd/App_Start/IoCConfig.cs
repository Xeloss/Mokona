namespace Mokona.FrontEnd
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using Mokona.Core.DataAccess;
    using Mokona.Core.DataAccess.Context;
    using Mokona.Core.IoC;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using Utils.ModelValidator;

    public static class IoCConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            // Register your MVC controllers.
            builder.RegisterControllers(assembly);
            builder.RegisterApiControllers(assembly);

            RegisterContextsOn(builder);

            builder.RegisterModule<RepositoriesModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<ValidatorsModule>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            var dependencyResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(dependencyResolver);

            // Set Web API Dependency Resolver
            var webApiDependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiDependencyResolver;

            ContainerAccessor.Container = () => dependencyResolver.RequestLifetimeScope;

            return container;
        }

        private static void RegisterContextsOn(ContainerBuilder theBuilder)
        {
            theBuilder.RegisterType<EntityContext>()
                      .As<IDBContext>()
                      .AsSelf()
                      .InstancePerRequest();
        }
    }
}

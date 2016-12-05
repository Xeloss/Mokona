namespace Mokona.Core.IoC
{
    using Autofac;
    using Autofac.Extras.DynamicProxy2;
    using Utils.Email;
    using Mokona.Core.Services;

    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder theBuilder)
        {
            base.Load(theBuilder);

            this.RegisterInterceptorsOn(theBuilder);
            this.RegisterServicesOn(theBuilder);
        }

        private void RegisterInterceptorsOn(ContainerBuilder theBuilder)
        {
            theBuilder.RegisterType<InterceptorState>()
                      .AsSelf()
                      .InstancePerRequest();

            theBuilder.RegisterType<AutomaticSaveChangesInterceptor>()
                      .AsSelf()
                      .InstancePerRequest();
        }

        private void RegisterServicesOn(ContainerBuilder theBuilder)
        {
            theBuilder.RegisterGeneric(typeof(BasicService<>))
                   .As(typeof(IService<>))
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(AutomaticSaveChangesInterceptor));

            theBuilder.RegisterGeneric(typeof(CacheService<>))
                   .As(typeof(ICacheService<>));

            theBuilder.RegisterType<SecurityService>()
                      .As<ISecurityService>()
                      .EnableInterfaceInterceptors()
                      .InterceptedBy(typeof(AutomaticSaveChangesInterceptor));

            theBuilder.RegisterType<CompanyService>()
                      .As<ICompanyService>()
                      .EnableInterfaceInterceptors()
                      .InterceptedBy(typeof(AutomaticSaveChangesInterceptor));

            theBuilder.RegisterType<EmailService>()
                      .As<IEmailService>();

            //Others
            theBuilder.RegisterType<EmailSender>()
                      .As<IEmailSender>();
        }
    }
}

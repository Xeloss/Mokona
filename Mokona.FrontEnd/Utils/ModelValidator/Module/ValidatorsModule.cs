namespace Mokona.FrontEnd.Utils.ModelValidator
{
    using Autofac;
    using FluentValidation;
    using Models;
    using Models.Resources;

    public class ValidatorsModule : Module
    {
        protected override void Load(ContainerBuilder theBuilder)
        {
            base.Load(theBuilder);
            
            theBuilder.RegisterType<CompanyValidator>()
                      .As<IValidator<CompanyResource>>();
        }
    }
}

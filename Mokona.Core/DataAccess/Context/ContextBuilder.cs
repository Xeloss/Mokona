namespace Mokona.Core.DataAccess.Context
{
    using Mokona.Core.IoC;
    using Mokona.Core.Services;
    using Mokona.Entities;

    public class ContextBuilder<T> where T : Entity
    {
        IDBContext context;

        public static IDBContext AutoBuild()
        {
            return new ContextBuilder<T>()
                        .WithSecurity()
                        .WithDeletableEntity()
                        .WithAuditableEntity()
                        .Build();
        }

        public ContextBuilder()
        {
            this.context = ContainerAccessor.Resolve<IDBContext>();
        }

        public ContextBuilder<T> WithSecurity()
        {
            var securityService = ContainerAccessor.Resolve<ISecurityService>();
            this.context = new AuthorizedEntitiesOnlyContext(context, securityService);

            return this;
        }

        public ContextBuilder<T> WithDeletableEntity()
        {
            if (!typeof(T).IsSubclassOf(typeof(SoftDeletableEntity)))
                return this;

            this.context = new SoftDeletableEntityContext(context);

            return this;
        }

        public ContextBuilder<T> WithAuditableEntity()
        {
            if (!typeof(T).IsSubclassOf(typeof(AuditableEntity)))
                return this;

            var securityService = ContainerAccessor.Resolve<ISecurityService>();
            this.context = new AuditableEntityContext(context, securityService);

            return this;
        }

        public ContextBuilder<T> WithUntracked()
        {
            this.context = new UntrackedContext(context);

            return this;
        }

        public IDBContext Build()
        {
            return this.context;
        }
    }
}

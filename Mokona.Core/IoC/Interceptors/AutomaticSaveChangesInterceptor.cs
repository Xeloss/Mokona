namespace Mokona.Core.IoC
{
    using Castle.DynamicProxy;
    using Mokona.Core.DataAccess.Context;
    using System;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public class AutomaticSaveChangesInterceptor : IInterceptor
    {
        private InterceptorState State;

        public AutomaticSaveChangesInterceptor()
        {
            this.State = ContainerAccessor.Resolve<InterceptorState>();
        }

        public void Intercept(IInvocation anInvocation)
        {
            try
            {
                this.State.StackInterception();

                anInvocation.Proceed();

                this.State.UnstackInterception();
                if (!this.State.CanFinishInterception || !ShouldSaveChangesGeneratedBy(anInvocation))
                    return;

                ContextHelper.SaveChanges();
            }
            catch (Exception ex)
            {
                this.State.UnstackInterception();

                //TODO: Log something here
                throw ex;
            }
        }

        private bool ShouldSaveChangesGeneratedBy(IInvocation anInvocation)
        {
            var attr = anInvocation.MethodInvocationTarget.GetCustomAttributes(typeof(AutomaticSaveChangesAttribute), true);

            if (!attr.Any())
                attr = anInvocation.TargetType.GetCustomAttributes(typeof(AutomaticSaveChangesAttribute), true);

            if (!attr.Any())
                return true;

            return ((AutomaticSaveChangesAttribute)attr.First()).Enabled;
        }
    }
}

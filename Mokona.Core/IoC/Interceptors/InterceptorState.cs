namespace Mokona.Core.IoC
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    public class InterceptorState
    {
        private int NestedInterceptions = 0;

        public void StackInterception()
        {
            this.NestedInterceptions++;
        }

        public void UnstackInterception()
        {
            this.NestedInterceptions--;
        }

        public bool CanFinishInterception
        {
            get { return this.NestedInterceptions == 0; }
        }
    }
}

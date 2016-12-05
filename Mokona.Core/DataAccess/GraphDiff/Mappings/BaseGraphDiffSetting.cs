namespace Mokona.Core.DataAccess.GraphDiff
{
    using Mokona.Core.Exceptions;
    using RefactorThis.GraphDiff;
    using System;
    using System.Linq.Expressions;

    public abstract class BaseGraphDiffSetting<T> : IGraphDiffSetting
    {
        public Expression GetConfiguration(GraphDiffAction action)
        {
            if (action == GraphDiffAction.Create)
                return ForCreate();
            if (action == GraphDiffAction.Update)
                return ForUpdate();

            throw new TechnicalException("Invalid GraphDiffAction: " + action.ToString());
        }

        protected virtual Expression<Func<IUpdateConfiguration<T>, object>> ForCreate()
        { return (map) => null; }

        protected virtual Expression<Func<IUpdateConfiguration<T>, object>> ForUpdate()
        { return (map) => null; }
    }
}

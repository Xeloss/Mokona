namespace Mokona.Core.DataAccess.GraphDiff
{
    using System.Linq.Expressions;

    public interface IGraphDiffSetting
    {
        Expression GetConfiguration(GraphDiffAction action);
    }
}

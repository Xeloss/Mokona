namespace Mokona.Core.DataAccess.Context
{
    using System.Data.Entity.Core.Common.CommandTrees;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Infrastructure.Interception;

    /// <summary>
    /// https://channel9.msdn.com/Events/TechEd/NorthAmerica/2014/DEV-B417#fbid=
    /// </summary>
    public class SoftDeleteInterseptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace != DataSpace.SSpace)
                return;

            var queryCommand = interceptionContext.Result as DbQueryCommandTree;
            if (queryCommand == null)
                return;

            DbExpression newQuery = queryCommand.Query.Accept(new SoftDeleteQueryVisitor());
            interceptionContext.Result = new DbQueryCommandTree(queryCommand.MetadataWorkspace, queryCommand.DataSpace, newQuery);
        }
    }
}

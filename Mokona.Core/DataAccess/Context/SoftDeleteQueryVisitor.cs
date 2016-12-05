namespace Mokona.Core.DataAccess.Context
{
    using System.Data.Entity.Core.Common.CommandTrees;
    using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Linq;

    /// <summary>
    /// https://channel9.msdn.com/Events/TechEd/NorthAmerica/2014/DEV-B417#fbid=
    /// </summary>
    public class SoftDeleteQueryVisitor : DefaultExpressionVisitor
    {
        public override DbExpression Visit(DbScanExpression expression)
        {
            var column = this.GetSoftDeleteColumnName(expression.Target.ElementType);
            if (column != null)
            {
                var table = (EntityType)expression.Target.ElementType;
                if (table.Properties.Any(p => p.Name == column))
                {
                    var binding = DbExpressionBuilder.Bind(expression);

                    return DbExpressionBuilder.Filter(
                        binding,
                        DbExpressionBuilder.NotEqual(
                            DbExpressionBuilder.Property(
                                DbExpressionBuilder.Variable(binding.VariableType, binding.VariableName),
                                column),
                            DbExpression.FromBoolean(true)));
                }
            }

            return base.Visit(expression);
        }

        private string GetSoftDeleteColumnName(EdmType type)
        {
            // TODO Find the soft delete annotation and get the property name
            //      Name of annotation will be something like: 
            //      http://schemas.microsoft.com/ado/2013/11/edm/customannotation:SoftDeleteColumnName

            MetadataProperty annotation = type.MetadataProperties
                .Where(p => p.Name.EndsWith("customannotation:SoftDeleteColumnName"))
                .SingleOrDefault();

            return annotation == null ? null : (string)annotation.Value;
        }
    }
}

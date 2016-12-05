namespace Mokona.Core.DataAccess.GraphDiff
{
    using Entities;
    using Mokona.Core.Exceptions;
    using RefactorThis.GraphDiff;
    using System;
    using System.Linq.Expressions;

    public class CompanyGraphDiffSettings : BaseGraphDiffSetting<Company>
    {
        //protected override Expression<Func<IUpdateConfiguration<Company>, object>> ForCreate()
        //{
        //    return m => m.OwnedCollection(c => c.CostCenters)
        //                 .OwnedCollection(c => c.DocumentTypes)
        //                 .OwnedCollection(c => c.ExpenseTypes)
        //                 .AssociatedCollection(c => c.Currencies);
        //}

        //protected override Expression<Func<IUpdateConfiguration<Company>, object>> ForUpdate()
        //{
        //    return m => m.AssociatedCollection(c => c.Currencies);
        //}
    }
}

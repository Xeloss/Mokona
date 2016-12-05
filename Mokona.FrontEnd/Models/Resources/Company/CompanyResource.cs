namespace Mokona.FrontEnd.Models.Resources
{
    using System.Collections.Generic;
    using System.Linq;
    using Mokona.Entities;

    public class CompanyResource : BasicCompanyResource
    {
        public CompanyResource()
            : base()
        { }

        public CompanyResource(Company company)
            : base(company)
        { }
        
        public override Company AsCompany()
        {
            var company = base.AsCompany();

            return company;
        }
    }
}

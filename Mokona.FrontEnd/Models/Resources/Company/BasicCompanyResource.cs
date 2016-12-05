namespace Mokona.FrontEnd.Models.Resources
{
    using System.Collections.Generic;
    using System.Linq;
    using Mokona.Entities;

    public class BasicCompanyResource
    {
        public BasicCompanyResource()
        { }

        public BasicCompanyResource(Company company)
        {
            this.Id = company.Id;
            this.Name = company.Name;
            this.Annotations = company.Annotations;
            this.Domain = company.Domain;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Annotations { get; set; }

        public string Domain { get; set; }
        
        public virtual Company AsCompany()
        {
            var company = new Company()
            {
                Id = this.Id,
                Name = this.Name,
                Annotations = this.Annotations,
                Domain = this.Domain
            };

            return company;
        }
    }
}

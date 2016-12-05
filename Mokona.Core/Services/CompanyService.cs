namespace Mokona.Core.Services
{
    using Mokona.Core.DataAccess.Repositories;
    using Mokona.Core.IoC;
    using Mokona.Entities;
    using System.Linq;
    using Exceptions;
    using System.Collections.Generic;
    using Mokona.Utils.Extensions;

    [AutomaticSaveChanges(true)]
    public class CompanyService : BasicService<Company>, ICompanyService
    {
        public CompanyService(IRepository<Company> companyRepository)
            : base(companyRepository)
        { }

        public override void Update(Company anEntity)
        {
            var persisted = this.repository.GetBy(anEntity.Id);

            anEntity.Annotations = persisted.Annotations;
            anEntity.Domain = persisted.Domain;

            base.Update(anEntity);
        }

        [AutomaticSaveChanges(false)]
        public bool CompanyDomainExists(string domain)
        {
            return this.repository.AsUntracked()
                                  .AsUnsecured()
                                  .ListAll()
                                  .Any(c => c.Domain.ToLower() == domain.ToLower());

        }
    }
}

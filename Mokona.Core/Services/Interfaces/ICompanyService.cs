namespace Mokona.Core.Services
{
    using System.Collections.Generic;
    using Mokona.Entities;

    public interface ICompanyService : IService<Company>
    {
        bool CompanyDomainExists(string domain);
    }
}

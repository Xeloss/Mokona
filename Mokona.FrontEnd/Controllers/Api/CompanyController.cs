namespace Mokona.FrontEnd.Controllers.Api
{
    using Mokona.Core.Exceptions;
    using Mokona.Core.Services;
    using Mokona.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Resources;
    using System.Web.Http.OData.Query;
    using Utils;
    using Mokona.Utils.Interfaces;
    using System.Web.Http;
    using Util.CustomQueries;
    using FluentValidation;
    using Utils.ModelValidator;

    public class CompanyController : BaseApiController
    {
        private ICompanyService companyService;
        private IValidator<CompanyResource> companyValidator;

        public CompanyController(ICompanyService companyService, IValidator<CompanyResource> companyValidator)
        {
            this.companyService = companyService;
            this.companyValidator = companyValidator;
        }

        public QueryResultSet<CompanyResource> Get(ODataQueryOptions<Company> queryOptions)
        {
            return companyService.ListWhere(queryOptions.AsCustomQuery())
                                 .AsQueryResultSetOf(c => new CompanyResource(c));
        }

        public CompanyResource Get(int id)
        {
            var entity = companyService.GetBy(id);
            if (entity == null)
                throw new EntityNotFoundException(EntityTypes.Company, id);

            return new CompanyResource(entity);
        }

        public void Post(CompanyResource eMokonaReport)
        {
            companyService.Create(eMokonaReport.AsCompany());
        }

        public void Put(CompanyResource company)
        {
            var result = this.companyValidator.Validate(company, ruleSet: "default");
            if(!result.IsValid)
                throw new ObjectValidationException(company, result);

            companyService.Update(company.AsCompany());
        }

        public void Delete(int id)
        {
            companyService.Delete(id);
        }
    }
}

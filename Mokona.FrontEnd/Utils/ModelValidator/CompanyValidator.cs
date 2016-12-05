using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Mokona.Core.DataAccess.Repositories;
using Mokona.Core.Services;
using Mokona.Entities;
using Mokona.FrontEnd.Models;
using Mokona.FrontEnd.Models.Resources;

namespace Mokona.FrontEnd.Utils.ModelValidator
{
    public class CompanyValidator : AbstractValidator<CompanyResource>
    {
        private IRepository<Company> companyRepository;

        public CompanyValidator(IRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository.AsUntracked()
                                                      .AsUnsecured();
            this.SetUpRules();
        }

        private void SetUpRules()
        {
            RuleSet("Create", () =>
            {
                RuleFor(x => x.Domain)
                    .NotEmpty()
                    .WithMessage(Resources.Resources.CompanyValidation_EmptyDomain)
                    .DependentRules(d =>
                        d.RuleFor(y => y.Domain)
                         .Length(3, 50)
                         .WithMessage(Resources.Resources.CompanyValidation_LengthDomain)
                         .Must(BeOnlyAlfanumerics)
                         .WithMessage(Resources.Resources.CompanyValidation_AlfanumericsDomain)
                         .Must(NotBeAlreadyTaken)
                         .WithMessage(Resources.Resources.CompanyValidation_ExistsDomain)
                    );
            });

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Resources.Resources.CompanyValidation_EmptyName)
                .Length(2, 20)
                .WithMessage(Resources.Resources.CompanyValidation_LengthName);
        }

        private bool NotBeAlreadyTaken(string companyDomain)
        {
            return !this.companyRepository.ListAll()
                                          .Any(c => c.Domain.ToLower() == companyDomain.ToLower());
        }
        private bool BeOnlyAlfanumerics(string companyDomain)
        {
            return !Regex.IsMatch(companyDomain, @"\W");
        }
    }
}
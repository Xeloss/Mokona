using FluentValidation;
using FluentValidation.Results;
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
    public static class ValidationResultExtensions
    {
        public static void Merge(this ValidationResult result, params ValidationResult[] anotherResult)
        {
            foreach (var validationResult in anotherResult)
            {
                foreach (var error in validationResult.Errors)
                {
                    result.Errors.Add(error);
                }
            }
        }
    }
}
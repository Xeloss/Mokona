using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mokona.Entities;

namespace Mokona.Core.Services.DTOs.GeneralReport
{
    public class PageContext
    {
        public int? PageSize { get; set; }

        public int? CurrentPage { get; set; }

        public IQueryable<T> ApplyTo<T>(IOrderedQueryable<T> query)
        {
            IQueryable<T> result = query;
            if (this.PageSize.HasValue && this.CurrentPage.HasValue)
            {
                result = query.Skip(this.PageSize.Value * this.CurrentPage.Value)
                              .Take(this.PageSize.Value);
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mokona.Entities;

namespace Mokona.Core.Services.DTOs.GeneralReport
{
    public class ReportResult<T> where T : class
    {
        public int TotalCount { get; set; }

        public IEnumerable<T> Records { get; set; }
    }
}

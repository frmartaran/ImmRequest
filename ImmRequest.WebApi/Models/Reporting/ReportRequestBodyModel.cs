using ImmRequest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models.Reporting
{
    public class ReportRequestBodyModel
    {
        public string Email { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

    }
}

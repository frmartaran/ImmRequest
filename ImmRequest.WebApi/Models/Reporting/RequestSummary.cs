using ImmRequest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models.Reporting
{
    public class RequestSummary
    {
        public int Count { get; set; }

        public string Status { get; set; }

        public List<long> RequestNumbers { get; set; }
    }
}

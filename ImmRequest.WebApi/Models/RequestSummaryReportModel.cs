using ImmRequest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class RequestSummaryReportModel
    {
        public string Email { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<Tuple<RequestStatus, int>> RequestSummary { get; set; }
    }
}

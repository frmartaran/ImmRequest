using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models.Reporting
{
    public class TypeSummary
    {
        public int Count { get; set; }

        public string Name { get; set; }

        public DateTime TypeCreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class SuccessfulCreateModel
    {
        public SuccessfulCreateModel(long id, string message)
        {
            Id = id;
            Message = message;
        }
        public long Id { get; set; }

        public string Message { get; set; }

    }
}

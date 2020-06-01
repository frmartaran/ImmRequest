using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class ImporterModel
    {
        public string Importer { get; set; }

        public IFormFile File { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [AuthorizationFilter]
    [EnableCors("CorsPolicy")]
    public class ImporterController : ControllerBase
    {
        private IImporterLogic Logic { get; set; }

        public ImporterController(IImporterLogic logic)
        {
            Logic = logic;

        }

        [HttpPost]
        public ActionResult Import(IFormFile file)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var allImporters = Logic.GetImporterOptions();

            return Ok(allImporters);
        }
    }
}
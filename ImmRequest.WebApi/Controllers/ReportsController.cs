using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Filters;
using ImmRequest.WebApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [AuthorizationFilter]
    [EnableCors("CorsPolicy")]

    public class ReportsController : ControllerBase
    {
        private IFinder Finder { get; set; }

        public ReportsController(IFinder finder)
        {
            Finder = finder;
        }

        [HttpGet("RequestSummary")]
        public ActionResult RequestSummaryReportGet([FromBody] RequestSummaryReportModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("TypesSummary")]
        public ActionResult TypeSummaryReportGet()
        {
            throw new NotImplementedException();
        }


    }
}
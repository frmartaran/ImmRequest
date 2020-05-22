using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Enums;
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
            var requests = Finder.FindAll<CitizenRequest>(cr => (cr.CreatedDate >= model.Start
                                                                && cr.CreatedDate < model.End)
                                                                && cr.Email == model.Email);
            var byStatus = requests
                .GroupBy(cr => cr.Status)
                .Select(g => new Tuple<RequestStatus, int>
                (
                    item1: g.Key,
                    item2: g.Count()
                ))
                .ToList();
            model.RequestSummary = byStatus;
            return Ok(model);

        }

        [HttpGet("TypesSummary")]
        public ActionResult TypeSummaryReportGet()
        {
            throw new NotImplementedException();
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Enums;
using ImmRequest.WebApi.Filters;
using ImmRequest.WebApi.Models;
using ImmRequest.WebApi.Models.Reporting;
using ImmRequest.WebApi.Resources;
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
        private ILogic<CitizenRequest> Logic { get; set; }

        public ReportsController(ILogic<CitizenRequest> logic)
        {
            Logic = logic;
        }

        [HttpGet("RequestSummary")]
        public ActionResult RequestSummaryReportGet([FromBody] RequestSummaryReportModel model)
        {
            var requests = Logic.GetAll()
                .Where(cr => cr.CreatedDate >= model.Start)
                .Where(cr => cr.CreatedDate < model.End)
                .Where(cr => cr.Email == model.Email)
                .ToList();

            if (requests.Count == 0)
            {
                var message = string.Format(WebApiResource.Reports_NoRequests, model.Email,
                    model.Start, model.End);
                return BadRequest(message);
            }

            var byStatus = requests
                .GroupBy(cr => cr.Status)
                .Select(g => new RequestSummary
                {
                    Status = g.Key,
                    Count = g.Count(),
                    RequestNumbers = g.Select(cr => cr.Id).ToList()
                })
                .ToList();
            model.Summary = byStatus;
            return Ok(model);

        }

        [HttpGet("TypesSummary")]
        public ActionResult TypeSummaryReportGet([FromBody] RequestSummaryReportModel model)
        {
            throw new NotImplementedException();
        }


    }
}
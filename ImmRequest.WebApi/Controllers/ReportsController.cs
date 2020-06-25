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

        /// <summary>
        /// Retorna la cantidad de solicitudes por estado realizadas por un mismo usuario en un rango de 
        /// fechas.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("RequestSummary")]
        public ActionResult RequestSummaryReportGet([FromBody] ReportRequestBodyModel model)
        {
            var requests = Logic.GetAll()
                .Where(cr => cr.CreatedDate >= model.Start)
                .Where(cr => cr.CreatedDate < model.End)
                .Where(cr => cr.Email == model.Email)
                .ToList();

            if (requests.Count == 0)
            {
                var message = string.Format(WebApiResource.Reports_NoRequestsForUser, model.Email,
                    model.Start, model.End);
                return BadRequest(message);
            }

            var byStatus = requests
                .GroupBy(cr => cr.Status)
                .Select(g => new RequestSummary
                {
                    Status = g.Key.ToString(),
                    Count = g.Count(),
                    RequestNumbers = g.Select(cr => cr.Id).ToList()
                })
                .ToList();
            
            return Ok(byStatus);

        }

        /// <summary>
        /// Retorna los tipos más usados en las solicitudes en un rango de fechas.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("TypesSummary")]
        public ActionResult TypeSummaryReportGet([FromBody] ReportRequestBodyModel model)
        {
            var requests = Logic.GetAll()
                .Where(cr => cr.CreatedDate >= model.Start)
                .Where(cr => cr.CreatedDate < model.End)
                .GroupBy(cr => cr.TopicTypeId)
                .Where(g => !g.FirstOrDefault().TopicType.IsDeleted)
                .Select(g => new TypeSummary
                {
                    Count = g.Count(),
                    Name = g.FirstOrDefault().TopicType.Name,
                    TypeCreatedAt = g.FirstOrDefault().TopicType.CreatedAt
                })
                .OrderByDescending(s => s.Count)
                .ThenBy(s => s.TypeCreatedAt)
                .ToList();

            if(requests.Count == 0)
            {
                var message = string.Format(WebApiResource.Report_NoRequest,
                    model.Start, model.End);
                return BadRequest(message);
            }

            return Ok(requests);
        }


    }
}
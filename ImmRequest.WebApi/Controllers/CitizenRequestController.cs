using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Enums;
using ImmRequest.WebApi.Exceptions;
using ImmRequest.WebApi.Filters;
using ImmRequest.WebApi.Helpers;
using ImmRequest.WebApi.Models;
using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class CitizenRequestController: ControllerBase
    {
        private ILogic<CitizenRequest> CitizenRequestLogic { get; set; }

        public CitizenRequestController(ILogic<CitizenRequest> CitizenRequestLogic)
        {
            this.CitizenRequestLogic = CitizenRequestLogic;
        }

        [HttpPost]
        public IActionResult CreateCitizenRequest([FromBody] CitizenRequestModel requestModel)
        {
            try
            {
                if(requestModel == null)
                {
                    return BadRequest(WebApiResource.CitizenRequest_EmptyRequestMessage);
                }
                var request = requestModel.ToDomain();
                CitizenRequestLogic.Create(request);
                return Ok(WebApiResource.CitizenRequest_CreatedMessage);
            }
            catch (HttpContextException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}

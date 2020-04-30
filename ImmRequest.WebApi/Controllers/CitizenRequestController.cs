using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Enums;
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
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        [AuthorizationFilter]
        public IActionResult GetCitizenRequest(long requestId)
        {
            try
            {
                var request = CitizenRequestLogic.Get(requestId);
                var requestModel = new CitizenRequestModel();
                requestModel.SetModel(request);
                return Ok(requestModel);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("status/{id}")]
        public IActionResult GetCitizenRequestStatus(long requestId)
        {
            try
            {
                var request = CitizenRequestLogic.Get(requestId);
                var statusRequestMessage = string.Format(WebApiResource.CitizenRequest_GetStatusMessage,
                    request.CitizenName, request.Description, request.Status.ToString());
                return Ok(statusRequestMessage);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        [AuthorizationFilter]
        public IActionResult GetCitizenRequests()
        {
            var requests = CitizenRequestLogic.GetAll();
            var requestsModels = CitizenRequestModel.ToModel(requests);
            return Ok(requestsModels);
        }

        [HttpPut("{id}")]
        [AuthorizationFilter]
        public IActionResult UpdateCitizenRequestStatus(long requestId, [FromBody] string status)
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    return BadRequest(WebApiResource.CitizenRequest_EmptyStatusMessage);
                }
                var request = CitizenRequestLogic.Get(requestId);
                request.Status = status.ToEnum<RequestStatus>();
                CitizenRequestLogic.Update(request);
                var statusUpdatedMessage = string.Format(WebApiResource.CitizenRequest_StatusUpdatedMessage,
                    request.Id);
                return Ok(statusUpdatedMessage);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ArgumentException)
            {
                return BadRequest(WebApiResource.CitizenRequest_StatusDoesntExistsMessage);
            }
        }
    }
}

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
using System.Linq;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class CitizenRequestController : ControllerBase
    {
        private ILogic<CitizenRequest> CitizenRequestLogic { get; set; }

        private IFinder<Topic> TopicFinder { get; set; }

        private IFinder<Area> AreaFinder { get; set; }

        public CitizenRequestController(ILogic<CitizenRequest> CitizenRequestLogic, IFinder<Topic> TopicFinder, IFinder<Area> AreaFinder)
        {
            this.CitizenRequestLogic = CitizenRequestLogic;
            this.TopicFinder = TopicFinder;
            this.AreaFinder = AreaFinder;
        }

        [HttpPost]
        public IActionResult CreateCitizenRequest([FromBody] CitizenRequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                {
                    return BadRequest(WebApiResource.EmptyRequestMessage);
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

        [HttpGet("Status/{id}")]
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

        [HttpGet("Areas")]
        public IActionResult GetAllAreas()
        {
            var allAreas = AreaFinder.FindAll();
            var allAreasModel = AreaModel.ToModel(allAreas);
            return Ok(allAreasModel);
        }

        [HttpGet("Topics/{parentAreaId}")]
        public ActionResult GetAllTopicsFromArea(long parentAreaId)
        {
            var all = TopicFinder.FindAll(t => t.AreaId == parentAreaId)
                .ToList();
            var models = TopicModel.ToModel(all);
            return Ok(models);

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
        public IActionResult UpdateCitizenRequestStatus(long requestId, [FromBody] RequestStatus status)
        {
            try
            {
                var request = CitizenRequestLogic.Get(requestId);
                request.Status = status;
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

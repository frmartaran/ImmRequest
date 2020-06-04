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
        private ILogic<CitizenRequest> Logic { get; set; }

        private IFinder<Topic> TopicFinder { get; set; }

        private IFinder<Area> AreaFinder { get; set; }

        public CitizenRequestController(ILogic<CitizenRequest> CitizenRequestLogic, IFinder<Topic> TopicFinder, IFinder<Area> AreaFinder)
        {
            this.Logic = CitizenRequestLogic;
            this.TopicFinder = TopicFinder;
            this.AreaFinder = AreaFinder;
        }

        /// <summary>
        /// Permite al usuario ingresar una solicitud al sistema
        /// </summary>
        /// <param name="requestModel">Este modelo contiene la información acerca de la solicitud</param>
        /// <response code="200">Se creó la solicitud con éxito</response>
        /// <response code="400">Error. No se creó la solicitud</response>
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
                Logic.Create(request);
                Logic.Save();

                return Ok(WebApiResource.CitizenRequest_CreatedMessage);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Permite al administrador obtener una solicitud existente en el sistema
        /// </summary>
        /// <param name="requestId">Este parámetro contiene el número de la solicitud existente</param>
        /// <response code="200">Se devuelve la solicitud requerida.</response>
        /// <response code="400">La solicitud no ha sido encontrada.</response>
        [HttpGet("{requestId}")]
        [AuthorizationFilter]
        public IActionResult GetCitizenRequest(long requestId)
        {
            try
            {
                var request = Logic.Get(requestId);
                var requestModel = new CitizenRequestModel();
                requestModel.SetModel(request);
                return Ok(requestModel);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Permite a un usuario obtener el status de su solicitud
        /// </summary>
        /// <param name="requestId">Este parámetro contiene el número del solicitud existente</param>
        /// <response code="200">Se devuelve el status de la solicitud.</response>
        /// <response code="400">La solicitud no ha sido encontrada.</response>
        [HttpGet("Status/{requestId}")]
        public IActionResult GetCitizenRequestStatus(long requestId)
        {
            try
            {
                var request = Logic.Get(requestId);
                var statusRequestMessage = string.Format(WebApiResource.CitizenRequest_GetStatusMessage,
                    request.CitizenName, request.Description, request.Status.ToString());
                return Ok(statusRequestMessage);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Permite a un usuario obtener todas las áreas del sistema
        /// </summary>
        /// <response code="200">Se devuelven las áreas existentes en el sistema.</response>
        [HttpGet("Areas")]
        public IActionResult GetAllAreas()
        {
            var allAreas = AreaFinder.FindAll();
            var allAreasModel = AreaModel.ToModel(allAreas);
            return Ok(allAreasModel);
        }

        /// <summary>
        /// Permite a un usuario obtener todas los temas de un área del sistema.
        /// </summary>
        /// <param name="parentAreaId">Este parámetro contiene el número de área al cual los temas pertenecen</param>
        /// <response code="200">Se devuelven los temas para el área en el sistema.</response>
        [HttpGet("Topics/{parentAreaId}")]
        public ActionResult GetAllTopicsFromArea(long parentAreaId)
        {
            var all = TopicFinder.FindAll(t => t.AreaId == parentAreaId)
                .ToList();
            var models = TopicModel.ToModel(all);
            return Ok(models);
        }

        /// <summary>
        /// Permite al administrador obtener todas las solicitudes del sistema.
        /// </summary>
        /// <response code="200">Se devuelve la solicitud requerida.</response>
        [HttpGet]
        [AuthorizationFilter]
        public IActionResult GetCitizenRequests()
        {
            var requests = Logic.GetAll();
            var requestsModels = CitizenRequestModel.ToModel(requests);
            return Ok(requestsModels);
        }

        /// <summary>
        /// Permite al administrador actualizar el status de una solicitud existente en el sistema
        /// </summary>
        /// <param name="requestId">Este parámetro contiene el número de la solicitud existente</param>
        /// <param name="model">Este parámetro contiene el nuevo status que debe tener la solicitud</param>
        /// <response code="200">La solicitud requerida fue actualizada.</response>
        /// <response code="400">Status no existe o error en el sistema.</response>
        [HttpPut("{requestId}")]
        [AuthorizationFilter]
        public IActionResult UpdateCitizenRequestStatus(long requestId, [FromBody] StatusModel model)
        {
            try
            {
                var request = Logic.Get(requestId);
                var untrackedRequest = CitizenRequestModel
                    .ToModel(request)
                    .ToDomain();
                untrackedRequest.Status = model.Status;
                Logic.Update(untrackedRequest);
                Logic.Save();

                var statusUpdatedMessage = string.Format(WebApiResource.CitizenRequest_StatusUpdatedMessage,
                    request.Id);
                return Ok(statusUpdatedMessage);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ValidationException exception)
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

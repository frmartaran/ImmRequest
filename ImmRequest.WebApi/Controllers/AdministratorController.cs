using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Filters;
using ImmRequest.WebApi.Models.UserManagement;
using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [AuthorizationFilter]
    public class AdministratorController : ControllerBase
    {
        private IAdministratorLogic Logic { get; set; }

        public AdministratorController(IAdministratorLogic administratorLogic)
        {
            Logic = administratorLogic;
        }

        /// <summary>
        /// Permite a un administrador obtener información de cualquier administrador del sistema
        /// </summary>
        /// <param name="id">Este parámetro contiene el identificador del administrador</param>
        /// <response code="200">Se devuelve la información requerida.</response>
        /// <response code="400">Administrador no existente.</response>
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            try
            {
                var administrator = Logic.Get(id);
                var model = AdministratorModel.ToModel(administrator);
                return Ok(model);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        /// <summary>
        /// Permite a un administrador obtener información de todos los administradores del sistema
        /// </summary>
        /// <response code="200">Se devuelve la información requerida.</response>
        [HttpGet]
        public ActionResult GetAll()
        {
            var allAdministrator = Logic.GetAll();
            var models = AdministratorModel.ToModel(allAdministrator);
            return Ok(models);
        }

        /// <summary>
        /// Permite a un administrador crear otro administrador en el sistema
        /// </summary>
        /// <param name="model">Este modelo contiene la información del administrador</param>
        /// <response code="200">Se creó el administrador</response>
        /// <response code="400">Error. No se creó el administrador</response>
        [HttpPost]
        public ActionResult Create([FromBody] AdministratorModel model)
        {
            if (model == null)
                return BadRequest(WebApiResource.EmptyRequestMessage);
            try
            {
                var administratorToCreate = model.ToDomain();
                Logic.Create(administratorToCreate);
                var responseMessage = string.Format("{0} {1}", WebApiResource.Entities_Administrator,
                    WebApiResource.Action_Created);
                return Ok(responseMessage);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        /// <summary>
        /// Permite a un administrador actualizar información de otro administrador en el sistema
        /// </summary>
        /// <param name="id">Este parámetro contiene el identificador del administrador</param>
        /// <param name="model">Este modelo contiene la información a actualizar del administrador</param>
        /// <response code="200">Se actualizó información del administrador</response>
        /// <response code="400">Error. No se actualizó información del administrador</response>
        [HttpPut("{id}")]
        public ActionResult Update(long id, [FromBody] AdministratorModel model)
        {
            if (model == null)
                return BadRequest(WebApiResource.EmptyRequestMessage);

            try
            {
                var modifiedAdministrator = model.ToDomain();
                modifiedAdministrator.Id = id;
                Logic.Update(modifiedAdministrator);
                var message = string.Format("{0}: {1} {2}", WebApiResource.Entities_Administrator,
                    modifiedAdministrator.UserName, WebApiResource.Action_Updated);
                return Ok(message);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        /// <summary>
        /// Permite a un administrador borrar otro administrador del sistema
        /// </summary>
        /// <param name="id">Este parámetro contiene el identificador del administrador</param>
        /// <response code="200">Se borró el administrador del sistema</response>
        /// <response code="400">Error. No se pudo borrar al administrador</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                Logic.Delete(id);
                var responseMessage = string.Format("{0} {1}", WebApiResource.Entities_Administrator,
                        WebApiResource.Action_Deleted);
                return Ok(responseMessage);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
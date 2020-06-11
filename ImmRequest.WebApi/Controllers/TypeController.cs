using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Exceptions;
using ImmRequest.WebApi.Filters;
using ImmRequest.WebApi.Models;
using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class TypeController : ControllerBase
    {
        private ILogic<TopicType> Logic { get; set; }

        private IFinder<Topic> Finder { get; set; }

        public TypeController(ILogic<TopicType> logic, IFinder<Topic> finder)
        {
            Logic = logic;
            Finder = finder;
        }

        /// <summary>
        /// Permite a un usuario crear un tipo de solicitud especificando su tema.
        /// </summary>
        /// <param name="parentTopicID">Este parámetro contiene el identificador del tema</param>
        /// <param name="model">Este modelo contiene la información del nuevo tipo.</param>
        /// <response code="200">Se creó el tipo con éxito</response>
        /// <response code="400">Error. No se pudo crear el tipo.</response>        
        [HttpPost("{parentTopicID}")]
        [AuthorizationFilter]
        public ActionResult Create(long parentTopicID, [FromBody] TypeModel model)
        {
            if (model == null)
                return BadRequest(WebApiResource.EmptyRequestMessage);

            try
            {
                var parentTopic = Finder.Find(t => t.Id == parentTopicID);
                var type = model.ToDomain();
                type.ParentTopic = parentTopic;
                Logic.Create(type);
                Logic.Save();

                var responseMessage = string.Format("{0} {1}", WebApiResource.Entities_Type,
                    WebApiResource.Action_Created);
                return Ok(new SuccessfulCreateModel(type.Id, responseMessage));
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (InvalidArgumentException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        /// <summary>
        /// Permite a un usuario obtener un tipo.
        /// </summary>
        /// <param name="id">Este parámetro contiene el identificador del tipo en el sistema</param>
        /// <response code="200">Se obtuvo el tipo con éxito</response>
        /// <response code="400">Error. No se pudo obtener el tipo.</response>
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            try
            {
                var type = Logic.Get(id);
                var model = TypeModel.ToModel(type);
                return Ok(model);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        /// <summary>
        /// Permite a un usuario obtener todos los tipos del sistema.
        /// </summary>
        /// <response code="200">Se borró el administrador del sistema</response>
        [HttpGet]
        public ActionResult GetAll()
        {
            var all = Logic.GetAll();
            var models = TypeModel.ToModel(all);
            return Ok(models);
        }

        /// <summary>
        /// Permite a un usuario obtener todos los tipos para un tema del sistema.
        /// </summary>
        /// <param name="parentTopicId">Este parámetro contiene el identificador del tema</param>
        /// <response code="200">Se obtuvieron los tipos con éxito<response>
        [HttpGet("All/{parentTopicId}")]
        public ActionResult GetAll(long parentTopicId)
        {
            var all = Logic.GetAll()
                .Where(t => t.ParentTopicId == parentTopicId)
                .ToList();
            var models = TypeModel.ToModel(all);
            return Ok(models);
        }

        /// <summary>
        /// Permite a un usuario borrar un tipo del sistema.
        /// </summary>
        /// <param name="id">Este parámetro contiene el identificador del tipo en el sistema</param>
        /// <response code="200">Se borró el tipo del sistema</response>
        /// <response code="400">Error. No se pudo borrar al tipo</response>
        [HttpDelete("{id}")]
        [AuthorizationFilter]
        public ActionResult Delete(long id)
        {
            try
            {
                Logic.Delete(id);
                Logic.Save();

                var responseMessage = string.Format("{0} {1}", WebApiResource.Entities_Type,
                        WebApiResource.Action_Deleted);
                return Ok(responseMessage);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Permite a un usuario actualizar la información de un tipo.
        /// </summary>
        /// <param name="id">Este parámetro contiene el identificador del tipo en el sistema</param>
        /// <response code="200">Se actualizó el tipo con éxito</response>
        /// <response code="400">Error. No se pudo actualizar el tipo.</response>
        [HttpPut("{id}")]
        [AuthorizationFilter]
        public ActionResult Update(long id, [FromBody] TypeModel model)
        {
            if (model == null)
                return BadRequest(WebApiResource.EmptyRequestMessage);
            try
            {
                var type = model.ToDomain();
                type.Id = id;
                var oldType = Logic.Get(id);
                type.ParentTopic = oldType.ParentTopic;
                Logic.Update(type);
                Logic.Save();

                var message = string.Format("{0}: {1} {2}", WebApiResource.Entities_Type,
                    type.Name, WebApiResource.Action_Updated);
                return Ok(message);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (InvalidArgumentException exception)
            {
                return BadRequest(exception.Message);
            }

        }

    }
}

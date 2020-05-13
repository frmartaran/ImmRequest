using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
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

        [HttpPost("{parentTopicID}")]
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
                var responseMessage = string.Format("{0} {1}", WebApiResource.Entities_Type,
                    WebApiResource.Action_Created);
                return Ok(responseMessage);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }

        }

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

        [HttpGet]
        public ActionResult GetAll()
        {
            var all = Logic.GetAll();
            var models = TypeModel.ToModel(all);
            return Ok(models);
        }

        [HttpGet("All/{parentTopicId}")]
        public ActionResult GetAll(long parentTopicId)
        {
            var all = Logic.GetAll()
                .Where(t => t.ParentTopicId == parentTopicId)
                .ToList();
            var models = TypeModel.ToModel(all);
            return Ok(models);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                Logic.Delete(id);
                var responseMessage = string.Format("{0} {1}", WebApiResource.Entities_Type,
                        WebApiResource.Action_Deleted);
                return Ok(responseMessage);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
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

        }

    }
}

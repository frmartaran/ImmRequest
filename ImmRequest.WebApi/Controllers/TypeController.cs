using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using ImmRequest.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private ILogic<TopicType> Logic { get; set; }

        private IFinder<Topic> Finder { get; set; }

        public TypeController(ILogic<TopicType> logic, IFinder<Topic> finder)
        {
            Logic = logic;
            Finder = finder;
        }

        [HttpPost("{id}")]
        public ActionResult Create(long parentTopicID, [FromBody] TypeModel model)
        {
            try
            {
                var parentTopic = Finder.Find(t => t.Id == parentTopicID);
                var type = model.ToDomain();
                type.ParentTopic = parentTopic;
                Logic.Create(type);

                return Ok("Type Created");
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
            catch(BusinessLogicException exception)
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
                return Ok("Deleted");
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(long id, [FromBody] TypeModel model)
        {
            try
            {
                var type = model.ToDomain();
                type.Id = id;
                Logic.Update(type);
                return Ok("Updated");
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

    }
}

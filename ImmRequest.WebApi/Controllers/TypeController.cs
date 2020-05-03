using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var parentTopic = Finder.Find(t => t.Id == parentTopicID);
            var type = model.ToDomain();
            type.ParentTopic = parentTopic;
            Logic.Create(type);

            return Ok("Type Created");
        }
    }
}

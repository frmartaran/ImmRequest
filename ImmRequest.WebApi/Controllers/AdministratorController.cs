using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Filters;
using ImmRequest.WebApi.Models.UserManagement;
using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationFilter]
    public class AdministratorController : ControllerBase
    {
        private IAdministratorLogic Logic { get; set; }

        public AdministratorController(IAdministratorLogic administratorLogic)
        {
            Logic = administratorLogic;
        }


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
                return BadRequest(exception);
            }

        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var allAdministrator = Logic.GetAll();
            var models = AdministratorModel.ToModel(allAdministrator);
            return Ok(models);
        }

        [HttpPost]
        public ActionResult Create([FromBody] AdministratorModel model)
        {
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
                return BadRequest(exception);
            }

        }

        [HttpPut("{id}")]
        public ActionResult Update(long id, [FromBody] AdministratorModel model)
        {
            throw new NotImplementedException();
        }

    }
}
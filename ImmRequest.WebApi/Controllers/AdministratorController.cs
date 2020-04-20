using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Filters;
using ImmRequest.WebApi.Models.UserManagement;
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
            var administrator = Logic.Get(id);
            var model = AdministratorModel.ToModel(administrator);
            return Ok(model);
        }

    }
}
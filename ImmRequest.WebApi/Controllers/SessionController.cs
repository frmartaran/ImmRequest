using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Models.UserManagement;
using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ISessionLogic Logic { get; set; }
        private IAdministratorLogic AdministratorLogic { get; set; }

        public SessionController(ISessionLogic logic, IAdministratorLogic administratorLogic)
        {
            Logic = logic;
            AdministratorLogic = administratorLogic;
        }

        [HttpPost]
        public ActionResult Login([FromBody] SessionModel model)
        {
            var administrator = AdministratorLogic
                .FindAdministratorByCredentials(model.Email, model.Password);
            if (administrator == null)
                return BadRequest(WebApiResource.SessionController_UserNotFound);

            try
            {
                var session = model.ToDomain();
                session.AdministratorId = administrator.Id;
                var token = Logic.Create(session);
                return Ok(token);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
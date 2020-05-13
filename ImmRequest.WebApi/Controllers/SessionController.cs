using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Exceptions;
using ImmRequest.WebApi.Helpers;
using ImmRequest.WebApi.Interfaces;
using ImmRequest.WebApi.Models.UserManagement;
using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class SessionController : ControllerBase
    {
        private SessionControllerInputHelper Inputs { get; set; }

        public SessionController(SessionControllerInputHelper inputs)
        {
            Inputs = inputs;
        }

        [HttpPost]
        public ActionResult Login([FromBody] SessionModel model)
        {
            if (model == null)
                return BadRequest(WebApiResource.EmptyRequestMessage);
            var administrator = Inputs.AdministratorLogic
                .FindAdministratorByCredentials(model.Email, model.Password);
            if (administrator == null)
                return BadRequest(WebApiResource.SessionController_UserNotFound);

            try
            {
                var session = model.ToDomain();
                session.AdministratorId = administrator.Id;
                var token = Inputs.Logic.Create(session);
                return Ok(token);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete]
        public ActionResult Logout()
        {
            try
            {
                var token = Inputs.ContextHelper.GetAuthorizationHeader(HttpContext);
                var sessionToDelete = Inputs.Logic.Get(token);
                Inputs.Logic.Delete(sessionToDelete.Id);
                return Ok();
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (HttpContextException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
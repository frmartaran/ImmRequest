using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.WebApi.Exceptions;
using ImmRequest.WebApi.Helpers.Inputs;
using ImmRequest.WebApi.Models.UserManagement;
using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Cors;
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

        /// <summary>
        /// Permite a un administrador loguearse al sistema.
        /// </summary>
        /// <param name="model">Este modelo contiene la información para iniciar sesión</param>
        /// <response code="200">Se inició sesión con éxito</response>
        /// <response code="400">Error. No se pudo iniciar sesión.</response>
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
                session.AdministratorInSessionId = administrator.Id;
                var token = Inputs.Logic.Create(session);
                Inputs.Logic.Save();
                model.Id = session.AdministratorInSessionId;
                model.Token = token;
                model.Username = administrator.UserName;
                model.Password = "";
                return Ok(model);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        /// <summary>
        /// Permite a un administrador cerrar sesión en el sistema.
        /// </summary>
        /// <response code="200">Se cerró sesión con éxito</response>
        /// <response code="400">Error. No se pudo cerrar la sesión.</response>
        [HttpDelete]
        public ActionResult Logout()
        {
            try
            {
                var token = Inputs.ContextHelper.GetAuthorizationHeader(HttpContext);
                var sessionToDelete = Inputs.Logic.Get(token);
                Inputs.Logic.Delete(sessionToDelete.Id);
                Inputs.Logic.Save();
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
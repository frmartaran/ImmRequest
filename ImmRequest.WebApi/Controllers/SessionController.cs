using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ISession Logic { get; set; }

        public SessionController(ISession logic)
        {
            Logic = logic;
        }

        [HttpPost]
        public ActionResult Login()
        {
            throw new NotImplementedException();
        }
    }
}
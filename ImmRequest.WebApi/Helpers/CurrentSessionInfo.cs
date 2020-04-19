using ImmRequest.WebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Helpers
{
    public class CurrentSessionInfo : IContextHelper
    {
        public Guid GetAuthorizationHeader(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}

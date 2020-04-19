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
        private const string Authorization_Header = "Authorization";
        public Guid GetAuthorizationHeader(HttpContext context)
        {
            var tokenString = context.Request.Headers[Authorization_Header];
            Guid.TryParse(tokenString, out var token);
            return token;
        }
    }
}

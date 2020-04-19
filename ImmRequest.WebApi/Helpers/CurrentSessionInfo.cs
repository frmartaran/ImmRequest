using ImmRequest.WebApi.Exceptions;
using ImmRequest.WebApi.Interfaces;
using ImmRequest.WebApi.Resources;
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
            var isGuid = Guid.TryParse(tokenString, out var token);
            if (!isGuid)
                throw new HttpContextException(WebApiResource.AuthorizationFilter_InvalidTokenFormat);
            return token;
        }
    }
}

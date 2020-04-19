using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ImmRequest.WebApi.Interfaces
{
    public interface IContextHelper
    {
        Guid GetAuthorizationHeader(HttpContext context);
    }
}

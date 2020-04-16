using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IActionFilter
    {
        private const string Authorization_Header = "Authorization";
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers[Authorization_Header];
            if (string.IsNullOrEmpty(token))
            {
                var message = WebApiResource.AuthorizationFilter_TokenEmpty;
                context.Result = new ContentResult
                {
                    StatusCode = 400,
                    Content = message
                };
                return;
            }
        }
    }
}

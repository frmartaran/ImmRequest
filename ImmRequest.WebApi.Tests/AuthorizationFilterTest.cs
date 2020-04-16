using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging.Abstractions;
using ImmRequest.WebApi.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ImmRequest.WebApi.Tests
{
    [TestClass]
    public class AuthorizationFilterTest
    {
        [TestMethod]
        public void TokenIsNullTest()
        {
            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "";

            var actionContext = new ActionContext
            (
                httpContext,
                Mock.Of<Microsoft.AspNetCore.Routing.RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var executingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            var filter = new AuthorizationFilter();
            filter.OnActionExecuting(executingContext);

            Assert.IsInstanceOfType(executingContext.Result, typeof(BadRequestObjectResult));
        }
    }
}

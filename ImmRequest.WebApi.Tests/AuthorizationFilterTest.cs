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
using System;

namespace ImmRequest.WebApi.Tests
{
    [TestClass]
    public class AuthorizationFilterTest
    {
        private static ActionExecutingContext CreateActionExecutingContextMock(string token)
        {
            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = token;

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
            return executingContext;
        }

        [TestMethod]
        public void TokenIsNullTest()
        {
            var executingContext = CreateActionExecutingContextMock("");

            var filter = new AuthorizationFilter();
            filter.OnActionExecuting(executingContext);

            Assert.IsInstanceOfType(executingContext.Result, typeof(ContentResult));
            var result = executingContext.Result as ContentResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void TokenIsNotGuidTest()
        {
            var executingContext = CreateActionExecutingContextMock("some token");
            var filter = new AuthorizationFilter();
            filter.OnActionExecuting(executingContext);

            Assert.IsInstanceOfType(executingContext.Result, typeof(ContentResult));
            var result = executingContext.Result as ContentResult;
            Assert.AreEqual(403, result.StatusCode);
        }

        
    }
}

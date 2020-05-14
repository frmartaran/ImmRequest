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
using ImmRequest.BusinessLogic.Logic;
using Microsoft.Extensions.DependencyInjection;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.UserManagement;
using ImmRequest.BusinessLogic.Interfaces;

namespace ImmRequest.WebApi.Tests.FilterTests
{
    [TestClass]
    public class AuthorizationFilterTest
    {


        private static ActionExecutingContext CreateActionExecutingContextMock(string token, Session session)
        {
            var mockValidator = new Mock<IValidator<Session>>().Object;

            var mockRepository = new Mock<IRepository<Session>>();
            mockRepository.Setup(m => m.GetAll()).Returns(new List<Session> { session });

            var services = new Mock<IServiceProvider>();
            services.Setup(m => m.GetService(It.IsAny<Type>()))
                .Returns(new SessionLogic(mockRepository.Object, mockValidator));

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = token;
            httpContext.RequestServices = services.Object;

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
            var executingContext = CreateActionExecutingContextMock("", new Session());

            var filter = new AuthorizationFilter();
            filter.OnActionExecuting(executingContext);

            Assert.IsInstanceOfType(executingContext.Result, typeof(ContentResult));
            var result = executingContext.Result as ContentResult;
            Assert.AreEqual(403, result.StatusCode);
        }

        [TestMethod]
        public void TokenIsNotGuidTest()
        {
            var executingContext = CreateActionExecutingContextMock("some token", new Session());
            var filter = new AuthorizationFilter();
            filter.OnActionExecuting(executingContext);

            Assert.IsInstanceOfType(executingContext.Result, typeof(ContentResult));
            var result = executingContext.Result as ContentResult;
            Assert.AreEqual(403, result.StatusCode);
        }

        [TestMethod]
        public void NoSessionWithToken()
        {
            var token = Guid.NewGuid();
            var executingContext = CreateActionExecutingContextMock(token.ToString(), new Session());
            var filter = new AuthorizationFilter();
            filter.OnActionExecuting(executingContext);

            Assert.IsInstanceOfType(executingContext.Result, typeof(ContentResult));
            var result = executingContext.Result as ContentResult;
            Assert.AreEqual(403, result.StatusCode);
        }

        [TestMethod]
        public void ValidToken()
        {
            var token = Guid.NewGuid();
            var sessionInDb = new Session { Token = token };

            var executingContext = CreateActionExecutingContextMock(token.ToString(), sessionInDb);
            var filter = new AuthorizationFilter();
            filter.OnActionExecuting(executingContext);

            Assert.IsNull(executingContext.Result);
        }
    }
}

using ImmRequest.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.WebApi.Tests.HelperTests
{
    [TestClass]
    public class CurrentSessionInfoTest
    {
        [TestMethod]
        public void ObtainAuthorizationTokenTest()
        {
            var token = Guid.NewGuid();
            var context = new DefaultHttpContext();
            context.Request.Headers["Authorization"] = token.ToString();

            var helper = new CurrentSessionInfo();
            var tokenInContext = helper.GetAuthorizationHeader(context);
            Assert.AreEqual(token, tokenInContext);

        }
    }
}

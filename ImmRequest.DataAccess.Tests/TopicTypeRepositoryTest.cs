using System;
using System.Collections.Generic;
using ImmRequest.DataAccess.Context;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class TopicTypeRepositoryTest
    {
        private ImmDbContext context;

        private TopicType type;

        [TestInitialize]
        public void SetUp()
        {
            type = new TopicType
            {
                Name = "Some Type",
                AllFields = new List<BaseField>()
            };
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Dispose();
        }


    }
}
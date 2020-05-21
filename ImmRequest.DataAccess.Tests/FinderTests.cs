using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Helpers;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.Domain;
using ImmRequest.Domain.Enums;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class FinderTests
    {
        [TestMethod]
        public void FindAreaByNameTest()
        {
            var context = ContextFactory.GetMemoryContext("Find Area");
            var area = new Area { Name = "Name" };
            context.Areas.Add(area);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var areaFound = finder.Find<Area>(a => a.Name == "Name");
            context.Dispose();
            Assert.IsNotNull(areaFound);
        }

        [TestMethod]
        public void FindTopicByNameTest()
        {
            var context = ContextFactory.GetMemoryContext("Find Topic");
            var topic = new Topic { Name = "Name" };
            context.Topics.Add(topic);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var topicFound = finder.Find<Topic>(a => a.Name == "Name");
            context.Dispose();
            Assert.IsNotNull(topicFound);
        }

        [TestMethod]
        public void FindTypeByNameTest()
        {
            var context = ContextFactory.GetMemoryContext("Find Topic");
            var type = new TopicType { Name = "Name" };
            context.TopicTypes.Add(type);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var typeFound = finder.Find<TopicType>(a => a.Name == "Name");
            context.Dispose();
            Assert.IsNotNull(typeFound);
        }

        [TestMethod]
        public void FindFieldByTypeTest()
        {
            var context = ContextFactory.GetMemoryContext("Find Topic");
            var field = new NumberField { Name = "Number" };
            var anotherField = new TextField();
            context.Fields.Add(field);
            context.Fields.Add(anotherField);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var fieldFound = finder.Find<BaseField>(f => f is NumberField);
            context.Dispose();
            Assert.IsNotNull(fieldFound);
            Assert.AreEqual(field.Name, fieldFound.Name);
        }

        [TestMethod]
        public void FindCitizenRequestByStatusTest()
        {
            var context = ContextFactory.GetMemoryContext("Find Topic");
            var request = new CitizenRequest { Status = RequestStatus.Acepted };
            context.CitizenRequests.Add(request);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var requestFound = finder.Find<CitizenRequest>(r => r.Status == RequestStatus.Acepted);
            context.Dispose();
            Assert.IsNotNull(requestFound);
        }

        [TestMethod]
        [ExpectedException(typeof(DBDeveloperException))]
        public void TypeWithoutDBSetTest()
        {
            var context = ContextFactory.GetMemoryContext("Find Topic");
            var topic = new MockClassWithOutDbSet { FilterProperty = "Test" };

            var finder = new DatabaseFinder(context);
            var topicFound = finder.Find<MockClassWithOutDbSet>(m => m.FilterProperty == "Test");
        }

        internal class MockClassWithOutDbSet
        {
            public string FilterProperty { get; set; }
        }
    }
}

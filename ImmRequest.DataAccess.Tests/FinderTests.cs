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
            var context = ContextFactory.GetMemoryContext("Find Type");
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
            var context = ContextFactory.GetMemoryContext("Find Field");
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
            var context = ContextFactory.GetMemoryContext("Find Request");
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
            var context = ContextFactory.GetMemoryContext("NO DB Set");
            var topic = new MockClassWithOutDbSet { FilterProperty = "Test" };

            var finder = new DatabaseFinder(context);
            var topicFound = finder.Find<MockClassWithOutDbSet>(m => m.FilterProperty == "Test");
        }

        [TestMethod]
        public void FindAllAreasWithoutTopics()
        {
            var context = ContextFactory.GetMemoryContext("Find All Areas");
            var area1 = new Area { Name = "Area1", Topics = new List<Topic>() };
            var area2 = new Area { Name = "Area2", Topics = new List<Topic>() };
            var area3 = new Area { Name = "Area2", Topics = new List<Topic> { new Topic() } };
            context.Areas.Add(area1);
            context.Areas.Add(area2);
            context.Areas.Add(area3);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var allAreasWithoutTopics = finder.FindAll<Area>(a => a.Topics.Count == 0);
            Assert.AreEqual(2, allAreasWithoutTopics.Count);


        }

        [TestMethod]
        public void FindAllTopicsWithoutTypes()
        {
            var context = ContextFactory.GetMemoryContext("Find All Topics");
            var topic = new Topic { Name = "Area1", Types = new List<TopicType>() };
            var topic2 = new Topic { Name = "Area2", Types = new List<TopicType>() };
            var topic3 = new Topic { Name = "Area2", Types = new List<TopicType> { new TopicType() } };
            context.Topics.Add(topic);
            context.Topics.Add(topic2);
            context.Topics.Add(topic3);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var allTopicsWithoutTypes = finder.FindAll<Topic>(a => a.Types.Count == 0);
            Assert.AreEqual(2, allTopicsWithoutTypes.Count);


        }

        [TestMethod]
        public void FindAllTypesWithoutFields()
        {
            var context = ContextFactory.GetMemoryContext("Find All Types");
            var type = new TopicType { Name = "Area1", AllFields = new List<BaseField>() };
            var type2 = new TopicType { Name = "Area2", AllFields = new List<BaseField>() };
            var type3 = new TopicType { Name = "Area2", AllFields = new List<BaseField> { new NumberField() } };
            context.TopicTypes.Add(type);
            context.TopicTypes.Add(type2);
            context.TopicTypes.Add(type3);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var allTypesWithoutFields = finder.FindAll<TopicType>(a => a.AllFields.Count == 0);
            Assert.AreEqual(2, allTypesWithoutFields.Count);
        }

        [TestMethod]
        public void FindAllFieldsWithoutName()
        {
            var context = ContextFactory.GetMemoryContext("Find All Fields");
            var numberField = new NumberField { Name = "" };
            var textField = new TextField { Name = "" };
            var dateTimefield = new DateTimeField { Name = "Birthday" };
            context.Fields.Add(numberField);
            context.Fields.Add(textField);
            context.Fields.Add(dateTimefield);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var allFieldsWithoutName = finder.FindAll<BaseField>(f => f.Name == "");
            Assert.AreEqual(2, allFieldsWithoutName.Count);
        }


        [TestMethod]
        public void FindCitizenRequestByCreatedDate()
        {
            var context = ContextFactory.GetMemoryContext("Find All Areas");
            var newYear = new DateTime(2020, 1, 1);
            var request = new CitizenRequest { CreatedDate = new DateTime(2020, 1, 1) };
            var request2 = new CitizenRequest { CreatedDate = new DateTime(2020, 2, 1) };
            var request3 = new CitizenRequest { CreatedDate = new DateTime(2020, 2, 10) };
            context.CitizenRequests.Add(request);
            context.CitizenRequests.Add(request2);
            context.CitizenRequests.Add(request3);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var allRequest = finder.FindAll<CitizenRequest>(cr => cr.CreatedDate > newYear);
            Assert.AreEqual(2, allRequest.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(DBDeveloperException))]
        public void FindAllWithoutDBSetTest()
        {
            var context = ContextFactory.GetMemoryContext("No set");
            var finder = new DatabaseFinder(context);
            finder.FindAll<MockClassWithOutDbSet>(m => m.FilterProperty == "");
            
        }




        internal class MockClassWithOutDbSet
        {
            public string FilterProperty { get; set; }
        }
    }
}

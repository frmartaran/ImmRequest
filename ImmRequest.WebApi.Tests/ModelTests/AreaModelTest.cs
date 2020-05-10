using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ImmRequest.WebApi.Tests.ModelTests
{
    [TestClass]
    public class AreaModelTest
    {
        private Area area;

        [TestInitialize]
        public void Setup()
        {
            var topicType = new TopicType
            {
                Id = 1,
                AllFields = new List<BaseField>(),
                Name = "Francisco's type",
                ParentTopicId = 1
            };
            var topicTypes = new List<TopicType>
            {
                topicType
            };
            var topic = new Topic
            {
                Id = 1,
                AreaId = 1,
                Name = "Francisco's topic",
                Types = topicTypes
            };
            area = new Area
            {
                Id = 1,
                Name = "Francisco's Area",
                Topics = new List<Topic>
                {
                    topic
                }
            };
        }

        [TestMethod]
        public void SetModel()
        {
            var areaModel = new AreaModel();

            areaModel.SetModel(area);

            Assert.AreEqual(area.Id, areaModel.Id);
            Assert.AreEqual(area.Name, areaModel.Name);
            Assert.AreEqual(area.Topics.FirstOrDefault().Id, areaModel.Topics.FirstOrDefault().Id);
        }
    }
}

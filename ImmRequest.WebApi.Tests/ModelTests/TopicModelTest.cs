using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.WebApi.Tests.ModelTests
{
    [TestClass]
    public class TopicModelTest
    {
        private Topic topic;

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
            topic = new Topic
            {
                Id = 1,
                AreaId = 1,
                Name = "Francisco's topic",
                Types = topicTypes
            };
        }

        [TestMethod]
        public void SetModel()
        {
            var topicModel = new TopicModel();

            topicModel.SetModel(topic);

            Assert.AreEqual(topic.Id, topicModel.Id);
            Assert.AreEqual(topic.AreaId, topicModel.AreaId);
            Assert.AreEqual(topic.Name, topicModel.Name);
            Assert.AreEqual(topic.Types.FirstOrDefault().Id, topicModel.Types.FirstOrDefault().Id);
        }

        [TestMethod]
        public void ToDomain()
        {
            var topicModel = TopicModel.ToModel
            (
                new List<Topic>
                {
                    topic
                }
            )
            .ToList()
            .FirstOrDefault();

            var request = topicModel.ToDomain();

            Assert.AreEqual(topicModel.Id, request.Id);
            Assert.AreEqual(topicModel.AreaId, request.AreaId);
            Assert.AreEqual(topicModel.Name, request.Name);
            Assert.AreEqual(topicModel.Types.FirstOrDefault().Id, request.Types.FirstOrDefault().Id);
        }
    }
}

using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class CitizenRequestValidatorTest : CitizenRequestValidator
    {
        public ImmDbContext context;

        public CitizenRequest citizenRequest;

        [TestInitialize]
        public void Setup()
        {
            var additionalTextValues = new TextField
            {
                Id = 1,
                Name = "AdditionalTextValues",
                RangeValues = new List<string>
                {
                    "Credencial", "Cedula"
                },
                ParentTypeId = 1
            };
            var topicType = new TopicType
            {
                Id = 1,
                AllFields = new List<BaseField>
                {
                    additionalTextValues
                },
                Name = "Renovacion",
                ParentTopicId = 1
            };
            var topic = new Topic
            {
                Id = 1,
                Name = "TramitesLegales",
                Types = new List<TopicType>
                {
                    topicType
                },
                AreaId = 1
            };
            var area = new Area
            {
                Id = 1,
                Name = "Area1",
                Topics = new List<Topic>
                {
                    topic
                }
            };
            var requestFieldValue = new RequestFieldValues
            {
                Id = 1,
                ParentCitizenRequestId = 1,
                FieldId = 1,
                Value = "11111"
            };

            citizenRequest = new CitizenRequest
            {
                Id = 1,
                CitizenName = "Francisco",
                Description = "Quiero mi credencial!",
                Email = "immrequest@gmail.com",
                Phone = "21233457",
                RequestNumber = 1,
                Status = Domain.Enums.RequestStatus.Created,
                AreaId = 1,
                TopicId = 1,
                TopicTypeId = 1,
                Values = new List<RequestFieldValues>
                {
                    requestFieldValue
                }
            };

            context = ContextFactory.GetMemoryContext("Citizen request validator");

            AreaRepository = new AreaRepository(context);
            TopicRepository = new TopicRepository(context);
            TopicTypeRepository = new TopicTypeRepository(context);
            FieldsRepository = new FieldsRepository(context);

            FieldsRepository.Insert(additionalTextValues);
            TopicTypeRepository.Insert(topicType);
            TopicRepository.Insert(topic);
            AreaRepository.Insert(area);
        }

        [TestCleanup]
        public void CleanUp()
        {
            context.Dispose();
        }
    }
}

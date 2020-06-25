using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ImmRequest.WebApi.Tests.ModelTests
{
    [TestClass]
    public class RequestFieldValuesModelTest
    {
        private RequestFieldValues requestFieldValues;

        [TestInitialize]
        public void Setup()
        {
            requestFieldValues = new RequestFieldValues
            {
                Id = 1,
                ParentCitizenRequestId = 1,
                FieldId = 1,
                Field= new TextField
                {
                    Name = "Documentos"
                },
                Values = new List<string> { "Credencial" }
            };
        }

        [TestMethod]
        public void SetModel()
        {
            var requestFieldValuesModel = new RequestFieldValuesModel();
            requestFieldValuesModel.SetModel(requestFieldValues);

            Assert.AreEqual(requestFieldValues.Id, requestFieldValuesModel.Id);
            Assert.AreEqual(requestFieldValues.FieldId, requestFieldValuesModel.FieldId);
            Assert.AreEqual(requestFieldValues.ParentCitizenRequestId, requestFieldValuesModel.ParentCitizenRequestId);
            Assert.AreEqual(requestFieldValues.Values, requestFieldValuesModel.Value);
        }

        [TestMethod]
        public void ToDomain()
        {
            var requestFieldValuesModel = new RequestFieldValuesModel
            {
                ParentCitizenRequestId = 1,
                FieldId = 1,
                Value = new List<string> { "Credencial" }
            };
            var requestFieldValues = requestFieldValuesModel.ToDomain();

            Assert.AreEqual(requestFieldValuesModel.FieldId, requestFieldValues.FieldId);
            Assert.AreEqual(requestFieldValuesModel.ParentCitizenRequestId, requestFieldValues.ParentCitizenRequestId);
            Assert.AreEqual(requestFieldValuesModel.Value, requestFieldValues.Values);
        }
    }
}

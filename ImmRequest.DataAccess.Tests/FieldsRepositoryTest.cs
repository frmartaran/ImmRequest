using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class FieldsRepositoryTest
    {
        private ImmDbContext context;

        private FieldsRepository repository;

        private NumberField numberField;

        private TextField textField;

        private DateTimeField dateTimeField;

        [TestInitialize]
        public void SetUp()
        {
            numberField = new NumberField
            {
                ParentType = new TopicType(),
                Name = "Ages",
                RangeStart = 18,
                RangeEnd = 50
            };

            textField = new TextField
            {
                ParentType = new TopicType(),
                Name = "Allergies",
                RangeValues = new List<string> { "Dust", "Spring", "Literally Everything Else" }

            };

            dateTimeField = new DateTimeField
            {
                ParentType = new TopicType(),
                Name = "Cool People Birth dates",
                Start = new DateTime(1990, 1, 1),
                End = new DateTime(1999, 12, 31),
            };
        }

        private void CreateRespostory(string name)
        {
            context = ContextFactory.GetMemoryContext(name);
            repository = new FieldsRepository(context);
        }

        [TestMethod]
        public void SaveTest()
        {
            CreateRespostory("Fields Save Test");
            context.Fields.Add(numberField);
            repository.Save();

            var fieldInDb = context.Fields.FirstOrDefault();
            Assert.IsNotNull(fieldInDb);
        }

        [TestMethod]
        public void InsertTest()
        {
            CreateRespostory("Fields Insert Test");
            repository.Insert(numberField);
            repository.Insert(textField);
            repository.Insert(dateTimeField);

            var numberFieldInDb = context.Fields
                .ToList()
                .OfType<NumberField>()
                .FirstOrDefault();
            var datesFieldInDb = context.Fields
                .ToList()
                .OfType<DateTimeField>()
                .FirstOrDefault();
            var textFieldInDb = context.Fields
                .ToList()
                .OfType<TextField>()
                .FirstOrDefault();

            Assert.IsNotNull(numberFieldInDb);
            Assert.IsNotNull(datesFieldInDb);
            Assert.IsNotNull(textField);
        }
    }
}
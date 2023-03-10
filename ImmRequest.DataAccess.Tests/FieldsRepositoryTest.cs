using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces.Exceptions;
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
            repository.Save();


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

        [TestMethod]
        public void GetTest()
        {
            CreateRespostory("Fields Get Test");
            context.Fields.Add(numberField);
            context.Fields.Add(textField);
            context.Fields.Add(dateTimeField);
            context.SaveChanges();

            var numberFieldInDb = repository.Get(1);
            var textFieldInDb = repository.Get(2);
            var datesFieldInDb = repository.Get(3);

            Assert.IsNotNull(numberFieldInDb);
            Assert.IsNotNull(datesFieldInDb);
            Assert.IsNotNull(textField);

        }

        [TestMethod]
        public void GetAllTest()
        {
            CreateRespostory("Fields Get All Test");
            context.Fields.Add(numberField);
            context.Fields.Add(textField);
            context.Fields.Add(dateTimeField);
            context.SaveChanges();

            var allFields = repository.GetAll();
            Assert.AreEqual(3, allFields.Count);
        }

        [TestMethod]
        public void ExistTest()
        {
            CreateRespostory("Field Exists Test");
            context.Fields.Add(numberField);
            context.SaveChanges();

            var exists = repository.Exists(numberField);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void DoesNotExistTest()
        {
            CreateRespostory("Field Does Not Exists Test");

            var exists = repository.Exists(numberField);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void DeleteTest()
        {
            CreateRespostory("Fields Delete Test");
            context.Fields.Add(numberField);
            context.SaveChanges();

            repository.Delete(1);
            repository.Save();

            var numberFieldInDb = context.Fields.FirstOrDefault();
            Assert.IsNull(numberFieldInDb);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void DeleteNotFound()
        {
            CreateRespostory("Field Delete Not Found");
            repository.Delete(1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            CreateRespostory("Field Update Test");
            context.Fields.Add(numberField);
            context.Fields.Add(textField);
            context.Fields.Add(dateTimeField);
            context.SaveChanges();

            var newDate = new DateTime();
            numberField.RangeEnd = 25;
            textField.RangeValues = new List<string>();
            dateTimeField.End = newDate;

            repository.Update(numberField);
            repository.Update(textField);
            repository.Update(dateTimeField);

            var fields = context.Fields.ToList();
            var numberFieldInDb = fields
                .OfType<NumberField>()
                .Cast<NumberField>()
                .FirstOrDefault();
            var textFieldInDb = fields
                .OfType<TextField>()
                .Cast<TextField>()
                .FirstOrDefault();
            var datesFieldInDb = fields
                .OfType<DateTimeField>()
                .Cast<DateTimeField>()
                .FirstOrDefault();

            Assert.AreEqual(25, numberFieldInDb.RangeEnd);
            Assert.AreEqual(0, textFieldInDb.RangeValues.Count);
            Assert.AreEqual(newDate, datesFieldInDb.End);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void UpdateNotFound()
        {
            CreateRespostory("Fields Update Not Found");
            repository.Update(numberField);
        }
    }
}
using AutoMapper;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Helpers.Automapper;
using ImmRequest.BusinessLogic.Helpers.Inputs;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic.ImporterLogic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.ImporterTests
{
    [TestClass]
    public class ImporterTests
    {
        private const string Path = @"~\..\..\..\..\ImporterTests\Files\";
        [TestMethod]
        public void GetImporterOptionsTest()
        {
            var inputs = new AreaImporterInput
            (
                areaRepository: new Mock<IRepository<Area>>().Object,
                topicRepository: new Mock<IRepository<Topic>>().Object,
                areaValidator: new Mock<IValidator<Area>>().Object,
                topicValidator: new Mock<IValidator<Topic>>().Object,
                typeValidator: new Mock<IValidator<TopicType>>().Object
            );

            var logic = new ImporterLogic(inputs);
            var allImporterNames = logic.GetImporterOptions();
            var jsonTypeImporter = "Json Area Importer";
            var xmlTypeImporter = "Xml Area Importer";

            Assert.IsTrue(allImporterNames.Contains(jsonTypeImporter));
            Assert.IsTrue(allImporterNames.Contains(xmlTypeImporter));

        }

        [TestMethod]
        public void GetsImporterOfAnotherAssembly()
        {
            var inputs = new AreaImporterInput
            (
                areaRepository: new Mock<IRepository<Area>>().Object,
                topicRepository: new Mock<IRepository<Topic>>().Object,
                areaValidator: new Mock<IValidator<Area>>().Object,
                topicValidator: new Mock<IValidator<Topic>>().Object,
                typeValidator: new Mock<IValidator<TopicType>>().Object
            );

            var logic = new ImporterLogic(inputs);
            var allImporterNames = logic.GetImporterOptions();
            var otherImporter = "Test Importer";

            Assert.IsTrue(allImporterNames.Contains(otherImporter));
        }

        [TestMethod]
        public void GetMapper()
        {
            var mapper = MapperProfile.GetMapper();
            Assert.IsInstanceOfType(mapper, typeof(IMapper));
        }

        [TestMethod]
        [TestCategory("New Areas with just topics")]
        public void ImportUsingJson()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var areaRepository = new AreaRepository(context);
            var topicRepository = new TopicRepository(context);
            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Json Area Importer", $"{Path}ImportArea.json");

            var areaInDb = context.Areas.First();
            Assert.AreEqual(2, areaInDb.Topics.Count);
        }

        [TestMethod]
        [TestCategory("New Areas with just topics")]
        public void ImportWtihTypesUsingJson()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var areaRepository = new AreaRepository(context);
            var topicRepository = new TopicRepository(context);
            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Json Area Importer", $"{Path}ImportAreaWithTypes.json");

            var areaInDb = context.Areas.First();
            Assert.AreEqual(2, areaInDb.Topics.Count);

            var secondTopic = areaInDb.Topics.Skip(1).First();
            Assert.AreEqual(1, secondTopic.Types.Count);
        }

        [TestMethod]
        [TestCategory("Add a new Type for existing topic and a new Topic for area")]
        public void ImportTypeToExistingTopic()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var area = new Area
            {
                Name = "Area 1",
                Topics = new List<Topic> {
                    new Topic
                    {
                        Name = "Topic B",
                        Types = new List<TopicType>(),
                    }
                }
            };
            var areaRepository = new AreaRepository(context);
            areaRepository.Insert(area);
            areaRepository.Save();

            var topicRepository = new TopicRepository(context);
            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Json Area Importer", $"{Path}AddTypeToExistingTopic.json");

            var areaInDb = context.Areas.First();
            Assert.AreEqual(2, areaInDb.Topics.Count);

            var alreadyExistingTopic = areaInDb.Topics.First();
            Assert.AreEqual(1, alreadyExistingTopic.Types.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(DeveloperException))]
        public void ImporterDoesntHaveConstructor()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var areaRepository = new AreaRepository(context);
            var topicRepository = new TopicRepository(context);
            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Test Importer", $"{Path}AddTypeToExistingTopic.json");
        }

        [TestMethod]
        [ExpectedException(typeof(DeveloperException))]
        public void ImporterWrongParameters()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var areaRepository = new AreaRepository(context);
            var topicRepository = new TopicRepository(context);
            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Wrong Parameters Importer", $"{Path}AddTypeToExistingTopic.json");
        }

        [TestMethod]
        [ExpectedException(typeof(DeveloperException))]
        public void ImporterNotFound()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var areaRepository = new AreaRepository(context);
            var topicRepository = new TopicRepository(context);
            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Non Existing Importer", $"{Path}AddTypeToExistingTopic.json");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void FileNotFound()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var areaRepository = new AreaRepository(context);
            var topicRepository = new TopicRepository(context);
            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Xml Area Importer", $"{Path}someFile.xml");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FileHasValidationError()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var areaRepository = new AreaRepository(context);
            var topicRepository = new TopicRepository(context);
            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Json Area Importer", $"{Path}ValidationError.json");

            var areaInDb = context.Areas.FirstOrDefault();
            Assert.IsNull(areaInDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NewAreaGetsExistingTopics()
        {
            var context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            var areaRepository = new AreaRepository(context);
            var topicRepository = new TopicRepository(context);
            var area = new Area
            {
                Name = "Another Area",
                Topics = new List<Topic> {
                    new Topic
                    {
                        Name = "Topic A",
                        Types = new List<TopicType>(),
                    }
                }
            };
            areaRepository.Insert(area);
            areaRepository.Save();

            var areaValidator = new AreaValidator(areaRepository);
            var topicValidator = new TopicValidator(topicRepository);
            var typeValidator = new TopicTypeValidator();

            var inputs = new AreaImporterInput
            (
                areaRepository,
                topicRepository,
                areaValidator,
                topicValidator,
                typeValidator
            );

            var importer = new ImporterLogic(inputs);
            importer.Import("Json Area Importer", $"{Path}FirstTopicExists.json");

            var areaInDb = context.Areas.FirstOrDefault();
            Assert.IsNotNull(areaInDb);

        }
    }
}

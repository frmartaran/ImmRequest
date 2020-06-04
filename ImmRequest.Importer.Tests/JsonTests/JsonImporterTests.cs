using ImmRequest.Importer.Importers;
using ImmRequest.Importer.Interfaces.Domain;
using ImmRequest.Importer.Interfaces.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ImmRequest.Importer.Tests
{
    [TestClass]
    public class JsonImporterTests
    {

        [TestMethod]
        public void ReadFile()
        {
            var filePath = $"{TestConstants.JsonPath}ImportArea.json";
            var importer = new JsonAreaImporter(filePath);
            var file = importer.ReadFile(filePath);
            Assert.IsNotNull(file);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileEmptyPath()
        {
            var path = "";
            new JsonAreaImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileNotFound()
        {
            var path = $"{TestConstants.JsonPath}notfound.json";
            new JsonAreaImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileDirectoryNotFound()
        {
            var path = $"{TestConstants.JsonPath}\\NonExistantDirectory\\ImportArea.json";
            new JsonAreaImporter(path);
        }

        [TestMethod]
        public void SuccessfulImport()
        {
            var path = $"{TestConstants.JsonPath}\\ImportArea.json";
            var importer = new JsonAreaImporter(path);
            var areas = importer.Import();

            Assert.AreEqual(1, areas.Count);
            var firstArea = areas.FirstOrDefault();

            Assert.AreEqual(2, firstArea.Topics.Count);
            var secondTopicFirstArea = firstArea.Topics.Skip(1).FirstOrDefault();

            Assert.AreEqual(1, secondTopicFirstArea.Types.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]
        public void UnsucessfulImport()
        {
            var path = $"{TestConstants.JsonPath}\\FormatError.json";
            var importer = new JsonAreaImporter(path);
            var types = importer.Import();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]
        public void EmptyJson()
        {
            var path = $"{TestConstants.JsonPath}\\EmptyJson.json";
            var importer = new JsonAreaImporter(path);
            var types = importer.Import();
        }

        [TestMethod]
        public void SuccessfulImportOfMultipleTypes()
        {
            var path = $"{TestConstants.JsonPath}\\MultipleAreas.json";
            var importer = new JsonAreaImporter(path);
            var areas = importer.Import();

            Assert.AreEqual(2, areas.Count);

            Assert.AreEqual(2, areas.Count);
            var firstArea = areas.FirstOrDefault();

            Assert.AreEqual(2, firstArea.Topics.Count);
            var secondTopicFirstArea = firstArea.Topics.Skip(1).FirstOrDefault();

            Assert.AreEqual(1, secondTopicFirstArea.Types.Count);

            var secondArea = areas.Skip(1).FirstOrDefault();
            Assert.AreEqual(2, secondArea.Topics.Count);

        }


    }
}

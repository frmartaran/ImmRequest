using ImmRequest.Importer.Importers;
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
            var filePath = $"{TestConstants.JsonPath}ImportType.json";
            var importer = new JsonTypeImporter(filePath);
            var file = importer.ReadFile(filePath);
            Assert.IsNotNull(file);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileEmptyPath()
        {
            var path = "";
            new JsonTypeImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileNotFound()
        {
            var path = $"{TestConstants.JsonPath}notfound.json";
            new JsonTypeImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileDirectoryNotFound()
        {
            var path = $"{TestConstants.JsonPath}\\NonExistantDirectory\\ImportType.json";
            new JsonTypeImporter(path);
        }

        [TestMethod]
        public void SuccessfulImport()
        {
            var path = $"{TestConstants.JsonPath}\\ImportType.json";
            var importer = new JsonTypeImporter(path);
            var types = importer.Import();

            Assert.AreEqual(1, types.Count);
            Assert.AreEqual(4, types.First().Fields.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]
        public void UnsucessfulImport()
        {
            var path = $"{TestConstants.JsonPath}\\FormatError.json";
            var importer = new JsonTypeImporter(path);
            var types = importer.Import();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]
        public void EmptyJson()
        {
            var path = $"{TestConstants.JsonPath}\\EmptyJson.json";
            var importer = new JsonTypeImporter(path);
            var types = importer.Import();
        }


    }
}

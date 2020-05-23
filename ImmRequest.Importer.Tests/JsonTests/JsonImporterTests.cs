using ImmRequest.Importer.Importers;
using ImmRequest.Importer.Interfaces.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.Importer.Tests
{
    [TestClass]
    public class JsonImporterTests
    {
        protected const string ROOT_PATH = "~\\..\\..\\..\\..\\JsonTests\\Files\\";

        [TestMethod]
        public void ReadFile()
        {
            var filePath = $"{ROOT_PATH}ImportType.json";
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
            var path = $"{ROOT_PATH}notfound.json";
            new JsonTypeImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileDirectoryNotFound()
        {
            var path = $"{ROOT_PATH}\\NonExistantDirectory\\ImportType.json";
            new JsonTypeImporter(path);
        }


    }
}

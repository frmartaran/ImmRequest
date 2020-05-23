using ImmRequest.Importer.Importers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.Importer.Tests
{
    [TestClass]
    public class JsonTypeImporterTests
    {
        private const string ROOT_PATH = "~\\..\\..\\..\\..\\JsonTests\\Files\\";

        [TestMethod]
        public void ReadFile()
        {
            var filePath = $"{ROOT_PATH}ImportType.json";
            var importer = new JsonTypeImporter(filePath);
            var file = importer.ReadFile(filePath);
            Assert.IsNotNull(file);
        }

    }
}

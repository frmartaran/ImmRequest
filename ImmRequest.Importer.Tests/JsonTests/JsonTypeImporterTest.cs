using ImmRequest.Importer.Importers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.Importer.Tests
{
    [TestClass]
    public class JsonTypeImporterTests
    {
        

        [TestMethod]
        public void ReadFile()
        {
            var filePath = "..\\Files\\ImportType.json";
            var importer = new JsonTypeImporter(filePath);
            var file = importer.ReadFile(filePath);
            Assert.IsNotNull(file);
        }

    }
}

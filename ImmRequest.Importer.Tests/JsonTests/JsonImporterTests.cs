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

        [TestMethod]
        public void SuccessfulImportOfMultipleTypes()
        {
            var path = $"{TestConstants.JsonPath}\\MultipleTypes.json";
            var importer = new JsonTypeImporter(path);
            var types = importer.Import();

            Assert.AreEqual(3, types.Count);

            var type1 = types.First();

            Assert.AreEqual(4, type1.Fields.Count);

            var numberField = type1.Fields.FirstOrDefault(f => f.DataType == DataType.Number);
            var textField = type1.Fields.FirstOrDefault(f => f.DataType == DataType.Text);
            var dateTimeField = type1.Fields.FirstOrDefault(f => f.DataType == DataType.DateTime);
            var boolField = type1.Fields.FirstOrDefault(f => f.DataType == DataType.Bool);

            Assert.IsNotNull(numberField);
            Assert.IsNotNull(textField);
            Assert.IsNotNull(dateTimeField);
            Assert.IsNotNull(boolField);

            var type2 = types.Skip(1).First();
            Assert.AreEqual(2, type2.Fields.Count);
            var numberField2 = type2.Fields.FirstOrDefault(f => f.DataType == DataType.Number);
            var textField2 = type2.Fields.FirstOrDefault(f => f.DataType == DataType.Text);
            var dateTimeField2 = type2.Fields.FirstOrDefault(f => f.DataType == DataType.DateTime);
            var boolField2 = type2.Fields.FirstOrDefault(f => f.DataType == DataType.Bool);

            Assert.IsNull(numberField2);
            Assert.IsNotNull(textField2);
            Assert.IsNotNull(dateTimeField2);
            Assert.IsNull(boolField2);

            var type3 = types.Skip(2).First();
            Assert.AreEqual(0, type3.Fields.Count);
        }


    }
}

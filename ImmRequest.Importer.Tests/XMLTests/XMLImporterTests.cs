using ImmRequest.Importer.Importers.XML;
using ImmRequest.Importer.Interfaces.Domain;
using ImmRequest.Importer.Interfaces.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.Importer.Tests.XMLTests
{
    [TestClass]
    public class XMLImporterTests
    {

        [TestMethod]
        public void ReadFile()
        {
            var filePath = $"{TestConstants.XMLPath}ImportType.xml";
            var importer = new XMLTypeImporter(filePath);
            var file = importer.ReadFile(filePath);
            Assert.IsNotNull(file);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileEmptyPath()
        {
            var path = "";
            new XMLTypeImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileNotFound()
        {
            var path = $"{TestConstants.XMLPath}notfound.xml";
            new XMLTypeImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileDirectoryNotFound()
        {
            var path = $"{TestConstants.XMLPath}\\NonExistantDirectory\\ImportType.xml";
            new XMLTypeImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileFormatError()
        {
            var path = $"{TestConstants.XMLPath}\\FormatError.xml";
            new XMLTypeImporter(path);
        }

        [TestMethod]
        public void SuccessfulImport()
        {
            var path = $"{TestConstants.XMLPath}\\TypeWithNoFields.xml";
            var importer = new XMLTypeImporter(path);
            var types = importer.Import();

            Assert.IsNotNull(types.First());
            Assert.AreEqual("Wrong Address", types.First().Name);
        }

        [TestMethod]
        public void SuccessfulImportWithFields()
        {
            var path = $"{TestConstants.XMLPath}\\ImportType.xml";
            var importer = new XMLTypeImporter(path);
            var types = importer.Import();
            var type = types.First();
            Assert.IsNotNull(type);
            Assert.AreEqual(4, type.Fields.Count);

            var numberField = type.Fields.FirstOrDefault(f => f.DataType == DataType.Number);
            var textField = type.Fields.FirstOrDefault(f => f.DataType == DataType.Text);
            var dateTimeField = type.Fields.FirstOrDefault(f => f.DataType == DataType.DateTime);
            var boolField = type.Fields.FirstOrDefault(f => f.DataType == DataType.Bool);

            Assert.IsNotNull(numberField);
            Assert.IsNotNull(textField);
            Assert.IsNotNull(dateTimeField);
            Assert.IsNotNull(boolField);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]
        public void NoTypesToImport()
        {
            var path = $"{TestConstants.XMLPath}\\EmptyFile.xml";
            var importer = new XMLTypeImporter(path);
            importer.Import();

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]

        public void NoDataTypeTag()
        {
            var path = $"{TestConstants.XMLPath}\\NoDataTypeTag.xml";
            var importer = new XMLTypeImporter(path);
            importer.Import();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]

        public void UnsupportedDataType()
        {
            var path = $"{TestConstants.XMLPath}\\UnsupportedDataType.xml";
            var importer = new XMLTypeImporter(path);
            importer.Import();
        }


    }
}

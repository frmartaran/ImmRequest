using ImmRequest.Importer.Importers.XML;
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


    }
}

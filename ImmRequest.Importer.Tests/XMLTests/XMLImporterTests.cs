using ImmRequest.Importer.Importers.XML;
using ImmRequest.Importer.Interfaces.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Tests.XMLTests
{
    [TestClass]
    public class XMLImporterTests
    {
        protected const string ROOT_PATH = "~\\..\\..\\..\\..\\XMLTests\\Files\\";

        [TestMethod]
        public void ReadFile()
        {
            var filePath = $"{ROOT_PATH}ImportType.xml";
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
            var path = $"{ROOT_PATH}notfound.xml";
            new XMLTypeImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileDirectoryNotFound()
        {
            var path = $"{ROOT_PATH}\\NonExistantDirectory\\ImportType.xml";
            new XMLTypeImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileFormatError()
        {
            var path = $"{ROOT_PATH}\\FormatError.xml";
            new XMLTypeImporter(path);
        }

       
    }
}

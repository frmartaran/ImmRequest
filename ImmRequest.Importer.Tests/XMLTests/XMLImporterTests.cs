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
            var filePath = $"{TestConstants.XMLPath}ImportArea.xml";
            var importer = new XmlAreaImporter(filePath);
            var file = importer.ReadFile(filePath);
            Assert.IsNotNull(file);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileEmptyPath()
        {
            var path = "";
            new XmlAreaImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileNotFound()
        {
            var path = $"{TestConstants.XMLPath}notfound.xml";
            new XmlAreaImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileDirectoryNotFound()
        {
            var path = $"{TestConstants.XMLPath}\\NonExistantDirectory\\ImportArea.xml";
            new XmlAreaImporter(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadFailureException))]
        public void ReadFileFormatError()
        {
            var path = $"{TestConstants.XMLPath}\\FormatError.xml";
            new XmlAreaImporter(path);
        }


        [TestMethod]
        public void SuccessfulImport()
        {
            var path = $"{TestConstants.XMLPath}\\ImportArea.xml";
            var importer = new XmlAreaImporter(path);
            var areas = importer.Import();

            Assert.AreEqual(1, areas.Count);
            var firstArea = areas.FirstOrDefault();

            Assert.AreEqual(2, firstArea.Topics.Count);
            var secondTopicFirstArea = firstArea.Topics.Skip(1).FirstOrDefault();

            Assert.AreEqual(2, secondTopicFirstArea.Types.Count);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]
        public void NoAreasToImport()
        {
            var path = $"{TestConstants.XMLPath}\\EmptyFile.xml";
            var importer = new XmlAreaImporter(path);
            importer.Import();

        }

        [TestMethod]
        public void SuccessfulImportOfMultipleTypes()
        {
            var path = $"{TestConstants.XMLPath}\\MultipleAreas.xml";
            var importer = new XmlAreaImporter(path);
            var areas = importer.Import();
            Assert.AreEqual(2, areas.Count);

            Assert.AreEqual(2, areas.Count);
            var firstArea = areas.FirstOrDefault();

            Assert.AreEqual(2, firstArea.Topics.Count);
            var secondTopicFirstArea = firstArea.Topics.Skip(1).FirstOrDefault();

            Assert.AreEqual(2, secondTopicFirstArea.Types.Count);

            var secondArea = areas.Skip(1).FirstOrDefault();
            Assert.AreEqual(2, secondArea.Topics.Count);

        }
    }
}

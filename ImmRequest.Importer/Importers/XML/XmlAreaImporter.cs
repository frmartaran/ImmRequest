using ImmRequest.Importer.Domain;
using ImmRequest.Importer.Extentions;
using ImmRequest.Importer.Interfaces.Domain;
using ImmRequest.Importer.Interfaces.Exceptions;
using ImmRequest.Importer.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ImmRequest.Importer.Importers.XML
{
    public class XmlAreaImporter : XmlImporter<IArea>
    {
        private const string TYPE_TAG = "Type";
        private const string AREA_TAG = "Area";
        private const string TOPIC_TAG = "Topic";

        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_ID = "id";

        public XmlAreaImporter(string filePath)
        {
            File = ReadFile(filePath);
        }
        public override ICollection<IArea> Import()
        {
            var allAreas = new List<Area>();
            var areaNodes = File.GetElementsByTagName(AREA_TAG).AsList();
            CancelImportOnEmptyFile(areaNodes);

            foreach (var xmlArea in areaNodes)
            {
                var topicNodes = xmlArea.ChildNodes.AsList()
                    .Where(xmle => xmle.Name == TOPIC_TAG)
                    .ToList();

                var topics = ImportTopics(topicNodes);
                var name = GetAttributeAs<string>(xmlArea, ATTRIBUTE_NAME);
                var id = GetAttributeAs<long>(xmlArea, ATTRIBUTE_ID);
                var area = new Area(topics)
                {
                    Name = name,
                    Id = id
                };
                allAreas.Add(area);
            }

            return allAreas.ToInterfaceList<IArea, Area>();
        }

        private List<Topic> ImportTopics(List<XmlElement> topicNodes)
        {
            var topics = new List<Topic>();
            foreach (var xmlTopic in topicNodes)
            {
                var typeNodes = xmlTopic.ChildNodes.AsList()
                    .Where(xmle => xmle.Name == TYPE_TAG)
                    .ToList();
                var types = GetTypes(typeNodes);
                var Tname = GetAttributeAs<string>(xmlTopic, ATTRIBUTE_NAME);
                var Tid = GetAttributeAs<long>(xmlTopic, ATTRIBUTE_ID);
                var topic = new Topic(types)
                {
                    Name = Tname,
                    Id = Tid
                };
                topics.Add(topic);

            }
            return topics;
        }

        private List<TopicType> GetTypes(List<XmlElement> typeNodes)
        {
            var types = new List<TopicType>();
            foreach (var xmlType in typeNodes)
            {
                var name = GetAttributeAs<string>(xmlType, ATTRIBUTE_NAME);
                var type = new TopicType { Name = name };
                types.Add(type);
            }
            return types;
        }

        private static void CancelImportOnEmptyFile(List<XmlElement> typeTags)
        {
            if (typeTags.Count == 0)
            {
                var message = string.Format(ImporterResource.NoEntityToImport,
                    ImporterResource.EntityToImport_Type);
                throw new InvalidFormatException(message);
            }
        }

    }
}

using ImmRequest.Importer.Domain;
using ImmRequest.Importer.Extentions;
using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ImmRequest.Importer.Importers.XML
{
    public class XMLTypeImporter : XMLImporter<IType>
    {
        private const string TYPE_TAG = "Type";
        private const string FIELD_TAG = "Field";
        private const string DATA_TYPE_TAG = "DataType";
        private const string RANGE_TAG = "Range";
        private const string RANGE_VALUE_TAG = "Value";


        public XMLTypeImporter(string filePath)
        {
            File = ReadFile(filePath);
        }
        public override ICollection<IType> Import()
        {
            var allTypes = new List<TopicType>();
            var typeTags = File.GetElementsByTagName(TYPE_TAG).AsList();

            foreach (var xmlType in typeTags)
            {
                var type = ImportType(xmlType);
                allTypes.Add(type);
            }

            return allTypes.ToInterfaceList<IType, TopicType>();
        }

        private TopicType ImportType(XmlElement xmlTypeNode)
        {
            var xmlFields = xmlTypeNode.ChildNodes.AsList()
                                .Where(xmle => xmle.Name == FIELD_TAG)
                                .ToList();

            var fields = ImportFieldsForType(xmlFields);
            var name = GetNodeName(xmlTypeNode);
            var type = new TopicType
            {
                Name = name,
                Fields = fields.ToInterfaceList<IField, Field>()
            };
            return type;
        }


        private List<Field> ImportFieldsForType(List<XmlElement> xmlFields)
        {
            var fields = new List<Field>();
            foreach (var xmlField in xmlFields)
            {
                var fieldName = GetNodeName(xmlField);
                var childNodes = xmlField.ChildNodes.AsList();
                DataType dataTypeEnum = GetDataType(childNodes);
                var rangeValues = GetRangeValues(childNodes);
                var field = new Field
                {
                    Name = fieldName,
                    DataType = dataTypeEnum,
                    RangeValues = rangeValues
                };

                fields.Add(field);
            }
            return fields;
        }

        private List<string> GetRangeValues(List<XmlElement> childNodes)
        {
            var rangeValues = new List<string>();
            var rangeNode = childNodes.FirstOrDefault(xmle => xmle.Name == RANGE_TAG);
            if (rangeNode != null)
            {
                rangeValues = rangeNode.ChildNodes.AsList()
                    .Where(xmle => xmle.Name == RANGE_VALUE_TAG)
                    .Select(xmle => xmle.InnerText)
                    .ToList();
            }

            return rangeValues;
        }

        private static DataType GetDataType(List<XmlElement> childNodes)
        {
            var dataType = childNodes
                .FirstOrDefault(xmle => xmle.Name == DATA_TYPE_TAG)
                .InnerText;

            DataType dataTypeEnum;
            Enum.TryParse(dataType, out dataTypeEnum);
            return dataTypeEnum;
        }
    }
}

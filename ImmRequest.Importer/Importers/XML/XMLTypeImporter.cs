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
        private string path;
        private const string TYPE_TAG = "Type";
        private const string FIELD_TAG = "Field";
        private const string DAT_TYPE_TAG = "DataType";
        private const string RANGE_TAG = "Range";
        private const string RANGE_VALUE_TAG = "Value";

        private const string ATTRIBUTE_NAME = "name";

        public XMLTypeImporter(string filePath)
        {
            path = filePath;
            File = ReadFile(filePath);
        }
        public override ICollection<IType> Import()
        {
            var allTypes = new List<TopicType>();
            var typeTags = File.GetElementsByTagName(TYPE_TAG).AsList();
            foreach (var xmlType in typeTags)
            {
                var name = xmlType.GetAttribute(ATTRIBUTE_NAME);
                var type = new TopicType
                {
                    Name = name,
                    Fields = new List<Field>().ToInterfaceList<IField, Field>()
                };
                allTypes.Add(type);
            }

            return allTypes.ToInterfaceList<IType, TopicType>();
        }
    }
}

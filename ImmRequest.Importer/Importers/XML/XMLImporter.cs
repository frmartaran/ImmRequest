using ImmRequest.Importer.Interfaces.Exceptions;
using ImmRequest.Importer.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ImmRequest.Importer.Importers.XML
{
    public abstract class XmlImporter<Entity> : Importer<XmlDocument, Entity>
    {
        protected XmlDocument File { get; set; }
        public override XmlDocument LoadFile(string file)
        {
            try
            {
                var document = new XmlDocument();
                document.Load(file);
                return document;

            }
            catch (XmlException)
            {
                throw new FileLoadFailureException(ImporterResource.Format_Invalid);
            }
        }

        protected T GetAttributeAs<T>(XmlElement node, string attributeName)
        {
            var value = node.GetAttribute(attributeName);
            if (string.IsNullOrEmpty(value))
                return default;

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}

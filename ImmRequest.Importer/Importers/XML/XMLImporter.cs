using ImmRequest.Importer.Interfaces.Exceptions;
using ImmRequest.Importer.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ImmRequest.Importer.Importers.XML
{
    public abstract class XMLImporter<Entity> : Importer<XmlDocument, Entity>
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
    }
}

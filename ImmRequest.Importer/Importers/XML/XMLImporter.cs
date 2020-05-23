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
            throw new NotImplementedException();
        }
    }
}

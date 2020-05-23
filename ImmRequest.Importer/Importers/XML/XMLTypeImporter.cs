using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Importers.XML
{
    public class XMLTypeImporter : XMLImporter<IType>
    {
        public XMLTypeImporter(string filePath)
        {
            File = ReadFile(filePath);
        }
        public override ICollection<IType> Import()
        {
            throw new NotImplementedException();
        }
    }
}

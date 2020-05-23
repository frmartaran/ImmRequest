using ImmRequest.Importer.Importers.Json;
using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Importers
{
    public class JsonTypeImporter : JsonImporter<IType>
    {
        public JsonTypeImporter(string file)
        {
            File = ReadFile(file);
        }
        public override ICollection<IType> Import()
        {
            throw new NotImplementedException();
        }
        
    }
}

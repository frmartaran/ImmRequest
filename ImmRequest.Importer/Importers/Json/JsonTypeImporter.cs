using ImmRequest.Importer.Domain;
using ImmRequest.Importer.Importers.Json;
using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var types = JsonConvert.DeserializeObject<List<TopicType>>(File);
            return types.Cast<IType>().ToList();
        }
        
    }
}

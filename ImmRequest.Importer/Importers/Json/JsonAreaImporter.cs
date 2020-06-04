using ImmRequest.Importer.Domain;
using ImmRequest.Importer.Importers.Json;
using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Domain;
using ImmRequest.Importer.Interfaces.Exceptions;
using ImmRequest.Importer.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.Importer.Importers
{
    public class JsonAreaImporter : JsonImporter<IArea>
    {
        public JsonAreaImporter(string file)
        {
            File = ReadFile(file);
        }
        public override ICollection<IArea> Import()
        {
            try
            {
                var types = JsonConvert.DeserializeObject<List<Area>>(File);
                CancelIfThereAreNoTypes(types);
                return types.Cast<IArea>().ToList();

            }
            catch (JsonSerializationException)
            {
                throw new InvalidFormatException(ImporterResource.Format_Invalid);
            }
        }

        private static void CancelIfThereAreNoTypes(List<Area> types)
        {
            if (types.Count == 0)
            {
                var message = string.Format(ImporterResource.NoEntityToImport,
                    ImporterResource.EntityToImport_Type);
                throw new InvalidFormatException(message);
            }
        }
    }
}

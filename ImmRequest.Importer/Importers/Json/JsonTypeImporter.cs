using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Importers
{
    public class JsonTypeImporter : IEntityImporter<string, IType>
    {
        protected string File { get; set; }

        public JsonTypeImporter(string file)
        {
            File = ReadFile(file);
        }
        public ICollection<IType> Import()
        {
            throw new NotImplementedException();
        }

        public string ReadFile(string file)
        {
            throw new NotImplementedException();
        }
    }
}

using ImmRequest.Importer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImmRequest.Importer.Importers.Json
{
    public abstract class JsonImporter<Entity> : IEntityImporter<string, Entity>
    {
        public abstract ICollection<Entity> Import();

        public virtual string ReadFile(string file)
        {
            using (var reader = new StreamReader(file))
            {
                var loadedFile = reader.ReadToEnd();
                return loadedFile;
            }
        }
    }
}

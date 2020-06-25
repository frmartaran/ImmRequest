using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Exceptions;
using ImmRequest.Importer.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImmRequest.Importer.Importers.Json
{
    public abstract class JsonImporter<Entity> : Importer<string, Entity>
    {
        protected string File { get; set; }
        public override string LoadFile(string file)
        {
            using (var reader = new StreamReader(file))
            {
                var loadedFile = reader.ReadToEnd();
                return loadedFile;
            }
        }
    }
}

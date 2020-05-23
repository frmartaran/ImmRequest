using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Exceptions;
using ImmRequest.Importer.Resources;
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
            try
            {
                using (var reader = new StreamReader(file))
                {
                    var loadedFile = reader.ReadToEnd();
                    return loadedFile;
                }
            }
            catch (ArgumentException)
            {
                throw new FileLoadFailureException(ImporterResource.FileLoad_EmptyPath);
            }
            catch (FileNotFoundException)
            {
                throw new FileLoadFailureException(ImporterResource.FileLoad_FileNotFound);
            }
            catch (DirectoryNotFoundException)
            {
                throw new FileLoadFailureException(ImporterResource.FileLoad_DirectoryNotFound);
            }
            
        }
    }
}

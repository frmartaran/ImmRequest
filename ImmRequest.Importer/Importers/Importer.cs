using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Exceptions;
using ImmRequest.Importer.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImmRequest.Importer.Importers
{
    public abstract class Importer<Source, Entity> : IEntityImporter<Source, Entity>
    {
        public abstract ICollection<Entity> Import();

        public abstract Source LoadFile(string file);

        public Source ReadFile(string path)
        {
            try
            {
                return LoadFile(path);
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

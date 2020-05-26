using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Exceptions;
using ImmRequest.Importer.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImmRequest.Importer.Importers
{
    public abstract class Importer<FileType, Entity> : IEntityImporter<FileType, Entity>
    {
        public abstract ICollection<Entity> Import();

        public abstract FileType LoadFile(string file);

        public FileType ReadFile(string path)
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

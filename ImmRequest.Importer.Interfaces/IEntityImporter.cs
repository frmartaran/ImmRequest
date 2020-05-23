using System;
using System.Collections;
using System.Collections.Generic;

namespace ImmRequest.Importer.Interfaces
{
    public interface IEntityImporter<FileType, Entity>
        where Entity : class
    {
        FileType ReadFile(string file);
        ICollection<Entity> Import();
    }
}

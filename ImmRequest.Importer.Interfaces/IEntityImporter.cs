using System;
using System.Collections;
using System.Collections.Generic;

namespace ImmRequest.Importer.Interfaces
{
    public interface IEntityImporter<Source, Entity>
    {
        Source ReadFile(string path);
        ICollection<Entity> Import();
    }
}

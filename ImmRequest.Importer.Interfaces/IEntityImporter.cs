using System;
using System.Collections;
using System.Collections.Generic;

namespace ImmRequest.Importer.Interfaces
{
    public interface IEntityImporter<Entity>
    {
        ICollection<Entity> Import();
    }
}

using ImmRequest.WebApi.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public abstract class Model<E, M> : IModel<E, M>
        where E : class
        where M : Model<E, M>, new()
    {
        public static IEnumerable<M> ToModel(IEnumerable<E> entities)
        {
            return entities.Select(x => ToModel(x));
        }

        public static M ToModel(E entity)
        {
            if (entity == null) 
                return null;
            return new M().SetModel(entity);
        }

        public static IEnumerable<E> ToEntity(IEnumerable<M> models)
        {
            return models.Select(x => ToEntity(x));
        }
        private static E ToEntity(M model)
        {
            if (model == null) 
                return null;

            return model.ToDomain();
        }
        public abstract E ToDomain();

        public abstract M SetModel(E entity);
    }
}

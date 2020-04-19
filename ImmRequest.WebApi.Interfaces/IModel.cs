using System;

namespace ImmRequest.WebApi.Interfaces
{
    public interface IModel<E, M>
        where E: class
        where M: IModel<E, M>
    {
        E ToDomain();
        M SetModel(E entity);
    }
}

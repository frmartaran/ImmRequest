using System;
using System.Collections.Generic;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface ILogic<T>
        where T : class
    {
        void Create(T objectToCreate);
        void Update(T objectToUpdate);
        void Delete(long id);
        T Get(long Id);
        ICollection<T> GetAll();
        void Save();

    }
}

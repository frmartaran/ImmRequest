using System;
using System.Collections.Generic;

namespace ImmRequest.DataAccess.Interfaces
{
    public interface IRepository<T>
    where T: class
    {
        void Insert(T id);

        T Update(T objectToUpdate);

        T Get(long id);

        ICollection<T> GetAll();

        void Delete(long id);

        bool Exists(long id);
        
        void Save();



        
    }
}

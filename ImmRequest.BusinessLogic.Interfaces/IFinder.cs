using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface IFinder<T>
        where T : class
    {
        T Find(long id);

        ICollection<T> FindAll();
        ICollection<T> FindAll(Predicate<T> condition);

        T Find(Predicate<T> condition);
    }
}

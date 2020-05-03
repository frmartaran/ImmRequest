using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface IFinder<T>
        where T : class
    {
        ICollection<T> FindAll();
        ICollection<T> FindAll(Predicate<T> condition);

        T Find(Predicate<T> condition);
    }
}

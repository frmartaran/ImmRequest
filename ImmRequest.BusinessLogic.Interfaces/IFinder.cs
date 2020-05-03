using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface IFinder<T>
        where T: class
    {
        T Find(long id);

        List<T> FindAll();

        T Find(Predicate<T> condition);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface IFinder
    {
        ICollection<T> FindAll<T>() where T : class;
        ICollection<T> FindAll<T>(Predicate<T> condition) where T : class;
        T Find<T>(Predicate<T> condition) where T : class;
    }
}

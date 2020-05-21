using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.DataAccess.Interfaces
{
    public interface IDatabaseFinder
    {
        T Find<T>(Predicate<T> condition) where T : class;

        ICollection<T> FindAll<T>(Predicate<T> condition);
    }
}

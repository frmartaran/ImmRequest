using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.Importer.Extentions
{
    public static class CollectionExtentions
    {

        public static ICollection<T> ToInterfaceList<T, E>(this List<E> listToCast)
        {
            return listToCast.Cast<T>().ToList();
        }
    }
}

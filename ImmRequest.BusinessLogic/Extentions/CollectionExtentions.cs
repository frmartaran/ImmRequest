using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Extentions
{
    public static class CollectionExtentions
    {
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> newElements)
        {
            foreach (var element in newElements)
                collection.Add(element);
        }
    }
}

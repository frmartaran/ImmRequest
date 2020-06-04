using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.BusinessLogic.Extentions
{
    public static class StringExtentions
    {

        public static string SplitByCamelCase(this string stringToSplit)
        {
            var toUpper = stringToSplit.ToUpper();
            if (toUpper.Equals(stringToSplit))
                return stringToSplit;

            var stringResult = Enumerable.Concat(stringToSplit.Take(1),
                InsertSpacesBeforeCaps(stringToSplit.Skip(1)))
                .ToArray();
            return new string(stringResult);
        }

        private static IEnumerable<char> InsertSpacesBeforeCaps(IEnumerable<char> input)
        {
            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    yield return ' ';
                }

                yield return c;
            }
        }
    }
}

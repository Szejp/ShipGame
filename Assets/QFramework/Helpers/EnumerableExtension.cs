using System.Collections.Generic;
using System.Linq;

namespace QFramework.Helpers
{
    public static class EnumerableExtension
    {
        public static T IndexAtOrLast<T>(this IEnumerable<T> enumerable, int index)
        {
            if (enumerable.Count() <= index)
                return enumerable.Last();

            return enumerable.ElementAt(index);
        }
    }
}
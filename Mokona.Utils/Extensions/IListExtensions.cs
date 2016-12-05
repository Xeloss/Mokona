namespace Mokona.Utils.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class IListExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            items.Foreach(list.Add);
        }
    }
}

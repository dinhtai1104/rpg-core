using System.Collections.Generic;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> IEnumerable extensions. </summary>
    public static class EnumerableExtensions
    {
        /// <summary> Number of elements in IEnumerable. </summary>
        /// <param name="self">IEnumerable</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Number of elements.</returns>
        public static int Count<T>(this IEnumerable<T> self)
        {
            int count = 0;
            IEnumerator<T> enumerator = self.GetEnumerator();

            while (enumerator.MoveNext() == true)
                count++;

            return count;
        }
    }
}
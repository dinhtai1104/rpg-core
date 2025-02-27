using System;
using System.Collections.Generic;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> List extensions. </summary>
    public static class ListExtensions
    {
        /// <summary> Returns an unordered list. </summary>
        /// <param name="self"> The list. </param>
        /// <returns> Unordered list. </returns>
        public static T Random<T>(this IList<T> self) => self[Rand.Range(0, self.Count)];

        /// <summary> Swaps a pair of elements. </summary>
        /// <param name="self"> The list. </param>
        /// <param name="i"> First element. </param>
        /// <param name="j"> Second element. </param>
        public static void Swap<T>(this IList<T> self, int i, int j) => (self[i], self[j]) = (self[j], self[i]);

        /// <summary> Eliminate an items range. </summary>
        /// <param name="self"> The list. </param>
        /// <param name="entries">Elements to be removed. </param>
        public static void RemoveRange<T>(this IList<T> self, IEnumerable<T> entries)
        {
            foreach (T item in entries)
                self.Remove(item);
        }

        /// <summary> Reverses a list of items in place. </summary>
        /// <param name="self"> The list. </param>
        public static void Reverse<T>(this IList<T> self) => Reverse(self, 0, self.Count);

        /// <summary> Reverses a list of items. </summary>
        /// <param name="self"> The list. </param>
        /// <param name="from"> From. </param>
        /// <param name="to"> To. </param>
        public static void Reverse<T>(this IList<T> self, int from, int to)
        {
            if (self is T[] array)
            {
                array.Reverse(from, to);
                return;
            }

            while (--to > from)
                self.Swap(from++, to);
        }

        /// <summary> It unordered the list. </summary>
        /// <param name="self"> The list. </param>
        public static void Shuffle<T>(this IList<T> self)
        {
            int n = self.Count - 1;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, self.Count);
                (self[k], self[n]) = (self[n], self[k]);
            }
        }

        /// <summary> Returns the maximum value in the list. </summary>
        /// <typeparam name="T">The type of value to check.</typeparam>
        /// <param name="self">The values to check.</param>
        /// <returns>The maximum value in the list.</returns>
        public static T Max<T>(this List<T> self) where T : IComparable<T>
        {
            if (self == null || self.Count == 0)
                return default;

            T max = self[0];
            int total = self.Count;
            for (int i = 1; i < total; ++i)
            {
                T element = self[i];
                if (element.CompareTo(max) > 0)
                    max = element;
            }

            return max;
        }

        /// <summary> Returns the minimum value in the list. </summary>
        /// <typeparam name="T">The type of value to check.</typeparam>
        /// <param name="self">The values to check.</param>
        /// <returns>The minimum value in the list.</returns>
        public static T Min<T>(this List<T> self) where T : IComparable<T>
        {
            if (self == null || self.Count == 0)
                return default;

            T min = self[0];
            int total = self.Count;
            for (int i = 1; i < total; ++i)
            {
                T element = self[i];
                if (element.CompareTo(min) < 0)
                    min = element;
            }

            return min;
        }
    }
}
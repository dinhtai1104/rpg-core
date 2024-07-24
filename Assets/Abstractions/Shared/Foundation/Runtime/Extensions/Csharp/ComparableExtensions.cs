using System;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> IComparable extensions. </summary>
    public static class ComparableExtensions
    {
        /// <summary> Checks if the value is between a min and max value. </summary>
        /// <typeparam name="T">The type of value to check.</typeparam>
        /// <param name="self">Value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="includeMin">The minimum value is inclusive if true, exclusive if false.</param>
        /// <param name="includeMax">The maximum value is inclusive if true, exclusive if false.</param>
        /// <returns>True if the value is between the min and max value.</returns>
        public static bool IsBetween<T>(this T self, T min, T max, bool includeMin = true, bool includeMax = true) where T : IComparable<T>
        {
            int minCompare = self.CompareTo(min);
            int maxCompare = self.CompareTo(max);

            if (minCompare < 0 || maxCompare > 0)
                return false;

            if (includeMin == false && minCompare == 0)
                return false;

            if (includeMax == false && maxCompare == 0)
                return false;

            return true;
        }

        /// <summary> Checks if the value is in the range (min..max). </summary>
        /// <typeparam name="T">The type of value to check.</typeparam>
        /// <param name="self">Value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>True if the value is in the range (min..max).</returns>
        public static bool IsBetweenExclusive<T>(this T self, T min, T max) where T : IComparable<T> => self.IsBetween(min, max, false, false);
    }
}
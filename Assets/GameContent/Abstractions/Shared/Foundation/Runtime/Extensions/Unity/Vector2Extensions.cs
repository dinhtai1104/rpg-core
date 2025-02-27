using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Vector2 extensions. </summary>
    public static class Vector2Extensions
    {
        /// <summary> Returns the absolute value of the vector. </summary>
        /// <param name="self">Value.</param>
        /// <returns>A new vector of the absolute value.</returns>
        public static Vector2 Abs(this Vector2 self) => new Vector2(Mathf.Abs(self.x), Mathf.Abs(self.y));

        /// <summary> Rounds the vector up to the nearest whole number. </summary>
        /// <param name="value">The vector to Value.</param>
        /// <returns>A new rounded vector.</returns>
        public static Vector2 Ceil(this Vector2 value) => new Vector2(Mathf.Ceil(value.x), Mathf.Ceil(value.y));

        /// <summary> Clamps the vector to the range [min..max]. </summary>
        /// <param name="self">Value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>A new clamped vector.</returns>
        public static Vector2 Clamp(this Vector2 self, Vector2 min, Vector2 max) =>
            new Vector2(Mathf.Clamp(self.x, min.x, max.x), Mathf.Clamp(self.y, min.y, max.y));

        /// <summary> Clamps the vector to the range [0..1]. </summary>
        /// <param name="self">Value.</param>
        /// <returns>A new clamped vector.</returns>
        public static Vector2 Clamp01(this Vector2 self) => new Vector2(Mathf.Clamp01(self.x), Mathf.Clamp01(self.y));

        /// <summary> Rounds the vector down to the nearest whole number. </summary>
        /// <param name="self">Value.</param>
        /// <returns>A new rounded vector.</returns>
        public static Vector2 Floor(this Vector2 self) => new Vector2(Mathf.Floor(self.x), Mathf.Floor(self.y));

        /// <summary> Checks for equality with another vector given a margin of error specified by an epsilon. </summary>
        /// <param name="a">The left-hand side of the equality check.</param>
        /// <param name="b">The right-hand side of the equality check.</param>
        /// <returns>True if the values are equal.</returns>
        public static bool NearlyEquals(this Vector2 a, Vector2 b, float epsilon = MathConstants.Epsilon) =>
            a.x.NearlyEquals(b.x, epsilon) && a.y.NearlyEquals(b.y, epsilon);

        /// <summary> Rounds the vector to the nearest whole number. </summary>
        /// <param name="self">Value.</param>
        /// <returns>A new rounded vector.</returns>
        public static Vector2 Rounded(this Vector2 self) => new Vector2(Mathf.Round(self.x), Mathf.Round(self.y));

        /// <summary> Apply the modulo operator. </summary>
        /// <param name="self">Value.</param>
        /// <returns>A new vector.</returns>
        public static Vector2 Remainder(this Vector2 self, Vector2 modulus) => new Vector2(self.x % modulus.x, self.y % modulus.y);

        /// <summary> Vector2 to string. </summary>
        /// <param name="self">Value</param>
        /// <returns>string</returns>
        public static string ToString(this Vector2 self) => $"{self.x},{self.y}";
    }
}
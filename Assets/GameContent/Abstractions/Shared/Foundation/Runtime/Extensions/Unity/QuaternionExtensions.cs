using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Quaternion extensions. </summary>
    public static class QuaternionExtensions
    {
        /// <summary> Quaternion magnitude. </summary>
        /// <param name="self">Value</param>
        /// <returns>Magnitude</returns>
        public static float Magnitude(this Quaternion self) => Mathf.Sqrt(self.x * self.x + self.y * self.y + self.z * self.z + self.w * self.w);

        /// <summary> Checks for equality with another quaternion given a margin of error specified by an epsilon. </summary>
        /// <param name="a">The left-hand side of the equality check.</param>
        /// <param name="b">The right-hand side of the equality check.</param>
        /// <returns>True if the values are equal.</returns>
        public static bool NearlyEquals(this Quaternion a, Quaternion b, float epsilon = MathConstants.Epsilon) =>
            1.0f - Mathf.Abs(Quaternion.Dot(a, b)) < epsilon;

        /// <summary> Quaternion to string. </summary>
        /// <param name="self">Value</param>
        /// <returns>string</returns>
        public static string ToString(this Quaternion self) => $"{self.x},{self.y},{self.z},{self.w}";
    }
}
using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Math utilities. </summary>
    public static partial class MathUtils
    {
        /// <summary> Sine of angle. </summary>
        /// <param name="rad">Angle in radians. </param>
        /// <returns>Value</returns>
        public static float Sin(float rad) => Mathf.Sin(rad);

        /// <summary> Cosine of angle. </summary>
        /// <param name="rad">Angle in radians. </param>
        /// <returns>Value</returns>
        public static float Cos(float rad) => Mathf.Cos(rad);

        /// <summary> Tangent of angle. </summary>
        /// <param name="rad">Angle in radians. </param>
        /// <returns>Value</returns>
        public static float Tan(float rad) => Mathf.Tan(rad);

        /// <summary> Arc-sine of angle. </summary>
        /// <param name="rad">Angle in radians. </param>
        /// <returns>Value</returns>
        public static float Asin(float rad) => Mathf.Asin(rad);

        /// <summary> Arc-cosine of angle. </summary>
        /// <param name="rad">Angle in radians. </param>
        /// <returns>Value</returns>
        public static float Acos(float rad) => Mathf.Acos(rad);

        /// <summary> Arc-tangent of angle. </summary>
        /// <param name="rad">Angle in radians. </param>
        /// <returns>Value</returns>
        public static float Atan(float rad) => Mathf.Atan(rad);

        /// <summary> Angle in radians whose Tan is y/x </summary>
        /// <param name="y">Value</param>
        /// <param name="x">Value</param>
        /// <returns>Value</returns>
        public static float Atan2(float y, float x) => Mathf.Atan2(y, x);

        /// <summary> Cosecant of angle. </summary>
        /// <param name="x">Value</param>
        /// <returns>Value</returns>
        public static float Csc(float x) => 1.0f / Mathf.Sin(x);

        /// <summary> Secant of angle. </summary>
        /// <param name="x">Value</param>
        /// <returns>Value</returns>
        public static float Sec(float x) => 1.0f / Mathf.Cos(x);

        /// <summary> Cotangent of angle. </summary>
        /// <param name="x">Value</param>
        /// <returns>Value</returns>
        public static float Cot(float x) => 1.0f / Mathf.Tan(x);

        /// <summary> Versine of angle. </summary>
        /// <param name="x">Value</param>
        /// <returns>Value</returns>
        public static float Ver(float x) => 1.0f - Mathf.Cos(x);

        /// <summary> Coversine of angle. </summary>
        /// <param name="x">Value</param>
        /// <returns>Value</returns>
        public static float Cvs(float x) => 1.0f - Mathf.Sin(x);

        /// <summary> Chord of angle. </summary>
        /// <param name="x">Value</param>
        /// <returns>Value</returns>
        public static float Crd(float x) => 2.0f * Mathf.Sin(x * 0.5f);
    }
}
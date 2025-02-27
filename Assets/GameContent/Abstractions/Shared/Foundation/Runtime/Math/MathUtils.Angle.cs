using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Math utilities. </summary>
    public static partial class MathUtils
    {
        /// <summary> Angle to direction in the XY plane. </summary>
        /// <param name="radian">Radian angle</param>
        /// <returns>Direction</returns>
        public static Vector2 AngToDir(float radian) => new Vector2(Cos(radian), Sin(radian));
    }
}

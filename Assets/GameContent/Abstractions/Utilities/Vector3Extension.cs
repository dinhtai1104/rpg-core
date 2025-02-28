using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Vector3Extension
{
    public static bool Compare(this Vector3 v1, Vector3 v2, double epsilon = 1e-4)
    {
        return Vector3.SqrMagnitude(v1 - v2) <= epsilon;
    }

    public static bool InsideCircle(this Vector3 v, Vector3 center, float radius)
    {
        return Vector3.SqrMagnitude(v - center) <= radius * radius;
    }

    public static Vector3 Round(this Vector3 vector3, int digit = 1)
    {
        return new Vector3((float)Math.Round(vector3.x, digit), (float)Math.Round(vector3.y, digit),
                           (float)Math.Round(vector3.z, digit));
    }

    public static Vector2 Round(this Vector2 vector3, int digit)
    {
        return new Vector2((float)Math.Round(vector3.x, digit), (float)Math.Round(vector3.y, digit));
    }

    public static Vector3 Round(this Vector3 vector3, int digit, MidpointRounding rounding)
    {
        return new Vector3((float)Math.Round(vector3.x, digit, rounding), (float)Math.Round(vector3.y, digit, rounding),
                           (float)Math.Round(vector3.z, digit, rounding));
    }

    public static Vector2 Round(this Vector2 vector3, int digit, MidpointRounding rounding)
    {
        return new Vector2((float)Math.Round(vector3.x, digit, rounding), (float)Math.Round(vector3.y, digit, rounding));
    }
}
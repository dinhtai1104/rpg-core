using System;
using System.Reflection;
using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Reflection extensions. </summary>
    public static class ReflectionExtensions
    {
        /// <summary> Returns an attribute or null. </summary>
        /// <param name="self"> The object. </param>
        /// <returns> Attribute or null. </returns>
        public static T GetAttribute<T>(this FieldInfo self, bool inherit = true) where T : Attribute
        {
            object[] objects = self.GetCustomAttributes(typeof(T), inherit);

            if (objects.Length == 0)
                Log.Warning($"Attribute '{typeof(T).Name}' not found");

            return objects.Length > 0 ? objects[0] as T : null;
        }

        /// <summary> Does it have the attribute? </summary>
        /// <returns> True or false. </returns>
        public static bool HasAttribute<T>(this FieldInfo self, bool inherit = true) where T : Attribute =>
            self.GetCustomAttributes(typeof(T), inherit).Length > 0;

        /// <summary> Returns property or null. </summary>
        /// <param name="self"> The object. </param>
        /// <param name="propertyName"> Property name. </param>
        /// <param name="attribute"> Attribute or null. </param>
        /// <param name="propertyInfo"> Property info. </param>
        /// <returns> True or false. </returns>
        public static bool GetProperty<T>(this object self, string propertyName, out T attribute, out PropertyInfo propertyInfo) where T : PropertyAttribute
        {
            attribute = null;
            propertyInfo = null;

            PropertyInfo[] properties = self.GetType().GetProperties();
            for (int i = 0; i < properties.Length && attribute == null; ++i)
            {
                if (properties[i].Name == propertyName)
                {
                    object[] attributes = properties[i].GetCustomAttributes(true);
                    for (int j = 0; j < attributes.Length; ++j)
                    {
                        attribute = attributes[j] as T;
                        propertyInfo = properties[i];
                    }
                }
            }

            return attribute != null && propertyInfo != null;
        }

        /// <summary> Returns private property by name or null. </summary>
        /// <param name="self"> The object. </param>
        /// <param name="propertyName"> Property name. </param>
        /// <returns> Property or null. </returns>
        public static PropertyInfo GetProperty(this object self, string propertyName) =>
            self.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary> Returns private field by name or null. </summary>
        /// <param name="self"> The object. </param>
        /// <param name="fieldName"> Field name. </param>
        /// <returns> Field or null. </returns>
        public static FieldInfo GetField(this object self, string fieldName) =>
            self.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
    }
}
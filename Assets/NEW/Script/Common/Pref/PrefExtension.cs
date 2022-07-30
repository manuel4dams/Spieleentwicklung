using System;
using System.Reflection;
using UnityEngine;

namespace Project
{
    public static class PrefExtension
    {
        public static PrefAttribute GetAttribute(this Prefs prefs)
        {
            var type = prefs.GetType();
            var fieldInfo = type.GetField(prefs.ToString());
            var attribute = fieldInfo.GetCustomAttribute(typeof(PrefAttribute), false) as PrefAttribute;
            if (attribute == null)
                throw new Exception("PrefExtension.GetAttribute was called on field without attribute.");
            return attribute;
        }

        public static string GetString(this Prefs prefs)
        {
            var attribute = CheckAndGetAttribute<string>(prefs);
            return PlayerPrefs.GetString(attribute.name, (string)attribute.defaultValue);
        }

        public static float GetFloat(this Prefs prefs)
        {
            var attribute = CheckAndGetAttribute<float>(prefs);
            return PlayerPrefs.GetFloat(attribute.name, (float)attribute.defaultValue);
        }

        public static int GetInt(this Prefs prefs)
        {
            var attribute = CheckAndGetAttribute<int>(prefs);
            return PlayerPrefs.GetInt(attribute.name, (int)attribute.defaultValue);
        }

        public static bool GetBool(this Prefs prefs)
        {
            var attribute = CheckAndGetAttribute<bool>(prefs);
            return PlayerPrefs.GetInt(attribute.name, (bool)attribute.defaultValue ? 1 : 0) == 1;
        }

        public static void Set(this Prefs prefs, string value)
        {
            var attribute = CheckAndGetAttribute<string>(prefs);
            PlayerPrefs.SetString(attribute.name, value);
        }

        public static void Set(this Prefs prefs, float value)
        {
            var attribute = CheckAndGetAttribute<float>(prefs);
            PlayerPrefs.SetFloat(attribute.name, value);
        }

        public static void Set(this Prefs prefs, int value)
        {
            var attribute = CheckAndGetAttribute<int>(prefs);
            PlayerPrefs.SetInt(attribute.name, value);
        }

        public static void Set(this Prefs prefs, bool value)
        {
            var attribute = CheckAndGetAttribute<bool>(prefs);
            PlayerPrefs.SetInt(attribute.name, value ? 1 : 0);
        }
        
        // TODO Maybe GetDefaultString, GetDefaultInt, ...?

        // public static T Get<T>(this Prefs prefs) where T : class
        // {
        //     var attribute = CheckAndGetAttribute<T>(prefs);
        //
        //     // Switch types
        //     if (typeof(string) == attribute.type)
        //         return PlayerPrefs.GetString(attribute.name, (string)attribute.defaultValue) as T;
        //     if (typeof(float) == attribute.type)
        //         return PlayerPrefs.GetFloat(attribute.name, (float)attribute.defaultValue) as T;
        //     if (typeof(int) == attribute.type)
        //         return PlayerPrefs.GetInt(attribute.name, (int)attribute.defaultValue) as T;
        //     if (typeof(bool) == attribute.type)
        //         return (PlayerPrefs.GetInt(attribute.name, (bool)attribute.defaultValue ? 1 : 0) == 1) as T;
        //
        //     throw new ArgumentException("Unknown type used for Prefs.Get<T>. Probably the type is not yet defined for Prefs.");
        // }
        //
        // public static void Set<T>(this Prefs prefs, T value) where T : struct
        // {
        //     var attribute = CheckAndGetAttribute<T>(prefs);
        //
        //     // Switch types
        //     if (typeof(string) == attribute.type)
        //         PlayerPrefs.SetString(attribute.name, value as string);
        //     if (typeof(float) == attribute.type)
        //         PlayerPrefs.SetFloat(attribute.name, value as float);
        //     if (typeof(int) == attribute.type)
        //         PlayerPrefs.SetInt(attribute.name, value as int);
        //     if (typeof(bool) == attribute.type)
        //         PlayerPrefs.SetInt(attribute.name, value as bool ? 1 : 0);
        //
        //     throw new ArgumentException("Unknown type used for Prefs.Set<T>. Probably the type is not yet defined for Prefs.");
        // }

        private static PrefAttribute CheckAndGetAttribute<T>(Prefs prefs)
        {
            var attribute = prefs.GetAttribute();
            var attributeType = attribute.defaultValue.GetType();

            // Error handling
            if (typeof(T) != attributeType)
                throw new Exception($"Generic type must match the annotated attributes type for name {attribute.name}.");
            if (typeof(object) == attributeType)
                throw new ArgumentException($"{nameof(Prefs.Undefined)} is not supported.");

            return attribute;
        }
    }
}

using System;
using UnityEngine;

namespace MyBox
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ConditionalFieldAttribute : PropertyAttribute
    {
        /// <param name="fieldToCheck">String name of field to check value</param>
        /// <param name="inverse">Inverse check result</param>
        /// <param name="compareValues">On which values field will be shown in inspector</param>
        public ConditionalFieldAttribute(string fieldToCheck, bool inverse = false, params object[] compareValues)
        {
        }


        public ConditionalFieldAttribute(string[] fieldToCheck, bool[] inverse = null, params object[] compare)
        {
        }

        public ConditionalFieldAttribute(params string[] fieldToCheck)
        {
        }

        public ConditionalFieldAttribute(bool useMethod, string method, bool inverse = false)
        {
        }
    }
}

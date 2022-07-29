using System;

namespace Project
{
    public class PrefAttribute : Attribute
    {
        public readonly string name;
        public readonly object defaultValue;

        public PrefAttribute(string name, object defaultValue)
        {
            this.name = name;
            this.defaultValue = defaultValue;
        }
    }
}

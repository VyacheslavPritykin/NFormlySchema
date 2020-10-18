using System;

namespace NFormlySchema
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumValueGroupAttribute : Attribute
    {
        public EnumValueGroupAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }
}
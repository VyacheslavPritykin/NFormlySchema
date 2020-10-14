using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumValueGroupAttribute : Attribute
    {
        public string Name { get; }

        public EnumValueGroupAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
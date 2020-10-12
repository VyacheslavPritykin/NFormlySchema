using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InputTypeAttribute : Attribute
    {
        public string Type { get; }

        public InputTypeAttribute(string type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
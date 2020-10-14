using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FieldTypeAttribute : Attribute
    {
        public string Type { get; }

        public FieldTypeAttribute(string type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
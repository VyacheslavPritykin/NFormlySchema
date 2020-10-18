using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FieldGroupAttribute : Attribute
    {
        public string? ClassName { get; set; }
    }
}
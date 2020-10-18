using System;

namespace NFormlySchema
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FieldGroupAttribute : Attribute
    {
        public string? ClassName { get; set; }
    }
}
using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FieldGroupClassNameAttribute : Attribute
    {
        public string ClassName { get; }

        public FieldGroupClassNameAttribute(string className)
        {
            ClassName = className ?? throw new ArgumentNullException(nameof(className));
        }
    }
}
using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ClassNameAttribute : Attribute
    {
        public string ClassName { get; }

        public ClassNameAttribute(string className)
        {
            ClassName = className ?? throw new ArgumentNullException(nameof(className));
        }
    }
}
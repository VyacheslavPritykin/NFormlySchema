using System;

namespace NFormlySchema
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ClassNameAttribute : Attribute
    {
        public ClassNameAttribute(string className)
        {
            ClassName = className ?? throw new ArgumentNullException(nameof(className));
        }

        public string ClassName { get; }
    }
}
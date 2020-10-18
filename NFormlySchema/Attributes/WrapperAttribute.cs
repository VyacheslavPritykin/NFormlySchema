using System;

namespace NFormlySchema
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class WrapperAttribute : Attribute
    {
        public WrapperAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }
}
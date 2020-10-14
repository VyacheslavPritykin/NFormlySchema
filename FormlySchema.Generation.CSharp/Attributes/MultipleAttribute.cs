using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MultipleAttribute : Attribute
    {
        public MultipleAttribute(bool multiple)
        {
            Multiple = multiple;
        }

        public bool Multiple { get; }
    }
}
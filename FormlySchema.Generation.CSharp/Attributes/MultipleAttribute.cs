using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MultipleAttribute : Attribute
    {
        public bool Multiple { get; }

        public MultipleAttribute(bool multiple)
        {
            Multiple = multiple;
        }
    }
}
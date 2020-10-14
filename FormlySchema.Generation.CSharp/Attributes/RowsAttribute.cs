using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RowsAttribute : Attribute
    {
        public RowsAttribute(int count)
        {
            Count = count;
        }

        public int Count { get; }
    }
}
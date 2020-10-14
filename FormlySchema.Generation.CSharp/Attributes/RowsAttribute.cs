using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RowsAttribute : Attribute
    {
        public int Count { get; }

        public RowsAttribute(int count)
        {
            Count = count;
        }
    }
}
using System;

namespace NFormlySchema
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
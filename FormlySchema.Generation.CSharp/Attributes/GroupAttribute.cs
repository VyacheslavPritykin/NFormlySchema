using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class GroupAttribute : Attribute
    {
        public string Name { get; }

        public GroupAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
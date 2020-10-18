using System;

namespace NFormlySchema
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AddonRightAttribute : Attribute
    {
        public string? Text { get; set; }

        public string? Class { get; set; }
    }
}
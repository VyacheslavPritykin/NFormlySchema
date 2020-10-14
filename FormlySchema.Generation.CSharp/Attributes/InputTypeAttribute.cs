﻿using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InputTypeAttribute : Attribute
    {
        public InputTypeAttribute(string type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public string Type { get; }
    }
}
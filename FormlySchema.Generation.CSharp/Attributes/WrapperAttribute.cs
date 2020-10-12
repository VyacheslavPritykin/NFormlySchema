﻿using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class WrapperAttribute : Attribute
    {
        public string Name { get; }

        public WrapperAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
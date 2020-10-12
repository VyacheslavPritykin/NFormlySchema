﻿using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AddonLeftAttribute : Attribute
    {
        public string? Text { get; set; }
        
        public string? Class { get; set; }
    }
}
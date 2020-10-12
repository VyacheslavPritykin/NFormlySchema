using System;

namespace FormlySchema.Generation.CSharp
{
    public class FormlySettings
    {
        public Func<Type, Attribute[], string?>? InputTypeResolver { get; set; }
    }
}
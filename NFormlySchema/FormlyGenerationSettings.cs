using System;

namespace NFormlySchema
{
    public class FormlyGenerationSettings
    {
        public Func<Type, Attribute[], string?>? InputTypeResolver { get; set; }
    }
}
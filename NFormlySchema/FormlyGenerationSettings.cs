using System;

namespace NFormlySchema
{
    public class FormlyGenerationSettings
    {
        private static Func<Type, Attribute[], string?> _inputTypeResolver =
            FormlySchemaDefaults.DefaultInputTypeResolver;

        public static Func<Type, Attribute[], string?> InputTypeResolver
        {
            get => _inputTypeResolver;
            set => _inputTypeResolver = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValidatorsAttribute : Attribute
    {
        public string[] Validators { get; }

        public ValidatorsAttribute(params string[] validators)
        {
            Validators = validators;
        }
    }
}
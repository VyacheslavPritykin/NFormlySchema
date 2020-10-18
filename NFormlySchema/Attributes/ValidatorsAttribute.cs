using System;

namespace NFormlySchema
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValidatorsAttribute : Attribute
    {
        public ValidatorsAttribute(params string[] validators)
        {
            Validators = validators;
        }

        public string[] Validators { get; }

        public bool IsAsync { get; set; }

        public bool IsFunction { get; set; }
    }
}
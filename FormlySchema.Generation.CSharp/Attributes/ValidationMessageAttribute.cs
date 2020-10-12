using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValidationMessageAttribute : Attribute
    {
        public ValidationMessageAttribute(string name, string messageExpression)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MessageExpression = messageExpression ?? throw new ArgumentNullException(nameof(messageExpression));
        }

        public string Name { get; }
        public string MessageExpression { get; }

        public bool IsFunction { get; set; }
    }
}
using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ExpressionPropertyAttribute : Attribute
    {
        /// <example>ExpressionProperty("templateOptions.disabled", "!model.text")</example>
        public ExpressionPropertyAttribute(string property, string expression)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public string Property { get; }
        public string Expression { get; }
        
        public bool IsFunction { get; set; }
    }
}
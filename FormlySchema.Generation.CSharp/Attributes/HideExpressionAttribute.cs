using System;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HideExpressionAttribute : Attribute
    {
        public HideExpressionAttribute(string expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public string Expression { get; }

        public bool IsFunction { get; set; }
    }
}
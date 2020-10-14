// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace FormlySchema.Generation.CSharp
{
    public class FormlyFieldConfig
    {
        public string? Key { get; set; }
        public string? Type { get; set; }
        public object? DefaultValue { get; set; }
        public object? HideExpression { get; set; }
        public FormlyTemplateOptions? TemplateOptions { get; set; }
        public ExpressionPropertyDictionary? ExpressionProperties { get; set; }
        public Validation? Validation { get; set; }
        public WrapperCollection? Wrappers { get; set; }
        public Validators? Validators { get; set; }
        public string? FieldGroupClassName { get; set; }
        public FormlyFieldConfigCollection? FieldGroup { get; set; }
        public FormlyFieldConfig? FieldArray { get; set; }
        public string? ClassName { get; set; }
    }
}
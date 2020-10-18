namespace NFormlySchema
{
    public class FormlyTemplateOptions
    {
        public string? Label { get; set; }
        public string? Placeholder { get; set; }
        public string? Description { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public bool? Required { get; set; }
        public string? Pattern { get; set; }
        public bool? Disabled { get; set; }
        public OptionCollection? Options { get; set; }
        public string? Type { get; set; }
        public bool? Multiple { get; set; }
        public int? Rows { get; set; }

        public Addon? AddonLeft { get; set; }

        public Addon? AddonRight { get; set; }
    }

    public class Addon
    {
        public string? Class { get; set; }

        public string? Text { get; set; }
    }
}
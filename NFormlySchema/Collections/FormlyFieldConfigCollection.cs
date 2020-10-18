using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace NFormlySchema
{
    public class FormlyFieldConfigCollection : Collection<FormlyFieldConfig>
    {
        public static readonly FormlyFieldConfigCollection Empty = new FormlyFieldConfigCollection();

        public FormlyFieldConfigCollection()
        {
        }

        public FormlyFieldConfigCollection(IList<FormlyFieldConfig> list) : base(list)
        {
        }

        public string ToJson(Formatting formatting = Formatting.Indented) =>
            this.SerializeToJson(formatting);
    }
}
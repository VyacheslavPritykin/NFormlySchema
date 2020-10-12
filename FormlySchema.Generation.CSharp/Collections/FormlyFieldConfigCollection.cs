using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FormlySchema.Generation.CSharp
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

        public string ToJson() => this.SerializeToJson();
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FormlySchema.Generation.CSharp
{
    public class OptionCollection : Collection<object>
    {
        public OptionCollection()
        {
        }

        public OptionCollection(IList<object> list) : base(list)
        {
        }
    }
}
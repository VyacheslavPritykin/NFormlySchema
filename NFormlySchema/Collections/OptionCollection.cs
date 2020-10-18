using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NFormlySchema
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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NFormlySchema
{
    public class WrapperCollection : Collection<string>
    {
        public WrapperCollection()
        {
        }

        public WrapperCollection(IList<string> list) : base(list)
        {
        }
    }
}
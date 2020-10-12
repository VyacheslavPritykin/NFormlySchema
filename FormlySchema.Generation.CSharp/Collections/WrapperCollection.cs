using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FormlySchema.Generation.CSharp
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
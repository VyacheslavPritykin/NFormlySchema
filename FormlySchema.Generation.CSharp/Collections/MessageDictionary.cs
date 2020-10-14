using System;
using System.Collections.Generic;

namespace FormlySchema.Generation.CSharp
{
    public class MessageDictionary : Dictionary<string, object>
    {
        public MessageDictionary() : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        public MessageDictionary(IDictionary<string, object> dictionary)
            : base(dictionary, StringComparer.InvariantCultureIgnoreCase)
        {
        }

        public MessageDictionary(IEnumerable<KeyValuePair<string, object>> list)
            : base(list, StringComparer.InvariantCultureIgnoreCase)
        {
        }
    }
}
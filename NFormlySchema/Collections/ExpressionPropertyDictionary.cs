﻿using System.Collections.Generic;

namespace NFormlySchema
{
    public class ExpressionPropertyDictionary : Dictionary<string, object>
    {
        public ExpressionPropertyDictionary()
        {
        }

        public ExpressionPropertyDictionary(IDictionary<string, object> dictionary) : base(dictionary)
        {
        }

        public ExpressionPropertyDictionary(IEnumerable<KeyValuePair<string, object>> list) : base(list)
        {
        }
    }
}
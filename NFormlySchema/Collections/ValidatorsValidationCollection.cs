using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NFormlySchema
{
    public class ValidatorsValidationCollection : Collection<object>
    {
        public ValidatorsValidationCollection()
        {
        }

        public ValidatorsValidationCollection(IList<object> list) : base(list)
        {
        }
    }
}
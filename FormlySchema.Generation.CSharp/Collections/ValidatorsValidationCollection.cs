using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FormlySchema.Generation.CSharp
{
    public class ValidatorsValidationCollection : Collection<string>
    {
        public ValidatorsValidationCollection()
        {
        }

        public ValidatorsValidationCollection(IList<string> list) : base(list)
        {
        }
    }
}
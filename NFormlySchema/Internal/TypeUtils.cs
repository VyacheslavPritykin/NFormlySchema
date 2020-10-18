using System;

namespace NFormlySchema.Internal
{
    internal class TypeUtils
    {
        public static bool IsFormGroup(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            return Type.GetTypeCode(type) == TypeCode.Object
                   && type != typeof(DateTimeOffset)
                   && type != typeof(Guid)
                   && !type.IsCollection();
        }
    }
}
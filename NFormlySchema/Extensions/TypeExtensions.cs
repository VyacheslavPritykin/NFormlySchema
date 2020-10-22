using System;
using System.Collections;

namespace NFormlySchema
{
    internal static class TypeExtensions
    {
        public static bool IsCollection(this Type type) =>
            type.GetInterface(nameof(ICollection)) != null;

        public static bool IsSimple(this Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            return type.IsPrimitive
                   || type.IsEnum
                   || type == typeof(string)
                   || type == typeof(decimal)
                   || type == typeof(DateTime)
                   || type == typeof(DateTimeOffset)
                   || type == typeof(TimeSpan)
                   || type == typeof(Guid);
        }
    }
}
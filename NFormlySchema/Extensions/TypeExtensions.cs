using System;
using System.Collections;

namespace NFormlySchema
{
    internal static class TypeExtensions
    {
        public static bool IsCollection(this Type type) =>
            type.GetInterface(nameof(ICollection)) != null;
    }
}
using System;
using System.Collections;

namespace FormlySchema.Generation.CSharp
{
    internal static class TypeExtensions
    {
        public static bool IsCollection(this Type type) =>
            type.GetInterface(nameof(ICollection)) != null;
    }
}
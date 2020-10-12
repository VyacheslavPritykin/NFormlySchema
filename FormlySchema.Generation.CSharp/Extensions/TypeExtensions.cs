using System;
using System.Collections;
using System.Collections.Generic;

namespace FormlySchema.Generation.CSharp
{
    internal static class TypeExtensions
    {
        public static bool IsCollection(this Type type)
        {
            return type.GetInterface(nameof(ICollection)) != null;

            var elementType = type.GetElementType();
            if (elementType == null)
                return false;

            return typeof(ICollection<>)
                .MakeGenericType(elementType)
                .IsAssignableFrom(type);
        }
    }
}
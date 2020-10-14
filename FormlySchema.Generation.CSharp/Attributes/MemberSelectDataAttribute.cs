using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FormlySchema.Generation.CSharp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MemberSelectDataAttribute : Attribute
    {
        public MemberSelectDataAttribute(string memberName, params object?[] parameters)
        {
            MemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public string MemberName { get; }

        public Type? MemberType { get; set; }

        public object?[]? Parameters { get; }

        public string GroupProp { get; set; } = "Group";

        public string ValueProp { get; set; } = "Value";

        public string LabelProp { get; set; } = "Label";

        public IEnumerable<object>? GetData(Type? memberType)
        {
            if (memberType == null) throw new ArgumentNullException(nameof(memberType));

            var type = MemberType ?? memberType;
            if (type == null)
                return null;

            var accessor = GetPropertyAccessor(type) ?? GetFieldAccessor(type) ?? GetMethodAccessor(type);
            if (accessor == null)
            {
                var parameterText = Parameters?.Length > 0
                    ? $" with parameter types: {string.Join(", ", Parameters.Select(p => p?.GetType().FullName ?? "(null)"))}"
                    : "";
                throw new ArgumentException(
                    $"Could not find public static member (property, field, or method) named '{MemberName}' on {type.FullName}{parameterText}");
            }

            var obj = accessor();
            if (obj == null)
                return null;

            if (!(obj is IEnumerable dataItems))
                throw new ArgumentException($"Property {MemberName} on {type.FullName} did not return IEnumerable");

            return dataItems.Cast<object>();
        }

        private Func<object?>? GetFieldAccessor(Type? type)
        {
            FieldInfo? fieldInfo = null;
            for (var reflectionType = type; reflectionType != null; reflectionType = reflectionType.BaseType)
            {
                fieldInfo = reflectionType.GetRuntimeField(MemberName);
                if (fieldInfo != null)
                    break;
            }

            if (fieldInfo == null || !fieldInfo.IsStatic)
                return null;

            return () => fieldInfo.GetValue(null);
        }

        private Func<object?>? GetMethodAccessor(Type? type)
        {
            MethodInfo? methodInfo = null;
            var parameterTypes = Parameters == null ? new Type[0] : Parameters.Select(p => p?.GetType()).ToArray();
            for (var reflectionType = type; reflectionType != null; reflectionType = reflectionType.BaseType)
            {
                methodInfo =
                    reflectionType
                        .GetRuntimeMethods()
                        .FirstOrDefault(m =>
                            m.Name == MemberName && ParameterTypesCompatible(m.GetParameters(), parameterTypes));

                if (methodInfo != null)
                    break;
            }

            if (methodInfo == null || !methodInfo.IsStatic)
                return null;

            return () => methodInfo.Invoke(null, Parameters);
        }

        private Func<object?>? GetPropertyAccessor(Type? type)
        {
            PropertyInfo? propInfo = null;
            for (var reflectionType = type; reflectionType != null; reflectionType = reflectionType.BaseType)
            {
                propInfo = reflectionType.GetRuntimeProperty(MemberName);
                if (propInfo != null)
                    break;
            }

            if (propInfo == null || propInfo.GetMethod == null || !propInfo.GetMethod.IsStatic)
                return null;

            return () => propInfo.GetValue(null, null);
        }

        private static bool ParameterTypesCompatible(IReadOnlyList<ParameterInfo>? parameters,
            IReadOnlyList<Type?> parameterTypes)
        {
            if (parameters?.Count != parameterTypes.Count)
                return false;

            for (var i = 0; i < parameters.Count; ++i)
                if (parameterTypes[i] != null &&
                    !parameters[i].ParameterType.IsAssignableFrom(parameterTypes[i]!))
                    return false;

            return true;
        }
    }
}
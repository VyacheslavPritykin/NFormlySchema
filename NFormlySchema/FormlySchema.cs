using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFormlySchema.Generation;
using NFormlySchema.Internal;

namespace NFormlySchema
{
    public static class FormlySchema
    {
        private static readonly ConcurrentDictionary<CacheKey, FormlyFieldConfigCollection> Cache =
            new ConcurrentDictionary<CacheKey, FormlyFieldConfigCollection>();

        private static readonly FormlyGenerationSettings DefaultFormlyGenerationSettings =
            new FormlyGenerationSettings();

        public static FormlyFieldConfigCollection FromType<T>() =>
            FromType<T>(DefaultFormlyGenerationSettings);

        public static FormlyFieldConfigCollection FromType<T>(FormlyGenerationSettings setting) =>
            FromType(typeof(T), setting);

        public static FormlyFieldConfigCollection FromType(Type type) =>
            FromType(type, DefaultFormlyGenerationSettings);

        public static FormlyFieldConfigCollection FromType(Type type, FormlyGenerationSettings setting) =>
            FromType(type, setting, null);

        private static FormlyFieldConfigCollection FromType(Type type,
            FormlyGenerationSettings setting,
            string? parentKey)
        {
            if (Cache.TryGetValue(new CacheKey(type, parentKey), out FormlyFieldConfigCollection result))
                return result;

            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            result = new FormlyFieldConfigCollection(type.GetProperties(bindingFlags)
                .Select(propertyInfo => new
                {
                    PropertyInfo = propertyInfo,
                    Attributes = Attribute.GetCustomAttributes(propertyInfo)
                })
                .Where(x => !x.Attributes.Any(IsIgnoreAttribute))
                .OrderBy(x => ResolveOrder(x.Attributes))
                .SelectMany(x =>
                {
                    IEnumerable<FormlyFieldConfig> fieldConfigs;
                    if (IsInlineNestedFieldGroup(x.PropertyInfo, x.Attributes))
                    {
                        var key = ResolveKey(parentKey, x.PropertyInfo.Name, x.Attributes);
                        fieldConfigs = FromType(x.PropertyInfo.PropertyType, setting, key);
                        return fieldConfigs;
                    }

                    fieldConfigs = new[]
                    {
                        BuildFormlyFieldConfig(x.PropertyInfo, x.Attributes, setting, type, parentKey)
                    };

                    return fieldConfigs;
                })
                .ToList());

            var typeCustomAttributes = type.GetCustomAttributes();
            var fieldTypeAttribute = typeCustomAttributes.OfType<FieldTypeAttribute>().FirstOrDefault();
            if (fieldTypeAttribute != null)
            {
                result = new FormlyFieldConfigCollection
                {
                    new FormlyFieldConfig
                    {
                        Type = fieldTypeAttribute.Type,
                        FieldGroup = result
                    }
                };
            }

            Cache.TryAdd(new CacheKey(type, parentKey), result);

            return result;
        }

        private static bool IsIgnoreAttribute(Attribute attr) => attr is IgnoreDataMemberAttribute ||
                                                                 attr is JsonIgnoreAttribute ||
                                                                 attr is System.Text.Json.Serialization
                                                                     .JsonIgnoreAttribute;

        private static int ResolveOrder(Attribute[] attributes) =>
            attributes.OfType<DisplayAttribute>().FirstOrDefault()?.GetOrder() ?? 10000;

        private static FormlyFieldConfig BuildFormlyFieldConfig(PropertyInfo propertyInfo,
            Attribute[] attributes,
            FormlyGenerationSettings setting,
            Type type,
            string? parentKey)
        {
            var formlyFieldConfig = new FormlyFieldConfig
            {
                Key = ResolveKey(parentKey, propertyInfo.Name, attributes),
                Type = ResolveFieldType(propertyInfo.PropertyType, attributes),
                DefaultValue = ResolveDefaultValue(attributes),
                TemplateOptions =
                    ResolveFormlyTemplateOptions(propertyInfo.PropertyType, attributes, setting, type),
                HideExpression = ResolveHideExpression(attributes),
                ExpressionProperties = ResolveExpressionProperties(attributes),
                Validation = ResolveValidation(attributes),
                Validators = ResolveValidators(attributes),
                AsyncValidators = ResolveAsyncValidators(attributes),
                Wrappers = ResolveWrappers(attributes),
                ClassName = ResolveClassName(attributes),
                FieldGroupClassName = ResolveFieldGroupClassName(attributes),
                FieldGroup = ResolveFieldGroup(propertyInfo, attributes, setting),
                FieldArray = ResolveFieldArray(propertyInfo, setting),
            };

            return formlyFieldConfig;
        }

        private static string? ResolveClassName(Attribute[] attributes) =>
            attributes.OfType<ClassNameAttribute>().FirstOrDefault()?.ClassName;

        private static string? ResolveFieldGroupClassName(Attribute[] attributes) =>
            attributes.OfType<FieldGroupAttribute>().FirstOrDefault()?.ClassName;

        private static FormlyFieldConfig BuildFormlyFieldConfigForSimpleArrayElement(Type propertyType)
        {
            var attributes = Array.Empty<Attribute>();
            var formlyFieldConfig = new FormlyFieldConfig
            {
                Type = ResolveFieldType(propertyType, attributes),
            };

            return formlyFieldConfig;
        }

        private static FormlyFieldConfig BuildFormlyFieldConfigForCustomArrayElement(PropertyInfo pi, FormlyGenerationSettings settings)
        {
            var arrayElementType = pi.PropertyType.GetElementType();
            var fieldGroupForArrayElement = FromType(arrayElementType, settings);
            var formlyFieldConfig = new FormlyFieldConfig
            {
                FieldGroup = fieldGroupForArrayElement
            };
            return formlyFieldConfig;
        }



        private static FormlyFieldConfig? ResolveFieldArray(PropertyInfo propertyInfo, FormlyGenerationSettings setting)
        {
            if (!propertyInfo.PropertyType.IsCollection())
                return null;

            var elementType = propertyInfo.PropertyType.GetElementType()!;
            if (elementType.IsSimple())
                return BuildFormlyFieldConfigForSimpleArrayElement(elementType);
            else
                return BuildFormlyFieldConfigForCustomArrayElement(propertyInfo, setting);

            // return null; // TODO
        }

        private static FormlyFieldConfigCollection? ResolveFieldGroup(PropertyInfo propertyInfo,
            Attribute[] attributes,
            FormlyGenerationSettings setting)
        {
            return !propertyInfo.PropertyType.IsSimple()
                   && !propertyInfo.PropertyType.IsCollection()
                   && attributes.OfType<FieldGroupAttribute>().Any()
                ? FromType(propertyInfo.PropertyType, setting)
                : null;
        }

        private static bool IsInlineNestedFieldGroup(PropertyInfo propertyInfo, Attribute[] attributes) =>
            !propertyInfo.PropertyType.IsSimple()
            && !propertyInfo.PropertyType.IsCollection()
            && !attributes.OfType<FieldGroupAttribute>().Any();

        private static Validators? ResolveValidators(Attribute[] attributes)
        {
            var validators = new Validators
            {
                Validation = ResolveValidatorsValidation(attributes, false)
            };

            return validators.HasData() ? validators : null;
        }

        private static Validators? ResolveAsyncValidators(Attribute[] attributes)
        {
            var validators = new Validators
            {
                Validation = ResolveValidatorsValidation(attributes, true)
            };

            return validators.HasData() ? validators : null;
        }

        private static ValidatorsValidationCollection? ResolveValidatorsValidation(Attribute[] attributes, bool isAsync)
        {
            var validators = attributes.OfType<ValidatorsAttribute>()
                .Where(x => x.IsAsync == isAsync)
                .SelectMany(x => x.IsFunction ? x.Validators.Select(v => (object)new JRaw(v)) : x.Validators)
                .ToList();
            return validators.Any() ? new ValidatorsValidationCollection(validators) : null;
        }

        private static WrapperCollection? ResolveWrappers(Attribute[] attributes)
        {
            var wrappers = attributes.OfType<WrapperAttribute>().Select(attr => attr.Name).ToList();
            return wrappers.Any() ? new WrapperCollection(wrappers) : null;
        }

        private static ExpressionPropertyDictionary? ResolveExpressionProperties(Attribute[] attributes)
        {
            var pairs = attributes.OfType<ExpressionPropertyAttribute>()
                .Select(attr => new KeyValuePair<string, object>(
                    attr.Property,
                    attr.IsFunction ? (object)new JRaw(attr.Expression) : attr.Expression))
                .ToList();

            return pairs.Any() ? new ExpressionPropertyDictionary(pairs) : null;
        }

        private static FormlyTemplateOptions? ResolveFormlyTemplateOptions(Type propertyType,
            Attribute[] attributes,
            FormlyGenerationSettings setting,
            Type type)
        {
            var templateOptions = new FormlyTemplateOptions
            {
                Label = ResolveLabel(attributes),
                Required = ResolveRequired(attributes),
                Placeholder = ResolvePlaceholder(attributes),
                Description = ResolveDescription(attributes),
                Min = ResolveMin(attributes),
                Max = ResolveMax(attributes),
                MinLength = ResolveMinLength(attributes),
                MaxLength = ResolveMaxLength(attributes),
                Pattern = ResolvePattern(attributes),
                Disabled = ResolveDisabled(attributes),
                Options = ResolveOptions(propertyType, attributes, type),
                Type = FormlyGenerationSettings.InputTypeResolver.Invoke(propertyType, attributes),
                Multiple = ResolveMultiple(propertyType, attributes),
                Rows = ResolveRows(attributes),
                AddonLeft = ResolveAddonLeft(attributes),
                AddonRight = ResolveAddonRight(attributes),
            };

            return templateOptions.HasData() ? templateOptions : null;
        }

        private static Addon? ResolveAddonLeft(Attribute[] attributes) =>
            attributes.OfType<AddonLeftAttribute>()
                .Select(x => new Addon { Class = x.Class, Text = x.Text })
                .FirstOrDefault();

        private static Addon? ResolveAddonRight(Attribute[] attributes) =>
            attributes.OfType<AddonRightAttribute>()
                .Select(x => new Addon { Class = x.Class, Text = x.Text })
                .FirstOrDefault();

        private static int? ResolveRows(Attribute[] attributes) =>
            attributes.OfType<RowsAttribute>().FirstOrDefault()?.Count;

        private static bool? ResolveMultiple(Type propertyType, Attribute[] attributes)
        {
            var multipleAttribute = attributes.OfType<MultipleAttribute>().FirstOrDefault();
            if (multipleAttribute != null)
            {
                return multipleAttribute.Multiple ? true : (bool?)null;
            }

            if (propertyType.IsEnum && propertyType.GetCustomAttribute<FlagsAttribute>() != null)
            {
                return true;
            }

            return null;
        }

        private static OptionCollection? ResolveOptions(Type propertyType, Attribute[] attributes, Type type)
        {
            if (propertyType.IsEnum)
            {
                var options = new OptionCollection();
                foreach (var enumName in propertyType.GetEnumNames())
                {
                    var memberInfo = propertyType.GetMember(enumName).First();
                    var label = memberInfo.GetCustomAttribute<DisplayAttribute>()?.Name ?? memberInfo.Name;
                    var value = memberInfo.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName
                                ?? memberInfo.GetCustomAttribute<DataMemberAttribute>()?.Name
                                ?? memberInfo.Name;
                    var group = memberInfo.GetCustomAttribute<EnumValueGroupAttribute>()?.Name;
                    options.Add(new { label, value, group });
                }

                return options;
            }

            var memberSelectDataAttribute = attributes.OfType<MemberSelectDataAttribute>().FirstOrDefault();
            var list = memberSelectDataAttribute?.GetData(type)?.ToList();
            if (list != null && list.Count > 0)
            {
                var options = new OptionCollection();
                foreach (var element in list)
                {
                    var elementType = element.GetType();
                    var valuePropertyInfo = elementType.GetProperty(memberSelectDataAttribute!.ValueProp);
                    var labelPropertyInfo = elementType.GetProperty(memberSelectDataAttribute.LabelProp);
                    var groupPropertyInfo = elementType.GetProperty(memberSelectDataAttribute.GroupProp);

                    var value = valuePropertyInfo?.GetValue(element);
                    var label = labelPropertyInfo?.GetValue(element);
                    var group = groupPropertyInfo?.GetValue(element);

                    options.Add(new { value, label, group });
                }

                return options;
            }

            return null;
        }

        private static string? ResolveDescription(Attribute[] attributes)
        {
            var description = attributes.OfType<DescriptionAttribute>().FirstOrDefault()?.Description;
            return !string.IsNullOrEmpty(description)
                ? description
                : attributes.OfType<DisplayAttribute>().FirstOrDefault()?.Description;
        }

        private static Validation? ResolveValidation(Attribute[] attributes)
        {
            var validation = new Validation
            {
                Messages = ResolveValidationMessages(attributes)
            };

            return validation.HasData() ? validation : null;
        }

        private static MessageDictionary? ResolveValidationMessages(Attribute[] attributes)
        {
            var pairsFromValidationAttributes = attributes.OfType<ValidationAttribute>()
                .Select(attr => new { ValidationName = ResolveValidationName(attr), attr.ErrorMessage })
                .Where(x => x.ValidationName != null && !string.IsNullOrEmpty(x.ErrorMessage))
                .Select(x => new KeyValuePair<string, object>(x.ValidationName!, x.ErrorMessage));

            var pairsFromValidationMessageAttributes = attributes.OfType<ValidationMessageAttribute>()
                .Select(attr => new KeyValuePair<string, object>(attr.Name,
                    attr.IsFunction ? (object)new JRaw(attr.MessageExpression) : attr.MessageExpression));

            var keyValuePairs = pairsFromValidationAttributes.Concat(pairsFromValidationMessageAttributes).ToList();

            return keyValuePairs.Any() ? new MessageDictionary(keyValuePairs) : null;
        }

        private static string? ResolveValidationName(ValidationAttribute validationAttribute) =>
            validationAttribute switch
            {
                RequiredAttribute _ => nameof(FormlyTemplateOptions.Required),
                RegularExpressionAttribute _ => nameof(FormlyTemplateOptions.Pattern),
                MaxLengthAttribute _ => nameof(FormlyTemplateOptions.MaxLength),
                MinLengthAttribute _ => nameof(FormlyTemplateOptions.MinLength),
                _ => null
            };

        private static bool? ResolveDisabled(Attribute[] attributes) =>
            attributes.OfType<ReadOnlyAttribute>().FirstOrDefault()?.IsReadOnly == true ? true : (bool?)null;

        private static string? ResolvePattern(Attribute[] attributes) =>
            attributes.OfType<RegularExpressionAttribute>().FirstOrDefault()?.Pattern;

        private static int? ResolveMinLength(Attribute[] attributes) =>
            attributes.OfType<MinLengthAttribute>().FirstOrDefault()?.Length;

        private static int? ResolveMaxLength(Attribute[] attributes) =>
            attributes.OfType<MaxLengthAttribute>().FirstOrDefault()?.Length;

        private static double? ResolveMin(Attribute[] attributes) =>
            attributes.OfType<RangeAttribute>().FirstOrDefault()?.Minimum switch
            {
                int minInt => minInt,
                double minDouble => minDouble,
                _ => null
            };

        private static double? ResolveMax(Attribute[] attributes) =>
            attributes.OfType<RangeAttribute>().FirstOrDefault()?.Maximum switch
            {
                int maxInt => maxInt,
                double maxDouble => maxDouble,
                _ => null
            };

        private static string? ResolvePlaceholder(Attribute[] attributes) =>
            attributes.OfType<DisplayAttribute>().FirstOrDefault()?.Prompt;

        private static object? ResolveHideExpression(Attribute[] attributes)
        {
            var hideExpressionAttribute = attributes.OfType<HideExpressionAttribute>().FirstOrDefault();
            if (hideExpressionAttribute == null)
                return null;

            return hideExpressionAttribute.IsFunction
                ? (object?)new JRaw(hideExpressionAttribute.Expression)
                : hideExpressionAttribute.Expression;
        }

        private static bool? ResolveRequired(Attribute[] attributes)
        {
            if (attributes.OfType<RequiredAttribute>().Any()) return true;
            if (attributes.OfType<JsonRequiredAttribute>().Any()) return true;

            var dataMemberAttribute = attributes.OfType<DataMemberAttribute>().FirstOrDefault();
            if (dataMemberAttribute?.IsRequired == true) return true;

            return null;
        }

        private static string? ResolveKey(string? parentKey, string propertyName, Attribute[] attributes)
        {
            string key;
            var dataMemberAttribute = attributes.OfType<DataMemberAttribute>().FirstOrDefault();
            if (dataMemberAttribute?.Name != null)
            {
                key = dataMemberAttribute.Name;
            }
            else
            {
                var jsonPropertyAttribute = attributes.OfType<JsonPropertyAttribute>().FirstOrDefault();
                key = jsonPropertyAttribute?.PropertyName ?? propertyName;
            }

            return string.IsNullOrEmpty(parentKey) ? key : $"{parentKey}.{key}";
        }

        private static string? ResolveLabel(Attribute[] attributes)
        {
            var displayNameAttribute = attributes.OfType<DisplayNameAttribute>().FirstOrDefault();
            if (!string.IsNullOrEmpty(displayNameAttribute?.DisplayName)) return displayNameAttribute.DisplayName;

            var displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            return displayAttribute?.Name;
        }

        private static object? ResolveDefaultValue(Attribute[] attributes) =>
            attributes.OfType<DefaultValueAttribute>().FirstOrDefault()?.Value;

        private static string? ResolveFieldType(Type propertyType, Attribute[] attributes)
        {
            if (!propertyType.IsSimple() && !propertyType.IsCollection())
                return null;

            var fieldType = attributes.OfType<FieldTypeAttribute>().FirstOrDefault()?.Type;
            if (fieldType != null)
                return fieldType;

            var dataTypeAttribute = attributes
                .OfType<DataTypeAttribute>()
                .FirstOrDefault();

            if (dataTypeAttribute != null)
            {
                fieldType = ResolveFieldTypeFromDataType(dataTypeAttribute);
                if (fieldType != null) return fieldType;
            }

            if (attributes.OfType<MemberSelectDataAttribute>().Any())
                return KnownFieldTypes.Select;

            var underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (underlyingType.IsEnum)
                return KnownFieldTypes.Select;

            if (underlyingType == typeof(bool)) return KnownFieldTypes.CheckBox;

            if (propertyType.IsCollection())
                return KnownFieldTypes.Repeat;

            return KnownFieldTypes.Input;
        }

        private static string? ResolveFieldTypeFromDataType(DataTypeAttribute dataTypeAttribute) =>
            dataTypeAttribute.DataType switch
            {
                DataType.Custom => dataTypeAttribute.CustomDataType,
                DataType.DateTime => KnownFieldTypes.Input,
                DataType.Date => KnownFieldTypes.Input,
                DataType.Time => KnownFieldTypes.Input,
                DataType.Duration => KnownFieldTypes.Input,
                DataType.PhoneNumber => KnownFieldTypes.Input,
                DataType.Currency => KnownFieldTypes.Input,
                DataType.Text => KnownFieldTypes.Input,
                DataType.Html => KnownFieldTypes.TextArea,
                DataType.MultilineText => KnownFieldTypes.TextArea,
                DataType.EmailAddress => KnownFieldTypes.Input,
                DataType.Password => KnownFieldTypes.Input,
                DataType.Url => KnownFieldTypes.Input,
                DataType.ImageUrl => KnownFieldTypes.Input,
                DataType.CreditCard => KnownFieldTypes.Input,
                DataType.PostalCode => KnownFieldTypes.Input,
                DataType.Upload => KnownFieldTypes.File,
                _ => null
            };

        private sealed class CacheKey
        {
            public CacheKey(Type type, string? parentKey)
            {
                Type = type;
                ParentKey = parentKey;
            }

            public Type Type { get; }
            public string? ParentKey { get; }

            private bool Equals(CacheKey other) => Type == other.Type && ParentKey == other.ParentKey;

            public override bool Equals(object? obj) =>
                ReferenceEquals(this, obj) || obj is CacheKey other && Equals(other);

            public override int GetHashCode() => HashCode.Combine(Type, ParentKey);
        }
    }
}
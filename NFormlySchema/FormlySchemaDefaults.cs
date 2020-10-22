using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NFormlySchema
{
    public static class FormlySchemaDefaults
    {
        public static string? DefaultInputTypeResolver(Type propertyType, Attribute[] attributes)
        {
            var inputType = attributes.OfType<InputTypeAttribute>().FirstOrDefault()?.Type;
            if (inputType != null)
                return inputType;

            var dataTypeAttribute = attributes.OfType<DataTypeAttribute>().FirstOrDefault();
            if (dataTypeAttribute != null)
            {
                inputType = dataTypeAttribute.DataType switch
                {
                    DataType.Custom => dataTypeAttribute.CustomDataType,
                    DataType.DateTime => "datetime-local",
                    DataType.Date => "date",
                    DataType.Time => "time",
                    DataType.Duration => null,
                    DataType.PhoneNumber => "tel",
                    DataType.Currency => "number",
                    DataType.Text => "text",
                    DataType.Html => null,
                    DataType.MultilineText => null,
                    DataType.EmailAddress => "email",
                    DataType.Password => "password",
                    DataType.Url => "url",
                    DataType.ImageUrl => "url",
                    DataType.CreditCard => "tel",
                    DataType.PostalCode => null,
                    DataType.Upload => "file",
                    _ => null
                };

                if (inputType != null)
                    return inputType;
            }

            var underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (underlyingType == typeof(DateTime))
                return "datetime-local";

            if (underlyingType == typeof(byte)
                || underlyingType == typeof(int)
                || underlyingType == typeof(float)
                || underlyingType == typeof(double)
                || underlyingType == typeof(decimal))
                return "number";

            return null;
        }
    }
}
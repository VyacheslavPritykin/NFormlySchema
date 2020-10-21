NFormlySchema for .NET
=

![build](https://github.com/VyacheslavPritykin/FormlySchema.Generation.CSharp/workflows/build/badge.svg)

NFormlySchema is a .NET library for the Formly Schema generation. 

Formly Schema is used by the [ngx-formly](https://github.com/ngx-formly/ngx-formly) 
project which is a dynamic (JSON powered) form library for Angular.

## Usage
```csharp
using NFormlySchema;
...
var formlyFieldConfigCollection = FormlySchema.FromType<Foo>();
var formlySchema = formlyFieldConfigCollection.ToJson();
```

Below is a .NET type for the Formly's Introduction Example
https://formly.dev/examples/introduction:
```csharp
public class IntroductionExample
{
    private const string AwesomeLabelExpression = @"(model, formState) =>
        formState.awesomeIsForced
          ? ""Too bad, formly is really awesome...""
          : ""Is formly totally awesome? (uncheck this and see what happens)""";

    private const string WhyNotPlaceholderExpression = @"(model, formState) =>
        formState.awesomeIsForced
          ? ""Too bad... It really is awesome! Wasn't that cool?""
          : ""Type in here... I dare you""";

    [Required]
    [Display(Name = "Text", Prompt = "Formly is terrific!")]
    public string Text { get; set; }

    public NestedType Nested { get; set; }

    [ExpressionProperty("templateOptions.disabled", "formState.awesomeIsForced")]
    [ExpressionProperty("templateOptions.label", AwesomeLabelExpression, IsFunction = true)]
    public bool Awesome { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Why Not?", Prompt = "Type in here... I dare you")]
    [HideExpression("model.awesome")]
    [ExpressionProperty("templateOptions.placeholder", WhyNotPlaceholderExpression, IsFunction = true)]
    [ExpressionProperty("templateOptions.disabled", "formState.awesomeIsForced")]
    public string WhyNot { get; set; }

    [FieldType("custom")]
    [DisplayName("Custom inlined")]
    public string Custom { get; set; }
}

public class NestedType
{
    private const string StoryDescriptionExpression = @"(model, formState) =>
        formState.awesomeIsForced ? 'And look! This field magically got focus!' : null";

    [DataType(DataType.MultilineText)]
    [Display(Name = "Some sweet story",
        Prompt = "It allows you to build and maintain your forms with the ease of JavaScript :-)")]
    [ExpressionProperty("focus", "formState.awesomeIsForced")]
    [ExpressionProperty("templateOptions.description", StoryDescriptionExpression, IsFunction = true)]
    public string Story { get; set; }
}
```
   
## .NET type to Formly Schema mapping
##### Key
- `System.Runtime.Serialization.DataMemberAttribute: Name`
- `Newtonsoft.Json.JsonPropertyAttribute: PropertyName`
- Property name
##### Type
- `NFormlySchema.FieldTypeAttribute: Type`
- `System.ComponentModel.DataAnnotationsAttribute: DataType` map:
  ```CSharp
  DataType.Custom => dataAnnotationsAttribute.CustomDataType,
  DataType.DateTime => "input"
  DataType.Date => "input"
  DataType.Time => "input"
  DataType.Duration => "input"
  DataType.PhoneNumber => "input"
  DataType.Currency => "input"
  DataType.Text => "input"
  DataType.Html => "textarea"
  DataType.MultilineText => "textarea"
  DataType.EmailAddress => "input"
  DataType.Password => "input"
  DataType.Url => "input"
  DataType.ImageUrl => "input"
  DataType.CreditCard => "input"
  DataType.PostalCode => "input"
  DataType.Upload => "file"
  ```
- `NFormlySchema.MemberSelectDataAttribute` - "select"
- `Enum` - "select"
- `bool` - "checkbox"
- `ICollection` - "repeat"
  > Note: "repeat" is not a standard Formly field type. See https://formly.dev/examples/advanced/repeating-section


##### DefaultValue
`System.ComponentModel.DefaultValueAttribute: Value`

#### Template options
  ##### Label
  - `System.ComponentModel.DisplayNameAttribute: DisplayName`
  - `System.ComponentModel.DataAnnotation.DisplayAttribute: Name`
  
  ##### Required
  - `System.ComponentModel.DataAnnotations.RequiredAttribute`
  - `Newtonsoft.Json.JsonRequiredAttribute`
  - `System.Runtime.Serialization.DataMemberAttribute: IsRequired`
  
  ##### Placeholder
  `System.ComponentModel.DataAnnotations.DisplayAttribute: Prompt`
  
  ##### Description
  - `System.ComponentModel.DescriptionAttribute: Description`
  - `System.ComponentModel.DataAnnotations.DisplayAttribute: Description`
  
  ##### Min & Max
  `System.ComponentModel.DataAnnotations.RangeAttribute: (Minimum, Maximum)`
  
  ##### MinLength
  `System.ComponentModel.DataAnnotations.MinLengthAttribute: Length`
  
  ##### ManLength
  `System.ComponentModel.DataAnnotations.MaxLengthAttribute: Length`
  
  ##### Pattern
  `System.ComponentModel.DataAnnotations.RegularExpressionAttribute: Pattern`
  
  ##### Disabled
  `System.ComponentModel.ReadOnlyAttribute: IsReadOnly`
  
  ##### Options
  For `Enum`:
  - Label:
    - `System.ComponentModel.DataAnnotations.DisplayAttribute: Name`
    - Enum field name
    
  - Value:
    - `Newtonsoft.Json.JsonPropertyAttribute: PropertyName`
    - `System.Runtime.Serialization.DataMemberAttribute: Name`
    - Enum field name
    
  - Group:
    - `NFormlySchema.EnumValueGroupAttribute: Name`
  
  For other simple types:
  `NFormlySchema.MemberSelectDataAttribute`. The majority of its code was kindly taken from the
  `Xunit.MemberDataAttribute` and you may expect similar API. The attribute provides a data
  source for "select" fields,  with the data coming from one of the following sources:
   1. A static property
   2. A static field
   3. A static method (with parameters)
   
   The member must return something compatible with IEnumerable&lt;object&gt;.
   
   The `label`, `value` and `group` values are taken either from the corresponding
   property names, or from the properties which names are specified in the
   `NFormlySchema.MemberSelectDataAttribute (LabelProp, ValueProp, GroupProp)`
  
  ##### Multiple
  - `NFormlySchema.MultipleAttribute: Multiple`
  - `System.FlagsAttribute`
  
  ##### Rows
  `NFormlySchema.RowsAttribute: Count`
  
  ##### Type ("templateOptions.type")
  - `NFormlySchema.InputTypeAttribute: Type`
  - `System.ComponentModel.DataAnnotations.DataTypeAttribute: DataType` map:
     ```CSharp
     DataType.Custom => dataTypeAttribute.CustomDataType
     DataType.DateTime => "datetime-local"
     DataType.Date => "date"
     DataType.Time => "time"
     DataType.Duration => null
     DataType.PhoneNumber => "tel"
     DataType.Currency => "number"
     DataType.Text => "text"
     DataType.Html => null
     DataType.MultilineText => null
     DataType.EmailAddress => "email"
     DataType.Password => "password"
     DataType.Url => "url"
     DataType.ImageUrl => "url"
     DataType.CreditCard => "tel"
     DataType.PostalCode => null
     DataType.Upload => "file" // see https://formly.dev/examples/other/input-file
     ```
  - `DateTime` - "datetime-local"
  - `byte`, `int`, `float`, `double`, `decimal` and nullable counterparts - "number"
    
  ##### AddonLeft
  `NFormlySchema.AddonLeftAttribute: (Class, Text)`
  
  ##### AddonRight
  `NFormlySchema.AddonRightAttribute: (Class, Text)`

##### HideExpression
`NFormlySchema.HideExpressionAttribute: (Expression, IsFunction)`

##### ExpressionProperties
`NFormlySchema.ExpressionPropertyAttribute: (Property, Expression, IsFunction)`

##### Validation
Messages:
  - `System.ComponentModel.DataAnnotations.RequiredAttribute: (ErrorMessage)`
  - `System.ComponentModel.DataAnnotations.RegularExpressionAttribute: (ErrorMessage)`
  - `System.ComponentModel.DataAnnotations.MaxLengthAttribute: (ErrorMessage)`
  - `System.ComponentModel.DataAnnotations.MinLengthAttribute: (ErrorMessage)`
  - `NFormlySchema.ValidationMessageAttribute: (Name, MessageExpression, IsFunction)`
  
##### Validators & AsyncValidators
`NFormlySchema.ValidatorsAttribute: (Validators, IsFunction, IsAsync)`
 
##### Wrappers
`NFormlySchema.WrapperAttribute: Name`

##### ClassName
`NFormlySchema.ClassNameAttribute: ClassName`

##### FieldGroup
`NFormlySchema.FieldGroupAttribute` over a complex property type.
> If FieldGroupAttribute is not set over the complex property type, then the
> generated Formly Schema is flat and nested properties are represented by complex
> keys like: "RootProperty.SubRootProperty.ChildProperty

##### FieldGroupClassName
`NFormlySchema.FieldGroupAttribute: ClassName`

##### FieldArray
Only `ICollection` of simple types is supported for now.

### Field order
The `GetProperties` method does not guarantee that it will return properties in declaration order, so you can
use the `System.ComponentModel.DataAnnotations.DisplayAttribute: Order` to explicitly specify the order.
> When an order is not specified, NFormlySchema assumes that it is 10000.
> This value allows for explicitly-ordered fields to be displayed before 
> and after the fields that don't specify an order.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFormlySchema.Generation;
using NFormlySchema.Samples.Shared;
using Xunit;
using Xunit.Abstractions;

namespace NFormlySchema.UnitTests
{
    public class FormlySchemaTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public FormlySchemaTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void EmptyType()
        {
            // act
            var schema = FormlySchema.FromType<EmptyType>();

            // assert
            schema.Should().BeEmpty();
            _testOutputHelper.WriteLine(schema.ToJson());
        }

        [Fact]
        public void GodTest()
        {
            // act
            var schema = FormlySchema.FromType<Foo>();

            // assert
            schema.Should().BeEquivalentTo(
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.FirstName),
                    Type = "input",
                    DefaultValue = "Dan"
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.IsMarried),
                    Type = "checkbox"
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.Dob),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "datetime-local"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.DataMemberNotSet),
                    Type = "input"
                },
                new FormlyFieldConfig
                {
                    Key = "DataMemberProp",
                    Type = "input"
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.JsonPropertyNotSet),
                    Type = "input"
                },
                new FormlyFieldConfig
                {
                    Key = "JsonPropertyProp",
                    Type = "input"
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.DisplayNameNotSet),
                    Type = "input"
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.DisplayName),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Label = nameof(Foo.DisplayName) + "Prop"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.DisplayNotSet),
                    Type = "input"
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.Display),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Label = nameof(Foo.Display) + "Prop"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.Required),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Required = true
                    },
                    Validation = new Validation
                    {
                        Messages = new MessageDictionary { { "required", "Custom" } }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.JsonRequired),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Required = true
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.DataMemberRequired),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Required = true
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.HideExpression),
                    Type = "input",
                    HideExpression = "some expression"
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.HideExpressionFunction),
                    Type = "input",
                    HideExpression = new JRaw("(model) => !this.model.firstName")
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.Placeholder),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Placeholder = "PlaceholderValue"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.ExpressionProperty),
                    Type = "input",
                    ExpressionProperties = new ExpressionPropertyDictionary
                    {
                        {"templateOptions.disabled", "!model.text"},
                        {"a", "b"}
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.ExpressionPropertyFunction),
                    Type = "input",
                    ExpressionProperties = new ExpressionPropertyDictionary
                    {
                        {"a", new JRaw("() => b")}
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.RangeInt),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Min = 5,
                        Max = 10,
                        Type = "number"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.RangeDouble),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Min = 5.5,
                        Max = 10.5,
                        Type = "number"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.MaxLength),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        MaxLength = 5,
                    },
                    Validation = new Validation
                    {
                        Messages = new MessageDictionary { { "maxLength", "Custom" } }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.MinLength),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        MinLength = 5
                    },
                    Validation = new Validation
                    {
                        Messages = new MessageDictionary { { "minLength", "Custom" } }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.RegExp),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Pattern = "[a-z]"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.ReadOnly),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Disabled = true,
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.Description),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Description = "Custom",
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.DisplayDescription),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Description = "Custom",
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.Select),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "Red", value = "Red"},
                            new {label = "Green", value = "Green"},
                            new {label = "Blue", value = "Blue"}
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.SelectWithDisplayNames),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "Midnight Green", value = "GreenColor"},
                            new {label = "Deep Blue", value = "BlueColor"}
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.CustomWrapper),
                    Type = "input",
                    Wrappers = new WrapperCollection { "panel1", "panel2" }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.FlagsEnumMultiselect),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Multiple = true,
                        Options = new OptionCollection
                        {
                            new {label = "None", value = "None"},
                            new {label = "Create", value = "Create"},
                            new {label = "Read", value = "Read"},
                            new {label = "Update", value = "Update"},
                            new {label = "Delete", value = "Delete"},
                            new {label = "All", value = "All"},
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.FlagsEnumMultiselectOverriden),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "None", value = "None"},
                            new {label = "Create", value = "Create"},
                            new {label = "Read", value = "Read"},
                            new {label = "Update", value = "Update"},
                            new {label = "Delete", value = "Delete"},
                            new {label = "All", value = "All"},
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.FieldType),
                    Type = "custom"
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.InputType),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "custom"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.TextAreaRows),
                    Type = "textarea",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Rows = 6
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.CustomValidators),
                    Type = "input",
                    Validators = new Validators
                    {
                        Validation = new ValidatorsValidationCollection
                        {
                            "ip", "ip2", "ip3"
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.CustomAsyncValidators),
                    Type = "input",
                    AsyncValidators = new Validators
                    {
                        Validation = new ValidatorsValidationCollection
                        {
                            "ipAsync", "ip2Async"
                        }
                    },
                    Validators = new Validators
                    {
                        Validation = new ValidatorsValidationCollection
                        {
                            "ip3"
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.CustomFunctionValidators),
                    Type = "input",
                    AsyncValidators = new Validators
                    {
                        Validation = new ValidatorsValidationCollection
                        {
                            new JRaw("ipAsync"), new JRaw("ip2Async")
                        }
                    },
                    Validators = new Validators
                    {
                        Validation = new ValidatorsValidationCollection
                        {
                            new JRaw("ip3"),
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.Marvel),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "Iron Man", value = "IronMan", group = "Male"},
                            new {label = "Captain America", value = "CaptainAmerica", group = "Male"},
                            new {label = "Black Widow", value = "BlackWidow", group = "Female"},
                            new {label = "Captain Marvel", value = "CaptainMarvel"},
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.RightAddon),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        AddonRight = new Addon
                        {
                            Class = "right class",
                            Text = "right text",
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.LeftAddon),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        AddonLeft = new Addon
                        {
                            Class = "left class",
                            Text = "left text",
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Foo.WithClassName),
                    Type = "input",
                    ClassName = "custom-class",
                });

            LogSchema(schema);
        }

        private void LogSchema(FormlyFieldConfigCollection schema)
        {
            _testOutputHelper.WriteLine(schema.ToJson());
        }

        [Fact]
        public void DataTypeAttribute()
        {
            // act
            var schema = FormlySchema.FromType<VariousInputTypes>();

            // assert
            schema.Should().BeEquivalentTo(
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.DateTime),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "datetime-local"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Date),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "date"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Time),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "time"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Duration),
                    Type = "input",
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.PhoneNumber),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "tel"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Currency),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "number"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Text),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "text"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Html),
                    Type = "textarea",
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.MultilineText),
                    Type = "textarea",
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.EmailAddress),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "email"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Password),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "password"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Url),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "url"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.ImageUrl),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "url"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.CreditCard),
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "tel"
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.PostalCode),
                    Type = "input",
                },
                new FormlyFieldConfig
                {
                    Key = nameof(VariousInputTypes.Upload),
                    Type = "file",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Type = "file"
                    }
                }
            );

            _testOutputHelper.WriteLine(schema.ToJson());
        }



        [Fact]
        public void CustomObjectFieldArray()
        {
            // act
            var schema = FormlySchema.FromType<CustomObjectFieldArrayRoot>();

            var expected = new FormlyFieldConfigCollection
            {
                new FormlyFieldConfig{
                    Key= "PurchaseOrderRef",
                    Type = "input",
                    TemplateOptions= new FormlyTemplateOptions{
                        Label = "Purchase Order Ref"
                    }
                },
                new FormlyFieldConfig{
                    Key= "PurchaseTimestamp",
                    Type = "input",
                    TemplateOptions = new FormlyTemplateOptions{
                        Type = "datetime-local",
                        Label = "Purchase Timestamp"
                    }
                },
                new FormlyFieldConfig{
                    Key= "OrderedItems",
                    Type = "repeat",
                    Wrappers = new WrapperCollection{
                     "panel"
                    },
                    TemplateOptions = new FormlyTemplateOptions{
                        Label = "Ordered Items"
                    },
                    FieldArray = new FormlyFieldConfig{
                        FieldGroup = new FormlyFieldConfigCollection{
                            new FormlyFieldConfig{
                            Key = "ItemKey",
                            Type = "input",
                            TemplateOptions = new FormlyTemplateOptions{
                                    Label = "Item Key"
                                }
                            },
                        new FormlyFieldConfig{
                            Key = "ItemPrice",
                            Type = "input",
                            TemplateOptions = new FormlyTemplateOptions{
                                    Type = "number",
                                    Label = "Item Price"
                                }
                            },
                        new FormlyFieldConfig{
                            Key = "ItemQuantity",
                            Type = "input",
                            TemplateOptions = new FormlyTemplateOptions{
                                    Type = "number",
                                    Label = "Item Quantity"
                                }
                            }
                        }
                    }
                }
            };

            _testOutputHelper.WriteLine($"Expected == Actual: {expected.ToJson() == schema.ToJson()}");

            _testOutputHelper.WriteLine($"Expected:\n {expected.ToJson()}");
            _testOutputHelper.WriteLine($"Actual:\n {schema.ToJson()}");

            // assert
            schema.Should().BeEquivalentTo(expected);

        }
    }


    internal class EmptyType
    {
    }

    internal enum Color
    {
        Red,
        Green,
        Blue
    }

    internal enum MarketingColor
    {
        [Display(Name = "Midnight Green")]
        [JsonProperty(PropertyName = "GreenColor")]
        Green,

        [Display(Name = "Deep Blue")]
        [DataMember(Name = "BlueColor")]
        Blue
    }

    [Flags]
    internal enum Access
    {
        None = 0,
        Create = 1,
        Read = 2,
        Update = 4,
        Delete = 8,
        All = Create | Read | Update | Delete
    }

    internal enum Marvel
    {
        [Display(Name = "Iron Man")]
        [EnumValueGroup("Male")]
        IronMan,

        [Display(Name = "Captain America")]
        [EnumValueGroup("Male")]
        CaptainAmerica,

        [Display(Name = "Black Widow")]
        [EnumValueGroup("Female")]
        BlackWidow,

        [Display(Name = "Captain Marvel")]
        CaptainMarvel
    }

    internal class Foo
    {
        [DefaultValue("Dan")]
        public string FirstName { get; set; }

        public static string Static { get; set; }

        public bool? IsMarried { get; set; }

        public DateTime? Dob { get; set; }

        [DataMember]
        public string DataMemberNotSet { get; set; }

        [DataMember(Name = "DataMemberProp")]
        public string DataMember { get; set; }

        [JsonProperty]
        public string JsonPropertyNotSet { get; set; }

        [JsonProperty("JsonPropertyProp")]
        public string JsonProperty { get; set; }

        [DisplayName]
        public string DisplayNameNotSet { get; set; }

        [DisplayName("DisplayNameProp")]
        public string DisplayName { get; set; }

        [Display]
        public string DisplayNotSet { get; set; }

        [Display(Name = "DisplayProp")]
        public string Display { get; set; }

        [Required(ErrorMessage = "Custom")]
        public string Required { get; set; }

        [JsonRequired]
        public string JsonRequired { get; set; }

        [DataMember(IsRequired = true)]
        public string DataMemberRequired { get; set; }

        [HideExpression("some expression")]
        public string HideExpression { get; set; }

        [HideExpression("(model) => !this.model.firstName", IsFunction = true)]
        public string HideExpressionFunction { get; set; }

        [Display(Prompt = "PlaceholderValue")]
        public string Placeholder { get; set; }

        [ExpressionProperty("templateOptions.disabled", "!model.text")]
        [ExpressionProperty("a", "b")]
        public string ExpressionProperty { get; set; }

        [ExpressionProperty("a", "() => b", IsFunction = true)]
        public string ExpressionPropertyFunction { get; set; }

        [Range(5, 10)]
        public int RangeInt { get; set; }

        [Range(5.5, 10.5)]
        public int RangeDouble { get; set; }

        [MaxLength(5, ErrorMessage = "Custom")]
        public string MaxLength { get; set; }

        [MinLength(5, ErrorMessage = "Custom")]
        public string MinLength { get; set; }

        [RegularExpression("[a-z]")]
        public string RegExp { get; set; }

        [ReadOnly(true)]
        public string ReadOnly { get; set; }

        [Description("Custom")]
        public string Description { get; set; }

        [Display(Description = "Custom")]
        public string DisplayDescription { get; set; }

        public Color Select { get; set; }

        public MarketingColor SelectWithDisplayNames { get; set; }

        [Wrapper("panel1")]
        [Wrapper("panel2")]
        public string CustomWrapper { get; set; }

        public Access FlagsEnumMultiselect { get; set; }

        [Multiple(false)]
        public Access FlagsEnumMultiselectOverriden { get; set; }

        [IgnoreDataMember]
        public string IgnoreDataMember { get; set; }

        [JsonIgnore]
        public string NewtonsoftJsonIgnore { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string JsonIgnore { get; set; }

        [FieldType("custom")]
        public string FieldType { get; set; }

        [InputType("custom")]
        public string InputType { get; set; }

        [DataType(DataType.MultilineText)]
        [Rows(6)]
        public string TextAreaRows { get; set; }

        [Validators("ip", "ip2")]
        [Validators("ip3")]
        public string CustomValidators { get; set; }

        [Validators("ipAsync", "ip2Async", IsAsync = true)]
        [Validators("ip3")]
        public string CustomAsyncValidators { get; set; }

        [Validators("Ip1", "Ip2", IsAsync = true, IsFunction = true)]
        [Validators("Ip3", IsFunction = true)]
        public string CustomFunctionValidators { get; set; }

        public Marvel Marvel { get; set; }

        [AddonRight(Text = "right text", Class = "right class")]
        public string RightAddon { get; set; }

        [AddonLeft(Text = "left text", Class = "left class")]
        public string LeftAddon { get; set; }

        [ClassName("custom-class")]
        public string WithClassName { get; set; }

        //public string CustomMessage { get; set; }
    }

    internal class VariousInputTypes
    {
        [DataType(DataType.DateTime)]
        public string DateTime { get; set; }

        [DataType(DataType.Date)]
        public string Date { get; set; }

        [DataType(DataType.Time)]
        public string Time { get; set; }

        [DataType(DataType.Duration)]
        public string Duration { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Currency)]
        public string Currency { get; set; }

        [DataType(DataType.Text)]
        public string Text { get; set; }

        [DataType(DataType.Html)]
        public string Html { get; set; }

        [DataType(DataType.MultilineText)]
        public string MultilineText { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataType(DataType.CreditCard)]
        public string CreditCard { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [DataType(DataType.Upload)]
        public string Upload { get; set; }
    }
}
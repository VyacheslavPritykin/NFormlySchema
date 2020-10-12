using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FormlySchema.Generation.CSharp.UnitTests
{
    public class MemberSelectDataTests
    {
        [Fact]
        public void Test()
        {
            // act
            var schema = Formly.Generate<MemberSelectData>();

            // assert
            schema.Should().BeEquivalentTo(
                new FormlyFieldConfig
                {
                    Key = nameof(MemberSelectData.SameTypeProperty),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "label1", value = "value1", group = "group1"},
                            new {label = "label2", value = "value2"},
                            new {label = "label3", value = "value3", group = "group3"},
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(MemberSelectData.SameTypeMethod),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "label1", value = "value1", group = "group1"},
                            new {label = "label2", value = "value2", group = "group2"},
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(MemberSelectData.OtherTypeProperty),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "label1", value = "value1", group = "group1"},
                            new {label = "label2", value = "value2"},
                            new {label = "label3", value = "value3", group = "group3"},
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(MemberSelectData.OtherTypeMethod),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "label1", value = "value1", group = "group1"},
                            new {label = "label2", value = "value2", group = "group2"},
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = nameof(MemberSelectData.CustomProperties),
                    Type = "select",
                    TemplateOptions = new FormlyTemplateOptions
                    {
                        Options = new OptionCollection
                        {
                            new {label = "label1", value = "value1", group = "group1"},
                            new {label = "label2", value = "value2", group = "group2"},
                        }
                    }
                }

            );
        }
    }

    public class MemberSelectData
    {
        [MemberSelectData(nameof(SameTypePropertyData))]
        public string SameTypeProperty { get; set; }
        
        [MemberSelectData(nameof(SameTypeMethodData), 2)]
        public string SameTypeMethod { get; set; }

        [MemberSelectData(nameof(OtherTypeData.OtherTypePropertyData), MemberType = typeof(OtherTypeData))]
        public string OtherTypeProperty { get; set; }
        
        [MemberSelectData(nameof(OtherTypeData.OtherTypeMethodData), 2, MemberType = typeof(OtherTypeData))]
        public string OtherTypeMethod { get; set; }
        
        [MemberSelectData(nameof(CustomPropertiesData), GroupProp = "CustomGroup", ValueProp = "CustomValue", LabelProp = "CustomLabel")]
        public string CustomProperties { get; set; }

        public static IEnumerable<CustomData> CustomPropertiesData => new[]
        {
            new CustomData
            {
                CustomGroup = "group1",
                CustomValue = "value1",
                CustomLabel = "label1",
            },
            new CustomData
            {
                CustomGroup = "group2",
                CustomValue = "value2",
                CustomLabel = "label2",
            }
        };
    
        public static IEnumerable<Data> SameTypePropertyData => new[]
        {
            new Data {Label = "label1", Value = "value1", Group = "group1"},
            new Data {Label = "label2", Value = "value2"},
            new Data {Label = "label3", Value = "value3", Group = "group3"},
        };

        public static IEnumerable<Data> SameTypeMethodData(int count) =>
            Enumerable.Range(0, count).Select(i => new Data
            {
                Value = "value" + (i + 1),
                Label = "label" + (i + 1),
                Group = "group" + (i + 1)
            });
    }

    public class CustomData
    {
        public string CustomGroup { get; set; }
        
        public string CustomValue { get; set; }
        
        public string CustomLabel { get; set; }
    }

    public class Data
    {
        public string Label { get; set; }

        public string Value { get; set; }

        public string Group { get; set; }
    }

    public class OtherTypeData
    {
        public static IEnumerable<Data> OtherTypePropertyData => new[]
        {
            new Data {Label = "label1", Value = "value1", Group = "group1"},
            new Data {Label = "label2", Value = "value2"},
            new Data {Label = "label3", Value = "value3", Group = "group3"},
        };

        public static IEnumerable<Data> OtherTypeMethodData(int count) =>
            Enumerable.Range(0, count).Select(i => new Data
            {
                Value = "value" + (i + 1),
                Label = "label" + (i + 1),
                Group = "group" + (i + 1)
            });
    }
}
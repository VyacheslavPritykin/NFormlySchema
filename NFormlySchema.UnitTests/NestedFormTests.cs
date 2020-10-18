using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace NFormlySchema.UnitTests
{
    public class NestedFormTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NestedFormTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void NestedForms()
        {
            // act
            var schema = FormlySchema.FromType<Parent>();

            // assert
            _testOutputHelper.WriteLine(schema.ToJson());
            schema.Should().BeEquivalentTo(
                new FormlyFieldConfig
                {
                    Key = nameof(Parent.EmptyChild),
                    FieldGroupClassName = "custom-class",
                    FieldGroup = FormlyFieldConfigCollection.Empty
                },
                new FormlyFieldConfig
                {
                    Key = nameof(Parent.ChildWithData),
                    FieldGroup = new FormlyFieldConfigCollection
                    {
                        new FormlyFieldConfig
                        {
                            Key = nameof(Parent.ChildWithData.FirstName),
                            Type = "input"
                        }
                    }
                },
                new FormlyFieldConfig
                {
                    Key = $"{nameof(Parent.InlineChildWithData)}.{nameof(Parent.InlineChildWithData.ZipCode)}",
                    Type = "input"
                },
                new FormlyFieldConfig
                {
                    Key = $"{nameof(InlineChildWithNestedData)}.{nameof(InlineChildWithNestedData.Data)}",
                    Type = "input"
                },
                new FormlyFieldConfig
                {
                    Key = $"{nameof(InlineChildWithNestedData)}.{nameof(InlineChildWithNestedData.InlineChildWithData)}.{nameof(InlineChildWithNestedData.InlineChildWithData.ZipCode)}",
                    Type = "input"
                });
        }
    }

    internal class Parent
    {
        [FieldGroup(ClassName = "custom-class")]
        public EmptyChild EmptyChild { get; set; }

        [FieldGroup]
        public ChildWithData ChildWithData { get; set; }

        public InlineChildWithData InlineChildWithData { get; set; }

        public InlineChildWithNestedData InlineChildWithNestedData { get; set; }
    }

    internal class InlineChildWithData
    {
        public string ZipCode { get; set; }
    }

    internal class InlineChildWithNestedData
    {
        public string Data { get; set; }

        public InlineChildWithData InlineChildWithData { get; set; }
    }

    internal class EmptyChild
    {
    }

    internal class ChildWithData
    {
        public string FirstName { get; set; }
    }
}
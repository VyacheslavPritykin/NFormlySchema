using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace FormlySchema.Generation.CSharp.UnitTests
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
            var schema = Formly.Generate<Parent>();

            // assert
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
                });
            _testOutputHelper.WriteLine(schema.ToJson());
        }
    }

    internal class Parent
    {
        [FieldGroupClassName("custom-class")]
        public EmptyChild EmptyChild { get; set; }

        public ChildWithData ChildWithData { get; set; }
    }

    internal class EmptyChild
    {
    }

    internal class ChildWithData
    {
        public string FirstName { get; set; }
    }
}
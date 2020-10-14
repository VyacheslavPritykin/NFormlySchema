using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace FormlySchema.Generation.CSharp.UnitTests
{
    public class NestedCollectionsTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NestedCollectionsTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Arrays()
        {
            // act
            var schema = Formly.Generate<CollectionRoot>();

            // assert
            _testOutputHelper.WriteLine(schema.ToJson());
            schema.Should().BeEquivalentTo(
                new FormlyFieldConfig
                {
                    Key = nameof(CollectionRoot.Names),
                    Type = "repeat",
                    FieldArray = new FormlyFieldConfig
                    {
                        Type = "input"
                    }
                });
        }
    }

    public class CollectionRoot
    {
        public string[] Names { get; set; }
    }
}
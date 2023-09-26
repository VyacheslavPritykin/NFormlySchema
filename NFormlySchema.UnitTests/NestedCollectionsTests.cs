using FluentAssertions;
using NFormlySchema.Generation;
using Xunit;
using Xunit.Abstractions;

namespace NFormlySchema.UnitTests
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
            var schema = FormlySchema.FromType<CollectionRoot>();

            // assert
            _testOutputHelper.WriteLine(schema.ToJson());
            schema.Should().BeEquivalentTo(new[]
            {
                new FormlyFieldConfig
                {
                    Key = nameof(CollectionRoot.Names),
                    Type = "repeat",
                    FieldArray = new FormlyFieldConfig
                    {
                        Type = "input"
                    }
                }
            });
        }
    }

    public class CollectionRoot
    {
        public string[] Names { get; set; }
    }
}
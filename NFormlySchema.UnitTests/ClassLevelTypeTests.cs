using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using NFormlySchema.Generation;
using Xunit;
using Xunit.Abstractions;

namespace NFormlySchema.UnitTests
{
    public class ClassLevelTypeTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ClassLevelTypeTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public void Test()
        {
            // act
            var schema = FormlySchema.FromType<ClassLevelType>();

            // assert
            _testOutputHelper.WriteLine(schema.ToJson());

            schema.Should().BeEquivalentTo(
                new FormlyFieldConfig
                {
                    Type = "tabs",
                    FieldGroup = new FormlyFieldConfigCollection
                    {
                        new FormlyFieldConfig
                        {
                            Key = nameof(ClassLevelType.FirstName),
                            Type = "input"
                        },
                        new FormlyFieldConfig
                        {
                            Key = nameof(ClassLevelType.Complex),
                            FieldGroup = new FormlyFieldConfigCollection
                            {
                                new FormlyFieldConfig
                                {
                                    Key = nameof(ComplexType.Address),
                                    Type = "input"
                                }
                            }
                        }
                    }
                });
        }
    }

    [FieldType("tabs")]
    public class ClassLevelType
    {
        public string FirstName { get; set; }

        [FieldGroup]
        public ComplexType Complex { get; set; }
    }

    public class ComplexType
    {
        public string Address { get; set; }
    }
}
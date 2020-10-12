using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAssertions;
using FormlySchema.Generation.CSharp.Internal;
using Xunit;

namespace FormlySchema.Generation.CSharp.UnitTests
{
    public class UtilsTests
    {
        [Theory]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(DateTime), false)]
        [InlineData(typeof(DateTimeOffset), false)]
        [InlineData(typeof(TestEnum), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(Guid), false)]
        [InlineData(typeof(TestClass), true)]
        [InlineData(typeof(TestStruct), true)]
        [InlineData(typeof(object), true)]
        [InlineData(typeof(Collection<string>), false)]
        [InlineData(typeof(List<string>), false)]
        [InlineData(typeof(string[]), false)]
        public void IsFormGroup(Type type, bool expectedResult)
        {
            // act
            var isFormGroup = TypeUtils.IsFormGroup(type);

            // assert
            isFormGroup.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(int?), false)]
        [InlineData(typeof(DateTime?), false)]
        [InlineData(typeof(DateTimeOffset?), false)]
        [InlineData(typeof(TestEnum?), false)]
        [InlineData(typeof(Guid?), false)]
        [InlineData(typeof(TestStruct?), true)]
        public void IsFormGroupWithNullableTypes(Type type, bool expectedResult)
        {
            // act
            var isFormGroup = TypeUtils.IsFormGroup(type);

            // assert
            isFormGroup.Should().Be(expectedResult);
        }
    }

    internal enum TestEnum
    {
    }

    internal class TestClass
    {
    }

    internal struct TestStruct
    {
    }
}
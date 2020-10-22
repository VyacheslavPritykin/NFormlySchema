using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAssertions;
using NFormlySchema.Internal;
using Xunit;

namespace NFormlySchema.UnitTests
{
    public class UtilsTests
    {
        [Theory]
        [InlineData(typeof(int), true)]
        [InlineData(typeof(DateTime), true)]
        [InlineData(typeof(DateTimeOffset), true)]
        [InlineData(typeof(TestEnum), true)]
        [InlineData(typeof(string), true)]
        [InlineData(typeof(Guid), true)]
        [InlineData(typeof(TimeSpan), true)]
        [InlineData(typeof(TestClass), false)]
        [InlineData(typeof(TestStruct), false)]
        [InlineData(typeof(object), false)]
        public void IsSimple(Type type, bool expectedResult)
        {
            // act
            var isSimple = type.IsSimple();

            // assert
            isSimple.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(int?), true)]
        [InlineData(typeof(DateTime?), true)]
        [InlineData(typeof(DateTimeOffset?), true)]
        [InlineData(typeof(TimeSpan?), true)]
        [InlineData(typeof(TestEnum?), true)]
        [InlineData(typeof(Guid?), true)]
        [InlineData(typeof(TestStruct?), false)]
        public void IsSimpleWithNullableTypes(Type type, bool expectedResult)
        {
            // act
            var isSimple = type.IsSimple();

            // assert
            isSimple.Should().Be(expectedResult);
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
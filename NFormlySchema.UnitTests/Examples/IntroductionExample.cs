using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace NFormlySchema.UnitTests.Examples
{
    /// <summary>
    /// See https://formly.dev/examples/introduction
    /// </summary>
    public class IntroductionExampleTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public IntroductionExampleTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test()
        {
            // arrange
            var expectedFormlySchema = @"[
  {
    ""key"": ""Text"",
    ""type"": ""input"",
    ""templateOptions"": {
      ""label"": ""Text"",
      ""placeholder"": ""Formly is terrific!"",
      ""required"": true
    }
  },
  {
    ""key"": ""Nested.Story"",
    ""type"": ""textarea"",
    ""templateOptions"": {
      ""label"": ""Some sweet story"",
      ""placeholder"": ""It allows you to build and maintain your forms with the ease of JavaScript :-)""
    },
    ""expressionProperties"": {
      ""focus"": ""formState.awesomeIsForced"",
      ""templateOptions.description"": (model, formState) =>
        formState.awesomeIsForced ? 'And look! This field magically got focus!' : null
    }
  },
  {
    ""key"": ""Awesome"",
    ""type"": ""checkbox"",
    ""expressionProperties"": {
      ""templateOptions.disabled"": ""formState.awesomeIsForced"",
      ""templateOptions.label"": (model, formState) =>
        formState.awesomeIsForced
          ? ""Too bad, formly is really awesome...""
          : ""Is formly totally awesome? (uncheck this and see what happens)""
    }
  },
  {
    ""key"": ""WhyNot"",
    ""type"": ""textarea"",
    ""hideExpression"": ""model.awesome"",
    ""templateOptions"": {
      ""label"": ""Why Not?"",
      ""placeholder"": ""Type in here... I dare you""
    },
    ""expressionProperties"": {
      ""templateOptions.placeholder"": (model, formState) =>
        formState.awesomeIsForced
          ? ""Too bad... It really is awesome! Wasn't that cool?""
          : ""Type in here... I dare you"",
      ""templateOptions.disabled"": ""formState.awesomeIsForced""
    }
  },
  {
    ""key"": ""Custom"",
    ""type"": ""custom"",
    ""templateOptions"": {
      ""label"": ""Custom inlined""
    }
  }
]";

            // act
            var formlySchema = FormlySchema.FromType<IntroductionExample>();
            var formlySchemaJson = formlySchema.ToJson();
 
            // assert
            _testOutputHelper.WriteLine(formlySchemaJson);
            formlySchemaJson.Should().Be(expectedFormlySchema);
        }
    }

    internal class IntroductionExample
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

    internal class NestedType
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
}
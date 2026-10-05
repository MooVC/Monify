namespace Monify.Semantics.INamedTypeSymbolExtensionsTests;

using Monify.Semantics;

public sealed class WhenHasJsonConverterIsCalled
{
    private const string Declarations = """
        using System;
        using System.Text.Json.Serialization;
        using ConverterAttribute = System.Text.Json.Serialization.JsonConverterAttribute;

        namespace Sample
        {
            [JsonConverter(typeof(object))]
            public sealed class Decorated { }

            [ConverterAttribute(typeof(object))]
            public sealed class Aliased { }

            [CustomJsonConverter]
            public sealed class Derived { }

            public sealed class Plain { }

            public sealed class CustomJsonConverterAttribute : JsonConverterAttribute { }
        }

        namespace Other
        {
            [AttributeUsage(AttributeTargets.Class)]
            public sealed class JsonConverterAttribute : Attribute { }

            [JsonConverter]
            public sealed class Decorated { }
        }
        """;

    [Theory]
    [InlineData("Sample.Decorated")]
    [InlineData("Sample.Aliased")]
    [InlineData("Sample.Derived")]
    public void GivenJsonConverterAttributeThenTrueIsReturned(string typeName)
    {
        // Arrange
        var compilation = JsonCompilation.Create(Declarations);
        var subject = compilation.GetTypeByMetadataName(typeName).ShouldNotBeNull();

        // Act
        bool result = subject.HasJsonConverter();

        // Assert
        result.ShouldBeTrue();
    }

    [Theory]
    [InlineData("Sample.Plain")]
    [InlineData("Other.Decorated")]
    public void GivenNoJsonConverterAttributeThenFalseIsReturned(string typeName)
    {
        // Arrange
        var compilation = JsonCompilation.Create(Declarations);
        var subject = compilation.GetTypeByMetadataName(typeName).ShouldNotBeNull();

        // Act
        bool result = subject.HasJsonConverter();

        // Assert
        result.ShouldBeFalse();
    }
}
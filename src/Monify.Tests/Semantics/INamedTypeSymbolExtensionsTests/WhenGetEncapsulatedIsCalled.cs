namespace Monify.Semantics.INamedTypeSymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Monify.Model;
using Monify.Semantics;

public sealed class WhenGetEncapsulatedIsCalled
{
    [Fact]
    public void GivenEncapsulatedStringThenBuiltInBinaryOperatorsAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(string))]
            public sealed partial class Wrapper
            {
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].BinaryOperators.Length.ShouldBe(3);

        encapsulated[0].BinaryOperators.ShouldContain(@operator => @operator.Operator == "op_Addition"
            && @operator.IsLeftSubject
            && @operator.IsRightSubject
            && @operator.IsReturnSubject
            && @operator.Symbol == "+");

        encapsulated[0].BinaryOperators.ShouldContain(@operator => @operator.Operator == "op_Addition"
            && @operator.IsLeftSubject
            && !@operator.IsRightSubject
            && @operator.IsReturnSubject
            && @operator.Right == "object"
            && @operator.Symbol == "+");

        encapsulated[0].BinaryOperators.ShouldContain(@operator => @operator.Operator == "op_Addition"
            && !@operator.IsLeftSubject
            && @operator.IsRightSubject
            && @operator.IsReturnSubject
            && @operator.Left == "object"
            && @operator.Symbol == "+");
    }

    [Fact]
    public void GivenEncapsulatedStringThenInterfacesAreCaptured()
    {
        // Arrange
        Encapsulated encapsulated = GetEncapsulatedFor("string");

        // Act
        ImmutableArray<string> interfaces = encapsulated.Interfaces;

        // Assert
        interfaces.ShouldContain("global::System.IComparable");
        interfaces.ShouldContain("global::System.IComparable<string>");
        interfaces.ShouldNotContain("global::System.IConvertible");
        interfaces.ShouldNotContain("global::System.IEquatable<string>");
        interfaces.ShouldNotContain("global::System.IParsable<string>");
        interfaces.ShouldNotContain("global::System.ISpanParsable<string>");
        encapsulated.Methods.ShouldContain(method => method.Name == "CompareTo"
            && method.Parameters.Length == 1
            && method.Parameters[0].Type == "string");
    }

    [Fact]
    public void GivenEncapsulatedSortableSpecialTypesThenInterfacesAreCaptured()
    {
        // Arrange
        (string Type, string InterfaceType)[] cases =
        {
            (Type: "byte", InterfaceType: "byte"),
            (Type: "decimal", InterfaceType: "decimal"),
            (Type: "int", InterfaceType: "int"),
        };

        foreach ((string type, string interfaceType) in cases)
        {
            Encapsulated encapsulated = GetEncapsulatedFor(type);

            // Act
            ImmutableArray<string> interfaces = encapsulated.Interfaces;

            // Assert
            interfaces.ShouldContain("global::System.IComparable");
            interfaces.ShouldContain($"global::System.IComparable<{interfaceType}>");
            interfaces.ShouldNotContain("global::System.IConvertible");
            encapsulated.Methods.ShouldContain(method => method.Name == "CompareTo"
                && method.Parameters.Length == 1
                && method.Parameters[0].Type == interfaceType);
            encapsulated.Properties.ShouldBeEmpty();
        }
    }

    [Fact]
    public void GivenEncapsulatedBoolThenBuiltInOperatorsAreCaptured()
    {
        // Arrange
        Encapsulated encapsulated = GetEncapsulatedFor("bool");

        // Act
        ImmutableArray<BinaryOperator> binaryOperators = encapsulated.BinaryOperators;
        ImmutableArray<UnaryOperator> unaryOperators = encapsulated.UnaryOperators;

        // Assert
        binaryOperators.Length.ShouldBe(3);

        ShouldContainBinaryOperator(binaryOperators, "op_BitwiseAnd", "&", isReturnSubject: true);
        ShouldContainBinaryOperator(binaryOperators, "op_BitwiseOr", "|", isReturnSubject: true);
        ShouldContainBinaryOperator(binaryOperators, "op_ExclusiveOr", "^", isReturnSubject: true);

        unaryOperators.Length.ShouldBe(3);

        ShouldContainUnaryOperator(unaryOperators, "op_LogicalNot", "!", isReturnSubject: true);
        ShouldContainUnaryOperator(unaryOperators, "op_False", "false", isReturnSubject: false, returnType: "bool");
        ShouldContainUnaryOperator(unaryOperators, "op_True", "true", isReturnSubject: false, returnType: "bool");
    }

    [Fact]
    public void GivenEncapsulatedIntThenBuiltInUnaryOperatorsAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(int))]
            public sealed partial class Wrapper
            {
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].UnaryOperators.ShouldContain(@operator => @operator.Operator == "op_UnaryPlus"
            && @operator.IsReturnSubject
            && @operator.Symbol == "+");

        encapsulated[0].UnaryOperators.ShouldContain(@operator => @operator.Operator == "op_UnaryNegation"
            && @operator.IsReturnSubject
            && @operator.Symbol == "-");
    }

    [Fact]
    public void GivenEncapsulatedNumericTypesThenBuiltInBinaryOperatorsAreCaptured()
    {
        // Arrange
        (string Type, bool IsIntegral, bool IsPromoted)[] cases =
        {
            (Type: "byte", IsIntegral: true, IsPromoted: true),
            (Type: "char", IsIntegral: true, IsPromoted: true),
            (Type: "decimal", IsIntegral: false, IsPromoted: false),
            (Type: "double", IsIntegral: false, IsPromoted: false),
            (Type: "float", IsIntegral: false, IsPromoted: false),
            (Type: "int", IsIntegral: true, IsPromoted: false),
            (Type: "long", IsIntegral: true, IsPromoted: false),
            (Type: "sbyte", IsIntegral: true, IsPromoted: true),
            (Type: "short", IsIntegral: true, IsPromoted: true),
            (Type: "uint", IsIntegral: true, IsPromoted: false),
            (Type: "ulong", IsIntegral: true, IsPromoted: false),
            (Type: "ushort", IsIntegral: true, IsPromoted: true),
        };

        foreach ((string type, bool isIntegral, bool isPromoted) in cases)
        {
            Encapsulated encapsulated = GetEncapsulatedFor(type);
            int expectedCount = isIntegral ? 14 : 9;
            string expectedReturn = isPromoted ? "int" : "global::Sample.Wrapper";

            // Act
            ImmutableArray<BinaryOperator> binaryOperators = encapsulated.BinaryOperators;

            // Assert
            binaryOperators.Length.ShouldBe(expectedCount);

            ShouldContainBinaryOperator(binaryOperators, "op_Addition", "+", isReturnSubject: !isPromoted, returnType: expectedReturn);
            ShouldContainBinaryOperator(binaryOperators, "op_Division", "/", isReturnSubject: !isPromoted, returnType: expectedReturn);
            ShouldContainBinaryOperator(binaryOperators, "op_Modulus", "%", isReturnSubject: !isPromoted, returnType: expectedReturn);
            ShouldContainBinaryOperator(binaryOperators, "op_Multiply", "*", isReturnSubject: !isPromoted, returnType: expectedReturn);
            ShouldContainBinaryOperator(binaryOperators, "op_Subtraction", "-", isReturnSubject: !isPromoted, returnType: expectedReturn);

            ShouldContainBinaryOperator(binaryOperators, "op_GreaterThan", ">", isReturnSubject: false, returnType: "bool");
            ShouldContainBinaryOperator(binaryOperators, "op_GreaterThanOrEqual", ">=", isReturnSubject: false, returnType: "bool");
            ShouldContainBinaryOperator(binaryOperators, "op_LessThan", "<", isReturnSubject: false, returnType: "bool");
            ShouldContainBinaryOperator(binaryOperators, "op_LessThanOrEqual", "<=", isReturnSubject: false, returnType: "bool");

            if (!isIntegral)
            {
                continue;
            }

            ShouldContainBinaryOperator(binaryOperators, "op_BitwiseAnd", "&", isReturnSubject: !isPromoted, returnType: expectedReturn);
            ShouldContainBinaryOperator(binaryOperators, "op_BitwiseOr", "|", isReturnSubject: !isPromoted, returnType: expectedReturn);
            ShouldContainBinaryOperator(binaryOperators, "op_ExclusiveOr", "^", isReturnSubject: !isPromoted, returnType: expectedReturn);
            ShouldContainBinaryOperator(binaryOperators, "op_LeftShift", "<<", isRightSubject: false, isReturnSubject: !isPromoted, rightType: "int", returnType: expectedReturn);
            ShouldContainBinaryOperator(binaryOperators, "op_RightShift", ">>", isRightSubject: false, isReturnSubject: !isPromoted, rightType: "int", returnType: expectedReturn);
        }
    }

    [Fact]
    public void GivenEncapsulatedNumericTypesThenBuiltInUnaryOperatorsAreCaptured()
    {
        // Arrange
        (string Type, bool IsIntegral, bool IsPromoted, bool HasNegation)[] cases =
        {
            (Type: "byte", IsIntegral: true, IsPromoted: true, HasNegation: true),
            (Type: "char", IsIntegral: true, IsPromoted: true, HasNegation: true),
            (Type: "decimal", IsIntegral: false, IsPromoted: false, HasNegation: true),
            (Type: "double", IsIntegral: false, IsPromoted: false, HasNegation: true),
            (Type: "float", IsIntegral: false, IsPromoted: false, HasNegation: true),
            (Type: "int", IsIntegral: true, IsPromoted: false, HasNegation: true),
            (Type: "long", IsIntegral: true, IsPromoted: false, HasNegation: true),
            (Type: "sbyte", IsIntegral: true, IsPromoted: true, HasNegation: true),
            (Type: "short", IsIntegral: true, IsPromoted: true, HasNegation: true),
            (Type: "uint", IsIntegral: true, IsPromoted: false, HasNegation: false),
            (Type: "ulong", IsIntegral: true, IsPromoted: false, HasNegation: false),
            (Type: "ushort", IsIntegral: true, IsPromoted: true, HasNegation: true),
        };

        foreach ((string type, bool isIntegral, bool isPromoted, bool hasNegation) in cases)
        {
            Encapsulated encapsulated = GetEncapsulatedFor(type);
            int expectedCount = 3 + (isIntegral ? 1 : 0) + (hasNegation ? 1 : 0);
            string expectedReturn = isPromoted ? "int" : "global::Sample.Wrapper";

            // Act
            ImmutableArray<UnaryOperator> unaryOperators = encapsulated.UnaryOperators;

            // Assert
            unaryOperators.Length.ShouldBe(expectedCount);

            ShouldContainUnaryOperator(unaryOperators, "op_Decrement", "--", isReturnSubject: true);
            ShouldContainUnaryOperator(unaryOperators, "op_Increment", "++", isReturnSubject: true);
            ShouldContainUnaryOperator(unaryOperators, "op_UnaryPlus", "+", isReturnSubject: !isPromoted, returnType: expectedReturn);

            if (hasNegation)
            {
                ShouldContainUnaryOperator(unaryOperators, "op_UnaryNegation", "-", isReturnSubject: !isPromoted, returnType: expectedReturn);
            }

            if (isIntegral)
            {
                ShouldContainUnaryOperator(unaryOperators, "op_OnesComplement", "~", isReturnSubject: !isPromoted, returnType: expectedReturn);
            }
        }
    }

    [Fact]
    public void GivenEncapsulatedTypeWithBinaryOperatorsThenTheyAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Value))]
            public sealed partial class Wrapper
            {
            }

            public sealed class Value
            {
                public static Value operator +(Value left, Value right) => left;

                public static Value operator -(Value left, int right) => left;

                public static bool operator <(Value left, Value right) => true;

                public static bool operator >=(int left, Value right) => true;
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].BinaryOperators.Length.ShouldBe(4);

        encapsulated[0].BinaryOperators.ShouldContain(@operator => @operator.Operator == "op_Addition"
            && @operator.IsLeftSubject
            && @operator.IsRightSubject
            && @operator.IsReturnSubject
            && @operator.Symbol == "+");

        encapsulated[0].BinaryOperators.ShouldContain(@operator => @operator.Operator == "op_Subtraction"
            && @operator.IsLeftSubject
            && !@operator.IsRightSubject
            && @operator.IsReturnSubject
            && @operator.Right == "int"
            && @operator.Symbol == "-");

        encapsulated[0].BinaryOperators.ShouldContain(@operator => @operator.Operator == "op_LessThan"
            && @operator.IsLeftSubject
            && @operator.IsRightSubject
            && !@operator.IsReturnSubject
            && @operator.Return == "bool"
            && @operator.Symbol == "<");

        encapsulated[0].BinaryOperators.ShouldContain(@operator => @operator.Operator == "op_GreaterThanOrEqual"
            && !@operator.IsLeftSubject
            && @operator.IsRightSubject
            && !@operator.IsReturnSubject
            && @operator.Left == "int"
            && @operator.Symbol == ">=");
    }

    [Fact]
    public void GivenEncapsulatedTypeWithConversionsThenTheyAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Value))]
            public sealed partial class Wrapper
            {
            }

            public sealed class Value
            {
                public static implicit operator string(Value value) => value.ToString();

                public static explicit operator Value(string value) => new Value();
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].Conversions.Length.ShouldBe(2);
        encapsulated[0].Conversions[0].IsReturnSubject.ShouldBeTrue();
        encapsulated[0].Conversions[0].Parameter.ShouldBe("string");
        encapsulated[0].Conversions[0].Return.ShouldBe("global::Sample.Wrapper");
        encapsulated[0].Conversions[1].IsParameterSubject.ShouldBeTrue();
        encapsulated[0].Conversions[1].Parameter.ShouldBe("global::Sample.Wrapper");
        encapsulated[0].Conversions[1].Return.ShouldBe("string");
    }

    [Fact]
    public void GivenEncapsulatedTypeWithInterfacesThenTheyAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using System;
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Value))]
            public sealed partial class Wrapper
            {
            }

            public interface IMarker
            {
            }

            public interface IChild
                : IMarker
            {
            }

            public sealed class Value
                : IChild,
                  IComparable<Value>,
                  IEquatable<Value>,
                  IEquatable<Wrapper>
            {
                public int CompareTo(Value other) => 0;

                public bool Equals(Value other) => true;

                public bool Equals(Wrapper other) => true;
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].Interfaces.Length.ShouldBe(3);
        encapsulated[0].Interfaces.ShouldContain("global::Sample.IChild");
        encapsulated[0].Interfaces.ShouldContain("global::Sample.IMarker");
        encapsulated[0].Interfaces.ShouldContain("global::System.IComparable<global::Sample.Value>");
        encapsulated[0].Interfaces.ShouldNotContain("global::System.IEquatable<global::Sample.Value>");
        encapsulated[0].Interfaces.ShouldNotContain("global::System.IEquatable<global::Sample.Wrapper>");
    }

    [Fact]
    public void GivenEncapsulatedTypeWithMembersThenTheyAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Value))]
            public sealed partial class Wrapper
            {
            }

            public interface IExplicit
            {
                int Count { get; }

                string this[int index] { get; set; }

                string Format(int value);
            }

            public sealed class Value
                : IExplicit
            {
                public string Name { get; set; }

                internal int Number { get; }

                public string Format(string value) => value;

                int IExplicit.Count => 1;

                string IExplicit.this[int index]
                {
                    get => index.ToString();
                    set
                    {
                    }
                }

                string IExplicit.Format(int value) => value.ToString();
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].Properties.Length.ShouldBe(4);
        encapsulated[0].Properties.ShouldContain(property => property.Accessibility == "public"
            && property.Name == "Name"
            && property.Type == "string"
            && property.HasGetter
            && property.HasSetter);
        encapsulated[0].Properties.ShouldContain(property => property.Accessibility == "internal"
            && property.Name == "Number"
            && property.Type == "int"
            && property.HasGetter
            && !property.HasSetter);
        encapsulated[0].Properties.ShouldContain(property => property.ExplicitInterface == "global::Sample.IExplicit"
            && property.Name == "Count"
            && property.Type == "int"
            && property.HasGetter
            && !property.HasSetter);
        encapsulated[0].Properties.ShouldContain(property => property.ExplicitInterface == "global::Sample.IExplicit"
            && property.IsIndexer
            && property.Type == "string"
            && property.HasGetter
            && property.HasSetter);

        encapsulated[0].Methods.Length.ShouldBe(2);
        encapsulated[0].Methods.ShouldContain(method => method.Accessibility == "public"
            && method.Name == "Format"
            && method.Return == "string"
            && method.Parameters[0].Type == "string");
        encapsulated[0].Methods.ShouldContain(method => method.ExplicitInterface == "global::Sample.IExplicit"
            && method.Name == "Format"
            && method.Return == "string"
            && method.Parameters[0].Type == "int");
    }

    [Fact]
    public void GivenAnnotatedTypeWithMatchingMembersThenTheyAreNotCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Value))]
            public sealed partial class Wrapper
            {
                public string Name { get; }

                public string Format(string value) => value;
            }

            public sealed class Value
            {
                public string Name { get; set; }

                public int Number { get; }

                public string Format(string value) => value;

                public string Other(string value) => value;
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].Properties.Length.ShouldBe(1);
        encapsulated[0].Properties[0].Name.ShouldBe("Number");
        encapsulated[0].Methods.Length.ShouldBe(1);
        encapsulated[0].Methods[0].Name.ShouldBe("Other");
    }

    [Fact]
    public void GivenAnnotatedTypeWithMatchingInterfaceThenPassthroughMembersAreNotCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Value))]
            public sealed partial class Wrapper
                : IExplicit
            {
                public int Count => 0;

                public string Format(int value) => value.ToString();
            }

            public interface IExplicit
            {
                int Count { get; }

                string Format(int value);
            }

            public sealed class Value
                : IExplicit
            {
                int IExplicit.Count => 1;

                string IExplicit.Format(int value) => value.ToString();
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].Interfaces.ShouldNotContain("global::Sample.IExplicit");
        encapsulated[0].Methods.ShouldBeEmpty();
        encapsulated[0].Properties.ShouldBeEmpty();
    }

    [Fact]
    public void GivenEncapsulatedTypeWithStrategyGeneratedMethodsThenTheyAreNotCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Value))]
            public sealed partial class Wrapper
            {
            }

            public sealed class Value
            {
                public bool Equals(Value other) => true;

                public bool Equals(Wrapper other) => true;

                public override bool Equals(object value) => true;

                public string Format() => string.Empty;

                public override int GetHashCode() => 0;

                public override string ToString() => string.Empty;
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].Methods.Length.ShouldBe(1);
        encapsulated[0].Methods[0].Name.ShouldBe("Format");
    }

    [Fact]
    public void GivenPassthroughTypeWithGeneratedEquatableMembersThenTheyAreNotCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using System;
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Inner))]
            public sealed partial class Outer
            {
            }

            [Monify(Type = typeof(string))]
            public sealed partial class Inner
                : IEquatable<Inner>,
                  IEquatable<string>
            {
                public bool Equals(Inner other) => true;

                public bool Equals(string other) => true;

                public string Format() => string.Empty;
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? outer = compilation.GetTypeByMetadataName("Sample.Outer");

        _ = outer.ShouldNotBeNull();
        outer.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = outer.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated.Length.ShouldBe(2);
        encapsulated[0].Interfaces.ShouldNotContain("global::System.IEquatable<global::Sample.Inner>");
        encapsulated[0].Interfaces.ShouldNotContain("global::System.IEquatable<string>");
        encapsulated[0].Methods.ShouldNotContain(method => method.Name == nameof(Equals)
            && method.Parameters[0].Type == "global::Sample.Inner");
        encapsulated[0].Methods.ShouldNotContain(method => method.Name == nameof(Equals)
            && method.Parameters[0].Type == "string");
        encapsulated[0].Methods.ShouldContain(method => method.Name == "Format");
    }

    [Fact]
    public void GivenEncapsulatedTypeWithUnaryOperatorsThenTheyAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Value))]
            public sealed partial class Wrapper
            {
            }

            public sealed class Value
            {
                public static Value operator +(Value value) => value;

                public static Value operator -(Value value) => value;

                public static Value operator !(Value value) => value;

                public static Value operator ~(Value value) => value;

                public static Value operator ++(Value value) => value;

                public static Value operator --(Value value) => value;

                public static bool operator true(Value value) => true;

                public static bool operator false(Value value) => false;
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = wrapper.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated[0].UnaryOperators.Length.ShouldBe(8);

        encapsulated[0].UnaryOperators.ShouldContain(@operator => @operator.Operator == "op_UnaryPlus"
            && @operator.IsReturnSubject
            && @operator.Return == "global::Sample.Wrapper"
            && @operator.Symbol == "+");

        encapsulated[0].UnaryOperators.ShouldContain(@operator => @operator.Operator == "op_True"
            && !@operator.IsReturnSubject
            && @operator.Return == "bool"
            && @operator.Symbol == "true");
    }

    [Fact]
    public void GivenMonifyAttributesWithNamedArgumentsThenPassthroughValuesAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Inner))]
            public sealed partial class Outer
            {
            }

            [Monify(Type = typeof(string))]
            public sealed partial class Inner
            {
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? outer = compilation.GetTypeByMetadataName("Sample.Outer");

        _ = outer.ShouldNotBeNull();
        outer.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = outer.GetEncapsulated(compilation, model, value);

        // Assert
        encapsulated.Length.ShouldBe(2);
        encapsulated[0].Type.ShouldBe("global::Sample.Inner");
        encapsulated[1].Type.ShouldBe("string");
        encapsulated[1].Interfaces.ShouldBeEmpty();
        encapsulated[1].Methods.ShouldBeEmpty();
        encapsulated[1].Properties.ShouldBeEmpty();
    }

    private static Encapsulated GetEncapsulatedFor(string type)
    {
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        string declarations = $$"""
            using Monify;

            namespace Sample;

            [Monify(Type = typeof({{type}}))]
            public sealed partial class Wrapper
            {
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");

        _ = wrapper.ShouldNotBeNull();
        wrapper.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        return wrapper.GetEncapsulated(compilation, model, value)[0];
    }

    private static void ShouldContainBinaryOperator(
        ImmutableArray<BinaryOperator> binaryOperators,
        string operatorName,
        string symbol,
        bool isLeftSubject = true,
        bool isRightSubject = true,
        bool isReturnSubject = true,
        string leftType = "global::Sample.Wrapper",
        string rightType = "global::Sample.Wrapper",
        string returnType = "global::Sample.Wrapper")
    {
        binaryOperators.ShouldContain(@operator => @operator.Operator == operatorName
            && @operator.IsLeftSubject == isLeftSubject
            && @operator.IsRightSubject == isRightSubject
            && @operator.IsReturnSubject == isReturnSubject
            && @operator.Left == leftType
            && @operator.Right == rightType
            && @operator.Return == returnType
            && @operator.Symbol == symbol);
    }

    private static void ShouldContainUnaryOperator(
        ImmutableArray<UnaryOperator> unaryOperators,
        string operatorName,
        string symbol,
        bool isReturnSubject = true,
        string returnType = "global::Sample.Wrapper")
    {
        unaryOperators.ShouldContain(@operator => @operator.Operator == operatorName
            && @operator.IsReturnSubject == isReturnSubject
            && @operator.Return == returnType
            && @operator.Symbol == symbol);
    }
}
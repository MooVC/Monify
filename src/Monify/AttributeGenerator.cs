namespace Monify;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Generates the Monify attribute, used to denote when a type should serve as a wrapper for a single value.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class AttributeGenerator
    : IIncrementalGenerator
{
    /// <summary>
    /// The source code for the Generic attribute that will be output by the generator.
    /// </summary>
    internal const string Generic = $$"""
        namespace Monify
        {
            using System;
            using System.Diagnostics.CodeAnalysis;

            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
            internal sealed class {{Name}}Attribute<T>
                : Attribute
            {
                public bool Passthrough { get; set; } = true;
            }
        }
        """;

    /// <summary>
    /// The source code for the NonGeneric attribute that will be output by the generator.
    /// </summary>
    internal const string NonGeneric = $$"""
        namespace Monify
        {
            using System;
            using System.Diagnostics.CodeAnalysis;

            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
            internal sealed class {{Name}}Attribute
                : Attribute
            {
                private bool _passthrough = true;
                private Type _type;

                public bool Passthrough
                {
                    get
                    {
                        return _passthrough;
                    }
                    set
                    {
                        _passthrough = value;
                    }
                }

                public Type Type
                {
                    get
                    {
                        return _type;
                    }
                    set
                    {
                        _type = value;
                    }
                }
            }
        }
        """;

    /// <summary>
    /// The name of the attribute (without the suffix).
    /// </summary>
    internal const string Name = "Monify";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.ParseOptionsProvider, (context, options) =>
        {
            if (options is CSharpParseOptions csharp && csharp.LanguageVersion >= LanguageVersion.CSharp11)
            {
                Generate(Generic, context, "Generic");
            }

            Generate(NonGeneric, context, "NonGeneric");
        });
    }

    private static void Generate(string content, SourceProductionContext context, string name)
    {
        var text = SourceText.From(content, Encoding.UTF8);

        context.AddSource($"{Name}Attribute.{name}.g.cs", text);
    }
}
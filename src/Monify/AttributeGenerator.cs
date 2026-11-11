namespace Monify
{
    using System.Text;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Text;
    using Monify.Syntax;

    using static Monify.AttributeGenerator_Resources;

    /// <summary>
    /// Generates the Monify attribute, used to denote when a type should serve as a wrapper for a single value.
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public sealed class AttributeGenerator
        : IIncrementalGenerator
    {
        /// <summary>
        /// The name of the attribute (without the suffix).
        /// </summary>
        internal const string Name = "Monify";

        /// <summary>
        /// The source code for the Generic attribute that will be output by the generator.
        /// </summary>
        internal static readonly string Generic = CreateSource(GenericSource);

        /// <summary>
        /// The source code for the NonGeneric attribute that will be output by the generator.
        /// </summary>
        internal static readonly string NonGeneric = CreateSource(NonGenericSource);

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterSourceOutput(context.ParseOptionsProvider, (productionContext, options) =>
            {
                if (options is CSharpParseOptions csharp && csharp.LanguageVersion >= LanguageVersion.CSharp11)
                {
                    Generate(Generic, productionContext, "Generic");
                }

                Generate(NonGeneric, productionContext, "NonGeneric");
            });
        }

        private static string CreateSource(string content)
        {
            return GenerationSyntax.Block(GenerationSyntax.ToSnippet(string.Format(content, Name)), "namespace Monify").ToString().NormalizeLineEndings();
        }

        private static void Generate(string content, SourceProductionContext context, string name)
        {
            var text = SourceText.From(content, Encoding.UTF8);

            context.AddSource($"{Name}Attribute.{name}.g.cs", text);
        }
    }
}
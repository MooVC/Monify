namespace Monify
{
    using Microsoft.CodeAnalysis;
    using Monify.Syntax;

    using static Monify.HashCodeGenerator_Resources;

    /// <summary>
    /// Generates an internal HashCode static class that is used to support hash code generation.
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public sealed class HashCodeGenerator
        : IIncrementalGenerator
    {
        /// <summary>
        /// The source code that will be output by the generator.
        /// </summary>
        public static readonly string Content = GenerationSyntax.Block(
            GenerationSyntax.Block(GenerationSyntax.ToSnippet(GeneratedSource), "internal static class HashCode"),
            "namespace Monify.Internal").ToString().NormalizeLineEndings();

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(productionContext => productionContext.AddSource("Monify.Internal.HashCode.g.cs", Content));
        }
    }
}
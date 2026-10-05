namespace Monify.Semantics
{
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
    /// </summary>
    internal static partial class INamedTypeSymbolExtensions
    {
        private const string JsonConverterAttributeTypeName = "global::System.Text.Json.Serialization.JsonConverterAttribute";

        /// <summary>
        /// Determines whether the <paramref name="subject"/> is annotated with a JSON converter attribute.
        /// </summary>
        /// <param name="subject">
        /// The symbol to be checked for the JSON converter attribute.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a JSON converter attribute is present; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool HasJsonConverter(this INamedTypeSymbol subject)
        {
            return subject.HasAttribute(candidate => IsJsonConverterAttribute(candidate.AttributeClass));
        }

        private static bool IsJsonConverterAttribute(INamedTypeSymbol attribute)
        {
            while (attribute is object)
            {
                if (attribute.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == JsonConverterAttributeTypeName)
                {
                    return true;
                }

                attribute = attribute.BaseType;
            }

            return false;
        }
    }
}
namespace Monify.Semantics
{
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
    /// </summary>
    internal static partial class INamedTypeSymbolExtensions
    {
        private const string DebuggerDisplayAttributeTypeName = "global::System.Diagnostics.DebuggerDisplayAttribute";

        /// <summary>
        /// Determines whether or not the <paramref name="subject"/> is annotated with <c>DebuggerDisplayAttribute</c>.
        /// </summary>
        /// <param name="subject">
        /// The symbol to be checked for the debugger display attribute.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="subject"/> is annotated with <c>DebuggerDisplayAttribute</c>, otherwise <see langword="false"/>.
        /// </returns>
        public static bool HasDebuggerDisplay(this INamedTypeSymbol subject)
        {
            return subject.HasAttribute(candidate => candidate.AttributeClass is object
                                                  && candidate.AttributeClass.ToDisplayString(format: SymbolDisplayFormat.FullyQualifiedFormat) == DebuggerDisplayAttributeTypeName);
        }
    }
}
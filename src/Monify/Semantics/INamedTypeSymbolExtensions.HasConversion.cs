namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const string ImplciitOperatorName = "op_Implicit";
    private const int ExpectedParametersForConversion = 1;

    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> declares an its own conversion for <paramref name="type"/>.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <param name="type">
    /// The type from which the conversion is made.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> declares the conversion, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasConversion(this INamedTypeSymbol subject, ITypeSymbol type)
    {
        return subject
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Any(method => method.MethodKind == MethodKind.Conversion
                        && method.Name == ImplciitOperatorName
                        && method.Parameters.Length == ExpectedParametersForConversion
                        && method.Parameters[0].Type.Equals(type, SymbolEqualityComparer.Default));
    }
}

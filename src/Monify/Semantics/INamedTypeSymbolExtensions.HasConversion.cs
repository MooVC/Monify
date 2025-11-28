namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const string ExplicitOperatorName = "op_Explicit";
    private const string ImplicitOperatorName = "op_Implicit";
    private const int ExpectedParametersForConversion = 1;

    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> declares an implicit conversion with the provided signature.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <param name="parameter">
    /// The parameter type associated with the conversion.
    /// </param>
    /// <param name="result">
    /// The return type associated with the conversion.
    /// </param>
    /// <param name="operatorName">
    /// The name of the conversion operator being checked.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> declares the conversion, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasConversion(
        this INamedTypeSymbol subject,
        ITypeSymbol parameter,
        ITypeSymbol result,
        string operatorName = ImplicitOperatorName)
    {
        return subject
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Any(method => method.MethodKind == MethodKind.Conversion
                        && method.Name == operatorName
                        && method.Parameters.Length == ExpectedParametersForConversion
                        && method.Parameters[0].Type.Equals(parameter, SymbolEqualityComparer.IncludeNullability)
                        && method.ReturnType.Equals(result, SymbolEqualityComparer.IncludeNullability));
    }
}
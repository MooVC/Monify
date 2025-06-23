namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int ExpectedParametersForOperator = 2;
    private const int OffsetForLeftParameter = 0;
    private const int OffsetForRightParameter = 1;

    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> declares an its own operator named <paramref name="operator"/>.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <param name="operator">
    /// The name of the operator to check.
    /// </param>
    /// <param name="type">
    /// The type to which the <paramref name="subject"/> is being compared.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> declares the operator, otherwise <see langword="false"/>.
    /// </returns>
    private static bool HasOperator(this INamedTypeSymbol subject, string @operator, ITypeSymbol? type = default)
    {
        bool IsComparingToSelf(IMethodSymbol method)
        {
            return SymbolEqualityComparer.Default.Equals(method.Parameters[OffsetForLeftParameter].Type, method.Parameters[OffsetForRightParameter].Type)
                && SymbolEqualityComparer.Default.Equals(method.Parameters[OffsetForLeftParameter].Type, subject);
        }

        bool IsComparingExpectedTypes(IMethodSymbol method)
        {
            return !SymbolEqualityComparer.Default.Equals(method.Parameters[OffsetForLeftParameter].Type, method.Parameters[OffsetForRightParameter].Type)
                && IsOfTypes(method.Parameters[OffsetForLeftParameter].Type)
                && IsOfTypes(method.Parameters[OffsetForRightParameter].Type);
        }

        bool IsOfTypes(ITypeSymbol symbol)
        {
            return SymbolEqualityComparer.Default.Equals(symbol, subject) || SymbolEqualityComparer.Default.Equals(symbol, type);
        }

        Predicate<IMethodSymbol> condition = type is null
            ? IsComparingToSelf
            : IsComparingExpectedTypes;

        return subject
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Any(method => method.MethodKind == MethodKind.UserDefinedOperator
                        && method.Name == @operator
                        && method.Parameters.Length == ExpectedParametersForOperator
                        && condition(method));
    }
}

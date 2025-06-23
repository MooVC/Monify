namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const string EqualityOperatorName = "op_Equality";

    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> declares an its own equals operator.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <param name="type">
    /// The type to which the operator is applied.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> declares the equals operator, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasEqualityOperator(this INamedTypeSymbol subject, ITypeSymbol? type = default)
    {
        return subject.HasOperator(EqualityOperatorName, type: type);
    }
}

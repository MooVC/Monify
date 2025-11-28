namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int OffsetForLeftOperand = 0;
    private const int OffsetForRightOperand = 1;

    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> declares its own binary operator named <paramref name="operator"/>.
    /// </summary>
    /// <param name="subject">The <paramref name="subject"/> to be checked.</param>
    /// <param name="operator">The name of the operator to check.</param>
    /// <param name="left">The type of the left operand.</param>
    /// <param name="right">The type of the right operand.</param>
    /// <returns><see langword="true"/> if the <paramref name="subject"/> declares the operator, otherwise <see langword="false"/>.</returns>
    public static bool HasBinaryOperator(this INamedTypeSymbol subject, string @operator, ITypeSymbol left, ITypeSymbol right)
    {
        return subject
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Any(method => method.MethodKind == MethodKind.UserDefinedOperator
                        && method.Name == @operator
                        && method.Parameters.Length == ExpectedParametersForOperator
                        && method.Parameters[OffsetForLeftOperand].Type.Equals(left, SymbolEqualityComparer.IncludeNullability)
                        && method.Parameters[OffsetForRightOperand].Type.Equals(right, SymbolEqualityComparer.IncludeNullability));
    }
}
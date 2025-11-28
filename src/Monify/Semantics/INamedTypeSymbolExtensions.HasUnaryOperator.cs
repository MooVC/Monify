namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> declares its own unary operator.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <param name="operator">
    /// The name of the operator to check.
    /// </param>
    /// <param name="returnType">
    /// The return type associated with the operator.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> declares the operator, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasUnaryOperator(this INamedTypeSymbol subject, string @operator, ITypeSymbol returnType)
    {
        return subject
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Any(method => method.MethodKind == MethodKind.UserDefinedOperator
                        && method.Name == @operator
                        && method.Parameters.Length == ExpectedParametersForUnaryOperator
                        && method.Parameters[0].Type.Equals(subject, SymbolEqualityComparer.IncludeNullability)
                        && method.ReturnType.Equals(returnType, SymbolEqualityComparer.IncludeNullability));
    }
}
namespace Monify.Semantics;

using Microsoft.CodeAnalysis;
using Monify.Strategies;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Returns any explicitly defined constructors for <paramref name="subject"/>.
    /// </summary>
    /// <param name="subject">
    /// The subject for which constructability is to be determined.
    /// </param>
    /// <returns>
    /// The constructors for <paramref name="subject"/>.
    /// </returns>
    public static IMethodSymbol[] GetConstructors(this INamedTypeSymbol subject)
    {
        return subject
            .GetMembers()
            .Where(member => member.Kind == SymbolKind.Method
                          && !member.IsStatic
                          && member.Name == ConstructorStrategy.Name
                          && !member.IsImplicitlyDeclared)
            .OfType<IMethodSymbol>()
            .ToArray();
    }
}
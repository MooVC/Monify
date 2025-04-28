namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int ExpectedFieldsForStatelessType = 0;
    private const int ExpectedFieldsWhenFieldIsAlreadyDefined = 1;

    /// <summary>
    /// Determines whether or not the <paramref name="class"/> is stateless.
    /// </summary>
    /// <param name="class">
    /// The subject for which the state holding capability is to be determined.
    /// </param>
    /// <param name="value">
    /// The type associated with the encapsulated value.
    /// </param>
    /// <param name="hasFieldForEncapsulatedValue">
    /// <see langword="true"/> if the <paramref name="class"/> already defines a field for the encapsulated value, otherwise <see langword="false"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="class"/> is stateless, otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// If the class holds state, then it points to a design issue, as the class is intended to represent a single state.
    /// </remarks>
    public static bool IsStateless(this INamedTypeSymbol @class, ITypeSymbol value, out bool hasFieldForEncapsulatedValue)
    {
        IFieldSymbol[] fields = @class
            .GetMembers()
            .Where(member => member.Kind == SymbolKind.Field && !member.IsStatic)
            .OfType<IFieldSymbol>()
            .ToArray();

        hasFieldForEncapsulatedValue = false;

        if (fields.Length == ExpectedFieldsForStatelessType)
        {
            return true;
        }

        if (fields.Length == ExpectedFieldsWhenFieldIsAlreadyDefined)
        {
            hasFieldForEncapsulatedValue = SymbolEqualityComparer.IncludeNullability.Equals(fields[0].Type, value);
        }

        return hasFieldForEncapsulatedValue;
    }
}
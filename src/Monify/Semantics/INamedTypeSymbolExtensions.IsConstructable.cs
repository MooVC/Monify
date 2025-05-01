namespace Monify.Semantics;

using Microsoft.CodeAnalysis;
using Monify.Strategies;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int OffsetForConstructorWhenDefined = 0;
    private const int ExpectedConstructorsWhenUndefined = 0;
    private const int ExpectedConstructorsWhenDefined = 1;
    private const int OffsetForConstructorParameterWhenDefined = 1;
    private const int ExpectedParametersForConstructorWhenDefined = 1;

    /// <summary>
    /// Determines whether or not the <paramref name="class"/> is constructable.
    /// </summary>
    /// <param name="class">
    /// The subject for which constructability is to be determined.
    /// </param>
    /// <param name="value">
    /// The type associated with the encapsulated value.
    /// </param>
    /// <param name="hasConstructorForEncapsulatedValue">
    /// <see langword="true"/> if the <paramref name="class"/> already defines a constructor for the encapsulated value, otherwise <see langword="false"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="class"/> is constructable, otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// If the class has different constructors, then it points to a design issue, as the class is intended to represent a single state with just one constructor.
    /// </remarks>
    public static bool IsConstructable(this INamedTypeSymbol @class, ITypeSymbol value, out bool hasConstructorForEncapsulatedValue)
    {
        IMethodSymbol[] constructors = @class
            .GetMembers()
            .Where(member => member.Kind == SymbolKind.Method && !member.IsStatic && member.Name == ConstructorStrategy.Name)
            .OfType<IMethodSymbol>()
            .ToArray();

        hasConstructorForEncapsulatedValue = false;

        if (constructors.Length == ExpectedConstructorsWhenUndefined)
        {
            return true;
        }

        if (constructors.Length != ExpectedConstructorsWhenDefined)
        {
            return false;
        }

        IMethodSymbol constructor = constructors[OffsetForConstructorWhenDefined];

        if (constructor.Parameters.Length != ExpectedParametersForConstructorWhenDefined)
        {
            return false;
        }

        IParameterSymbol parameter = constructor.Parameters[OffsetForConstructorParameterWhenDefined];

        hasConstructorForEncapsulatedValue = SymbolEqualityComparer.IncludeNullability.Equals(parameter.Type, value);

        return hasConstructorForEncapsulatedValue;
    }
}
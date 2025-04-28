namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int ExpectedArgumentsForEquatable = 1;
    private const string EquatableTypeName = "System.IEquatable`1";
    private const int EquatableTypeArgument = 0;

    /// <summary>
    /// Determines whether or not the <paramref name="class"/> implements <see cref="IEquatable{T}"/>.
    /// </summary>
    /// <param name="class">
    /// The symbol for the class to be checked for the declaration.
    /// </param>
    /// <param name="compilation">
    /// The <see cref="Compilation"/> used to source the symbol for <see cref="IEquatable{T}"/>.
    /// </param>
    /// <param name="type">
    /// The type to which the <paramref name="class"/> is being compared.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="class"/> declares that it implements <see cref="IEquatable{T}"/>, otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// The <paramref name="class"/> is assumed to be a class.
    /// </remarks>
    public static bool IsEquatable(this INamedTypeSymbol @class, Compilation compilation, ITypeSymbol? type = default)
    {
        INamedTypeSymbol? equatable = compilation.GetTypeByMetadataName(EquatableTypeName);

        if (equatable is null)
        {
            return false;
        }

        type ??= @class;

        bool IsEquatable(INamedTypeSymbol @interface)
        {
            return SymbolEqualityComparer.Default.Equals(@interface.ConstructedFrom, equatable)
                && @interface.TypeArguments.Length == ExpectedArgumentsForEquatable
                && SymbolEqualityComparer.Default.Equals(@interface.TypeArguments[EquatableTypeArgument], type);
        }

        return @class.AllInterfaces.Any(IsEquatable);
    }
}
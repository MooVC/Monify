namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> inherits a sealed override for the method denoted by <paramref name="name"/>.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <param name="name">
    /// The name of the method to locate.
    /// </param>
    /// <param name="return">
    /// The return type for the method denoted by <paramref name="name"/>.
    /// </param>
    /// <param name="predicate">
    /// Allows for the specification of an optional parameter check on the override method.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> inherits a sealed override to the method denoted by <paramref name="name"/>, otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// When no <paramref name="predicate"/> is specified, it is assumed that the method accepts no parameters.
    /// </remarks>
    public static bool InheritsSealed(this INamedTypeSymbol subject, string name, SpecialType @return, Predicate<IMethodSymbol>? predicate = default)
    {
        predicate ??= method => method.Parameters.Length == DefaultParameterCountOnMethodOverrides;

        for (INamedTypeSymbol? @base = subject.BaseType; @base is not null && @base.SpecialType != SpecialType.System_Object; @base = @base.BaseType)
        {
            bool InheritsSealed(IMethodSymbol method)
            {
                return method.IsSealed && predicate(method);
            }

            if (@base.HasOverride(name, @return, InheritsSealed))
            {
                return true;
            }
        }

        return false;
    }
}
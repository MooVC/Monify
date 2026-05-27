namespace Monify.Semantics;

using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Monify.Model;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Identifies the methods declared by the <paramref name="encapsulated"/> type that should be forwarded.
    /// </summary>
    /// <param name="encapsulated">
    /// The encapsulated type whose methods are to be inspected.
    /// </param>
    /// <param name="compilation">
    /// The compilation used to resolve well-known interface symbols.
    /// </param>
    /// <param name="subject">
    /// The subject type being generated.
    /// </param>
    /// <param name="interfaces">
    /// The interfaces that will be forwarded.
    /// </param>
    /// <returns>
    /// The methods that should be forwarded to the encapsulated value.
    /// </returns>
    public static ImmutableArray<PassthroughMethod> GetPassthroughMethods(
        this INamedTypeSymbol encapsulated,
        Compilation compilation,
        INamedTypeSymbol subject,
        ImmutableArray<string> interfaces)
    {
        INamedTypeSymbol? equatable = compilation.GetTypeByMetadataName(EquatableTypeName);

        return encapsulated
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Where(method => method.IsPassthroughMethodCandidate(encapsulated, subject, equatable, interfaces))
            .Where(method => !subject.HasPassthroughMethod(method))
            .Select(CreatePassthroughMethod)
            .OrderBy(method => method.ExplicitInterface)
            .ThenBy(method => method.Name)
            .ThenBy(method => string.Join(",", method.Parameters.Select(parameter => parameter.Type)))
            .ToImmutableArray();
    }

    private static PassthroughMethod CreatePassthroughMethod(IMethodSymbol method)
    {
        IMethodSymbol? explicitImplementation = method.ExplicitInterfaceImplementations.FirstOrDefault();
        IMethodSymbol declaration = explicitImplementation ?? method;

        return new PassthroughMethod
        {
            Accessibility = explicitImplementation is null ? method.DeclaredAccessibility.ToSource() : string.Empty,
            Constraints = explicitImplementation is null ? declaration.GetTypeParameterConstraints() : ImmutableArray<string>.Empty,
            ExplicitInterface = explicitImplementation?.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? string.Empty,
            Name = declaration.Name,
            Parameters = declaration.Parameters.Select(CreatePassthroughParameter).ToImmutableArray(),
            Return = declaration.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            TypeParameters = declaration.TypeParameters.Select(parameter => parameter.Name).ToImmutableArray(),
        };
    }

    private static bool HasPassthroughMethod(this INamedTypeSymbol subject, IMethodSymbol method)
    {
        IMethodSymbol? explicitImplementation = method.ExplicitInterfaceImplementations.FirstOrDefault();

        foreach (IMethodSymbol candidate in subject.GetMembers().OfType<IMethodSymbol>())
        {
            if (explicitImplementation is not null
             && candidate.ExplicitInterfaceImplementations.Any(implementation => SymbolEqualityComparer.Default.Equals(implementation, explicitImplementation)))
            {
                return true;
            }

            if (explicitImplementation is null
             && candidate.MethodKind == MethodKind.Ordinary
             && candidate.Name == method.Name
             && candidate.TypeParameters.Length == method.TypeParameters.Length
             && candidate.Parameters.Length == method.Parameters.Length
             && candidate.Parameters.Zip(method.Parameters, IsEquivalentParameter).All(isEquivalent => isEquivalent))
            {
                return true;
            }
        }

        return false;
    }

    private static bool CanForwardExplicitImplementation(
        this IMethodSymbol implementation,
        INamedTypeSymbol encapsulated,
        INamedTypeSymbol subject,
        ImmutableArray<string> interfaces)
    {
        string @interface = implementation.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return implementation.ContainingType.DeclaredAccessibility.CanForward(encapsulated, subject)
            && interfaces.Contains(@interface);
    }

    private static bool IsDuplicateOfGeneratedMethod(
        IMethodSymbol method,
        INamedTypeSymbol encapsulated,
        INamedTypeSymbol subject,
        INamedTypeSymbol? equatable)
    {
        IMethodSymbol declaration = method.ExplicitInterfaceImplementations.FirstOrDefault() ?? method;

        if (declaration.Name == nameof(object.GetHashCode) && declaration.Parameters.Length == 0)
        {
            return true;
        }

        if (declaration.Name == nameof(object.ToString) && declaration.Parameters.Length == 0)
        {
            return true;
        }

        if (declaration.Name != nameof(object.Equals) || declaration.Parameters.Length != 1)
        {
            return false;
        }

        ITypeSymbol parameter = declaration.Parameters[0].Type;

        return parameter.SpecialType == SpecialType.System_Object
            || SymbolEqualityComparer.Default.Equals(parameter, encapsulated)
            || SymbolEqualityComparer.Default.Equals(parameter, subject)
            || method.ExplicitInterfaceImplementations.Any(implementation => implementation.ContainingType.IsEquatableFor(equatable, encapsulated))
            || method.ExplicitInterfaceImplementations.Any(implementation => implementation.ContainingType.IsEquatableFor(equatable, subject));
    }

    private static bool IsEquivalentParameter(IParameterSymbol left, IParameterSymbol right)
    {
        return left.RefKind == right.RefKind
            && SymbolEqualityComparer.Default.Equals(left.Type, right.Type);
    }

    private static bool IsAccessor(this IMethodSymbol method)
    {
        return IsAccessorName(method.Name)
            || method.ExplicitInterfaceImplementations.Any(implementation => implementation.AssociatedSymbol is not null || IsAccessorName(implementation.Name));
    }

    private static bool IsAccessorName(string name)
    {
        return name.StartsWith("add_", StringComparison.Ordinal)
            || name.StartsWith("get_", StringComparison.Ordinal)
            || name.StartsWith("remove_", StringComparison.Ordinal)
            || name.StartsWith("set_", StringComparison.Ordinal);
    }

    private static bool IsPassthroughMethodCandidate(
        this IMethodSymbol method,
        INamedTypeSymbol encapsulated,
        INamedTypeSymbol subject,
        INamedTypeSymbol? equatable,
        ImmutableArray<string> interfaces)
    {
        if (method.IsStatic
         || method.AssociatedSymbol is not null
         || method.IsAccessor()
         || (subject.IsRecord && method.Name == nameof(ICloneable.Clone))
         || method.ReturnsByRef
         || method.ReturnsByRefReadonly
         || !method.HasSourceCompatibleTypeParameterConstraints())
        {
            return false;
        }

        if (method.ExplicitInterfaceImplementations.Length > 0)
        {
            return method.MethodKind == MethodKind.ExplicitInterfaceImplementation
                && method.ExplicitInterfaceImplementations.Any(implementation => implementation.CanForwardExplicitImplementation(encapsulated, subject, interfaces))
                && !IsDuplicateOfGeneratedMethod(method, encapsulated, subject, equatable);
        }

        return method.MethodKind == MethodKind.Ordinary
            && method.CanForwardOrdinaryMethod(encapsulated, subject, interfaces)
            && !IsDuplicateOfGeneratedMethod(method, encapsulated, subject, equatable);
    }

    private static bool CanForwardOrdinaryMethod(
        this IMethodSymbol method,
        INamedTypeSymbol encapsulated,
        INamedTypeSymbol subject,
        ImmutableArray<string> interfaces)
    {
        return method.DeclaredAccessibility.CanForward(encapsulated, subject)
            && (encapsulated.SpecialType == SpecialType.None || method.ImplementsForwardedInterfaceMember(encapsulated, interfaces));
    }

    private static bool ImplementsForwardedInterfaceMember(
        this IMethodSymbol method,
        INamedTypeSymbol encapsulated,
        ImmutableArray<string> interfaces)
    {
        foreach (INamedTypeSymbol @interface in encapsulated.AllInterfaces)
        {
            string interfaceName = @interface.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            if (!interfaces.Contains(interfaceName))
            {
                continue;
            }

            foreach (IMethodSymbol interfaceMember in @interface.GetMembers().OfType<IMethodSymbol>())
            {
                ISymbol? implementation = encapsulated.FindImplementationForInterfaceMember(interfaceMember);

                if (SymbolEqualityComparer.Default.Equals(method, implementation))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static ImmutableArray<string> GetTypeParameterConstraints(this IMethodSymbol method)
    {
        return method
            .TypeParameters
            .Select(GetTypeParameterConstraint)
            .Where(constraint => constraint.Length > 0)
            .ToImmutableArray();
    }
}
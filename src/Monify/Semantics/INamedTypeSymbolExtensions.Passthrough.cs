namespace Monify.Semantics;

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Monify.Model;

/// <summary>
/// Provides extensions relating to symbols.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private static bool CanForward(this Accessibility accessibility, INamedTypeSymbol encapsulated, INamedTypeSymbol subject)
    {
        return accessibility == Accessibility.Public
            || (accessibility == Accessibility.Internal
             && SymbolEqualityComparer.Default.Equals(encapsulated.ContainingAssembly, subject.ContainingAssembly));
    }

    private static PassthroughParameter CreatePassthroughParameter(IParameterSymbol parameter)
    {
        string argumentModifier = parameter.RefKind switch
        {
            RefKind.In => "in",
            RefKind.Out => "out",
            RefKind.Ref => "ref",
            _ => string.Empty,
        };

        string declarationModifier = parameter.RefKind switch
        {
            RefKind.In => "in",
            RefKind.Out => "out",
            RefKind.Ref => "ref",
            _ when parameter.IsParams => "params",
            _ => string.Empty,
        };

        return new PassthroughParameter
        {
            ArgumentModifier = argumentModifier,
            DeclarationModifier = declarationModifier,
            Name = parameter.Name,
            Type = parameter.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        };
    }

    private static string GetTypeParameterConstraint(ITypeParameterSymbol parameter)
    {
        ImmutableArray<string>.Builder constraints = ImmutableArray.CreateBuilder<string>();

        if (parameter.HasNotNullConstraint)
        {
            constraints.Add("notnull");
        }

        if (parameter.HasUnmanagedTypeConstraint)
        {
            constraints.Add("unmanaged");
        }
        else if (parameter.HasValueTypeConstraint)
        {
            constraints.Add("struct");
        }
        else if (parameter.HasReferenceTypeConstraint)
        {
            constraints.Add("class");
        }

        constraints.AddRange(parameter.ConstraintTypes.Select(type => type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)));

        if (parameter.HasConstructorConstraint)
        {
            constraints.Add("new()");
        }

        return constraints.Count == 0
            ? string.Empty
            : $"where {parameter.Name} : {string.Join(", ", constraints)}";
    }

    private static bool HasSourceCompatibleTypeParameterConstraints(this IMethodSymbol method)
    {
        return method
            .TypeParameters
            .SelectMany(parameter => parameter.ConstraintTypes)
            .All(IsSourceCompatibleConstraint);
    }

    private static bool IsSourceCompatibleConstraint(ITypeSymbol type)
    {
        if (type is ITypeParameterSymbol)
        {
            return true;
        }

        return type is INamedTypeSymbol named
            && (named.TypeKind == TypeKind.Interface
             || (named.TypeKind == TypeKind.Class && !named.IsSealed));
    }

    private static string ToSource(this Accessibility accessibility)
    {
        return accessibility switch
        {
            Accessibility.Internal => "internal",
            Accessibility.Public => "public",
            _ => string.Empty,
        };
    }
}
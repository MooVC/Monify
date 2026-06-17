namespace Monify.Semantics
{
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Monify.Model;
    using static Monify.Semantics.INamedTypeSymbolExtensions_Resources;
    using LanguageVersion = Microsoft.CodeAnalysis.CSharp.LanguageVersion;
    using LanguageVersionFacts = Microsoft.CodeAnalysis.CSharp.LanguageVersionFacts;

    /// <summary>
    /// Provides extensions relating to symbols.
    /// </summary>
    internal static partial class INamedTypeSymbolExtensions
    {
        private const int MinimumLanguageVersionForFunctionPointers = 900;
        private const int MinimumLanguageVersionForInParameters = 720;
        private const int MinimumLanguageVersionForNotNullConstraints = 800;
        private const int MinimumLanguageVersionForParamsCollections = 1300;
        private const int MinimumLanguageVersionForRefStructs = 720;
        private const int MinimumLanguageVersionForUnmanagedConstraints = 730;

        private static bool CanForward(this Accessibility accessibility, INamedTypeSymbol encapsulated, INamedTypeSymbol subject)
        {
            return accessibility == Accessibility.Public
                || (accessibility == Accessibility.Internal
                 && SymbolEqualityComparer.Default.Equals(encapsulated.ContainingAssembly, subject.ContainingAssembly));
        }

        private static PassthroughParameter CreatePassthroughParameter(IParameterSymbol parameter)
        {
            string argument = GetArgumentModifier(parameter);
            string declaration = GetDeclarationModifier(parameter);

            return new PassthroughParameter
            {
                ArgumentModifier = argument,
                DeclarationModifier = declaration,
                Name = parameter.Name,
                Type = parameter.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            };
        }

        private static string GetDeclarationModifier(IParameterSymbol parameter)
        {
            switch (parameter.RefKind)
            {
                case RefKind.In:

                    return "in";

                case RefKind.Out:

                    return "out";

                case RefKind.Ref:

                    return "ref";

                default:

                    return parameter.IsParams
                        ? "params"
                        : string.Empty;
            }
        }

        private static string GetArgumentModifier(IParameterSymbol parameter)
        {
            switch (parameter.RefKind)
            {
                case RefKind.In:

                    return "in";

                case RefKind.Out:

                    return "out";

                case RefKind.Ref:

                    return "ref";

                default:

                    return string.Empty;
            }
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
                : string.Format(TypeParameterConstraintSource, parameter.Name, string.Join(", ", constraints));
        }

        private static bool HasSourceCompatibleSignature(this IMethodSymbol method, LanguageVersion languageVersion)
        {
            return method.ReturnType.HasSourceCompatibleType(languageVersion)
                && method.Parameters.All(parameter => parameter.HasSourceCompatibleSignature(languageVersion));
        }

        private static bool HasSourceCompatibleSignature(this IPropertySymbol property, LanguageVersion languageVersion)
        {
            return property.Type.HasSourceCompatibleType(languageVersion)
                && property.Parameters.All(parameter => parameter.HasSourceCompatibleSignature(languageVersion));
        }

        private static bool HasSourceCompatibleSignature(this IParameterSymbol parameter, LanguageVersion languageVersion)
        {
            return IsSourceCompatibleRefKind(parameter.RefKind, languageVersion)
                && parameter.Type.HasSourceCompatibleType(languageVersion)
                && (!parameter.IsParams || parameter.Type is IArrayTypeSymbol || SupportsParamsCollections(languageVersion));
        }

        private static bool HasSourceCompatibleType(this ITypeSymbol type, LanguageVersion languageVersion)
        {
            if (type is IArrayTypeSymbol array)
            {
                return array.ElementType.HasSourceCompatibleType(languageVersion);
            }

            if (type is IFunctionPointerTypeSymbol)
            {
                return SupportsLanguageVersion(languageVersion, MinimumLanguageVersionForFunctionPointers);
            }

            if (type is IPointerTypeSymbol pointer)
            {
                return pointer.PointedAtType.HasSourceCompatibleType(languageVersion);
            }

            if (type.IsRefLikeType && !SupportsLanguageVersion(languageVersion, MinimumLanguageVersionForRefStructs))
            {
                return false;
            }

            var named = type as INamedTypeSymbol;

            return named is null
                || named.TypeArguments.All(argument => argument.HasSourceCompatibleType(languageVersion));
        }

        private static bool HasSourceCompatibleTypeParameterConstraints(this IMethodSymbol method, LanguageVersion languageVersion)
        {
            return method.TypeParameters.All(parameter => parameter.HasSourceCompatibleTypeParameterConstraints(languageVersion));
        }

        private static bool HasSourceCompatibleTypeParameterConstraints(this ITypeParameterSymbol parameter, LanguageVersion languageVersion)
        {
            if (parameter.HasNotNullConstraint
             && !SupportsLanguageVersion(languageVersion, MinimumLanguageVersionForNotNullConstraints))
            {
                return false;
            }

            if (parameter.HasUnmanagedTypeConstraint
             && !SupportsLanguageVersion(languageVersion, MinimumLanguageVersionForUnmanagedConstraints))
            {
                return false;
            }

            return parameter.ConstraintTypes.All(type => IsSourceCompatibleConstraint(type) && type.HasSourceCompatibleType(languageVersion));
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

        private static bool IsSourceCompatibleRefKind(RefKind kind, LanguageVersion languageVersion)
        {
            switch (kind)
            {
                case RefKind.In:
                    return SupportsLanguageVersion(languageVersion, MinimumLanguageVersionForInParameters);
                case RefKind.None:
                case RefKind.Out:
                case RefKind.Ref:
                    return true;
                default:
                    return false;
            }
        }

        private static bool SupportsLanguageVersion(LanguageVersion languageVersion, int minimum)
        {
            LanguageVersion effective = LanguageVersionFacts.MapSpecifiedToEffectiveVersion(languageVersion);

            return (int)effective >= minimum;
        }

        private static bool SupportsParamsCollections(LanguageVersion languageVersion)
        {
            return SupportsLanguageVersion(languageVersion, MinimumLanguageVersionForParamsCollections);
        }

        private static string ToSource(this Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Internal:
                    return "internal";
                case Accessibility.Public:
                    return "public";
                default:
                    return string.Empty;
            }
        }
    }
}
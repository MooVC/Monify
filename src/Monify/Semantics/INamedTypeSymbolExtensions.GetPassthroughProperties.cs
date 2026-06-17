namespace Monify.Semantics
{
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Monify.Model;
    using LanguageVersion = Microsoft.CodeAnalysis.CSharp.LanguageVersion;

    /// <summary>
    /// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
    /// </summary>
    internal static partial class INamedTypeSymbolExtensions
    {
        /// <summary>
        /// Identifies the properties declared by the <paramref name="encapsulated"/> type that should be forwarded.
        /// </summary>
        /// <param name="encapsulated">
        /// The encapsulated type whose properties are to be inspected.
        /// </param>
        /// <param name="interfaces">
        /// The interfaces that will be forwarded.
        /// </param>
        /// <param name="languageVersion">
        /// The language version used by the subject type.
        /// </param>
        /// <param name="subject">
        /// The subject type being generated.
        /// </param>
        /// <returns>
        /// The properties that should be forwarded to the encapsulated value.
        /// </returns>
        public static ImmutableArray<PassthroughProperty> GetPassthroughProperties(
            this INamedTypeSymbol encapsulated,
            ImmutableArray<string> interfaces,
            LanguageVersion languageVersion,
            INamedTypeSymbol subject)
        {
            return encapsulated
                .GetMembers()
                .OfType<IPropertySymbol>()
                .Where(property => property.IsPassthroughPropertyCandidate(encapsulated, subject, interfaces, languageVersion))
                .Where(property => !subject.HasPassthroughProperty(property))
                .Select(property => property.CreatePassthroughProperty(encapsulated, subject))
                .OrderBy(property => property.ExplicitInterface)
                .ThenBy(property => property.Name)
                .ThenBy(property => string.Join(",", property.Parameters.Select(parameter => parameter.Type)))
                .ToImmutableArray();
        }

        private static PassthroughProperty CreatePassthroughProperty(this IPropertySymbol property, INamedTypeSymbol encapsulated, INamedTypeSymbol subject)
        {
            IPropertySymbol explicitImplementation = property.ExplicitInterfaceImplementations.FirstOrDefault();
            IPropertySymbol declaration = explicitImplementation ?? property;

            return new PassthroughProperty
            {
                Accessibility = explicitImplementation is null
                    ? property.DeclaredAccessibility.ToSource()
                    : string.Empty,
                ExplicitInterface = explicitImplementation?.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? string.Empty,
                GetterAccessibility = explicitImplementation is null
                    ? GetAccessorAccessibility(property, property.GetMethod, encapsulated, subject)
                    : string.Empty,
                HasGetter = explicitImplementation is null
                    ? property.GetMethod?.DeclaredAccessibility.CanForward(encapsulated, subject) is true
                    : declaration.GetMethod is object,
                HasSetter = explicitImplementation is null
                    ? property.SetMethod?.DeclaredAccessibility.CanForward(encapsulated, subject) is true
                    : declaration.SetMethod is object,
                IsIndexer = declaration.IsIndexer,
                Name = declaration.Name,
                Parameters = declaration.Parameters.Select(CreatePassthroughParameter).ToImmutableArray(),
                SetterAccessibility = explicitImplementation is null
                    ? GetAccessorAccessibility(property, property.SetMethod, encapsulated, subject)
                    : string.Empty,
                Type = declaration.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            };
        }

        private static string GetAccessorAccessibility(
            IPropertySymbol property,
            IMethodSymbol accessor,
            INamedTypeSymbol encapsulated,
            INamedTypeSymbol subject)
        {
            if (accessor is null
             || !accessor.DeclaredAccessibility.CanForward(encapsulated, subject)
             || accessor.DeclaredAccessibility == property.DeclaredAccessibility)
            {
                return string.Empty;
            }

            return accessor.DeclaredAccessibility.ToSource();
        }

        private static bool HasPassthroughProperty(this INamedTypeSymbol subject, IPropertySymbol property)
        {
            IPropertySymbol explicitImplementation = property.ExplicitInterfaceImplementations.FirstOrDefault();

            foreach (IPropertySymbol candidate in subject.GetMembers().OfType<IPropertySymbol>())
            {
                if (explicitImplementation is object
                 && candidate.ExplicitInterfaceImplementations.Any(implementation => SymbolEqualityComparer.Default.Equals(implementation, explicitImplementation)))
                {
                    return true;
                }

                if (explicitImplementation is null
                 && candidate.Name == property.Name
                 && candidate.Parameters.Length == property.Parameters.Length
                 && candidate.Parameters.Zip(property.Parameters, IsEquivalentParameter).All(isEquivalent => isEquivalent))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsPassthroughPropertyCandidate(
            this IPropertySymbol property,
            INamedTypeSymbol encapsulated,
            INamedTypeSymbol subject,
            ImmutableArray<string> interfaces,
            LanguageVersion languageVersion)
        {
            if (property.IsStatic
             || property.ReturnsByRef
             || property.ReturnsByRefReadonly
             || !property.HasSourceCompatibleSignature(languageVersion))
            {
                return false;
            }

            if (property.ExplicitInterfaceImplementations.Length > 0)
            {
                return property.ExplicitInterfaceImplementations.Any(implementation => implementation.CanForwardExplicitImplementation(encapsulated, subject, interfaces));
            }

            bool canForwardGetter = property.GetMethod?.DeclaredAccessibility.CanForward(encapsulated, subject) is true;
            bool canForwardSetter = property.SetMethod?.DeclaredAccessibility.CanForward(encapsulated, subject) is true;

            return property.DeclaredAccessibility.CanForward(encapsulated, subject)
                && (canForwardGetter || canForwardSetter);
        }

        private static bool CanForwardExplicitImplementation(
            this IPropertySymbol implementation,
            INamedTypeSymbol encapsulated,
            INamedTypeSymbol subject,
            ImmutableArray<string> interfaces)
        {
            string @interface = implementation.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            return implementation.ContainingType.DeclaredAccessibility.CanForward(encapsulated, subject)
                && interfaces.Contains(@interface);
        }
    }
}
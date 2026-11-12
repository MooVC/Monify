namespace Monify.Semantics
{
    using System;
    using System.Collections.Generic;
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
        /// Maps the required Semantics from the <paramref name="subject"/> and places it within an instance of <see cref="Subject"/>.
        /// </summary>
        /// <param name="subject">
        /// The subject from which the semantics are identified.
        /// </param>
        /// <param name="compilation">
        /// The <see cref="Compilation"/> used to source the symbol for <see cref="IEquatable{T}"/>.
        /// </param>
        /// <param name="model">
        /// The semantic model based for the current execution context.
        /// </param>
        /// <param name="nesting">
        /// The declaration syntax for the parents of the <paramref name="syntax"/>.
        /// </param>
        /// <param name="value">
        /// The type of the value encapsulated by the <see cref="Subject"/>.
        /// </param>
        /// <param name="passthrough">
        /// A value indicating whether passthrough interfaces, methods and properties should be generated.
        /// </param>
        /// <param name="debuggerDisplay">
        /// A value indicating whether the debugger display should be generated.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Subject"/> containing the required semantics.
        /// </returns>
        /// <remarks>
        /// If the declaration associated with the type cannot be determined, the method will return <see langword="null" />.
        /// </remarks>
        public static Subject ToSubject(
            this INamedTypeSymbol subject,
            Compilation compilation,
            SemanticModel model,
            ImmutableArray<Nesting> nesting,
            ITypeSymbol value,
            bool debuggerDisplay = true,
            bool passthrough = true)
        {
            string @namespace = subject.ContainingNamespace.IsGlobalNamespace
               ? string.Empty
               : subject.ContainingNamespace.ToDisplayString();

            string declaration = subject.GetDeclaration();

            if (declaration is null || subject.Equals(value, SymbolEqualityComparer.IncludeNullability))
            {
                return default;
            }

            _ = subject.IsStateless(value, out bool hasFieldForEncapsulatedValue);

            string qualification = subject.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
            ImmutableArray<Encapsulated> encapsulated = subject.GetEncapsulated(compilation, model, value, passthrough);
            RemoveDuplicateOperators(encapsulated, qualification);

            return new Subject()
                .CanOverrideEquals(subject.CanOverrideEquals())
                .CanOverrideGetHashCode(subject.CanOverrideGetHashCode())
                .CanOverrideToString(subject.CanOverrideToString())
                .WithDeclaration(declaration)
                .WithEncapsulatedValues(encapsulated.ToArray())
                .GenerateDebuggerDisplay(debuggerDisplay && !subject.HasDebuggerDisplay())
                .HasEqualityOperator(subject.HasEqualityOperator())
                .HasEquatable(subject.HasEquatable())
                .HasField(hasFieldForEncapsulatedValue)
                .HasInequalityOperator(subject.HasInequalityOperator())
                .IsEquatable(subject.IsEquatable(compilation))
                .WithName(subject.Name)
                .WithNamespace(@namespace)
                .WithNesting(nesting.ToArray())
                .WithQualification(qualification)
                .WithQualifiedName(subject.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
                .WithSerialization(subject.ToSerialization(compilation));
        }

        private static void RemoveDuplicateOperators(ImmutableArray<Encapsulated> encapsulated, string qualification)
        {
            var binarySignatures = new HashSet<(string Operator, string Left, string Right)>();
            var unarySignatures = new HashSet<string>(StringComparer.Ordinal);

            foreach (Encapsulated value in encapsulated)
            {
                value.BinaryOperators = value.BinaryOperators
                    .Where(binary => binarySignatures.Add((
                        binary.Operator,
                        binary.IsLeftSubject ? qualification : binary.Left,
                        binary.IsRightSubject ? qualification : binary.Right)))
                    .ToImmutableArray();
                value.UnaryOperators = value.UnaryOperators
                    .Where(unary => unarySignatures.Add(unary.Operator))
                    .ToImmutableArray();
            }
        }
    }
}
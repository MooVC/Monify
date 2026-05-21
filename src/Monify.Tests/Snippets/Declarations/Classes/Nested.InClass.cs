namespace Monify.Snippets.Declarations.Classes;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Declarations.Attributes.Annotations;
using static Monify.Snippets.Declarations.BuiltInInt32Operators;

internal static partial class Nested
{
    public static partial class InClass
    {
        public static readonly Snippets NonNullable = new(
            [Generic, NonGeneric],
            Declarations.Main,
            [
                .. CreateBinaryOperators(
                    "Monify.Testing.Classes",
                    "Monify.Testing.Classes.Outter.Inner",
                    [
                        new Nesting("partial class", "Outter<T>"),
                    ],
                    "sealed partial class",
                    "Inner",
                    supportsNullableReferenceTypes: false),
                Expected.NonNullable.ConstructorForEncapsulatedValue,
                Expected.NonNullable.ConversionFromValue,
                Expected.NonNullable.ConversionToValue,
                Expected.NonNullable.EqualityOperatorForSelf,
                Expected.NonNullable.EqualityOperatorForValue,
                Expected.NonNullable.Equals,
                Expected.NonNullable.EquatableForSelf,
                Expected.NonNullable.EquatableToSelf,
                Expected.NonNullable.EquatableForValue,
                Expected.NonNullable.EquatableToValue,
                Expected.NonNullable.FieldForEncapsulatedValue,
                Expected.NonNullable.GetHashCode,
                Expected.NonNullable.InequalityOperatorForSelf,
                Expected.NonNullable.InequalityOperatorForValue,
                Expected.NonNullable.ToString,
                .. CreateUnaryOperators(
                    "Monify.Testing.Classes",
                    "Monify.Testing.Classes.Outter.Inner",
                    [
                        new Nesting("partial class", "Outter<T>"),
                    ],
                    "sealed partial class",
                    "Inner",
                    supportsNullableReferenceTypes: false),
            ],
            [
                new(Expected.NonNullable.ConstructorForEncapsulatedValue.Content, Extensions.HasConstructorForEncapsulatedValue),
                new(Expected.NonNullable.ConversionFromValue.Content, Extensions.HasConversionFrom),
                new(Expected.NonNullable.ConversionToValue.Content, Extensions.HasConversionTo),
                new(Expected.NonNullable.EqualityOperatorForSelf.Content, Extensions.HasEqualityOperatorForSelf),
                new(Expected.NonNullable.EqualityOperatorForValue.Content, Extensions.HasEqualityOperatorForValue),
                new(Expected.NonNullable.Equals.Content, Extensions.HasEqualsOverride),
                new(Expected.NonNullable.EquatableForSelf.Content, Extensions.HasEquatableForSelf),
                new(Expected.NonNullable.EquatableToSelf.Content, Extensions.IsEquatableToSelf),
                new(Expected.NonNullable.EquatableForValue.Content, Extensions.HasEquatableForValue),
                new(Expected.NonNullable.EquatableToValue.Content, Extensions.IsEquatableToValue),
                new(Expected.NonNullable.FieldForEncapsulatedValue.Content, Extensions.HasFieldForEncapsulatedValue),
                new(Expected.NonNullable.GetHashCode.Content, Extensions.HasGetHashCodeOverride),
                new(Expected.NonNullable.InequalityOperatorForSelf.Content, Extensions.HasInequalityOperatorForSelf),
                new(Expected.NonNullable.InequalityOperatorForValue.Content, Extensions.HasInequalityOperatorForValue),
                new(Expected.NonNullable.ToString.Content, Extensions.HasToStringOverride),
            ],
            nameof(NonNullable),
            Maximum: LanguageVersion.CSharp7_3);

        public static readonly Snippets Nullable = new(
            [Generic, NonGeneric],
            Declarations.Main,
            [
                .. CreateBinaryOperators(
                    "Monify.Testing.Classes",
                    "Monify.Testing.Classes.Outter.Inner",
                    [
                        new Nesting("partial class", "Outter<T>"),
                    ],
                    "sealed partial class",
                    "Inner",
                    supportsNullableReferenceTypes: true),
                Expected.Nullable.ConstructorForEncapsulatedValue,
                Expected.Nullable.ConversionFromValue,
                Expected.Nullable.ConversionToValue,
                Expected.Nullable.EqualityOperatorForSelf,
                Expected.Nullable.EqualityOperatorForValue,
                Expected.Nullable.Equals,
                Expected.Nullable.EquatableForSelf,
                Expected.Nullable.EquatableToSelf,
                Expected.Nullable.EquatableForValue,
                Expected.Nullable.EquatableToValue,
                Expected.Nullable.FieldForEncapsulatedValue,
                Expected.Nullable.GetHashCode,
                Expected.Nullable.InequalityOperatorForSelf,
                Expected.Nullable.InequalityOperatorForValue,
                Expected.Nullable.ToString,
                .. CreateUnaryOperators(
                    "Monify.Testing.Classes",
                    "Monify.Testing.Classes.Outter.Inner",
                    [
                        new Nesting("partial class", "Outter<T>"),
                    ],
                    "sealed partial class",
                    "Inner",
                    supportsNullableReferenceTypes: true),
            ],
            [
                new(Expected.Nullable.ConstructorForEncapsulatedValue.Content, Extensions.HasConstructorForEncapsulatedValue),
                new(Expected.Nullable.ConversionFromValue.Content, Extensions.HasConversionFrom),
                new(Expected.Nullable.ConversionToValue.Content, Extensions.HasConversionTo),
                new(Expected.Nullable.EqualityOperatorForSelf.Content, Extensions.HasEqualityOperatorForSelf),
                new(Expected.Nullable.EqualityOperatorForValue.Content, Extensions.HasEqualityOperatorForValue),
                new(Expected.Nullable.Equals.Content, Extensions.HasEqualsOverride),
                new(Expected.Nullable.EquatableForSelf.Content, Extensions.HasEquatableForSelf),
                new(Expected.Nullable.EquatableToSelf.Content, Extensions.IsEquatableToSelf),
                new(Expected.Nullable.EquatableForValue.Content, Extensions.HasEquatableForValue),
                new(Expected.Nullable.EquatableToValue.Content, Extensions.IsEquatableToValue),
                new(Expected.Nullable.FieldForEncapsulatedValue.Content, Extensions.HasFieldForEncapsulatedValue),
                new(Expected.Nullable.GetHashCode.Content, Extensions.HasGetHashCodeOverride),
                new(Expected.Nullable.InequalityOperatorForSelf.Content, Extensions.HasInequalityOperatorForSelf),
                new(Expected.Nullable.InequalityOperatorForValue.Content, Extensions.HasInequalityOperatorForValue),
                new(Expected.Nullable.ToString.Content, Extensions.HasToStringOverride),
            ],
            nameof(Nullable),
            Minimum: LanguageVersion.CSharp8);
    }
}
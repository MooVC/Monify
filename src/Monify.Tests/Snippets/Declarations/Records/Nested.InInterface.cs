namespace Monify.Snippets.Declarations.Records;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Declarations.Attributes.Annotations;
using static Monify.Snippets.Declarations.BuiltInInt32Operators;

internal static partial class Nested
{
    public static partial class InInterface
    {
        public static readonly Snippets NonNullable = new(
            [Generic, NonGeneric],
            Declarations.Main,
            [
                .. CreateBinaryOperators(
                    "Monify.Testing.Records",
                    "Monify.Testing.Records.IOutter.Inner",
                    [
                        new Nesting("partial interface", "IOutter<T>"),
                    ],
                    "sealed partial record",
                    "Inner",
                    supportsNullableReferenceTypes: false),
                Expected.NonNullable.ConstructorForEncapsulatedValue,
                Expected.NonNullable.ConversionFromValue,
                Expected.NonNullable.ConversionToValue,
                Expected.NonNullable.EqualityOperatorForValue,
                Expected.NonNullable.EquatableForValue,
                Expected.NonNullable.EquatableToValue,
                Expected.NonNullable.FieldForEncapsulatedValue,
                Expected.NonNullable.InequalityOperatorForValue,
                .. CreateUnaryOperators(
                    "Monify.Testing.Records",
                    "Monify.Testing.Records.IOutter.Inner",
                    [
                        new Nesting("partial interface", "IOutter<T>"),
                    ],
                    "sealed partial record",
                    "Inner",
                    supportsNullableReferenceTypes: false),
            ],
            [
                new(Expected.NonNullable.ConstructorForEncapsulatedValue.Content, Extensions.HasConstructorForEncapsulatedValue),
                new(Expected.NonNullable.ConversionFromValue.Content, Extensions.HasConversionFrom),
                new(Expected.NonNullable.ConversionToValue.Content, Extensions.HasConversionTo),
                new(Expected.NonNullable.EqualityOperatorForValue.Content, Extensions.HasEqualityOperatorForValue),
                new(Expected.NonNullable.EquatableForValue.Content, Extensions.HasEquatableForValue),
                new(Expected.NonNullable.EquatableToValue.Content, Extensions.IsEquatableToValue),
                new(Expected.NonNullable.FieldForEncapsulatedValue.Content, Extensions.HasFieldForEncapsulatedValue),
                new(Expected.NonNullable.InequalityOperatorForValue.Content, Extensions.HasInequalityOperatorForValue),
            ],
            nameof(NonNullable),
            Maximum: LanguageVersion.CSharp7_3);

        public static readonly Snippets Nullable = new(
            [Generic, NonGeneric],
            Declarations.Main,
            [
                .. CreateBinaryOperators(
                    "Monify.Testing.Records",
                    "Monify.Testing.Records.IOutter.Inner",
                    [
                        new Nesting("partial interface", "IOutter<T>"),
                    ],
                    "sealed partial record",
                    "Inner",
                    supportsNullableReferenceTypes: true),
                Expected.Nullable.ConstructorForEncapsulatedValue,
                Expected.Nullable.ConversionFromValue,
                Expected.Nullable.ConversionToValue,
                Expected.Nullable.EqualityOperatorForValue,
                Expected.Nullable.EquatableForValue,
                Expected.Nullable.EquatableToValue,
                Expected.Nullable.FieldForEncapsulatedValue,
                Expected.Nullable.InequalityOperatorForValue,
                .. CreateUnaryOperators(
                    "Monify.Testing.Records",
                    "Monify.Testing.Records.IOutter.Inner",
                    [
                        new Nesting("partial interface", "IOutter<T>"),
                    ],
                    "sealed partial record",
                    "Inner",
                    supportsNullableReferenceTypes: true),
            ],
            [
                new(Expected.Nullable.ConstructorForEncapsulatedValue.Content, Extensions.HasConstructorForEncapsulatedValue),
                new(Expected.Nullable.ConversionFromValue.Content, Extensions.HasConversionFrom),
                new(Expected.Nullable.ConversionToValue.Content, Extensions.HasConversionTo),
                new(Expected.Nullable.EqualityOperatorForValue.Content, Extensions.HasEqualityOperatorForValue),
                new(Expected.Nullable.EquatableForValue.Content, Extensions.HasEquatableForValue),
                new(Expected.Nullable.EquatableToValue.Content, Extensions.IsEquatableToValue),
                new(Expected.Nullable.FieldForEncapsulatedValue.Content, Extensions.HasFieldForEncapsulatedValue),
                new(Expected.Nullable.InequalityOperatorForValue.Content, Extensions.HasInequalityOperatorForValue),
            ],
            nameof(Nullable),
            Minimum: LanguageVersion.CSharp8);
    }
}
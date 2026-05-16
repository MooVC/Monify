namespace Monify.Snippets.Declarations.Records;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Declarations.Attributes.Annotations;

internal static partial class Simple
{
    public static readonly Snippets NonNullable = new(
        [Generic, NonGeneric],
        Declarations.Main,
        [
            Expected.NonNullable.ConstructorForEncapsulatedValue,
            Expected.NonNullable.ConversionFromValue,
            Expected.NonNullable.ConversionToValue,
            Expected.NonNullable.EqualityOperatorForValue,
            Expected.NonNullable.EquatableForValue,
            Expected.NonNullable.EquatableToValue,
            Expected.NonNullable.FieldForEncapsulatedValue,
            Expected.NonNullable.InequalityOperatorForValue,
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
            Expected.Nullable.ConstructorForEncapsulatedValue,
            Expected.Nullable.ConversionFromValue,
            Expected.Nullable.ConversionToValue,
            Expected.Nullable.EqualityOperatorForValue,
            Expected.Nullable.EquatableForValue,
            Expected.Nullable.EquatableToValue,
            Expected.Nullable.FieldForEncapsulatedValue,
            Expected.Nullable.InequalityOperatorForValue,
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
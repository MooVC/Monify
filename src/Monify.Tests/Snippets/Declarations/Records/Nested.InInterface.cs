namespace Monify.Snippets.Declarations.Records;

using static Monify.Snippets.Declarations.Attributes.Annotations;

internal static partial class Nested
{
    public static partial class InInterface
    {
        public static readonly Snippets Declaration = new(
            [Generic, NonGeneric],
            Declarations.Main,
            [
                Expected.ConstructorForEncapsulatedValue,
                Expected.ConversionFromValue,
                Expected.ConversionToValue,
                Expected.EqualityOperatorForValue,
                Expected.EquatableForValue,
                Expected.EquatableToValue,
                Expected.FieldForEncapsulatedValue,
                Expected.InequalityOperatorForValue,
            ],
            [
                new(Expected.ConstructorForEncapsulatedValue.Content, Extensions.HasConstructorForEncapsulatedValue),
                new(Expected.ConversionFromValue.Content, Extensions.HasConversionFrom),
                new(Expected.ConversionToValue.Content, Extensions.HasConversionTo),
                new(Expected.EqualityOperatorForValue.Content, Extensions.HasEqualityOperatorForValue),
                new(Expected.EquatableForValue.Content, Extensions.HasEquatableForValue),
                new(Expected.EquatableToValue.Content, Extensions.IsEquatableToValue),
                new(Expected.FieldForEncapsulatedValue.Content, Extensions.HasFieldForEncapsulatedValue),
                new(Expected.InequalityOperatorForValue.Content, Extensions.HasInequalityOperatorForValue),
            ],
            nameof(InInterface));
    }
}
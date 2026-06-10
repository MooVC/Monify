namespace Monify.Semantics
{
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
    /// </summary>
    internal static partial class INamedTypeSymbolExtensions
    {
        private static SpecialType GetBuiltInPromotedSpecialType(this SpecialType specialType)
        {
            return specialType.IsSmallBuiltInIntegral()
                ? SpecialType.System_Int32
                : specialType;
        }

        private static bool IsBuiltInIntegral(this SpecialType specialType)
        {
            return specialType == SpecialType.System_Byte
                || specialType == SpecialType.System_Char
                || specialType == SpecialType.System_Int16
                || specialType == SpecialType.System_Int32
                || specialType == SpecialType.System_Int64
                || specialType == SpecialType.System_SByte
                || specialType == SpecialType.System_UInt16
                || specialType == SpecialType.System_UInt32
                || specialType == SpecialType.System_UInt64;
        }

        private static bool IsBuiltInNumeric(this SpecialType specialType)
        {
            return specialType.IsBuiltInIntegral()
                || specialType == SpecialType.System_Decimal
                || specialType == SpecialType.System_Double
                || specialType == SpecialType.System_Single;
        }

        private static bool IsSmallBuiltInIntegral(this SpecialType specialType)
        {
            return specialType == SpecialType.System_Byte
                || specialType == SpecialType.System_Char
                || specialType == SpecialType.System_Int16
                || specialType == SpecialType.System_SByte
                || specialType == SpecialType.System_UInt16;
        }
    }
}
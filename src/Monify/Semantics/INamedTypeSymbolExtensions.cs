namespace Monify.Semantics;

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
        return specialType is SpecialType.System_Byte
            or SpecialType.System_Char
            or SpecialType.System_Int16
            or SpecialType.System_Int32
            or SpecialType.System_Int64
            or SpecialType.System_SByte
            or SpecialType.System_UInt16
            or SpecialType.System_UInt32
            or SpecialType.System_UInt64;
    }

    private static bool IsBuiltInNumeric(this SpecialType specialType)
    {
        return specialType.IsBuiltInIntegral()
            || specialType is SpecialType.System_Decimal
                or SpecialType.System_Double
                or SpecialType.System_Single;
    }

    private static bool IsSmallBuiltInIntegral(this SpecialType specialType)
    {
        return specialType is SpecialType.System_Byte
            or SpecialType.System_Char
            or SpecialType.System_Int16
            or SpecialType.System_SByte
            or SpecialType.System_UInt16;
    }
}
namespace Monify.Semantics;

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
    private const int ExpectedParametersForBinaryOperator = 2;

    private static readonly IReadOnlyDictionary<string, string> _supportedBinaryOperators = new Dictionary<string, string>
    {
        ["op_Addition"] = "+",
        ["op_BitwiseAnd"] = "&",
        ["op_BitwiseOr"] = "|",
        ["op_Division"] = "/",
        ["op_ExclusiveOr"] = "^",
        ["op_GreaterThan"] = ">",
        ["op_GreaterThanOrEqual"] = ">=",
        ["op_LeftShift"] = "<<",
        ["op_LessThan"] = "<",
        ["op_LessThanOrEqual"] = "<=",
        ["op_Modulus"] = "%",
        ["op_Multiply"] = "*",
        ["op_RightShift"] = ">>",
        ["op_Subtraction"] = "-",
    };

    /// <summary>
    /// Identifies the binary operators declared by the <paramref name="encapsulated"/> type.
    /// </summary>
    /// <param name="encapsulated">The encapsulated type whose operators are to be inspected.</param>
    /// <param name="subject">The subject type being generated.</param>
    /// <returns>The binary operators that should be forwarded to the subject.</returns>
    public static ImmutableArray<BinaryOperator> GetBinaryOperators(this INamedTypeSymbol encapsulated, INamedTypeSymbol subject)
    {
        ImmutableArray<BinaryOperator>.Builder binaryOperators = ImmutableArray.CreateBuilder<BinaryOperator>();

        foreach (IMethodSymbol method in encapsulated.GetMembers().OfType<IMethodSymbol>())
        {
            if (method.MethodKind != MethodKind.UserDefinedOperator || method.Parameters.Length != ExpectedParametersForBinaryOperator)
            {
                continue;
            }

            if (!_supportedBinaryOperators.TryGetValue(method.Name, out string symbol))
            {
                continue;
            }

            bool isLeftSubject = method.Parameters[0].Type.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);
            bool isRightSubject = method.Parameters[1].Type.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);

            if (!isLeftSubject && !isRightSubject)
            {
                continue;
            }

            ITypeSymbol leftType = isLeftSubject ? subject : method.Parameters[0].Type;
            ITypeSymbol rightType = isRightSubject ? subject : method.Parameters[1].Type;
            bool isReturnSubject = method.ReturnType.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);
            ITypeSymbol returnType = isReturnSubject ? subject : method.ReturnType;

            if (subject.HasBinaryOperator(method.Name, leftType, rightType))
            {
                continue;
            }

            binaryOperators.Add(new BinaryOperator
            {
                IsLeftSubject = isLeftSubject,
                IsReturnSubject = isReturnSubject,
                IsRightSubject = isRightSubject,
                Left = leftType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Operator = method.Name,
                Return = returnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Right = rightType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Symbol = symbol,
            });
        }

        return binaryOperators
            .OrderBy(@operator => @operator.Operator)
            .ThenBy(@operator => @operator.Left)
            .ThenBy(@operator => @operator.Right)
            .ThenBy(@operator => @operator.Return)
            .ToImmutableArray();
    }
}
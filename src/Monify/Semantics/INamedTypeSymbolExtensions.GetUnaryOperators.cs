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
    private const int ExpectedParametersForUnaryOperator = 1;

    private static readonly IReadOnlyDictionary<string, string> _supportedUnaryOperators = new Dictionary<string, string>
    {
        ["op_Decrement"] = "--",
        ["op_Increment"] = "++",
        ["op_LogicalNot"] = "!",
        ["op_OnesComplement"] = "~",
        ["op_True"] = "true",
        ["op_False"] = "false",
        ["op_UnaryNegation"] = "-",
        ["op_UnaryPlus"] = "+",
    };

    /// <summary>
    /// Identifies the unary operators declared by the <paramref name="encapsulated"/> type.
    /// </summary>
    /// <param name="encapsulated">The encapsulated type whose operators are to be inspected.</param>
    /// <param name="compilation">The compilation used to resolve built-in operator type symbols.</param>
    /// <param name="subject">The subject type being generated.</param>
    /// <returns>The unary operators that should be forwarded to the subject.</returns>
    public static ImmutableArray<UnaryOperator> GetUnaryOperators(
        this INamedTypeSymbol encapsulated,
        Compilation compilation,
        INamedTypeSymbol subject)
    {
        ImmutableArray<UnaryOperator>.Builder unaryOperators = ImmutableArray.CreateBuilder<UnaryOperator>();

        AddBuiltInUnaryOperators(unaryOperators, compilation, encapsulated, subject);

        foreach (IMethodSymbol method in encapsulated.GetMembers().OfType<IMethodSymbol>())
        {
            if (!method.IsUnaryOperatorCandidate(encapsulated, out string symbol))
            {
                continue;
            }

            bool isReturnSubject = method.ReturnType.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);

            ITypeSymbol returnType = isReturnSubject
                ? subject
                : method.ReturnType;

            if (subject.HasUnaryOperator(method.Name, returnType)
             || ContainsUnaryOperator(unaryOperators, method.Name, returnType))
            {
                continue;
            }

            unaryOperators.Add(new UnaryOperator
            {
                IsReturnSubject = isReturnSubject,
                Operator = method.Name,
                Return = returnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Symbol = symbol,
            });
        }

        return unaryOperators
            .OrderBy(@operator => @operator.Operator)
            .ThenBy(@operator => @operator.Return)
            .ToImmutableArray();
    }

    private static void AddBuiltInUnaryOperator(
        ImmutableArray<UnaryOperator>.Builder unaryOperators,
        INamedTypeSymbol subject,
        string operatorName,
        string symbol,
        ITypeSymbol returnType,
        bool isReturnSubject)
    {
        if (subject.HasUnaryOperator(operatorName, returnType)
         || ContainsUnaryOperator(unaryOperators, operatorName, returnType))
        {
            return;
        }

        unaryOperators.Add(new UnaryOperator
        {
            IsReturnSubject = isReturnSubject,
            Operator = operatorName,
            Return = returnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            Symbol = symbol,
        });
    }

    private static void AddBuiltInUnaryOperator(
        ImmutableArray<UnaryOperator>.Builder unaryOperators,
        INamedTypeSymbol encapsulated,
        INamedTypeSymbol subject,
        string operatorName,
        string symbol,
        ITypeSymbol returnType)
    {
        bool isReturnSubject = returnType.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);

        AddBuiltInUnaryOperator(
            unaryOperators,
            subject,
            operatorName,
            symbol,
            isReturnSubject ? subject : returnType,
            isReturnSubject);
    }

    private static void AddBuiltInBooleanUnaryOperators(
        ImmutableArray<UnaryOperator>.Builder unaryOperators,
        Compilation compilation,
        INamedTypeSymbol encapsulated,
        INamedTypeSymbol subject)
    {
        INamedTypeSymbol boolean = compilation.GetSpecialType(SpecialType.System_Boolean);

        AddBuiltInUnaryOperator(unaryOperators, encapsulated, subject, "op_LogicalNot", "!", encapsulated);
        AddBuiltInUnaryOperator(unaryOperators, subject, "op_False", "false", boolean, isReturnSubject: false);
        AddBuiltInUnaryOperator(unaryOperators, subject, "op_True", "true", boolean, isReturnSubject: false);
    }

    private static void AddBuiltInUnaryOperators(
        ImmutableArray<UnaryOperator>.Builder unaryOperators,
        Compilation compilation,
        INamedTypeSymbol encapsulated,
        INamedTypeSymbol subject)
    {
        if (encapsulated.SpecialType == SpecialType.System_Boolean)
        {
            AddBuiltInBooleanUnaryOperators(unaryOperators, compilation, encapsulated, subject);

            return;
        }

        if (encapsulated.SpecialType.IsBuiltInNumeric())
        {
            AddBuiltInNumericUnaryOperators(unaryOperators, compilation, encapsulated, subject);
        }
    }

    private static void AddBuiltInNumericUnaryOperators(
        ImmutableArray<UnaryOperator>.Builder unaryOperators,
        Compilation compilation,
        INamedTypeSymbol encapsulated,
        INamedTypeSymbol subject)
    {
        SpecialType promotedType = encapsulated.SpecialType.GetBuiltInPromotedSpecialType();
        INamedTypeSymbol resultType = compilation.GetSpecialType(promotedType);

        AddBuiltInUnaryOperator(unaryOperators, encapsulated, subject, "op_Decrement", "--", encapsulated);
        AddBuiltInUnaryOperator(unaryOperators, encapsulated, subject, "op_Increment", "++", encapsulated);
        AddBuiltInUnaryOperator(unaryOperators, encapsulated, subject, "op_UnaryPlus", "+", resultType);

        if (encapsulated.SpecialType.IsUnaryNegationSupported())
        {
            AddBuiltInUnaryOperator(unaryOperators, encapsulated, subject, "op_UnaryNegation", "-", resultType);
        }

        if (encapsulated.SpecialType.IsBuiltInIntegral())
        {
            AddBuiltInUnaryOperator(unaryOperators, encapsulated, subject, "op_OnesComplement", "~", resultType);
        }
    }

    private static bool IsUnaryNegationSupported(this SpecialType specialType)
    {
        return specialType.IsSmallBuiltInIntegral()
            || specialType is SpecialType.System_Decimal
                or SpecialType.System_Double
                or SpecialType.System_Int32
                or SpecialType.System_Int64
                or SpecialType.System_Single;
    }

    private static bool ContainsUnaryOperator(ImmutableArray<UnaryOperator>.Builder unaryOperators, string operatorName, ITypeSymbol returnType)
    {
        string type = returnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return unaryOperators.Any(@operator => @operator.Operator == operatorName
            && @operator.Return == type);
    }

    private static bool IsUnaryOperatorCandidate(this IMethodSymbol method, INamedTypeSymbol encapsulated, out string symbol)
    {
        bool isOperator = method.MethodKind == MethodKind.UserDefinedOperator || method.MethodKind == MethodKind.BuiltinOperator;
        bool hasExpectedParameters = method.Parameters.Length == ExpectedParametersForUnaryOperator;

        symbol = string.Empty;

        return isOperator
            && hasExpectedParameters
            && _supportedUnaryOperators.TryGetValue(method.Name, out symbol)
            && method.Parameters[0].Type.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);
    }
}
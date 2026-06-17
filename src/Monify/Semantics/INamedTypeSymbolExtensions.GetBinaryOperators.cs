namespace Monify.Semantics
{
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

        private static readonly (string Operator, string Symbol)[] _arithmeticBinaryOperators =
        {
            (Operator: "op_Addition", Symbol: "+"),
            (Operator: "op_Division", Symbol: "/"),
            (Operator: "op_Modulus", Symbol: "%"),
            (Operator: "op_Multiply", Symbol: "*"),
            (Operator: "op_Subtraction", Symbol: "-"),
        };

        private static readonly (string Operator, string Symbol)[] _bitwiseBinaryOperators =
        {
            (Operator: "op_BitwiseAnd", Symbol: "&"),
            (Operator: "op_BitwiseOr", Symbol: "|"),
            (Operator: "op_ExclusiveOr", Symbol: "^"),
        };

        private static readonly (string Operator, string Symbol)[] _comparisonBinaryOperators =
        {
            (Operator: "op_GreaterThan", Symbol: ">"),
            (Operator: "op_GreaterThanOrEqual", Symbol: ">="),
            (Operator: "op_LessThan", Symbol: "<"),
            (Operator: "op_LessThanOrEqual", Symbol: "<="),
        };

        private static readonly (string Operator, string Symbol)[] _shiftBinaryOperators =
        {
            (Operator: "op_LeftShift", Symbol: "<<"),
            (Operator: "op_RightShift", Symbol: ">>"),
        };

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
        /// <param name="compilation">The compilation used to resolve built-in operator type symbols.</param>
        /// <param name="subject">The subject type being generated.</param>
        /// <returns>The binary operators that should be forwarded to the subject.</returns>
        public static ImmutableArray<BinaryOperator> GetBinaryOperators(
            this INamedTypeSymbol encapsulated,
            Compilation compilation,
            INamedTypeSymbol subject)
        {
            ImmutableArray<BinaryOperator>.Builder binaryOperators = ImmutableArray.CreateBuilder<BinaryOperator>();

            AddBuiltInBinaryOperators(binaryOperators, compilation, encapsulated, subject);

            foreach (IMethodSymbol method in encapsulated.GetMembers().OfType<IMethodSymbol>())
            {
                if (!method.IsBinaryOperatorCandidate(out string symbol))
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

                if (subject.HasBinaryOperator(method.Name, leftType, rightType)
                 || ContainsBinaryOperator(binaryOperators, method.Name, leftType, rightType))
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

        private static void AddBuiltInBinaryOperator(
            ImmutableArray<BinaryOperator>.Builder binaryOperators,
            INamedTypeSymbol subject,
            string operatorName,
            string symbol,
            ITypeSymbol leftType,
            bool isLeftSubject,
            ITypeSymbol rightType,
            bool isRightSubject,
            ITypeSymbol returnType,
            bool isReturnSubject)
        {
            if (subject.HasBinaryOperator(operatorName, leftType, rightType)
             || ContainsBinaryOperator(binaryOperators, operatorName, leftType, rightType))
            {
                return;
            }

            binaryOperators.Add(new BinaryOperator
            {
                IsLeftSubject = isLeftSubject,
                IsReturnSubject = isReturnSubject,
                IsRightSubject = isRightSubject,
                Left = leftType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Operator = operatorName,
                Return = returnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Right = rightType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Symbol = symbol,
            });
        }

        private static void AddBuiltInBinaryOperator(
            ImmutableArray<BinaryOperator>.Builder binaryOperators,
            INamedTypeSymbol encapsulated,
            INamedTypeSymbol subject,
            string operatorName,
            string symbol,
            ITypeSymbol returnType)
        {
            bool isReturnSubject = returnType.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);

            AddBuiltInBinaryOperator(
                binaryOperators,
                subject,
                operatorName,
                symbol,
                subject,
                isLeftSubject: true,
                subject,
                isRightSubject: true,
                isReturnSubject ? subject : returnType,
                isReturnSubject);
        }

        private static void AddBuiltInBooleanBinaryOperators(
            ImmutableArray<BinaryOperator>.Builder binaryOperators,
            INamedTypeSymbol encapsulated,
            INamedTypeSymbol subject)
        {
            foreach ((string operatorName, string symbol) in _bitwiseBinaryOperators)
            {
                AddBuiltInBinaryOperator(binaryOperators, encapsulated, subject, operatorName, symbol, encapsulated);
            }
        }

        private static void AddBuiltInBinaryOperators(
            ImmutableArray<BinaryOperator>.Builder binaryOperators,
            Compilation compilation,
            INamedTypeSymbol encapsulated,
            INamedTypeSymbol subject)
        {
            if (encapsulated.SpecialType == SpecialType.System_Boolean)
            {
                AddBuiltInBooleanBinaryOperators(binaryOperators, encapsulated, subject);

                return;
            }

            if (encapsulated.SpecialType == SpecialType.System_String)
            {
                AddBuiltInStringBinaryOperators(binaryOperators, compilation, subject);

                return;
            }

            if (encapsulated.SpecialType.IsBuiltInNumeric())
            {
                AddBuiltInNumericBinaryOperators(binaryOperators, compilation, encapsulated, subject);
            }
        }

        private static void AddBuiltInNumericBinaryOperators(
            ImmutableArray<BinaryOperator>.Builder binaryOperators,
            Compilation compilation,
            INamedTypeSymbol encapsulated,
            INamedTypeSymbol subject)
        {
            INamedTypeSymbol boolean = compilation.GetSpecialType(SpecialType.System_Boolean);
            INamedTypeSymbol integer = compilation.GetSpecialType(SpecialType.System_Int32);
            SpecialType promotedType = encapsulated.SpecialType.GetBuiltInPromotedSpecialType();
            INamedTypeSymbol resultType = compilation.GetSpecialType(promotedType);

            foreach ((string operatorName, string symbol) in _arithmeticBinaryOperators)
            {
                AddBuiltInBinaryOperator(binaryOperators, encapsulated, subject, operatorName, symbol, resultType);
            }

            foreach ((string operatorName, string symbol) in _comparisonBinaryOperators)
            {
                AddBuiltInBinaryOperator(binaryOperators, encapsulated, subject, operatorName, symbol, boolean);
            }

            if (!encapsulated.SpecialType.IsBuiltInIntegral())
            {
                return;
            }

            foreach ((string operatorName, string symbol) in _bitwiseBinaryOperators)
            {
                AddBuiltInBinaryOperator(binaryOperators, encapsulated, subject, operatorName, symbol, resultType);
            }

            foreach ((string operatorName, string symbol) in _shiftBinaryOperators)
            {
                bool isReturnSubject = resultType.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);

                AddBuiltInBinaryOperator(
                    binaryOperators,
                    subject,
                    operatorName,
                    symbol,
                    subject,
                    isLeftSubject: true,
                    integer,
                    isRightSubject: false,
                    isReturnSubject ? subject : resultType,
                    isReturnSubject);
            }
        }

        private static void AddBuiltInStringBinaryOperators(
            ImmutableArray<BinaryOperator>.Builder binaryOperators,
            Compilation compilation,
            INamedTypeSymbol subject)
        {
            INamedTypeSymbol @object = compilation.GetSpecialType(SpecialType.System_Object);

            AddBuiltInBinaryOperator(
                binaryOperators,
                subject,
                "op_Addition",
                "+",
                subject,
                isLeftSubject: true,
                subject,
                isRightSubject: true,
                subject,
                isReturnSubject: true);

            AddBuiltInBinaryOperator(
                binaryOperators,
                subject,
                "op_Addition",
                "+",
                subject,
                isLeftSubject: true,
                @object,
                isRightSubject: false,
                subject,
                isReturnSubject: true);

            AddBuiltInBinaryOperator(
                binaryOperators,
                subject,
                "op_Addition",
                "+",
                @object,
                isLeftSubject: false,
                subject,
                isRightSubject: true,
                subject,
                isReturnSubject: true);
        }

        private static bool ContainsBinaryOperator(
            ImmutableArray<BinaryOperator>.Builder binaryOperators,
            string operatorName,
            ITypeSymbol leftType,
            ITypeSymbol rightType)
        {
            string left = leftType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            string right = rightType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            return binaryOperators.Any(@operator => @operator.Operator == operatorName
                && @operator.Left == left
                && @operator.Right == right);
        }

        private static bool IsBinaryOperatorCandidate(this IMethodSymbol method, out string symbol)
        {
            bool isOperator = method.MethodKind == MethodKind.UserDefinedOperator || method.MethodKind == MethodKind.BuiltinOperator;
            bool hasExpectedParameterCount = method.Parameters.Length == ExpectedParametersForBinaryOperator;

            symbol = string.Empty;

            return isOperator && hasExpectedParameterCount && _supportedBinaryOperators.TryGetValue(method.Name, out symbol);
        }
    }
}
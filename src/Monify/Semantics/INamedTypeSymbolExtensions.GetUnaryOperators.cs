namespace Monify.Semantics;

using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Monify.Model;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int ExpectedParametersForUnaryOperator = 1;

    private static readonly IReadOnlyDictionary<string, string> _supported = new Dictionary<string, string>
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
    /// <param name="subject">The subject type being generated.</param>
    /// <returns>The unary operators that should be forwarded to the subject.</returns>
    public static ImmutableArray<UnaryOperator> GetUnaryOperators(this INamedTypeSymbol encapsulated, INamedTypeSymbol subject)
    {
        ImmutableArray<UnaryOperator>.Builder unaryOperators = ImmutableArray.CreateBuilder<UnaryOperator>();

        foreach (IMethodSymbol method in encapsulated.GetMembers().OfType<IMethodSymbol>())
        {
            if (method.MethodKind != MethodKind.UserDefinedOperator || method.Parameters.Length != ExpectedParametersForUnaryOperator)
            {
                continue;
            }

            if (!_supported.TryGetValue(method.Name, out string symbol))
            {
                continue;
            }

            if (!method.Parameters[0].Type.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability))
            {
                continue;
            }

            bool isReturnSubject = method.ReturnType.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);
            ITypeSymbol returnType = isReturnSubject ? subject : method.ReturnType;

            if (subject.HasUnaryOperator(method.Name, returnType))
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

        return unaryOperators.ToImmutable();
    }
}
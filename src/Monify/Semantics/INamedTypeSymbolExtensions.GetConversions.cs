namespace Monify.Semantics;

using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Monify.Model;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Identifies the implicit and explicit conversion operators declared by the <paramref name="encapsulated"/> type.
    /// </summary>
    /// <param name="encapsulated">The encapsulated type whose operators are to be inspected.</param>
    /// <param name="subject">The subject type being generated.</param>
    /// <returns>The conversions that should be forwarded to the subject.</returns>
    public static ImmutableArray<Conversion> GetConversions(this INamedTypeSymbol encapsulated, INamedTypeSymbol subject)
    {
        ImmutableArray<Conversion>.Builder conversions = ImmutableArray.CreateBuilder<Conversion>();

        foreach (IMethodSymbol method in encapsulated.GetMembers().OfType<IMethodSymbol>())
        {
            if (method.MethodKind != MethodKind.Conversion || method.Parameters.Length != ExpectedParametersForConversion)
            {
                continue;
            }

            if (!method.Name.Equals(ImplicitOperatorName, StringComparison.Ordinal)
             && !method.Name.Equals(ExplicitOperatorName, StringComparison.Ordinal))
            {
                continue;
            }

            bool isParameterEncapsulated = method.Parameters[0].Type.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);
            bool isReturnEncapsulated = method.ReturnType.Equals(encapsulated, SymbolEqualityComparer.IncludeNullability);

            if (!isParameterEncapsulated && !isReturnEncapsulated)
            {
                continue;
            }

            ITypeSymbol parameter = isParameterEncapsulated ? subject : method.Parameters[0].Type;
            ITypeSymbol result = isReturnEncapsulated ? subject : method.ReturnType;

            if (subject.HasConversion(parameter, result, method.Name))
            {
                continue;
            }

            conversions.Add(new Conversion
            {
                IsParameterSubject = isParameterEncapsulated,
                IsReturnSubject = isReturnEncapsulated,
                Operator = method.Name,
                Parameter = parameter.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Return = result.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            });
        }

        return conversions.ToImmutable();
    }
}
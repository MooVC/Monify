namespace Monify.Semantics;

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const string EncapsulatedValueTypeArgumentName = "Type";
    private const int ExpectedGenericArgumentCountForMonifyAttribute = 1;
    private const int OffsetForEncapsulatedTypeOnMonifyAttribute = 0;

    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> provided is annotated with the Monify attribute.
    /// </summary>
    /// <param name="subject">
    /// The symbol to be checked for the presence of the Monify attribute.
    /// </param>
    /// <param name="model">
    /// Allows asking semantic questions about a tree of syntax nodes in a Compilation.
    /// </param>
    /// <param name="value">
    /// The type of the value to be encapsulated by the <paramref name="subject"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the Monify attribute is present on the <paramref name="subject"/>, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasMonify(this INamedTypeSymbol subject, SemanticModel model, out ITypeSymbol value)
    {
        AttributeData data = subject
            .GetAttributes()
            .Where(attribute => attribute.AttributeClass is not null && attribute.AttributeClass.IsMonify())
            .Select(attribute => attribute)
            .FirstOrDefault();

        if (data is not null)
        {
            return GetEncapsulatedValueType(data.AttributeClass!, data, model, out value);
        }

        value = subject;

        return false;
    }

    private static bool GetEncapsulatedValueType(INamedTypeSymbol attribute, AttributeData data, SemanticModel model, out ITypeSymbol value)
    {
        if (attribute.IsGenericType && attribute.TypeArguments.Length == ExpectedGenericArgumentCountForMonifyAttribute)
        {
            value = attribute.TypeArguments.ElementAt(OffsetForEncapsulatedTypeOnMonifyAttribute);

            return true;
        }

        return GetTypeFromArgument(attribute, data, model, out value);
    }

    private static bool GetTypeFromArgument(INamedTypeSymbol attribute, AttributeData data, SemanticModel model, out ITypeSymbol value)
    {
        foreach (KeyValuePair<string, TypedConstant> argument in data.NamedArguments)
        {
            if (argument.Key == EncapsulatedValueTypeArgumentName
             && argument.Value.Kind == TypedConstantKind.Type
             && argument.Value.Value is ITypeSymbol symbol)
            {
                value = symbol;

                return true;
            }
        }

        value = attribute;

        return GetTypeFromArgumentSyntax(data, model, ref value);
    }

    private static bool GetTypeFromArgumentSyntax(AttributeData attribute, SemanticModel model, ref ITypeSymbol value)
    {
        if (attribute.ApplicationSyntaxReference?.GetSyntax() is not AttributeSyntax syntax || syntax.ArgumentList is null)
        {
            return false;
        }

        AttributeArgumentSyntax? type = syntax.ArgumentList.Arguments
            .FirstOrDefault(argument => argument.Expression is TypeOfExpressionSyntax
                                     && argument.NameEquals?.Name.Identifier.Text == nameof(Type));

        if (type?.Expression is not TypeOfExpressionSyntax typeOfSyntax)
        {
            return false;
        }

        ITypeSymbol? symbol = model.GetTypeInfo(typeOfSyntax.Type).Type;

        if (symbol is null)
        {
            return false;
        }

        value = symbol;

        return true;
    }
}
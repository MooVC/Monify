namespace Monify.Strategies;

using System.Collections.Immutable;
using Monify.Model;
using static Monify.Model.Subject;

/// <summary>
/// Generates members that forward calls to members supported by the encapsulated type.
/// </summary>
internal sealed class MemberPassthroughStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (subject.Encapsulated.IsDefaultOrEmpty)
        {
            yield break;
        }

        Encapsulated encapsulated = subject.Encapsulated[IndexForEncapsulatedValue];

        foreach (PassthroughProperty property in encapsulated.Properties)
        {
            yield return new Source(CreateProperty(subject, property), GetPropertyHint(property));
        }

        foreach (PassthroughMethod method in encapsulated.Methods)
        {
            yield return new Source(CreateMethod(subject, method), GetMethodHint(method));
        }
    }

    private static string CreateMethod(Subject subject, PassthroughMethod method)
    {
        string declaration = CreateMethodDeclaration(method);
        string call = CreateMethodCall(method);
        string body = method.Return == "void"
            ? $"{call};"
            : $"return {call};";

        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                {{declaration}}
                {
                    {{body}}
                }
            }
            """;
    }

    private static string CreateMethodCall(PassthroughMethod method)
    {
        string target = method.IsExplicit
            ? $"(({method.ExplicitInterface}){FieldStrategy.Name})"
            : FieldStrategy.Name;
        string arguments = CreateArguments(method.Parameters);
        string typeParameters = CreateTypeParameterList(method.TypeParameters);

        return $"{target}.{method.Name}{typeParameters}({arguments})";
    }

    private static string CreateMethodDeclaration(PassthroughMethod method)
    {
        string prefix = method.IsExplicit
            ? $"{method.Return} {method.ExplicitInterface}.{method.Name}"
            : $"{method.Accessibility} {method.Return} {method.Name}";

        string parameters = CreateParameters(method.Parameters);
        string typeParameters = CreateTypeParameterList(method.TypeParameters);
        string constraints = CreateConstraints(method.Constraints);

        return $"{prefix}{typeParameters}({parameters}){constraints}";
    }

    private static string CreateProperty(Subject subject, PassthroughProperty property)
    {
        string declaration = CreatePropertyDeclaration(property);
        string accessors = CreatePropertyAccessors(property);

        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                {{declaration}}
                {
            {{accessors}}
                }
            }
            """;
    }

    private static string CreatePropertyAccess(PassthroughProperty property)
    {
        string target = property.IsExplicit
            ? $"(({property.ExplicitInterface}){FieldStrategy.Name})"
            : FieldStrategy.Name;

        if (property.IsIndexer)
        {
            return $"{target}[{CreateArguments(property.Parameters)}]";
        }

        return $"{target}.{property.Name}";
    }

    private static string CreatePropertyAccessors(PassthroughProperty property)
    {
        string target = CreatePropertyAccess(property);
        ImmutableArray<string>.Builder accessors = ImmutableArray.CreateBuilder<string>();

        if (property.HasGetter)
        {
            string getter = CreateAccessorDeclaration(property.GetterAccessibility, "get");

            accessors.Add($$"""
                    {{getter}}
                    {
                        return {{target}};
                    }
            """);
        }

        if (property.HasSetter)
        {
            string setter = CreateAccessorDeclaration(property.SetterAccessibility, "set");

            accessors.Add($$"""
                    {{setter}}
                    {
                        {{target}} = value;
                    }
            """);
        }

        return string.Join("\n", accessors);
    }

    private static string CreatePropertyDeclaration(PassthroughProperty property)
    {
        string name = property.IsIndexer
            ? $"this[{CreateParameters(property.Parameters)}]"
            : property.Name;

        return property.IsExplicit
            ? $"{property.Type} {property.ExplicitInterface}.{name}"
            : $"{property.Accessibility} {property.Type} {name}";
    }

    private static string CreateAccessorDeclaration(string accessibility, string name)
    {
        return accessibility.Length == 0
            ? name
            : $"{accessibility} {name}";
    }

    private static string CreateArguments(ImmutableArray<PassthroughParameter> parameters)
    {
        return string.Join(", ", parameters.Select(CreateArgument));
    }

    private static string CreateArgument(PassthroughParameter parameter)
    {
        return parameter.ArgumentModifier.Length == 0
            ? parameter.Name
            : $"{parameter.ArgumentModifier} {parameter.Name}";
    }

    private static string CreateConstraints(ImmutableArray<string> constraints)
    {
        return constraints.IsDefaultOrEmpty
            ? string.Empty
            : $"\n        {string.Join("\n        ", constraints)}";
    }

    private static string CreateParameters(ImmutableArray<PassthroughParameter> parameters)
    {
        return string.Join(", ", parameters.Select(CreateParameter));
    }

    private static string CreateParameter(PassthroughParameter parameter)
    {
        string prefix = parameter.DeclarationModifier.Length == 0
            ? string.Empty
            : $"{parameter.DeclarationModifier} ";

        return $"{prefix}{parameter.Type} {parameter.Name}";
    }

    private static string CreateTypeParameterList(ImmutableArray<string> typeParameters)
    {
        return typeParameters.IsDefaultOrEmpty
            ? string.Empty
            : $"<{string.Join(", ", typeParameters)}>";
    }

    private static string GetMemberName(string explicitInterface, string name)
    {
        return explicitInterface.Length == 0
            ? name
            : $"{explicitInterface.NormalizeTypeForHint()}.{name}";
    }

    private static string GetMethodHint(PassthroughMethod method)
    {
        string name = GetMemberName(method.ExplicitInterface, method.Name);
        string parameters = GetParameterHint(method.Parameters);

        return parameters.Length == 0
            ? $"Methods.{name}"
            : $"Methods.{name}.{parameters}";
    }

    private static string GetParameterHint(ImmutableArray<PassthroughParameter> parameters)
    {
        return string.Join("-", parameters.Select(parameter => parameter.Type.NormalizeTypeForHint()));
    }

    private static string GetPropertyHint(PassthroughProperty property)
    {
        string propertyName = property.IsIndexer ? "this" : property.Name;
        string name = GetMemberName(property.ExplicitInterface, propertyName);
        string parameters = GetParameterHint(property.Parameters);

        return parameters.Length == 0
            ? $"Properties.{name}"
            : $"Properties.{name}.{parameters}";
    }
}
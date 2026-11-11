namespace Monify.Syntax
{
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Monify.Model;
    using Monify.Visitors;
    using MooVC.Syntax;

    using static Monify.Syntax.PassthroughMemberSyntax_Resources;

    /// <summary>
    /// Renders members that forward calls to the encapsulated value.
    /// </summary>
    internal static class PassthroughMemberSyntax
    {
        public static Source Create(Subject subject, PassthroughMethod method)
        {
            string declaration = CreateMethodDeclaration(method);
            Snippet member = GenerationSyntax.Block(GenerationSyntax.ToSnippet(CreateMethodBody(subject, method)), declaration);

            return GenerationSyntax.CreateSource(subject, GetMethodHint(method), member);
        }

        public static Source Create(Subject subject, PassthroughProperty property)
        {
            string declaration = CreatePropertyDeclaration(property);
            string accessors = CreatePropertyAccessors(property);
            Snippet member = GenerationSyntax.Block(GenerationSyntax.ToSnippet(accessors), declaration);

            return GenerationSyntax.CreateSource(subject, GetPropertyHint(property), member);
        }

        private static string CreateMethodBody(Subject subject, PassthroughMethod method)
        {
            ImmutableArray<string>.Builder statements = ImmutableArray.CreateBuilder<string>();

            foreach (PassthroughParameter parameter in method.Parameters.Where(IsObjectParameter))
            {
                statements.Add(CreateObjectParameterConversion(subject, parameter));
            }

            string call = CreateMethodCall(method);

            string body = method.Return == "void"
                ? string.Format(VoidMethodBodySource, call)
                : string.Format(ReturnMethodBodySource, call);

            statements.Add(body);

            string separator = string.Concat(SyntaxFactory.ElasticLineFeed, SyntaxFactory.ElasticLineFeed);

            return string.Join(separator, statements);
        }

        private static string CreateMethodCall(PassthroughMethod method)
        {
            string target = method.IsExplicit
                ? string.Format(ExplicitTargetSource, method.ExplicitInterface, GenerateValueFieldWhenEncapsulatedVisited.Name)
                : GenerateValueFieldWhenEncapsulatedVisited.Name;

            string arguments = CreateArguments(method.Parameters);
            string typeParameters = CreateTypeParameterList(method.TypeParameters);

            return string.Format(MethodCallSource, target, method.Name, typeParameters, arguments);
        }

        private static string CreateObjectParameterConversion(Subject subject, PassthroughParameter parameter)
        {
            string self = GenerationSyntax.Self(subject);

            return string.Format(ObjectParameterConversionSource, parameter.Name, self, parameter.Name, self, parameter.Name, GenerateValueFieldWhenEncapsulatedVisited.Name);
        }

        private static string CreateMethodDeclaration(PassthroughMethod method)
        {
            string prefix = method.IsExplicit
                ? string.Format(ExplicitMethodPrefixSource, method.Return, method.ExplicitInterface, method.Name)
                : string.Format(MethodPrefixSource, method.Accessibility, method.Return, method.Name);

            string parameters = CreateParameters(method.Parameters);
            string typeParameters = CreateTypeParameterList(method.TypeParameters);
            string constraints = CreateConstraints(method.Constraints);

            return string.Format(MethodDeclarationSource, prefix, typeParameters, parameters, constraints);
        }

        private static string CreatePropertyAccess(PassthroughProperty property)
        {
            string target = property.IsExplicit
                ? string.Format(ExplicitTargetSource, property.ExplicitInterface, GenerateValueFieldWhenEncapsulatedVisited.Name)
                : GenerateValueFieldWhenEncapsulatedVisited.Name;

            if (property.IsIndexer)
            {
                return string.Format(IndexerAccessSource, target, CreateArguments(property.Parameters));
            }

            return string.Format(PropertyAccessSource, target, property.Name);
        }

        private static string CreatePropertyAccessors(PassthroughProperty property)
        {
            string target = CreatePropertyAccess(property);
            ImmutableArray<string>.Builder accessors = ImmutableArray.CreateBuilder<string>();

            if (property.HasGetter)
            {
                string getter = CreateAccessorDeclaration(property.GetterAccessibility, "get");

                accessors.Add(GenerationSyntax.Block(GenerationSyntax.ToSnippet(string.Format(ReturnMethodBodySource, target)), getter));
            }

            if (property.HasSetter)
            {
                string setter = CreateAccessorDeclaration(property.SetterAccessibility, "set");

                accessors.Add(GenerationSyntax.Block(GenerationSyntax.ToSnippet($"{target} = value;"), setter));
            }

            return string.Join(SyntaxFactory.ElasticLineFeed.ToString(), accessors);
        }

        private static string CreatePropertyDeclaration(PassthroughProperty property)
        {
            string name = property.IsIndexer
                ? string.Format(IndexerDeclarationSource, CreateParameters(property.Parameters))
                : property.Name;

            return property.IsExplicit
                ? string.Format(ExplicitPropertyDeclarationSource, property.Type, property.ExplicitInterface, name)
                : string.Format(PropertyDeclarationSource, property.Accessibility, property.Type, name);
        }

        private static string CreateAccessorDeclaration(string accessibility, string name)
        {
            return accessibility.Length == 0
                ? name
                : string.Format(AccessorDeclarationSource, accessibility, name);
        }

        private static string CreateArguments(ImmutableArray<PassthroughParameter> parameters)
        {
            return string.Join(", ", parameters.Select(CreateArgument));
        }

        private static string CreateArgument(PassthroughParameter parameter)
        {
            return parameter.ArgumentModifier.Length == 0
                ? parameter.Name
                : string.Format(ArgumentSource, parameter.ArgumentModifier, parameter.Name);
        }

        private static string CreateConstraints(ImmutableArray<string> constraints)
        {
            return constraints.IsDefaultOrEmpty
                ? string.Empty
                : string.Format(ConstraintsSource, string.Join(string.Concat(SyntaxFactory.ElasticLineFeed, "        "), constraints));
        }

        private static string CreateParameters(ImmutableArray<PassthroughParameter> parameters)
        {
            return string.Join(", ", parameters.Select(CreateParameter));
        }

        private static string CreateParameter(PassthroughParameter parameter)
        {
            string prefix = parameter.DeclarationModifier.Length == 0
                ? string.Empty
                : string.Format(ParameterModifierSource, parameter.DeclarationModifier);

            return string.Format(ParameterSource, prefix, parameter.Type, parameter.Name);
        }

        private static string CreateTypeParameterList(ImmutableArray<string> typeParameters)
        {
            return typeParameters.IsDefaultOrEmpty
                ? string.Empty
                : string.Format(TypeParametersSource, string.Join(", ", typeParameters));
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

        private static bool IsObjectParameter(PassthroughParameter parameter)
        {
            return parameter.ArgumentModifier.Length == 0
                && parameter.DeclarationModifier.Length == 0
                && (parameter.Type == "object" || parameter.Type == "global::System.Object");
        }
    }
}
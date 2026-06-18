namespace Monify.Strategies
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Monify.Model;
    using static Monify.Model.Subject;
    using static Monify.Strategies.MemberPassthroughStrategy_Resources;

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

            string body = CreateMethodBody(subject, method)
                .Indent(skip: 0, whitespace: "        ");

            return string.Format(MethodSource, subject.Declaration, subject.Qualification, declaration, body);
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
                ? string.Format(ExplicitTargetSource, method.ExplicitInterface, FieldStrategy.Name)
                : FieldStrategy.Name;

            string arguments = CreateArguments(method.Parameters);
            string typeParameters = CreateTypeParameterList(method.TypeParameters);

            return string.Format(MethodCallSource, target, method.Name, typeParameters, arguments);
        }

        private static string CreateObjectParameterConversion(Subject subject, PassthroughParameter parameter)
        {
            return string.Format(ObjectParameterConversionSource, parameter.Name, subject.Qualification, parameter.Name, subject.Qualification, parameter.Name, FieldStrategy.Name);
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

        private static string CreateProperty(Subject subject, PassthroughProperty property)
        {
            string declaration = CreatePropertyDeclaration(property);
            string accessors = CreatePropertyAccessors(property);

            return string.Format(PropertySource, subject.Declaration, subject.Qualification, declaration, accessors);
        }

        private static string CreatePropertyAccess(PassthroughProperty property)
        {
            string target = property.IsExplicit
                ? string.Format(ExplicitTargetSource, property.ExplicitInterface, FieldStrategy.Name)
                : FieldStrategy.Name;

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

                accessors.Add(string.Format(GetterSource, getter, target));
            }

            if (property.HasSetter)
            {
                string setter = CreateAccessorDeclaration(property.SetterAccessibility, "set");

                accessors.Add(string.Format(SetterSource, setter, target));
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
namespace Monify.Syntax
{
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Monify.Model;
    using Monify.Visitors;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Syntax.PassthroughMemberSyntax_Resources;

    /// <summary>
    /// Renders members that forward calls to the encapsulated value.
    /// </summary>
    internal static class PassthroughMemberSyntax
    {
        private static readonly Snippet.Options _accessorBlocks = Configuration.Options.Snippets
            .WithBlock(block => block.WithInline(Snippet.Options.Blocks.Styles.MultiLine));

        public static Source Create(Subject subject, PassthroughMethod method)
        {
            Snippet body = ToSnippet(CreateMethodBody(subject, method));
            string hint = GetMethodHint(method);

            if (TryCreateParameters(method.Parameters, Configuration.Options, out Parameter[] parameters))
            {
                string name = method.IsExplicit ? $"{method.ExplicitInterface}.{method.Name}" : method.Name;
                Method member = Method.Undefined
                    .Named(declaration => declaration.Named(name)
                        .WithArguments(method.TypeParameters.ToArray()))
                    .Accepts(parameters)
                    .Returns(CreateResult(method.Return))
                    .WithBody(body)
                    .WithScope(method.IsExplicit ? Scopes.Unspecified : (Scopes)method.Accessibility);

                return subject.ToSource(hint, member);
            }

            return subject.ToSource(hint, body.Block(Configuration.Options, CreateMethodDeclaration(method)));
        }

        public static Source Create(Subject subject, PassthroughProperty property)
        {
            string hint = GetPropertyHint(property);
            string target = CreatePropertyAccess(property);
            Snippet getter = ToSnippet(string.Format(ReturnMethodBodySource, target));
            Snippet setter = ToSnippet($"{target} = value;");

            if (property.HasGetter && property.GetterAccessibility.Length == 0)
            {
                if (!property.IsIndexer)
                {
                    return subject.ToSource(hint, CreateProperty(property, getter, setter));
                }

                // MooVC indexers have a single parameter and cannot qualify individual accessors.
                if (!property.IsExplicit && property.Parameters.Length == 1 && property.SetterAccessibility.Length == 0
                    && TryCreateParameters(property.Parameters, Parameter.Options.Camel, out Parameter[] parameters))
                {
                    return subject.ToSource(hint, CreateIndexer(property, parameters[0], getter, setter));
                }
            }

            string declaration = CreatePropertyDeclaration(property);
            string accessors = CreatePropertyAccessors(property, getter, setter);

            return subject.ToSource(hint, ToSnippet(accessors).Block(Configuration.Options, declaration));
        }

        private static Snippet CreateIndexer(PassthroughProperty property, Parameter parameter, Snippet getter, Snippet setter)
        {
            var indexer = Indexer.Undefined
                .Accepts(parameter)
                .Returns(CreateResult(property.Type))
                .WithScope(property.IsExplicit ? Scopes.Unspecified : (Scopes)property.Accessibility)
                .WithBehaviours(behaviours => behaviours
                    .WithGet(getter)
                    .WithSet(property.HasSetter ? setter : Snippet.Empty));

            return indexer.ToSnippet(Configuration.Options.Types.Indexers.WithSnippets(_accessorBlocks));
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
            string self = subject.GetSelfType();

            return string.Format(ObjectParameterConversionSource, parameter.Name, self, parameter.Name, self, parameter.Name, GenerateValueFieldWhenEncapsulatedVisited.Name);
        }

        private static string CreateMethodDeclaration(PassthroughMethod method)
        {
            string prefix = method.IsExplicit
                ? string.Format(ExplicitMethodPrefixSource, method.Return, method.ExplicitInterface, method.Name)
                : string.Format(MethodPrefixSource, method.Accessibility, method.Return, method.Name);

            string parameters = CreateParameters(method.Parameters);
            string typeParameters = CreateTypeParameterList(method.TypeParameters);
            string constraints = CreateConstraints(method.TypeParameters);

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

        private static Snippet CreateProperty(PassthroughProperty property, Snippet getter, Snippet setter)
        {
            string name = property.IsExplicit ? $"{property.ExplicitInterface}.{property.Name}" : property.Name;
            var member = Property.Undefined
                .Named(name)
                .OfType(TypeReferences.FromRoslyn(property.Type))
                .WithScope(property.Accessibility)
                .WithBehaviours(behaviours => behaviours
                    .WithGet(getter)
                    .WithSet(accessor => accessor
                        .WithMode(property.HasSetter ? Property.Methods.Setter.Modes.Set : Property.Methods.Setter.Modes.ReadOnly)
                        .WithScope(property.SetterAccessibility)
                        .WithBehaviour(setter)));

            return member.ToSnippet(Configuration.Options.Types.Properties.WithSnippets(_accessorBlocks));
        }

        private static string CreatePropertyAccessors(PassthroughProperty property, Snippet getter, Snippet setter)
        {
            // MooVC's accessors always include get and cannot attach accessibility to a getter.
            ImmutableArray<string>.Builder accessors = ImmutableArray.CreateBuilder<string>();

            if (property.HasGetter)
            {
                accessors.Add(getter.Block(Configuration.Options, CreateAccessorDeclaration(property.GetterAccessibility, "get")));
            }

            if (property.HasSetter)
            {
                accessors.Add(setter.Block(Configuration.Options, CreateAccessorDeclaration(property.SetterAccessibility, "set")));
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

        private static string CreateConstraints(ImmutableArray<Generic> typeParameters)
        {
            string[] clauses = typeParameters.Select(parameter => parameter.ToSnippet(Configuration.Options).ToString())
                .Where(clause => clause.Length > 0)
                .ToArray();

            return clauses.Length == 0
                ? string.Empty
                : string.Format(ConstraintsSource, string.Join(string.Concat(SyntaxFactory.ElasticLineFeed, "        "), clauses));
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

        private static Result CreateResult(string type)
        {
            return type == "void"
                ? Result.Void
                : Result.Void.OfType(TypeReferences.FromRoslyn(type));
        }

        private static string CreateTypeParameterList(ImmutableArray<Generic> typeParameters)
        {
            return typeParameters.IsDefaultOrEmpty
                ? string.Empty
                : string.Format(TypeParametersSource, string.Join(", ", typeParameters.Select(Generic.Names)));
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

        private static Snippet ToSnippet(string text)
        {
            return Snippet.From(Configuration.Options, text);
        }

        private static bool TryCreateParameters(ImmutableArray<PassthroughParameter> parameters, Parameter.Options options, out Parameter[] syntax)
        {
            syntax = new Parameter[parameters.Length];

            for (int index = 0; index < parameters.Length; index++)
            {
                PassthroughParameter parameter = parameters[index];
                syntax[index] = Parameter.Undefined.Named(parameter.Name)
                    .OfType(TypeReferences.FromRoslyn(parameter.Type))
                    .WithModifier(parameter.DeclarationModifier);

                // MooVC normalizes variable names and moves params to the end of the signature.
                if (syntax[index].ToSnippet(options).ToString() != CreateParameter(parameter)
                    || (syntax[index].Modifier.IsParams && index != parameters.Length - 1))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
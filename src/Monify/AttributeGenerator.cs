namespace Monify
{
    using System;
    using System.Text;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Text;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;
    using Attribute = System.Attribute;
    using Type = System.Type;

    /// <summary>
    /// Generates the Monify attribute, used to denote when a type should serve as a wrapper for a single value.
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public sealed class AttributeGenerator
        : IIncrementalGenerator
    {
        /// <summary>
        /// The name of the attribute (without the suffix).
        /// </summary>
        internal const string Name = "Monify";

        /// <summary>
        /// The source code for the Generic attribute that will be output by the generator.
        /// </summary>
        internal static readonly string Generic = CreateSource(isGeneric: true);

        /// <summary>
        /// The source code for the NonGeneric attribute that will be output by the generator.
        /// </summary>
        internal static readonly string NonGeneric = CreateSource(isGeneric: false);

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterSourceOutput(context.ParseOptionsProvider, (productionContext, options) =>
            {
                if (options is CSharpParseOptions csharp && csharp.LanguageVersion >= LanguageVersion.CSharp11)
                {
                    Generate(Generic, productionContext, "Generic");
                }

                Generate(NonGeneric, productionContext, "NonGeneric");
            });
        }

        private static Class AddProperty(Class declaration, string name, string field, Symbol type, Snippet initialValue)
        {
            return declaration
                .WithFields(backing => backing
                    .Named(field)
                    .OfType(type)
                    .IsReadOnly(false)
                    .WithDefault(initialValue))
                .WithProperties(property => property
                    .Named(name)
                    .OfType(type)
                    .WithBehaviours(accessors => accessors
                        .WithGet($"return {field};")
                        .WithSet(setter => setter
                            .WithMode(Property.Methods.Setter.Modes.Set)
                            .WithBehaviour($"{field} = value;"))));
        }

        private static string CreateSource(bool isGeneric)
        {
            const string Targets = "global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct";
            Declaration name = $"{Name}Attribute";
            Options options = Configuration.Options;

            if (isGeneric)
            {
                name = name.WithArguments(argument => argument.Named("T"));
            }

            Class declaration = Class.Undefined
                .Named(name)
                .IsPartial(false)
                .WithExtensibility(Modifiers.Sealed)
                .WithScope(Scopes.Internal)
                .DerivesFrom(typeof(Attribute))
                .AttributedWith(usage => usage
                    .Named(typeof(AttributeUsageAttribute))
                    .WithArguments(
                        (Name: string.Empty, Value: Targets),
                        (Name: nameof(AttributeUsageAttribute.Inherited), Value: "false"),
                        (Name: nameof(AttributeUsageAttribute.AllowMultiple), Value: "false")));

            if (isGeneric)
            {
                foreach (string propertyName in new[] { "DebuggerDisplay", "Passthrough" })
                {
                    declaration = declaration.WithProperties(property => property
                        .Named(propertyName)
                        .OfType(typeof(bool))
                        .WithBehaviours(accessors => accessors.WithSet(setter => setter.WithMode(Property.Methods.Setter.Modes.Set)))
                        .WithDefault("true;"));
                }
            }
            else
            {
                declaration = AddProperty(declaration, "DebuggerDisplay", "_debuggerDisplay", typeof(bool), "true");
                declaration = AddProperty(declaration, "Passthrough", "_passthrough", typeof(bool), "true");
                declaration = AddProperty(declaration, "Type", "_type", typeof(Type), Snippet.Empty);

                options = options.WithTypes(types => types.WithProperties(properties => properties
                    .WithSnippets(snippets => snippets.WithBlock(block => block.WithInline(Snippet.Options.Blocks.Styles.MultiLine)))));
            }

            string source = Definition.Undefined
                .From(typeof(AttributeGenerator))
                .For<Class>(_ => declaration)
                .ToSnippet(options);

            return source.NormalizeLineEndings();
        }

        private static void Generate(string content, SourceProductionContext context, string name)
        {
            var text = SourceText.From(content, Encoding.UTF8);

            context.AddSource($"{Name}Attribute.{name}.g.cs", text);
        }
    }
}
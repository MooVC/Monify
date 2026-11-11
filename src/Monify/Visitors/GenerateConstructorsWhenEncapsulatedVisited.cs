namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;

    using static Monify.Visitors.GenerateConstructorsWhenEncapsulatedVisited_Resources;

    /// <summary>
    /// Generates a constructor that accepts the encapsulated type.
    /// </summary>
    internal sealed class GenerateConstructorsWhenEncapsulatedVisited
        : ISubjectVisitor<Subject.Graph.EncapsulatedValues.Encapsulated, Source>
    {
        /// <summary>
        /// Defines the method name associated with the constructor.
        /// </summary>
        public const string Name = ".ctor";

        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.EncapsulatedValues.Encapsulated instance)
        {
            Encapsulated encapsulated = instance._Value;

            if (encapsulated.HasConstructor)
            {
                yield break;
            }

            string hint = instance._Index == Subject.IndexForEncapsulatedValue
                ? Name
                : $"{Name}.Passthrough.{instance._Index:D2}";

            Subject subject = instance._Root;
            string declaration = $"public {subject.Name}({encapsulated.Type} value)";
            Snippet body = GenerationSyntax.ToSnippet(string.Format(ConstructorSource, GenerateValueFieldWhenEncapsulatedVisited.Name));

            yield return GenerationSyntax.CreateSource(subject, hint, GenerationSyntax.Block(body, declaration));
        }
    }
}
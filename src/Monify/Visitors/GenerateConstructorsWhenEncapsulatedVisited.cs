namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

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
            Class owner = Class.Undefined.Named(subject.Name);
            Constructor constructor = Constructor.Undefined
                .WithParameters(parameter => parameter.Named("Value").OfType(TypeReferences.FromRoslyn(encapsulated.Type)))
                .WithBody(Snippet.From(Configuration.Options, string.Format(ConstructorSource, GenerateValueFieldWhenEncapsulatedVisited.Name)));

            yield return subject.ToSource(hint, constructor.ToSnippet(Configuration.Options, owner));
        }
    }
}
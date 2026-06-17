namespace Monify.Strategies
{
    using System.Collections.Generic;
    using Monify.Model;

    using static Monify.Strategies.ConstructorStrategy_Resources;

    /// <summary>
    /// Generates the source needed to ensure the type can be contructed.
    /// </summary>
    internal sealed class ConstructorStrategy
        : IStrategy
    {
        /// <summary>
        /// Defines the method name associated with the constructor.
        /// </summary>
        public const string Name = ".ctor";

        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            for (int index = 0; index < subject.Encapsulated.Length; index++)
            {
                Encapsulated encapsulated = subject.Encapsulated[index];

                if (encapsulated.HasConstructor)
                {
                    continue;
                }

                string hint = index == Subject.IndexForEncapsulatedValue
                    ? Name
                    : $"{Name}.Passthrough.{index:D2}";

                string code = string.Format(ConstructorSource, subject.Declaration, subject.Qualification, subject.Name, encapsulated.Type, FieldStrategy.Name);

                yield return new Source(code, hint);
            }
        }
    }
}
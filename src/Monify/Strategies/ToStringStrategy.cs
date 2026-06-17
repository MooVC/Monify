namespace Monify.Strategies
{
    using System.Collections.Generic;
    using Monify.Model;

    using static Monify.Strategies.ToStringStrategy_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.ToString()"/>.
    /// </summary>
    internal sealed class ToStringStrategy
        : IStrategy
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            if (!subject.CanOverrideToString)
            {
                yield break;
            }

            string code = string.Format(ToStringSource, subject.Declaration, subject.Qualification, FieldStrategy.Name, FieldStrategy.Name);

            yield return new Source(code, nameof(ToString));
        }
    }
}
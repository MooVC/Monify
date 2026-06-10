namespace Monify.Strategies
{
    using System.Collections.Generic;
    using Monify.Model;

    using static Monify.Strategies.EqualsStrategy_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.Equals(object)"/>.
    /// </summary>
    internal sealed class EqualsStrategy
        : IStrategy
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            if (!subject.CanOverrideEquals)
            {
                yield break;
            }

            string code = string.Format(EqualsSource, subject.Declaration, subject.Qualification, subject.Qualification, subject.Qualification);

            yield return new Source(code, nameof(Equals));
        }
    }
}
namespace Monify.Strategies
{
    using System.Collections.Generic;
    using Monify.Model;

    using static Monify.Strategies.GetHashCodeStrategy_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.Equals(object)"/>.
    /// </summary>
    internal sealed class GetHashCodeStrategy
        : IStrategy
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            if (!subject.CanOverrideGetHashCode)
            {
                yield break;
            }

            string code = string.Format(GetHashCodeSource, subject.Declaration, subject.Qualification, FieldStrategy.Name);

            yield return new Source(code, nameof(GetHashCode));
        }
    }
}
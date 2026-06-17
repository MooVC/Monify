namespace Monify.Strategies
{
    using System.Collections.Generic;
    using Monify.Model;

    using static Monify.Strategies.DebuggerDisplayStrategy_Resources;

    /// <summary>
    /// Generates the source needed to support debugger display formatting.
    /// </summary>
    internal sealed class DebuggerDisplayStrategy
        : IStrategy
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            if (!subject.GenerateDebuggerDisplay)
            {
                yield break;
            }

            string code = string.Format(DebuggerDisplaySource, subject.Declaration, subject.Qualification, subject.Name, FieldStrategy.Name);

            yield return new Source(code, "DebuggerDisplay");
        }
    }
}
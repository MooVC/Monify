namespace Monify.Strategies;

using Monify.Model;

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

        string code = $$$"""
            [global::System.Diagnostics.DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
            {{{subject.Declaration}}} {{{subject.Qualification}}}
            {
                private string GetDebuggerDisplay()
                {
                    return string.Format("{{{subject.Name}}} {{ {0} }}", {{{FieldStrategy.Name}}});
                }
            }
            """;

        yield return new Source(code, "DebuggerDisplay");
    }
}
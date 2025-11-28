namespace Monify.Snippets;

using System.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

[DebuggerDisplay("{Hint,nq}")]
public sealed record Generated(string Content, Extensions Extensions, string Hint, Type? Generator = default)
{
    public void IsExpectedIn(SolutionState state)
    {
        Type generator = Generator ?? typeof(TypeGenerator);
        string normalizedContent = Content.ReplaceLineEndings("\n");

        state.GeneratedSources.Add((sourceGeneratorType: generator, filename: $"{Hint}.g.cs", content: normalizedContent));
    }
}
namespace Monify.Snippets;

using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

[DebuggerDisplay("{Minimum,nq} - {Maximum,nq}")]
public sealed record Expectations(string[] Declarations, Generated[] Generated, LanguageVersion Minimum, LanguageVersion Maximum)
{
    public void IsDeclaredIn(SolutionState state, bool supportsJsonSerialization = false)
    {
        foreach (string declaration in Declarations)
        {
            state.Sources.Add(declaration);
        }

        foreach (Generated generated in Generated.Where(source => !source.RequiresJsonSerialization || supportsJsonSerialization))
        {
            generated.IsExpectedIn(state);
        }
    }
}
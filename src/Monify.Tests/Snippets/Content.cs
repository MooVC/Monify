namespace Monify.Snippets;

using Microsoft.CodeAnalysis.CSharp;

public sealed record Content(string Body, LanguageVersion Minimum, LanguageVersion Maximum = LanguageVersion.Latest);
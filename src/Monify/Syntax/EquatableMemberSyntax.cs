namespace Monify.Syntax
{
    using System;
    using System.Collections.Generic;
    using Monify.Model;
    using MooVC.Syntax;

    using static Monify.Syntax.EquatableMemberSyntax_Resources;

    /// <summary>
    /// Renders the interface and method used for equatable comparisons.
    /// </summary>
    internal static class EquatableMemberSyntax
    {
        public static IEnumerable<Source> Create(
            Subject subject,
            string type,
            string name,
            bool isEquatable,
            bool hasEquatable,
            Func<string> implementation)
        {
            string hint = $"{nameof(IEquatable<Subject>)}.{name}";

            if (!isEquatable)
            {
                yield return GenerationSyntax.CreateInterfaceSource(subject, hint, $"global::System.IEquatable<{type}>");
            }

            if (!hasEquatable)
            {
                string declaration = $"public bool {nameof(object.Equals)}({type} other)";
                Snippet body = GenerationSyntax.ToSnippet(string.Format(ImplementationSource, implementation()));

                yield return GenerationSyntax.CreateSource(subject, $"{hint}.{nameof(object.Equals)}", GenerationSyntax.Block(body, declaration));
            }
        }
    }
}
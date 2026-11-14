namespace Monify.Syntax
{
    using System;
    using System.Collections.Generic;
    using Monify.Model;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

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
            Symbol operand = TypeReferences.FromRoslyn(type);

            if (!isEquatable)
            {
                Symbol contract = typeof(IEquatable<>);
                yield return subject.ToSource(hint, contract.WithArguments(operand));
            }

            if (!hasEquatable)
            {
                Method equals = Method.Undefined.Named(nameof(object.Equals))
                    .Accepts(parameter => parameter.Named("Other").OfType(operand))
                    .Returns(Result.Void.OfType(typeof(bool)))
                    .WithBody(Snippet.From(Configuration.Options, string.Format(ImplementationSource, implementation())));

                yield return subject.ToSource($"{hint}.{nameof(object.Equals)}", equals);
            }
        }
    }
}
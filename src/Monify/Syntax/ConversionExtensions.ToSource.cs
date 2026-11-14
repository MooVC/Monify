namespace Monify.Syntax
{
    using Monify.Model;
    using MooVC.Syntax.CSharp;
    using Conversion = MooVC.Syntax.CSharp.Conversion;

    internal static partial class ConversionExtensions
    {
        public static Source ToSource(this Conversion conversion, Subject subject, string hint)
        {
            Class operand = Class.Undefined.Named(subject.GetSelfType());
            return subject.ToSource(hint, conversion.ToSnippet(Configuration.Options, operand));
        }
    }
}
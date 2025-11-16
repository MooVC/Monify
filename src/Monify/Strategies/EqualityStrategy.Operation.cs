namespace Monify.Strategies;

/// <summary>
/// Generates the source needed to support the equality operator.
/// </summary>
internal partial class EqualityStrategy
{
    private readonly struct Operation
    {
        public Operation(bool hasOperator, string hint, bool isPassthrough, string type)
        {
            HasOperator = hasOperator;
            Hint = hint;
            IsPassthrough = isPassthrough;
            Type = type;
        }

        public bool HasOperator { get; }

        public string Hint { get; }

        public bool IsPassthrough { get; }

        public string Type { get; }
    }
}
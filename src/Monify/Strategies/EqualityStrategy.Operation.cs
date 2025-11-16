namespace Monify.Strategies;

/// <summary>
/// Generates the source needed to support the equality operator.
/// </summary>
internal partial class EqualityStrategy
{
    private readonly struct Operation
    {
        public Operation(bool hasOperator, string hint, string type)
        {
            HasOperator = hasOperator;
            Hint = hint;
            Type = type;
        }

        public bool HasOperator { get; }

        public string Hint { get; }

        public string Type { get; }
    }
}
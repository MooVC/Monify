namespace Monify.Strategies;

/// <summary>
/// Generates the source needed to support the inequality operator.
/// </summary>
internal partial class InequalityStrategy
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
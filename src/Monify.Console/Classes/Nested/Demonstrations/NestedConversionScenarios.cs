namespace Monify.Console.Classes.Nested.Demonstrations;

/// <summary>
/// Provides helpers that demonstrate how nested wrappers gain implicit conversions.
/// </summary>
public static class NestedConversionScenarios
{
    /// <summary>
    /// Converts the provided string into the outermost wrapper using nested implicit operators.
    /// </summary>
    /// <param name="value">The text to wrap.</param>
    /// <returns>The <see cref="OuterStringValue"/> created from <paramref name="value"/>.</returns>
    public static OuterStringValue ConvertStringToOuter(string value)
    {
        OuterStringValue outer = value;

        return outer;
    }

    /// <summary>
    /// Converts the provided <see cref="OuterStringValue"/> into the underlying string value.
    /// </summary>
    /// <param name="value">The wrapper to convert.</param>
    /// <returns>The string represented by the wrapper.</returns>
    public static string ConvertOuterToString(OuterStringValue value)
    {
        string result = value;

        return result;
    }

    /// <summary>
    /// Converts the provided <see cref="OuterStringValue"/> into the innermost wrapper to demonstrate chained conversions.
    /// </summary>
    /// <param name="value">The wrapper to convert.</param>
    /// <returns>The <see cref="CoreStringValue"/> nested inside the chain.</returns>
    public static CoreStringValue ConvertOuterToCore(OuterStringValue value)
    {
        CoreStringValue core = value;

        return core;
    }
}
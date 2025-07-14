#pragma warning disable SA1518
namespace Valuify;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
internal sealed class ValuifyAttribute : Attribute
{
}
#pragma warning restore SA1518
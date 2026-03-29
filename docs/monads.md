# Monads with Monify

This guide is for engineers who want encapsulated domain types with monad-style composition, without hand-writing repetitive conversion and equality code.

## 1) Define the encapsulated type

```csharp
using Monify;

[Monify<int>]
public readonly partial struct ResultCode;
```

Monify generates the constructor, conversion operators, equality, hash code, and string behavior.

## 2) Add minimal composition helpers

```csharp
public static class ResultCodeComposition
{
    public static TResult Map<TResult>(
        this ResultCode source,
        Func<int, TResult> projection)
    {
        var value = (int)source;
        return projection(value);
    }

    public static ResultCode Bind(
        this ResultCode source,
        Func<int, ResultCode> binder)
    {
        var value = (int)source;
        return binder(value);
    }
}
```

`Map` and `Bind` remain tiny because Monify already generated the wrapper mechanics.

## 3) Optional: enable query expression composition

```csharp
public static class ResultCodeLinq
{
    public static TResult Select<TResult>(
        this ResultCode source,
        Func<int, TResult> projection) =>
        source.Map(projection);

    public static ResultCode SelectMany(
        this ResultCode source,
        Func<int, ResultCode> binder) =>
        source.Bind(binder);
}

ResultCode value = 10;
ResultCode reduced =
    from number in value
    from next in (ResultCode)(number - 2)
    select next;
```

Use this only when the team benefits from query-expression syntax.

## 4) Adopt incrementally

- Begin with high-value wrappers where invalid value mixing has caused bugs.
- Keep behavior local to domain-specific extension methods.
- Add more wrappers and composition helpers as confidence grows.

This pattern keeps code approachable for object-oriented teams while opening a clear path to functional composition.

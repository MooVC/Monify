# MONFY03: Type captures State

<table>
<tr>
  <td>Type Name</td>
  <td>MONFY03_AttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>MONFY03</td>
</tr>
<tr>
  <td>Category</td>
  <td>Design</td>
</tr>
<tr>
  <td>Severity</td>
  <td>Info</td>
</tr>
<tr>
  <td>Is Enabled By Default</td>
  <td>Yes</td>
</tr>
</table>

## Cause

A type upon which `Monify` is applied defines fields or properties that capture state in addition to that of the encapsulated value.

## Rule Description

A violation of this rule occurs when a type annotated with the `Monify` attribute defines one or more fields or properties, either within the declaration itself or within its base type, that captures state in addition to that of the encapsulated value.

For example:

```csharp
[Monify<byte>]
public partial record Age(bool IsAdult);
```

## How to Fix Violations

Reevaluate the decision to apply the `Monify` attribute. If the type is intended to define fields or properties then usage of the `Monify` attribute is not recommended.

For example:

```csharp
public partial record Age(bool IsAdult, byte Value);
```
or alternatively:

```csharp
[Monify]
public partial record Age
{
    public bool IsAdult => _value >= 18;
}
```

## When to Suppress Warnings

It is not recommended to suppress the rule. Instead, it is suggested that the usage of the `Monify` attribute be reevaluated.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable MONFY03 // Type captures State
[Monify<byte>]
public partial record Age(bool IsAdult);
#pragma warning restore MONFY03 // Type captures State
```

or alternatively:

```csharp
[Monify<byte>]
[SuppressMessage("Design", "MONFY03:Type captures State", Justification = "Explanation for suppression")]
public partial record Age(bool IsAdult);
```

## How to Disable MONFY03

It is not recommended to disable the rule, as this may result in some confusion if the expected equality behavior is not observed.

```ini
# Disable MONFY03: Type captures State
[*.cs]
dotnet_diagnostic.MONFY03.severity = none
```
# MONFY02: Type is not Partial

<table>
<tr>
  <td>Type Name</td>
  <td>MONFY02_AttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>MONFY02</td>
</tr>
<tr>
  <td>Category</td>
  <td>Usage</td>
</tr>
<tr>
  <td>Severity</td>
  <td>Warning</td>
</tr>
<tr>
  <td>Is Enabled By Default</td>
  <td>Yes</td>
</tr>
</table>

## Cause

The declaration for a type annotated with the `Monify` attribute is not marked as partial.

## Rule Description

A violation of this rule occurs when a type is not marked with the `partial` keyword when the `Monify` attribute is applied. The `partial` keyword is needed to facilitate the generation of additional content within the assembly for the type.

For example:

```csharp
[Monify<byte>]
public record Age;
```

In this example, `Age` is excluded from Monify's consideration as the declaration is not `partial` and as a result, no additional content can be generated without resulting in an compilation failure.

## How to Fix Violations

Reevaluate the decision to apply the `Monify` attribute. If the intention is correct, add the `partial` keyword to the declaration, otherwise remove the `Monify` attribute.

For example:

```csharp
[Monify<byte>]
public partial record Age;
```
or alternatively:

```csharp
public record Age;
```

## When to Suppress Warnings

It is not recommended to suppress the rule. Instead, it is suggested that the usage of the `Monify` attribute be reevaluated.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable MONFY02 // Type is not Partial
[Monify<byte>]
public record Age;
#pragma warning restore MONFY02 // Type is not Partial
```

or alternatively:

```csharp
[Monify<byte>]
[SuppressMessage("Design", "MONFY02:Type is not Partial", Justification = "Explanation for suppression")]
public record Age;
```

## How to Disable MONFY02

It is not recommended to disable the rule, as this may result in some confusion if the expected equality behavior is not observed.

```ini
# Disable MONFY02: Descriptor is disregarded from consideration by Monify
[*.cs]
dotnet_diagnostic.MONFY02.severity = none
```
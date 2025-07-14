# MONFY01: Type is not compatible with Monify

<table>
<tr>
  <td>Type Name</td>
  <td>MONFY01_AttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>MONFY01</td>
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

A type declaration upon which the `Monify` attribute is placed is not a `class`, `record` or `struct`.

## Rule Description

A violation of this rule occurs when the `Monify` attribute is placed on a type declaration that is not a `class`, `record` or `struct`. There is no known instance when this should be the case but in logic terms, the possibility arises.

## How to Fix Violations

To fix a violation of this rule, determine if the declaration type is correct for the intended usage. If the type is correct, remove the `Monify` attribute from the declaration, otherwise change the declaration for the type to a `class`, `record` or `struct`.

For example:

```csharp
[Monify<byte>]
public partial record Age;
```

## How to Suppress Violations

It is not recommended to suppress the rule. Instead, it is suggested that the usage of the `Monify` attribute be reevaluated.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable MONFY01 // Type is not compatible with Monify
[Monify<byte>]
// Unknown Type Declaration
#pragma warning restore MONFY01 // Type is not compatible with Monify
```

or alternatively:

```csharp
[Monify<byte>]
[SuppressMessage("Design", "MONFY01:Type is not compatible with Monify", Justification = "Explanation for suppression")]
// Unknown Type Declaration
```

## How to Disable MONFY01

It is not recommended to disable the rule, as this may result in some confusion if the expected equality behavior is not observed.

```ini
# Disable MONFY01: Type is not compatible with Monify
[*.cs]
dotnet_diagnostic.MONFY01.severity = none
```
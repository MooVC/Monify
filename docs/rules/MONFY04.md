# MONFY04: Type references itself

<table>
<tr>
  <td>Type Name</td>
  <td>MONFY04_AttributeAnalyzer</td>
</tr>
<tr>
  <td>Diagnostic Id</td>
  <td>MONFY04</td>
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

The type referenced by the `Monify` attribute is the same as the type to which the attribute is applied.

## Rule Description

A violation of this rule occurs when a type annotated with the `Monify` attribute references itself as the encapsulated type. This configuration attempts to encapsulate the type within itself and is not supported.

For example:

```csharp
[Monify<Age>]
public partial record Age;
```

## How to Fix Violations

Reevaluate the type referenced by the `Monify` attribute. If the intent is to encapsulate a different value type, update the attribute to reference that type. Otherwise, remove the attribute.

For example:

```csharp
[Monify<byte>]
public partial record Age;
```

or alternatively:

```csharp
public partial record Age;
```

## When to Suppress Warnings

It is not recommended to suppress the rule. Instead, reconsider the usage of the `Monify` attribute.

If suppression is desired, one of the following approaches can be used:

```csharp
#pragma warning disable MONFY04 // Type references itself
[Monify<Age>]
public partial record Age;
#pragma warning restore MONFY04 // Type references itself
```

or alternatively:

```csharp
[Monify<Age>]
[SuppressMessage("Design", "MONFY04:Type references itself", Justification = "Explanation for suppression")]
public partial record Age;
```

## How to Disable MONFY04

It is not recommended to disable the rule, as this may result in confusion if the expected behaviour is not observed.

```ini
# Disable MONFY04: Type references itself
[*.cs]
dotnet_diagnostic.MONFY04.severity = none
```

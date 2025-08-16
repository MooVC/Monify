# Monify [![NuGet](https://img.shields.io/nuget/v/Monify?logo=nuget)](https://www.nuget.org/packages/Monify/) [![GitHub](https://img.shields.io/github/license/MooVC/Monify)](LICENSE.md)

Monify, a .NET Roslyn source generator, automates the creation of strongly-typed wrappers around single values, improving semantic clarity, type safety, and maintainability.

## Installation

To install Monify, use the following command in your package manager console:

```shell
install-package Monify
```

## Usage

Coming Soon™

## Analyzers

Monify includes several analyzers to assist engineers with its usage. These are:

Rule ID                          | Category | Severity | Notes
:--------------------------------|:---------|:---------|:-------------------------------------------------------------------------
[MONFY01](docs/rules/MONFY01.md) | Usage    | Warning  | Type is not compatible with Monify
[MONFY02](docs/rules/MONFY02.md) | Usage    | Warning  | Type is not Partial
[MONFY03](docs/rules/MONFY03.md) | Design   | Error    | Type captures State
[MONFY04](docs/rules/MONFY04.md) | Design   | Error    | Self Referencing Type

## Contributing

Contributions are welcome - see the [CONTRIBUTING.md](/.github/CONTRIBUTING.md) file for details.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
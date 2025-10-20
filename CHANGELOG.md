# Changelog
All notable changes to Monify will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

# [1.1.2] - 2025-10-20

## Fixed
- ConvertFrom now uses `ReferenceEquals` instead of `==` to avoid ambiguity when the encapsulated type is a reference type (#17).

# [1.1.1] - 2025-10-17

## Fixed
- Set **Valuify** to Version **1.7.0** instead of **1.7.0-rc.1**.

# [1.1.0] - 2025-10-17

## Changed
- Reverted **Microsoft.CodeAnalysis.Analyzers** to Version **3.11.0** to maximize compatibility with Visual Studio 2022.
- Reverted **Microsoft.CodeAnalysis.CSharp** to Version **4.4.0** to maximize compatibility with Visual Studio 2022.
- Reverted **Microsoft.CodeAnalysis.CSharp.Workspaces** Version **4.4.0** to maximize compatibility with Visual Studio 2022.
- Reverted **Microsoft.CodeAnalysis.Workspaces.Common** Version **4.4.0** to maximize compatibility with Visual Studio 2022.

# [1.0.0] - 2025-08-22

- Initial Release
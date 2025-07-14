# Boolit.NET

A lightweight, fast, and easy-to-use .NET library for parsing and evaluating boolean expressions from strings.

## Features

- **Intuitive Syntax:** Use common boolean operators like `and`, `or`, `not`, `xor`.
- **Case-Insensitive:** `TRUE AND false` is the same as `true and FALSE`.
- **Parentheses:** Group expressions with `()` for clarity and order of operations.
- **Cross-Platform:** Targets .NET Standard 2.0+, .NET 8, and .NET 9 for wide compatibility.
- **High Performance:** Optimized for speed with low memory allocations.

## Getting Started

### Installation

Boolit.NET is available on NuGet. You can install it using the .NET CLI:

```bash
dotnet add package Boolit.NET
```

Or via the NuGet Package Manager:

```powershell
Install-Package Boolit.NET
```

### Usage

Evaluating a boolean expression is simple:

```csharp
using Boolit.NET;

// Create an expression
var expression = BoolExpression.Create("true and (false or not true)");

// Evaluate it
bool result = expression.Evaluate();

Console.WriteLine(result); // Output: False
```

## Supported Operators

The following operators are supported, in order of precedence:

1.  `()` - Grouping
2.  `not`, `!` - Logical NOT
3.  `xor` - Exclusive OR
4.  `and` - Logical AND
5.  `or` - Logical OR

## Contributing

Contributions are welcome! If you'd like to contribute, please feel free to submit a pull request.

### Building from Source

To build the library from source, you'll need the .NET SDK.

1.  Clone the repository:
    ```bash
    git clone https://github.com/whawkward/Boolit.net.git
    ```
2.  Navigate to the source directory:
    ```bash
    cd Boolit.net/src/Boolit.net
    ```
3.  Build the project:
    ```bash
    dotnet build --configuration Release
    ```

### Testing

The solution includes unit tests. To run them, navigate to the root of the repository and execute:

```bash
dotnet test
```

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

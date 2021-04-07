# NQL - .NET Query Language

This project provides an API to compile text to LINQ using [C# Expression Trees](https://docs.microsoft.com/en-us/dotnet/csharp/expression-trees) and [ANTLR](https://github.com/antlr/antlr4).

## Usage

```csharp
var source = Enumerable.Range(1, 10).AsQueryable();
var nqlExpressionBuilder = (INqlExpressionBuilder)serviceProvider.GetService(typeof(INqlExpressionBuilder));
var expression = nqlExpressionBuilder.Build("| skip 5 | take 5", source.Expression);
var result = source.Provider.CreateQuery<int>(expression);

// Generated expression is equal to following LINQ

var source = Enumerable.Range(1, 10).AsQueryable();
var result = source.Skip(5).Take(5);
```
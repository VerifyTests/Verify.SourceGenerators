# <img src="/src/icon.png" height="30px"> Verify.SourceGenerators

[![Build status](https://ci.appveyor.com/api/projects/status/2ip7do6jk0gevt0v?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-SourceGenerators)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.SourceGenerators.svg)](https://www.nuget.org/packages/Verify.SourceGenerators/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of C# Source Generators.

<a href='https://dotnetfoundation.org' alt='Part of the .NET Foundation'><img src='https://raw.githubusercontent.com/VerifyTests/Verify/master/docs/dotNetFoundation.svg' height='30px'></a><br>
Part of the <a href='https://dotnetfoundation.org' alt=''>.NET Foundation</a>


## NuGet package

https://nuget.org/packages/Verify.SourceGenerators/


## Initialize

Call `VerifySourceGenerators.Enable()` once at assembly load time.


## Generator

Given a Source Generator:

<!-- snippet: HelloWorldGenerator.cs -->
<a id='snippet-HelloWorldGenerator.cs'></a>
```cs
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[Generator]
public class HelloWorldGenerator :
    ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var source = @"using System;

public static class HelloWorld
{
    public static void SayHello()
    {
        Console.WriteLine(""Hello from generated code!"");
    }
}";
        context.AddSource("helloWorldGenerator", SourceText.From(source, Encoding.UTF8));

        var descriptor = new DiagnosticDescriptor(
            id: "theId",
            title: "the title",
            messageFormat: "the descriptor",
            category: "the category",
            DiagnosticSeverity.Info,
            isEnabledByDefault: true);

        var location = Location.Create(
            "theFile",
            new TextSpan(1, 2),
            new LinePositionSpan(
                new LinePosition(1, 2),
                new LinePosition(3, 4)));
        var diagnostic = Diagnostic.Create(descriptor, location);
        context.ReportDiagnostic(diagnostic);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}
```
<sup><a href='/src/SampleGenerator/HelloWorldGenerator.cs#L1-L43' title='Snippet source file'>snippet source</a> | <a href='#snippet-HelloWorldGenerator.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Test

Can be tested as follows:

<!-- snippet: SampleTest.cs -->
<a id='snippet-SampleTest.cs'></a>
```cs
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class SampleTest
{
    [Fact]
    public Task Run()
    {
        var compilation = CreateCompilation();
        HelloWorldGenerator generator = new();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        var result = driver.GetRunResult();

        return Verifier.Verify(result);
    }

    static Compilation CreateCompilation()
    {
        return CSharpCompilation.Create(
            "compilation",
            Enumerable.Empty<SyntaxTree>(),
            new[] {MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location)},
            new CSharpCompilationOptions(OutputKind.ConsoleApplication));
    }
}
```
<sup><a href='/src/Tests/SampleTest.cs#L1-L35' title='Snippet source file'>snippet source</a> | <a href='#snippet-SampleTest.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Results

And wil result in the following verified files:

<!-- snippet: SampleTest.Run.00.verified.txt -->
<a id='snippet-SampleTest.Run.00.verified.txt'></a>
```txt
{
  Diagnostics: [
    {
      Id: theId,
      Title: the title,
      Severity: Info,
      WarningLevel: 1,
      Location: ExternalFile(theFile@2:3),
      MessageFormat: the descriptor,
      Category: the category
    }
  ]
}
```
<sup><a href='/src/Tests/SampleTest.Run.00.verified.txt#L1-L13' title='Snippet source file'>snippet source</a> | <a href='#snippet-SampleTest.Run.00.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

<!-- snippet: SampleTest.Run.01.verified.cs -->
<a id='snippet-SampleTest.Run.01.verified.cs'></a>
```cs
//HintName: helloWorldGenerator.cs
using System;

public static class HelloWorld
{
    public static void SayHello()
    {
        Console.WriteLine("Hello from generated code!");
    }
}
```
<sup><a href='/src/Tests/SampleTest.Run.01.verified.cs#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-SampleTest.Run.01.verified.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Notes:

 * [Source Generators Cookbook / Testing](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md#unit-testing-of-generators)


## Icon

[Sauce](https://thenounproject.com/term/sauce/952995/) designed by [April Hsuan](https://thenounproject.com/AprilHsuan/) from [The Noun Project](https://thenounproject.com/).

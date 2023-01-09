# <img src="/src/icon.png" height="30px"> Verify.SourceGenerators

[![Build status](https://ci.appveyor.com/api/projects/status/2ip7do6jk0gevt0v?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-SourceGenerators)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.SourceGenerators.svg)](https://www.nuget.org/packages/Verify.SourceGenerators/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of C# Source Generators.


## NuGet package

https://nuget.org/packages/Verify.SourceGenerators/

Install one of the Verify [testing framework adapters](https://github.com/verifytests/verify#nuget-packages) NuGet packages.


## Initialize

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Init() =>
    VerifySourceGenerators.Enable();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Generator

Given a Source Generator:

<!-- snippet: HelloWorldGenerator.cs -->
<a id='snippet-HelloWorldGenerator.cs'></a>
```cs
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[Generator]
public class HelloWorldGenerator :
    ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var source1 = @"using System;
public static class Helper
{
    public static void Method()
    {
    }
}";
        context.AddSource("helper", SourceText.From(source1, Encoding.UTF8));

        var source2 = @"using System;
public static class HelloWorld
{
    public static void SayHello()
    {
        Console.WriteLine(""Hello from generated code!"");
    }
}";
        context.AddSource("helloWorld", SourceText.From(source2, Encoding.UTF8));

        var descriptor = new DiagnosticDescriptor(
            id: "theId",
            title: "the title",
            messageFormat: "the message from {0}",
            category: "the category",
            DiagnosticSeverity.Info,
            isEnabledByDefault: true);

        var location = Location.Create(
            "theFile",
            new(1, 2),
            new(
                new(1, 2),
                new(3, 4)));
        var diagnostic = Diagnostic.Create(descriptor, location, "hello world generator");
        context.ReportDiagnostic(diagnostic);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}
```
<sup><a href='/src/SampleGenerator/HelloWorldGenerator.cs#L1-L50' title='Snippet source file'>snippet source</a> | <a href='#snippet-HelloWorldGenerator.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Test

Can be tested as follows:

This snippets assumes use of the XUnit Verify adapter, change the `using VerifyXUnit` if using other testing frameworks.

<!-- snippet: SampleTest.cs -->
<a id='snippet-SampleTest.cs'></a>
```cs
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

[UsesVerify]
    public class SampleTest
    {
    [Fact]
    public Task Driver()
    {
        var driver = GeneratorDriver();

        return Verify(driver);
    }

    [Fact]
    public Task RunResults()
    {
        var driver = GeneratorDriver();

        var runResults = driver.GetRunResult();
        return Verify(runResults);
    }

    [Fact]
    public Task RunResult()
    {
        var driver = GeneratorDriver();

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult);
    }

    static GeneratorDriver GeneratorDriver()
    {
        var compilation = CSharpCompilation.Create("name");
        var generator = new HelloWorldGenerator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        return driver.RunGenerators(compilation);
    }
}
```
<sup><a href='/src/Tests/SampleTest.cs#L1-L42' title='Snippet source file'>snippet source</a> | <a href='#snippet-SampleTest.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Results

And will result in the following verified files:


### Info file

An info file containing all metadata about the current state. eg any Diagnostics.

<!-- snippet: SampleTest.Driver.verified.txt -->
<a id='snippet-SampleTest.Driver.verified.txt'></a>
```txt
{
  Diagnostics: [
    {
      Id: theId,
      Title: the title,
      Severity: Info,
      WarningLevel: 1,
      Location: theFile: (1,2)-(3,4),
      Description: ,
      HelpLink: ,
      MessageFormat: the message from {0},
      Message: the message from hello world generator,
      Category: the category
    }
  ]
}
```
<sup><a href='/src/Tests/SampleTest.Driver.verified.txt#L1-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-SampleTest.Driver.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Source Files

Multiple source files. One for each `GeneratorDriverRunResult.Results.GeneratedSources`.

<!-- snippet: SampleTest.Driver#helloWorld.verified.cs -->
<a id='snippet-SampleTest.Driver#helloWorld.verified.cs'></a>
```cs
//HintName: helloWorld.cs
using System;
public static class HelloWorld
{
    public static void SayHello()
    {
        Console.WriteLine("Hello from generated code!");
    }
}
```
<sup><a href='/src/Tests/SampleTest.Driver#helloWorld.verified.cs#L1-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-SampleTest.Driver#helloWorld.verified.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Manipulating Source

To manipulating the source of the generated cs files, use [Scrubbers](https://github.com/VerifyTests/Verify/blob/main/docs/scrubbers.md.

For example to remove all lines start with `using`:

<!-- snippet: ScrubLines -->
<a id='snippet-scrublines'></a>
```cs
[Fact]
public Task ScrubLines()
{
    var driver = GeneratorDriver();

    return Verify(driver)
        .ScrubLines(_ => _.StartsWith("using "));
}
```
<sup><a href='/src/Tests/ScrubTest.cs#L7-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-scrublines' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
 

## Notes:

 * [Source Generators Cookbook / Testing](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md#unit-testing-of-generators)


## Icon

[Sauce](https://thenounproject.com/term/sauce/952995/) designed by [April Hsuan](https://thenounproject.com/AprilHsuan/) from [The Noun Project](https://thenounproject.com/).

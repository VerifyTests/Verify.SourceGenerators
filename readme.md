# <img src="/src/icon.png" height="30px"> Verify.SourceGenerators

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/2ip7do6jk0gevt0v?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-SourceGenerators)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.SourceGenerators.svg)](https://www.nuget.org/packages/Verify.SourceGenerators/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of C# Source Generators.<!-- singleLineInclude: intro. path: /docs/intro.include.md -->

**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.SourceGenerators) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](https://raw.githubusercontent.com/VerifyTests/Verify.SourceGenerators/refs/heads/main/docs/zzz.png)](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.SourceGenerators)<!-- endInclude -->


## NuGet

 * https://nuget.org/packages/Verify.SourceGenerators


## Initialize

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Init() =>
    VerifySourceGenerators.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Generator

Given a Source Generator:

<!-- snippet: HelloWorldGenerator.cs -->
<a id='snippet-HelloWorldGenerator.cs'></a>
```cs
[Generator]
public class HelloWorldGenerator :
    ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var source1 = """
                      using System;
                      public static class Helper
                      {
                          public static void Method()
                          {
                          }
                      }
                      """;
        context.AddSource("helper", SourceText.From(source1, Encoding.UTF8));

        var source2 = """
                      using System;
                      public static class HelloWorld
                      {
                          public static void SayHello()
                          {
                              Console.WriteLine("Hello from generated code!");
                          }
                      }
                      """;
        var sourceText = SourceText.From(source2, Encoding.UTF8);
        context.AddSource("helloWorld", sourceText);

        var descriptor = new DiagnosticDescriptor(
            id: "theId",
            title: "the title",
            messageFormat: "the message from {0}",
            category: "the category",
            DiagnosticSeverity.Info,
            isEnabledByDefault: true);

        var location = Location.Create(
            Path.Combine("dir", "theFile.cs"),
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
<sup><a href='/src/SampleGenerator/HelloWorldGenerator.cs#L1-L52' title='Snippet source file'>snippet source</a> | <a href='#snippet-HelloWorldGenerator.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Test

Can be tested as follows:

This snippets assumes use of the XUnit Verify adapter, change the `using VerifyXUnit` if using other testing frameworks.

<!-- snippet: SampleTest.cs -->
<a id='snippet-SampleTest.cs'></a>
```cs
public class SampleTest
{
    [Fact]
    public Task Driver()
    {
        var driver = BuildDriver();

        return Verify(driver);
    }

    [Fact]
    public Task RunResults()
    {
        var driver = BuildDriver();

        var results = driver.GetRunResult();
        return Verify(results);
    }

    [Fact]
    public Task RunResult()
    {
        var driver = BuildDriver();

        var result = driver.GetRunResult().Results.Single();
        return Verify(result);
    }

    static GeneratorDriver BuildDriver()
    {
        var compilation = CSharpCompilation.Create("name");
        var generator = new HelloWorldGenerator();

        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }
}
```
<sup><a href='/src/Tests/SampleTest.cs#L1-L37' title='Snippet source file'>snippet source</a> | <a href='#snippet-SampleTest.cs' title='Start of snippet'>anchor</a></sup>
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
      Location: dir\theFile.cs: (1,2)-(3,4),
      Message: the message from hello world generator,
      Severity: Info,
      WarningLevel: 1,
      Descriptor: {
        Id: theId,
        Title: the title,
        MessageFormat: the message from {0},
        Category: the category,
        DefaultSeverity: Info,
        IsEnabledByDefault: true
      }
    }
  ]
}
```
<sup><a href='/src/Tests/SampleTest.Driver.verified.txt#L1-L18' title='Snippet source file'>snippet source</a> | <a href='#snippet-SampleTest.Driver.verified.txt' title='Start of snippet'>anchor</a></sup>
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

To manipulating the source of the generated cs files, use [Scrubbers](https://github.com/VerifyTests/Verify/blob/main/docs/scrubbers.md).

For example to remove all lines start with `using`:

<!-- snippet: ScrubLines -->
<a id='snippet-ScrubLines'></a>
```cs
[Fact]
public Task ScrubLines()
{
    var driver = GeneratorDriver();

    return Verify(driver)
        .ScrubLines(_ => _.StartsWith("using "));
}
```
<sup><a href='/src/Tests/ScrubTest.cs#L3-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-ScrubLines' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
 

  ## Ignoring Files

To ignore specific source text use `IgnoreGeneratedResult`. This uses an expression of type `Func<GeneratedSourceResult, bool>` to determine which outputs are ignored.

For example to ignore files with the name `helper` or that contain the text `static void SayHello()`:

<!-- snippet: IgnoreFile -->
<a id='snippet-IgnoreFile'></a>
```cs
[Fact]
public Task IgnoreFile()
{
    var driver = GeneratorDriver();

    return Verify(driver)
        .IgnoreGeneratedResult(
            _ => _.HintName.Contains("helper") ||
                 _.SourceText
                     .ToString()
                     .Contains("static void SayHello()"));
}
```
<sup><a href='/src/Tests/IgnoreTest.cs#L3-L18' title='Snippet source file'>snippet source</a> | <a href='#snippet-IgnoreFile' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Notes:

 * [Source Generators Cookbook / Testing](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md#unit-testing-of-generators)


## Icon

[Sauce](https://thenounproject.com/term/sauce/952995/) designed by [April Hsuan](https://thenounproject.com/AprilHsuan/) from [The Noun Project](https://thenounproject.com/).

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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

        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }
}
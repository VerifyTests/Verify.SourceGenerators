using Microsoft.CodeAnalysis.VisualBasic;

public class SampleVbTest
{
    [Fact]
    public Task Driver()
    {
        var driver = BuildDriver<HelloWorldVbGenerator>();

        return Verify(driver);
    }

    [Fact]
    public Task RunResults()
    {
        var driver = BuildDriver<HelloWorldVbGenerator>();

        var results = driver.GetRunResult();
        return Verify(results);
    }

    [Fact]
    public Task RunResult()
    {
        var driver = BuildDriver<HelloWorldVbGenerator>();

        var result = driver.GetRunResult().Results.Single();
        return Verify(result);
    }

    [Fact]
    public Task DriverWithoutLocation()
    {
        var driver = BuildDriver<HelloWorldVbGeneratorWithoutLocation>();

        return Verify(driver);
    }

    [Fact]
    public Task RunResultsWithoutLocation()
    {
        var driver = BuildDriver<HelloWorldVbGeneratorWithoutLocation>();

        var results = driver.GetRunResult();
        return Verify(results);
    }

    [Fact]
    public Task RunResultWithoutLocation()
    {
        var driver = BuildDriver<HelloWorldVbGeneratorWithoutLocation>();

        var result = driver.GetRunResult().Results.Single();
        return Verify(result);
    }

    static GeneratorDriver BuildDriver<T>()
        where T : ISourceGenerator, new()
    {
        var compilation = VisualBasicCompilation.Create("name");
        var generator = new T();

        var vbGeneratorDriver = VisualBasicGeneratorDriver.Create([generator]);
        return vbGeneratorDriver.RunGenerators(compilation);
    }
}
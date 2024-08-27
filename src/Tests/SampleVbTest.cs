using Microsoft.CodeAnalysis.VisualBasic;

public class SampleVbTest
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
        var compilation = VisualBasicCompilation.Create("name");
        var generator = new HelloWorldVbGenerator();

        var vbGeneratorDriver = VisualBasicGeneratorDriver.Create([generator]);
        return vbGeneratorDriver.RunGenerators(compilation);
    }
}
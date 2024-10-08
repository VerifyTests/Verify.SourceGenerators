﻿public class SampleTest
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
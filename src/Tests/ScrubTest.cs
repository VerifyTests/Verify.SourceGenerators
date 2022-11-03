using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

[UsesVerify]
public class ScrubTest
{
    #region ScrubLines
    [Fact]
    public Task ScrubLines()
    {
        var driver = GeneratorDriver();

        return Verify(driver)
            .ScrubLines(_ => _.StartsWith("using "));
    }
    #endregion

    static GeneratorDriver GeneratorDriver()
    {
        var compilation = CSharpCompilation.Create("name");
        var generator = new HelloWorldGenerator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        return driver.RunGenerators(compilation);
    }
}
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

[UsesVerify]
public class SampleTest
{
    [Fact]
    public Task Run()
    {
        var compilation = CSharpCompilation.Create("name");
        HelloWorldGenerator generator = new();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        return Verify(driver);
    }
}
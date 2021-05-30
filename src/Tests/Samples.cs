using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class Samples
{
    [Fact]
    public Task SimpleGeneratorTest()
    {
        var inputCompilation = CreateCompilation();
        HelloWorldGenerator generator = new();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        var runResult = driver.GetRunResult();

        return Verifier.Verify(runResult);
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
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using VerifyTests;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class Samples
{
    static Samples()
    {
        #region Initialize
        VerifySourceGenerators.Enable();
        #endregion
    }

    [Fact]
    public Task SimpleGeneratorTest()
    {
        var inputCompilation = CreateCompilation(@"
public class Program
{
    public static void Main(string[] args)
    {
    }
}
");
        HelloWorldGenerator generator = new();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

        var runResult = driver.GetRunResult();

        return Verifier.Verify(runResult);
    }

    static Compilation CreateCompilation(string source)
    {
        return CSharpCompilation.Create("compilation",
            new[] {CSharpSyntaxTree.ParseText(source)},
            new[] {MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location)},
            new CSharpCompilationOptions(OutputKind.ConsoleApplication));
    }
}
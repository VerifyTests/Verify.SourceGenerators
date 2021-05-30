using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[Generator]
public class HelloWorldGenerator :
    ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var sourceBuilder = new StringBuilder(@"
using System;
public static class HelloWorld
{
    public static void SayHello()
    {
        Console.WriteLine(""Hello from generated code!"");
        Console.WriteLine(""The following syntax trees existed in the compilation that created this program:"");
    }
}");
        context.AddSource("helloWorldGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}
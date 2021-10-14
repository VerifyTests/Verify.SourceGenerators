using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[Generator]
public class HelloWorldGenerator :
    ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var source = @"using System;
public static class HelloWorld
{
    public static void SayHello()
    {
        Console.WriteLine(""Hello from generated code!"");
    }
}";
        context.AddSource("helloWorldGenerator", SourceText.From(source, Encoding.UTF8));

        var descriptor = new DiagnosticDescriptor(
            id: "theId",
            title: "the title",
            messageFormat: "the message from {0}",
            category: "the category",
            DiagnosticSeverity.Info,
            isEnabledByDefault: true);

        var location = Location.Create(
            "theFile",
            new TextSpan(1, 2),
            new LinePositionSpan(
                new LinePosition(1, 2),
                new LinePosition(3, 4)));
        var diagnostic = Diagnostic.Create(descriptor, location, "hello world generator");
        context.ReportDiagnostic(diagnostic);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}
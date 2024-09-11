using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[Generator]
public class HelloWorldVbGenerator :
    ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var source1 = """
                      Imports System
                      Public Module Helper
                          Public Sub Method()
                          End Sub
                      End Module
                      """;
        context.AddSource("helper", SourceText.From(source1, Encoding.UTF8));

        var source2 = """
                      Imports System

                      Public Module HelloWorld
                          Public Sub SayHello()
                              Console.WriteLine("Hello from generated code!")
                          End Sub
                      End Module
                      """;
        var sourceText = SourceText.From(source2, Encoding.UTF8);
        context.AddSource("helloWorld", sourceText);

        var descriptor = new DiagnosticDescriptor(
            id: "theId",
            title: "the title",
            messageFormat: "the message from {0}",
            category: "the category",
            DiagnosticSeverity.Info,
            isEnabledByDefault: true);

        var location = Location.Create(
            Path.Combine("dir", "theFile.vb"),
            new(1, 2),
            new(
                new(1, 2),
                new(3, 4)));
        var diagnostic = Diagnostic.Create(descriptor, location, "hello world generator");
        context.ReportDiagnostic(diagnostic);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}
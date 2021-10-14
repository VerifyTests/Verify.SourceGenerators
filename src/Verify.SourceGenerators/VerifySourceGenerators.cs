using Microsoft.CodeAnalysis;

namespace VerifyTests;

public static class VerifySourceGenerators
{
    public static void Enable()
    {
        VerifierSettings.ModifySerialization(settings =>
        {
            settings.AddExtraSettings(serializer =>
            {
                var converters = serializer.Converters;
                converters.Add(new LocalizableStringConverter());
                converters.Add(new DiagnosticConverter());
                converters.Add(new LocationConverter());
                converters.Add(new GeneratedSourceResultConverter());
                converters.Add(new DiagnosticDescriptorConverter());
                converters.Add(new SourceTextConverter());
            });
        });
        VerifierSettings.RegisterFileConverter<GeneratorDriver>(Convert);
        VerifierSettings.RegisterFileConverter<GeneratorDriverRunResult>(Convert);
    }

    static ConversionResult Convert(GeneratorDriverRunResult target, IReadOnlyDictionary<string, object> context)
    {
        var exceptions = new List<Exception>();
        var targets = new List<Target>();
        foreach (var result in target.Results)
        {
            if (result.Exception != null)
            {
                exceptions.Add(result.Exception);
            }

            targets.AddRange(result.GeneratedSources.Select(SourceToTarget));
        }

        if (exceptions.Count == 1)
        {
            throw exceptions.First();
        }

        if (exceptions.Count > 1)
        {
            throw new AggregateException(exceptions);
        }

        if (target.Diagnostics.Any())
        {
            var info = new
            {
                target.Diagnostics
            };
            return new(info, targets);
        }

        return new(null, targets);
    }

    static Target SourceToTarget(GeneratedSourceResult source)
    {
        var data = $@"//HintName: {source.HintName}
{source.SourceText}";
        return new("cs", data);
    }

    static ConversionResult Convert(GeneratorDriver target, IReadOnlyDictionary<string, object> context)
    {
        return Convert(target.GetRunResult(), context);
    }
}
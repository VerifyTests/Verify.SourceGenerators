using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace VerifyTests
{
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

            var info = new
            {
                target.Diagnostics,
                Exceptions = exceptions
            };
            return new ConversionResult(info, targets);
        }

        static Target SourceToTarget(GeneratedSourceResult source)
        {
            var data = $@"//HintName: {source.HintName}{source.SourceText}";
            return new Target("cs", data);
        }

        static ConversionResult Convert(GeneratorDriver target, IReadOnlyDictionary<string, object> context)
        {
            return Convert(target.GetRunResult(), context);
        }
    }
}
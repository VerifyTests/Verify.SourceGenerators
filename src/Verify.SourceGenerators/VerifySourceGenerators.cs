using System;
using System.Collections.Generic;
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

                foreach (var source in result.GeneratedSources)
                {
                    targets.Add(new Target("cs", source.SourceText.ToString()));
                }
            }

            var info = new
            {
                target.Diagnostics,
                Exceptions = exceptions
            };
            return new ConversionResult(info, targets);
        }

        static ConversionResult Convert(GeneratorDriver target, IReadOnlyDictionary<string, object> context)
        {
            return Convert(target.GetRunResult(), context);
        }
    }
}
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace VerifyTests
{
    public static class VerifySourceGenerators
    {
        public static void Enable()
        {
            VerifierSettings.RegisterFileConverter<GeneratorDriver>(Convert);
            VerifierSettings.RegisterFileConverter<GeneratorDriverRunResult>(Convert);
        }

        private static ConversionResult Convert(GeneratorDriverRunResult target, IReadOnlyDictionary<string, object> context)
        {
            var list = new List<Target>();
            foreach (var result in target.Results)
            {
                foreach (var source in result.GeneratedSources)
                {
                    list.Add(new Target("cs", source.SourceText.ToString()));
                }
            }

            return new ConversionResult(new {target.Diagnostics}, list);
        }

        private static ConversionResult Convert(GeneratorDriver target, IReadOnlyDictionary<string, object> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
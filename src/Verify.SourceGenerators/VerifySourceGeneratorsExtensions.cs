using Microsoft.CodeAnalysis;

namespace VerifyTests;

public static class VerifySourceGeneratorsExtensions
{
    internal const string IgnoreContextName = $"{nameof(VerifySourceGenerators)}.{nameof(IgnoreGeneratedResultInstance)}";

    public static SettingsTask IgnoreGeneratedResultInstance(this SettingsTask settingsTask, Func<GeneratedSourceResult, bool> shouldIgnore)
    {
        settingsTask.CurrentSettings.IgnoreGeneratedResultInstance(shouldIgnore);
        return settingsTask;
    }

    public static void IgnoreGeneratedResultInstance(this VerifySettings verifySettings, Func<GeneratedSourceResult, bool> shouldIgnore)
    {
        if (!verifySettings.Context.TryGetValue(IgnoreContextName, out var value))
        {
            value = new List<Func<GeneratedSourceResult, bool>>();
            verifySettings.Context.Add(IgnoreContextName, value);
        }

        if (value is not List<Func<GeneratedSourceResult, bool>> ignoreList)
        {
            throw new($"Unexpected value in {nameof(verifySettings.Context)}, type is not {nameof(List<Func<GeneratedSourceResult, bool>>)}");
        }

        ignoreList.Add(shouldIgnore);
    }
}
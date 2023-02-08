public static class ModuleInitializer
{
    #region enable

    [ModuleInitializer]
    public static void Init() =>
        VerifySourceGenerators.Initialize();

    #endregion

    [ModuleInitializer]
    public static void InitOther() =>
        VerifyDiffPlex.Initialize();
}
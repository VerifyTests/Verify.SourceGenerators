public static class ModuleInitializer
{
    #region enable

    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Enable();

        #endregion

        VerifyDiffPlex.Initialize();
    }
}
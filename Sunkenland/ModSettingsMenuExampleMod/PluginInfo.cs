namespace ModSettingsMenuExampleMod
{
    public static class PluginInfo
    {
        // Keep the GUID stable because both BepInEx and ModSettingsMenu use it to identify this plugin.
        public const string PLUGIN_GUID = "IceBoxStudio.Sunkenland.ModSettingsMenuExampleMod";
        // NAME is used in BepInEx metadata and as the fallback when no custom menu metadata is registered.
        public const string PLUGIN_NAME = "ModSettingsMenuExampleMod";

        // When changing the version, also check the csproj, AssemblyInfo, and release dependency declarations.
        public const string PLUGIN_VERSION = "1.0.0";

        // ModSettingsMenu displays this value directly in the mod information area.
        public const string PLUGIN_AUTHOR = "Ice Box Studio";
    }
}

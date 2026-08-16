namespace ModSettingsMenuLocalizationExampleMod
{
    public static class PluginInfo
    {
        // The GUID, name, and version are used by BepInEx metadata, config files, and the menu registry.
        public const string PLUGIN_GUID = "IceBoxStudio.Sunkenland.ModSettingsMenuLocalizationExampleMod";

        // The JSON mod.name overrides tab text, while this English name remains the fallback.
        public const string PLUGIN_NAME = "ModSettingsMenuLocalizationExampleMod";

        // Keep this version synchronized with BepInPlugin, the csproj, and assembly metadata.
        public const string PLUGIN_VERSION = "1.0.0";

        // Author metadata does not change with the language and appears directly in the mod information area.
        public const string PLUGIN_AUTHOR = "Ice Box Studio";
    }
}

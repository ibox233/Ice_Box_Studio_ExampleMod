using ModSettingsMenu.Api;

namespace ModSettingsMenuExampleMod.Configuration
{
    internal static class ModSettingsMenuRegistration
    {
        internal static void Register()
        {
            // Settings are discovered automatically; this registration adds metadata to the information area.
            // The non-localized example uses fixed English text for mods that provide an English-only interface.
            ModSettingsRegistry.Register(
                PluginInfo.PLUGIN_GUID,
                new ModSettingsModOptions
                {
                    Name = "Mod Settings Menu Example",
                    Description = "Demonstrates ModSettingsMenu without the localization API.",
                    Author = PluginInfo.PLUGIN_AUTHOR,
                    Version = PluginInfo.PLUGIN_VERSION
                });
        }
    }
}

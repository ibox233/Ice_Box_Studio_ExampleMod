using ModSettingsMenu.Api;

namespace ModSettingsMenuExampleMod.Configuration
{
    internal static class ModSettingsMenuRegistration
    {
        internal static void Register()
        {
            // Registration only supplies mod metadata and optional website links.
            // The menu finds this plugin's ConfigFile by the same plugin GUID.
            ModSettingsRegistry.Register(
                PluginInfo.PLUGIN_GUID,
                new ModSettingsModOptions
                {
                    Name = "Mod Settings Menu Example",
                    Description = "Demonstrates Mod Settings Menu without a localization API.",
                    Author = PluginInfo.PLUGIN_AUTHOR,
                    Version = PluginInfo.PLUGIN_VERSION,
                    NexusModsId = 37,
                    ThunderstoreTeam = "MyTeam",
                    ThunderstoreModName = "MyMod"
                });
        }
    }
}

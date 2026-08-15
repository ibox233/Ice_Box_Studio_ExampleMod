using ModSettingsMenu.Api;

namespace ModSettingsMenuLocalizationExampleMod.Configuration
{
    internal static class ModSettingsMenuRegistration
    {
        internal static void Register()
        {
            // Name and Description provide English fallbacks. The localized
            // callback and registered description are refreshed after the game
            // language changes.
            ModSettingsRegistry.Register(
                PluginInfo.PLUGIN_GUID,
                new ModSettingsModOptions
                {
                    Name = "Localized Mod Settings Menu Example",
                    LocalizedName = () => I18n.Text("mod.name"),
                    Description = I18n.Text("mod.description"),
                    Author = PluginInfo.PLUGIN_AUTHOR,
                    Version = PluginInfo.PLUGIN_VERSION,
                    NexusModsId = 37,
                    ThunderstoreTeam = "MyTeam",
                    ThunderstoreModName = "MyMod"
                });
        }
    }
}

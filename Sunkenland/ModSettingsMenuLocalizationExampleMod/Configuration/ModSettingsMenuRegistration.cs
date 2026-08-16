using ModSettingsMenu.Api;
using ModSettingsMenuLocalizationExampleMod.Localization;

namespace ModSettingsMenuLocalizationExampleMod.Configuration
{
    internal static class ModSettingsMenuRegistration
    {
        internal static void Register()
        {
            // Description captures the current language during registration, so Register must run again after a language change.
            // LocalizedName is a callback that resolves the current mod name whenever the menu rebuilds its tabs.
            ModSettingsRegistry.Register(
                PluginInfo.PLUGIN_GUID,
                new ModSettingsModOptions
                {
                    Name = "Mod Settings Localization Example",
                    LocalizedName = () => I18n.Text("mod.name"),
                    Description = I18n.Text("mod.description"),
                    Author = PluginInfo.PLUGIN_AUTHOR,
                    Version = PluginInfo.PLUGIN_VERSION
                });
        }
    }
}

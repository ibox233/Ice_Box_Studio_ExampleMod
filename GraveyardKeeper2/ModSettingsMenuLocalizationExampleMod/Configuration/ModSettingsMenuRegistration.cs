using ModSettingsMenu.Api;
using ModSettingsMenuLocalizationExampleMod.Localization;

namespace ModSettingsMenuLocalizationExampleMod.Configuration
{
    internal static class ModSettingsMenuRegistration
    {
        internal static void Register()
        {
            // Description captures the current language during registration, so register again after a language change.
            // The plugin's LanguageChanged handler already calls this method; no config rebinding is needed.
            // LocalizedName resolves the current mod name whenever the menu rebuilds its list; Name is the default name.
            ModSettingsRegistry.Register(
                PluginInfo.PLUGIN_GUID,
                new ModSettingsModOptions
                {
                    Name = "Mod Settings Localization Example",
                    LocalizedName = () => I18n.Text("mod.name"),
                    Description = I18n.Text("mod.description"),
                    Author = PluginInfo.PLUGIN_AUTHOR,
                    Version = PluginInfo.PLUGIN_VERSION,
                    // Demo values only: replace these with your published page details, or omit them to hide the links.
                    // NexusModsId is the numeric page ID. Platform identifiers remain unchanged across languages.
                    NexusModsId = 12345,
                    // Supply both Thunderstore fields using ASCII letters, numbers, and underscores only.
                    // They form /c/graveyard-keeper-2/p/ExampleTeam/ModSettingsMenuLocalizationExampleMod/.
                    ThunderstoreTeam = "ExampleTeam",
                    ThunderstoreModName = "ModSettingsMenuLocalizationExampleMod",
                    // Optional: uncomment with a real HTTPS JSON manifest containing version or latestVersion.
                    // For example: { "version": "1.2.0" }. The menu reports update status; it does not download updates.
                    // UpdateManifestUrl = "https://example.com/my-mod/version.json"
                });
        }
    }
}

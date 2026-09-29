using ModSettingsMenu.Api;

namespace ModSettingsMenuExampleMod.Configuration
{
    internal static class ModSettingsMenuRegistration
    {
        internal static void Register()
        {
            // Settings are discovered automatically; this optional registration adds metadata to the information area.
            // This example uses fixed English text and does not directly depend on the localization API.
            ModSettingsRegistry.Register(
                PluginInfo.PLUGIN_GUID,
                new ModSettingsModOptions
                {
                    Name = "Mod Settings Menu Example",
                    Description = "Demonstrates ModSettingsMenu without the localization API.",
                    Author = PluginInfo.PLUGIN_AUTHOR,
                    Version = PluginInfo.PLUGIN_VERSION,
                    // Demo values only: replace these with your published page details, or omit them to hide the links.
                    // NexusModsId is the numeric ID in the Nexus Mods URL, not a complete URL.
                    NexusModsId = 12345,
                    // Supply both Thunderstore fields using ASCII letters, numbers, and underscores only.
                    // They form /c/graveyard-keeper-2/p/ExampleTeam/ModSettingsMenuExampleMod/.
                    ThunderstoreTeam = "ExampleTeam",
                    ThunderstoreModName = "ModSettingsMenuExampleMod",
                    // Optional: uncomment with a real HTTPS JSON manifest containing version or latestVersion.
                    // For example: { "version": "1.2.0" }. The menu reports update status; it does not download updates.
                    // UpdateManifestUrl = "https://example.com/my-mod/version.json"
                });
        }
    }
}

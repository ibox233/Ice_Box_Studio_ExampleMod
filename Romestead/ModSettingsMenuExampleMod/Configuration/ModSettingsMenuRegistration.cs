using BepInEx.Configuration;
using ModSettingsMenu.Api;

namespace ModSettingsMenuExampleMod.Configuration
{
    internal static class ModSettingsMenuRegistration
    {
        public static void Register(ConfigFile config)
        {
            // Simple registration: mod metadata lives here, while entry and section metadata live next to Config.Bind.
            ModSettingsRegistry.Register(PluginInfo.PLUGIN_GUID, "Settings Menu API Example", config, new ModSettingsModOptions
            {
                Icon = "cog",
                // Optional: set IconPath to a PNG file if your mod ships a custom icon.
                // IconPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "icon.png"),
                Version = PluginInfo.PLUGIN_VERSION,
                Author = PluginInfo.PLUGIN_AUTHOR,
                Description = "Demonstrates the simplified Mod Settings Menu API: mod metadata, ConfigDescription tags, section ordering, entry display names, entry descriptions, slider steps, hidden entries, and native Bindings page keybinds.",
                // Optional: link to your NexusMods page.
                // NexusModsId = 8,
                // Optional: link to your Thunderstore page. Set both values together.
                // ThunderstoreTeam = "YourTeam",
                // ThunderstoreModName = "YourMod",
                // Optional: HTTPS JSON manifest used only for update checks.
                // UpdateManifestUrl = "https://example.com/my-mod/version.json",
                // Optional: show metadata-only mods even when there are no config entries.
                // HideWhenEmpty = false,
                // Both default to true. Set either to false to hide this mod from that menu.
                // ShowInMainMenu controls the menu opened from the main menu.
                ShowInMainMenu = true,
                // ShowInGame controls the menu opened from the in-game top bar.
                ShowInGame = true,
                // Mod list order. Lower values appear earlier in the left-side mod list.
                Order = -90
            });
        }
    }
}

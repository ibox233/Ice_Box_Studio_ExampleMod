using BepInEx.Configuration;
using ModSettingsMenu.Api;

namespace ModSettingsMenuLocalizationExampleMod.Configuration
{
    internal static class ModSettingsMenuRegistration
    {
        private static ConfigFile _config;
        private static string _locale;
        private static bool _subscribed;

        public static void Register(ConfigFile config)
        {
            _config = config;
            if (!_subscribed)
            {
                ModSettingsRegistry.RefreshRequested += Refresh;
                _subscribed = true;
            }

            RegisterCurrent(config);
        }

        private static void Refresh()
        {
            if (_config == null)
            {
                return;
            }

            var locale = I18n.L.CurrentLocale;
            if (_locale == locale && ModSettingsRegistry.TryGet(PluginInfo.PLUGIN_GUID, out _))
            {
                return;
            }

            RegisterCurrent(_config);
        }

        private static void RegisterCurrent(ConfigFile config)
        {
            // Simple registration: mod metadata lives here, while entry and section metadata live next to Config.Bind.
            ModSettingsRegistry.Register(PluginInfo.PLUGIN_GUID, Text("mod.name"), config, new ModSettingsModOptions
            {
                Icon = "cog",
                // Optional: set IconPath to a PNG file if your mod ships a custom icon.
                // IconPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "icon.png"),
                Version = PluginInfo.PLUGIN_VERSION,
                Author = PluginInfo.PLUGIN_AUTHOR,
                Description = Text("mod.description"),
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
                Order = -89
            });

            _locale = I18n.L.CurrentLocale;
        }

        private static string Text(string key)
        {
            return I18n.L.Text(key);
        }
    }
}

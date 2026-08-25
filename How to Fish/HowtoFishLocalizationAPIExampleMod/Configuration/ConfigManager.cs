using BepInEx.Configuration;
using YourMod.Localization;

namespace YourMod.Configuration
{
    internal static class ConfigManager
    {
        private static ConfigEntry<bool> _enabled;
        private static ConfigEntry<string> _message;

        internal static void Init(ConfigFile config)
        {
            // Localize menu labels with modmenu.config.<Section>.<Key> in the JSON file.
            _enabled = config.Bind("General", "Enabled", true, I18n.Text("config.enabled"));
            _message = config.Bind("Display", "Message", "Hello from YourMod.", I18n.Text("config.message"));
        }

        internal static bool Enabled => _enabled != null && _enabled.Value;
        internal static string Message => _message == null ? string.Empty : _message.Value;
    }
}

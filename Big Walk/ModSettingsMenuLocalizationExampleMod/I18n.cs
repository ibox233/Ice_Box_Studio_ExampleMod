using System.IO;
using System.Reflection;
using BigWalkLocalizationAPI.Api;

namespace ModSettingsMenuLocalizationExampleMod
{
    internal static class I18n
    {
        private const string FileName = "Localization.json";

        internal static ModLocalizer Localizer { get; private set; }

        internal static string Text(string key)
        {
            return Localizer.GetLocalizedText(key);
        }

        internal static string Text(string key, params object[] args)
        {
            return Localizer.GetLocalizedText(key, args);
        }

        internal static void Load()
        {
            // Load resources relative to the executing assembly. This works
            // for the game plugin folder, Nexus Mods archives, and Thunderstore
            // package layouts without relying on the current working directory.
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // LocalizationApi.For returns the localizer owned by this plugin.
            // RegisterJson only needs to run once; ModLocalizer reads the active
            // game language whenever a config label or value is requested.
            Localizer = LocalizationApi.For(PluginInfo.PLUGIN_GUID);
            Localizer.RegisterJson(Path.Combine(directory, FileName));
        }
    }
}

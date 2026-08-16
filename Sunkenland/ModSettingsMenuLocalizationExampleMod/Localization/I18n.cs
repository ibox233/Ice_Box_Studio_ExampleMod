using System.IO;
using System.Reflection;
using SunkenlandLocalizationAPI.Api;

namespace ModSettingsMenuLocalizationExampleMod.Localization
{
    internal static class I18n
    {
        // The JSON file must sit beside the plugin DLL and is copied to the game plugin directory during builds.
        private const string FileName = "ModSettingsMenuLocalizationExampleMod.Localization.json";

        // Static initialization loads the file once; language changes only resolve keys for the new language.
        private static readonly ModLocalizer _localizer = Load();

        internal static ModLocalizer Localizer
        {
            // ConfigManager reuses this instance for every setting instead of loading JSON for each binding.
            get { return _localizer; }
        }

        internal static string Text(string key, params object[] args)
        {
            // ModLocalizer checks the current locale first and falls back to en when a key is missing.
            return _localizer.GetLocalizedText(key, args);
        }

        private static ModLocalizer Load()
        {
            // Resolve the actual assembly location instead of assuming a fixed plugin folder name.
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ModLocalizer localizer = LocalizationApi.For(PluginInfo.PLUGIN_GUID);
            localizer.RegisterJson(Path.Combine(directory, FileName));
            return localizer;
        }
    }
}

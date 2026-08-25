using System.IO;
using System.Reflection;
using HowtoFishLocalizationAPI.Api;

namespace YourMod.Localization
{
    internal static class I18n
    {
        private const string FileName = "YourMod.Localization.json";
        private static readonly ModLocalizer _localizer = Load();

        internal static string Text(string key, params object[] args)
        {
            return _localizer.GetLocalizedText(key, args);
        }

        private static ModLocalizer Load()
        {
            // Keep the localization file beside the DLL instead of relying on a fixed plugin folder name.
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ModLocalizer localizer = LocalizationApi.For(PluginInfo.PLUGIN_GUID);
            localizer.RegisterJson(Path.Combine(directory, FileName));
            return localizer;
        }
    }
}

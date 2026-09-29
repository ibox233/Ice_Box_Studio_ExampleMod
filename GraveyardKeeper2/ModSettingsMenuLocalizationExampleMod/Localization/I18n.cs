using System.IO;
using System.Reflection;
using GraveyardKeeper2LocalizationAPI.Api;

namespace ModSettingsMenuLocalizationExampleMod.Localization
{
    internal static class I18n
    {
        private const string FileName = "ModSettingsMenuLocalizationExampleMod.Localization.json";

        private static readonly ModLocalizer _localizer = Load();

        internal static ModLocalizer Localizer
        {
            get { return _localizer; }
        }

        internal static string Text(string key, params object[] args)
        {
            return _localizer.GetLocalizedText(key, args);
        }

        private static ModLocalizer Load()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ModLocalizer localizer = LocalizationApi.For(PluginInfo.PLUGIN_GUID);
            localizer.RegisterJson(Path.Combine(directory, FileName));
            return localizer;
        }
    }
}

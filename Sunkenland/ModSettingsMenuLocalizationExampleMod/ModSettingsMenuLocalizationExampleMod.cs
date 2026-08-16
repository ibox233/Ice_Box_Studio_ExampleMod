using System;
using BepInEx;
using BepInEx.Logging;
using ModSettingsMenuLocalizationExampleMod.Configuration;
using SunkenlandLocalizationAPI;
using SunkenlandLocalizationAPI.Api;

namespace ModSettingsMenuLocalizationExampleMod
{
    // This example declares both menu and localization dependencies so their APIs initialize first.
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    // The menu API reads ConfigDescription.Tags and creates native-style settings controls.
    [BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID, "1.3.0")]
    // The localization API reads JSON, provides the en fallback, and raises game language change events.
    [BepInDependency(SunkenlandLocalizationAPI.PluginInfo.PLUGIN_GUID, "1.2.0")]
    public sealed class ModSettingsMenuLocalizationExampleModPlugin : BaseUnityPlugin
    {
        // The BepInEx logger records complete exceptions from localization or configuration failures.
        internal static ManualLogSource Log { get; private set; }

        private void Awake()
        {
            // Store the logger, then initialize in this order: bind settings, register the menu, subscribe to language changes.
            Log = Logger;

            try
            {
                // ConfigDescription tags created by ConfigManager resolve their text for the current language.
                ConfigManager.Bind(Config);

                // Description is captured during registration, so the initial language must be registered here.
                ModSettingsMenuRegistration.Register();

                // Re-register after language changes to refresh the mod title and information-area description.
                LocalizationApi.LanguageChanged += OnLanguageChanged;
                Log.LogInfo($"{PluginInfo.PLUGIN_NAME} loaded.");
            }
            catch (Exception ex)
            {
                // Missing JSON, duplicate config keys, or incompatible dependencies produce a complete exception here.
                Log.LogError($"Failed to initialize {PluginInfo.PLUGIN_NAME}: {ex}");
            }
        }

        private static void OnLanguageChanged(string language)
        {
            // language is the locale code raised by the API; I18n resolves text through the API's current language.
            ModSettingsMenuRegistration.Register();
        }
    }
}

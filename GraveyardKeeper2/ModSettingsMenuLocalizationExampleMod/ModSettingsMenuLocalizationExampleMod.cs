using System;
using BepInEx;
using BepInEx.Logging;
using ModSettingsMenuLocalizationExampleMod.Configuration;
using GraveyardKeeper2LocalizationAPI;
using GraveyardKeeper2LocalizationAPI.Api;

namespace ModSettingsMenuLocalizationExampleMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID, "1.0.0")]
    [BepInDependency(GraveyardKeeper2LocalizationAPI.PluginInfo.PLUGIN_GUID, "1.0.0")]
    public sealed class ModSettingsMenuLocalizationExampleModPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log { get; private set; }

        private void Awake()
        {
            Log = Logger;

            try
            {
                ConfigManager.Bind(Config);

                ModSettingsMenuRegistration.Register();

                LocalizationApi.LanguageChanged += OnLanguageChanged;
                Log.LogInfo($"{PluginInfo.PLUGIN_NAME} loaded.");
            }
            catch (Exception ex)
            {
                Log.LogError($"Failed to initialize {PluginInfo.PLUGIN_NAME}: {ex}");
            }
        }

        private static void OnLanguageChanged(string language)
        {
            ModSettingsMenuRegistration.Register();
        }
    }
}

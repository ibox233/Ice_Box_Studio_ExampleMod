using System;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using BigWalkLocalizationAPI.Api;
using ModSettingsMenuLocalizationExampleMod.Configuration;

namespace ModSettingsMenuLocalizationExampleMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(BigWalkLocalizationAPI.PluginInfo.PLUGIN_GUID, ">=1.1.0")]
    [BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID, ">=1.1.0")]
    public sealed class ModSettingsMenuLocalizationExampleMod : BasePlugin
    {
        public override void Load()
        {
            try
            {
                I18n.Load();
                ConfigManager.Bind(Config);
                ModSettingsMenuRegistration.Register();
                LocalizationApi.LanguageChanged += OnLanguageChanged;
                Log.LogInfo(I18n.Text("log.loaded", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION));
            }
            catch (Exception ex)
            {
                Log.LogError($"Failed to initialize {PluginInfo.PLUGIN_NAME}: {ex}");
            }
        }

        private void OnLanguageChanged(string language)
        {
            ModSettingsMenuRegistration.Register();
            Log.LogInfo(I18n.Text("log.language_changed", language));
        }
    }
}

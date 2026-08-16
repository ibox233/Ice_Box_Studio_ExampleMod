using System;
using BepInEx;
using BepInEx.Logging;
using ModSettingsMenuExampleMod.Configuration;

namespace ModSettingsMenuExampleMod
{
    // BepInPlugin is required for BepInEx 5 Mono to recognize the plugin entry point.
    // This example depends only on ModSettingsMenu and does not reference SunkenlandLocalizationAPI.
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    // The hard dependency ensures that BepInEx loads the menu API before this plugin's Awake method.
    [BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID, "1.3.0")]
    public sealed class ModSettingsMenuExampleModPlugin : BaseUnityPlugin
    {
        // Keep the BepInEx logger available so initialization failures can include the complete exception.
        internal static ManualLogSource Log { get; private set; }

        private void Awake()
        {
            // Store the logger before running configuration and menu registration code that may throw.
            Log = Logger;

            try
            {
                // ConfigManager creates this plugin's ConfigEntry instances, which the menu discovers automatically.
                ConfigManager.Bind(Config);

                // Register additional display metadata such as the mod name, description, author, and version.
                ModSettingsMenuRegistration.Register();
                Log.LogInfo($"{PluginInfo.PLUGIN_NAME} loaded.");
            }
            catch (Exception ex)
            {
                // Preserve the full stack trace so a failed Bind or Register call can be identified.
                Log.LogError($"Failed to initialize {PluginInfo.PLUGIN_NAME}: {ex}");
            }
        }
    }
}

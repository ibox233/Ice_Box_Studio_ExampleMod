using System;
using BepInEx;
using BepInEx.Logging;
using ModSettingsMenuExampleMod.Configuration;

namespace ModSettingsMenuExampleMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID, "1.0.0")]
    public sealed class ModSettingsMenuExampleModPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log { get; private set; }

        private void Awake()
        {
            Log = Logger;

            try
            {
                ConfigManager.Bind(Config);

                ModSettingsMenuRegistration.Register();
                Log.LogInfo($"{PluginInfo.PLUGIN_NAME} loaded.");
            }
            catch (Exception ex)
            {
                Log.LogError($"Failed to initialize {PluginInfo.PLUGIN_NAME}: {ex}");
            }
        }
    }
}

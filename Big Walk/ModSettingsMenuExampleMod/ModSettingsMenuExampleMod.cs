using BepInEx;
using BepInEx.Unity.IL2CPP;
using ModSettingsMenuExampleMod.Configuration;

namespace ModSettingsMenuExampleMod
{
    // This example deliberately has no BigWalkLocalizationAPI dependency.
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID, ">=1.1.0")]
    public sealed class ModSettingsMenuExampleMod : BasePlugin
    {
        public override void Load()
        {
            ConfigManager.Bind(Config);
            ModSettingsMenuRegistration.Register();
            Log.LogInfo($"{PluginInfo.PLUGIN_NAME} {PluginInfo.PLUGIN_VERSION} initialized.");
        }
    }
}

using System;
using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using ModSettingsMenu.Api;
using ModSettingsMenuExampleMod.Configuration;

namespace ModSettingsMenuExampleMod
{
    // This basic example deliberately has no RomesteadLocalizationAPI dependency.
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID)]
    public sealed class ModSettingsMenuExampleModPlugin : BasePlugin
    {
        internal static ManualLogSource Logger { get; private set; }

        public override void Load()
        {
            Logger = Log;

            try
            {
                ConfigManager.Init(Config);
                ModSettingsMenuRegistration.Register(Config);

                var multiHotkeyValue = ModKeybindValue.FromEntry(ConfigManager.ExampleMultiHotkey);
                Logger.LogInfo($"Example multi hotkey primary keyboard slot: {multiHotkeyValue.Keys[0]}");
                Logger.LogInfo($"Example multi hotkey display text: {multiHotkeyValue.ToDisplayString()}");
                Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} loaded.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"{PluginInfo.PLUGIN_NAME} initialization error: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}

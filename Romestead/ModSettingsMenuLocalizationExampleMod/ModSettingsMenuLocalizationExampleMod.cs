using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using ModSettingsMenu.Api;
using ModSettingsMenuLocalizationExampleMod.Configuration;
using RomesteadLocalizationAPI.Api;

namespace ModSettingsMenuLocalizationExampleMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID)]
    [BepInDependency(RomesteadLocalizationAPI.PluginInfo.PLUGIN_GUID)]
    public sealed class ModSettingsMenuLocalizationExampleModPlugin : BasePlugin
    {
        internal static ManualLogSource Logger { get; private set; }

        public override void Load()
        {
            Logger = Log;

            try
            {
                // Package managers can choose any plugin-folder name, so load JSON beside this DLL.
                var pluginDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                // Localization.json uses the locale -> key -> text layout.
                I18n.L.RegisterJson(Path.Combine(pluginDir, "Localization.json"));

                ConfigManager.Init(Config);
                ModSettingsMenuRegistration.Register(Config);
                I18n.L.LanguageChanged += OnLanguageChanged;

                var multiHotkeyValue = ModKeybindValue.FromEntry(ConfigManager.ExampleMultiHotkey);
                Logger.LogInfo(I18n.L.Text("log.multi_hotkey", multiHotkeyValue.ToDisplayString()));
                Logger.LogInfo(I18n.L.Text("log.loaded", PluginInfo.PLUGIN_NAME));
            }
            catch (Exception ex)
            {
                Logger.LogError($"{PluginInfo.PLUGIN_NAME} initialization error: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void OnLanguageChanged(string locale)
        {
            // Mod Settings Menu copies mod metadata during registration, so register it again for the new language.
            ModSettingsMenuRegistration.Register(Config);
            Logger.LogInfo(I18n.L.Text("log.language_changed", locale));
        }
    }
}

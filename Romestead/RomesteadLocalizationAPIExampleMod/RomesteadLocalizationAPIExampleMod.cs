using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using RomesteadLocalizationAPI.Api;

namespace RomesteadLocalizationAPIExampleMod
{
    // BepInPlugin is required by BepInEx. Without it, the plugin will not be discovered.
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    // This is a hard dependency. BepInEx loads Romestead Localization API before this mod,
    // so RomesteadLocalizationAPI.Api is ready when Load() runs.
    [BepInDependency(RomesteadLocalizationAPI.PluginInfo.PLUGIN_GUID)]
    public sealed class RomesteadLocalizationAPIExampleMod : BasePlugin
    {
        // Stored as static because the language-change callback is static in this example.
        // Real mods can keep this as an instance field if their refresh code is instance-based.
        internal static ManualLogSource Logger { get; private set; }

        public override void Load()
        {
            // BepInEx 6 calls Load() as the plugin entry point.
            // Use this instead of Unity lifecycle methods such as Awake(), Start(), or Update().
            Logger = Log;

            try
            {
                // Use the DLL's own folder as the base path for external files.
                //
                // This is safer than Path.Combine(Paths.PluginPath, PluginInfo.PLUGIN_NAME, ...)
                // because package managers such as r2modman can place the mod in generated
                // plugin folders whose names do not exactly match PluginInfo.PLUGIN_NAME.
                var pluginDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // Single JSON registration.
                //
                // Localization.json uses:
                //   locale -> key -> text
                //
                // Example:
                // {
                //   "en": {
                //     "mod.name": "Localization API Example"
                //   },
                //   "zh_CN": {
                //     "mod.name": "本地化 API 示例"
                //   }
                // }
                I18n.L.RegisterJson(Path.Combine(pluginDir, "Localization.json"));

                // Embedded text registration.
                //
                // This is useful for tiny mods or for strings that are easier to keep near code.
                // It can be mixed with JSON registration. The same localizer and same key scope
                // are used either way.
                I18n.L.RegisterTexts(
                    (RomesteadLocales.English, "embedded.message", "This line was registered from C# code."),
                    (RomesteadLocales.ChineseSimplified, "embedded.message", "这行文本来自 C# 内嵌注册。"),
                    (RomesteadLocales.ChineseTraditional, "embedded.message", "這行文字來自 C# 內嵌註冊。"));

                // The API reapplies registered strings when the game changes language.
                // If your mod has live UI labels, refresh them from this event.
                I18n.L.LanguageChanged += OnLanguageChanged;

                // Text() reads the current language and falls back automatically when missing.
                // Extra arguments are passed to string.Format, so "{0}" in the JSON becomes
                // PluginInfo.PLUGIN_NAME here.
                Logger.LogInfo(I18n.L.Text("log.loaded", PluginInfo.PLUGIN_NAME));
                Logger.LogInfo(I18n.L.Text("embedded.message"));
            }
            catch (Exception ex)
            {
                // Keep initialization failures visible in BepInEx logs. Missing or invalid JSON
                // will land here, which makes packaging mistakes much easier to diagnose.
                Logger.LogError($"{PluginInfo.PLUGIN_NAME} initialization error: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private static void OnLanguageChanged(string locale)
        {
            // Do not register localization again here. The API has already reapplied it.
            // In a real UI mod, this is where you would assign fresh text to labels/buttons.
            Logger.LogInfo(I18n.L.Text("log.language_changed", locale));
        }
    }
}

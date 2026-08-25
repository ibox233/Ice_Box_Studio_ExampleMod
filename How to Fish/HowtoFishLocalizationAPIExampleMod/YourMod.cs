using System;
using BepInEx;
using BepInEx.Logging;
using HowtoFishLocalizationAPI.Api;
using UnityEngine;
using YourMod.Configuration;
using YourMod.Localization;

namespace YourMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(HowtoFishLocalizationAPI.PluginInfo.PLUGIN_GUID, "1.0.0")]
    public sealed class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log { get; private set; }

        private string _language;

        private void Awake()
        {
            Log = Logger;
            try
            {
                ConfigManager.Init(Config);
                _language = LocalizationApi.CurrentLanguage;
                // Refresh cached localized UI text here after the language changes.
                LocalizationApi.LanguageChanged += LangChanged;
            }
            catch (Exception ex)
            {
                Log.LogError("Failed to initialize YourMod: " + ex);
            }
        }

        private void LangChanged(string language)
        {
            _language = language;
        }

        private void OnGUI()
        {
            if (!ConfigManager.Enabled)
            {
                return;
            }

            GUI.Window(PluginInfo.PLUGIN_NAME.GetHashCode(), new Rect(20f, 20f, 430f, 170f), DrawWindow, I18n.Text("window.title"));
        }

        private void DrawWindow(int id)
        {
            GUILayout.Label(I18n.Text("window.body", ConfigManager.Message));
            GUILayout.Label(I18n.Text("window.language", _language ?? LocalizationApi.CurrentLanguage));
            GUILayout.Label(I18n.Text("window.config_hint"));
            GUI.DragWindow(new Rect(0f, 0f, 10000f, 24f));
        }
    }
}

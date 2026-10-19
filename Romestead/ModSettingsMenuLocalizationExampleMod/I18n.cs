using RomesteadLocalizationAPI.Api;

namespace ModSettingsMenuLocalizationExampleMod
{
    internal static class I18n
    {
        // Bind this localizer to the plugin GUID once, then use short keys everywhere else.
        public static readonly RomesteadLocalizer L = RomesteadLocalization.For(PluginInfo.PLUGIN_GUID);
    }
}

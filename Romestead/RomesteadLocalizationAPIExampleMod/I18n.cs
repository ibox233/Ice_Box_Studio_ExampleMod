using RomesteadLocalizationAPI.Api;

namespace RomesteadLocalizationAPIExampleMod
{
    internal static class I18n
    {
        // Bind the localizer to this mod's GUID once, then use I18n.L everywhere.
        //
        // The API internally scopes every key with PluginInfo.PLUGIN_GUID, so this mod can
        // use short keys such as "mod.name" or "embedded.message" without colliding with
        // other mods that use the same short key names.
        //
        // Example:
        //   I18n.L.Text("mod.name")
        // is internally resolved as:
        //   YourName.Romestead.LocalizationAPIExampleMod.mod.name
        public static readonly RomesteadLocalizer L = RomesteadLocalization.For(PluginInfo.PLUGIN_GUID);
    }
}

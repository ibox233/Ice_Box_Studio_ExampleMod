using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;

namespace RomesteadContentFrameworkExampleMod
{
    // This is a regular game-client plugin for Romestead's CoreCLR loader.
    // The hard dependency makes BepInEx load the framework before this entry point.
    // Do not install this client example DLL on a dedicated server: it registers
    // icons, VFX and client spell callbacks that the server framework omits.
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(RomesteadContentFramework.PluginInfo.PLUGIN_GUID)]
    public sealed class RomesteadContentFrameworkExampleMod : BasePlugin
    {
        internal static ManualLogSource Logger { get; private set; }

        public override void Load()
        {
            Logger = Log;

            // Declare resources and effects before their item definitions, then add
            // recipes, shop entries and drops that reference those items. The framework
            // reapplies these declarations when the game rebuilds its databases;
            // the content mod must not call the vanilla database Setup methods itself.
            LumenCatalystIcon.Register();
            LumenCatalystVfx.Register();
            LumenCatalystEffect.Register();
            LumenCatalystItem.Register();
            LumenCatalystRecipe.Register();
            LumenCatalystWorkRecipe.Register();
            LumenCatalystShop.Register();
            LumenCatalystDrop.Register();

            // Reuse a vanilla spell with custom arguments instead of creating a new
            // effect handler. Select the optional icon before building the item data.
            VerdantTonicIcon.Register();
            VerdantTonicItem.Register();
            VerdantTonicRecipe.Register();

            // Reference an existing spell without custom parameters. The native spell
            // remains responsible for teleport rules and behavior.
            WayhomeCoinIcon.Register();
            WayhomeCoinItem.Register();
            WayhomeCoinRecipe.Register();

            // Connect a custom profession and work order to three building levels.
            // Declare the maps and job before the building data that references them.
            HunterLodgeIcon.Register();
            HunterLodgeJob.Register();
            HunterLodgeWorkRecipe.Register();
            HunterLodgeMaps.Register();
            HunterLodgeBuilding.Register();

            Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} loaded.");
        }
    }
}

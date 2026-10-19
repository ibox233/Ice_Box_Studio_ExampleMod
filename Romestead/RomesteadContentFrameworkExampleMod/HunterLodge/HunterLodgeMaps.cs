using System.IO;
using BepInEx;
using RomesteadContentFramework.Api;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class HunterLodgeMaps
    {
        public static void Register()
        {
            // Each exterior has its own doodad layout. All three CMX files share one
            // 8x8 TMX tile map; a separate namespaced ID reuses the native interior.
            RegisterExteriorMap(ExampleIds.HunterLodgeExteriorMapLevel1, "HunterLodge_1.cmx");
            RegisterExteriorMap(ExampleIds.HunterLodgeExteriorMapLevel2, "HunterLodge_2.cmx");
            RegisterExteriorMap(ExampleIds.HunterLodgeExteriorMapLevel3, "HunterLodge_3.cmx");
            RegisterInteriorMap();
        }

        private static void RegisterExteriorMap(string mapId, string customCmxName)
        {
            // Preserve the Content directory's internal paths. CMX database references
            // resolve CDB/CDX files under db/doodads, and their textures resolve under
            // media. The included TMX references TSX tilesets and their PNG images.
            string customCmx = ExampleAssetPaths.Asset("HunterLodge", Path.Combine("Content", "maps", customCmxName));
            string customTmx = ExampleAssetPaths.Asset("HunterLodge", Path.Combine("Content", "maps", "HunterLodge.tmx"));
            string contentRoot = ExampleAssetPaths.Asset("HunterLodge", "Content");

            RomesteadContent.Maps.Register(mapId, options =>
            {
                options.CmxPath = customCmx;
                options.TmxPath = customTmx;
                // This is the game-style Content root, not the maps directory. Both
                // client rendering and server map data must use matching resources.
                options.ContentRoot = contentRoot;
            });
        }

        private static void RegisterInteriorMap()
        {
            // Reuse files from the player's installed game rather than bundling copies
            // of this vanilla interior. The server package must use its own installation
            // and have the same native content available under its Content directory.
            string customCmx = Path.Combine(Paths.GameRootPath, "Content", "maps", "interiors_new", "leather_1.cmx");
            string customTmx = Path.Combine(Paths.GameRootPath, "Content", "maps", "interiors_new", "leather_1.tmx");
            string contentRoot = Path.Combine(Paths.GameRootPath, "Content");

            RomesteadContent.Maps.Register(ExampleIds.HunterLodgeInteriorMap, options =>
            {
                options.CmxPath = customCmx;
                options.TmxPath = customTmx;
                options.ContentRoot = contentRoot;
            });
        }
    }
}

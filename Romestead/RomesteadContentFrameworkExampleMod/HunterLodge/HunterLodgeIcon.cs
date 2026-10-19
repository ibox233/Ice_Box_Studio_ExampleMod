using System.IO;
using RomesteadContentFramework.Api;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class HunterLodgeIcon
    {
        // Reuse a native construction icon if the optional custom image is absent.
        // The full building image is used only as an icon; the exterior map is still
        // assembled from doodads and is not rendered as one oversized icon sprite.
        public const string FallbackIcon = "leatherworker";

        public static string ConstructionIcon { get; private set; } = FallbackIcon;

        public static void Register()
        {
            string path = ExampleAssetPaths.Asset("HunterLodge", "hunter_lodge_full_game.png");
            if (!File.Exists(path))
            {
                return;
            }

            // Job and building registration read ConstructionIcon after this method.
            RomesteadContent.Icons.RegisterPng(ExampleIds.HunterLodgeIcon, path);
            ConstructionIcon = ExampleIds.HunterLodgeIcon;
        }
    }
}

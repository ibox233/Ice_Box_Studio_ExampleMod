using RomesteadContentFramework.Api;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class LumenCatalystVfx
    {
        public static void Register()
        {
            // The PNG contains eight 32x32 frames. The final argument becomes each
            // native animation frame's Duration; it is not a frame count or pixel size.
            // This declares the animation. LumenCatalystEffect chooses when to play it.
            string path = ExampleAssetPaths.Asset("CustomVfxItem", "lumen_burst_vfx.png");
            RomesteadContent.Vfx.RegisterSpriteSheetPng(ExampleIds.LumenCatalystVfx, path, 32, 32, 8, 2);
        }
    }
}

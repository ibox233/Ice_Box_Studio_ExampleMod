using RomesteadContentFramework.Api;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class LumenCatalystIcon
    {
        public static void Register()
        {
            // This icon is required by this example and shipped with the project.
            // RegisterPng declares a loose PNG; GPU loading is deferred until the
            // client's graphics content is ready. No XNB conversion is needed.
            string iconPath = ExampleAssetPaths.Asset("CustomVfxItem", "lumen_catalyst.png");
            RomesteadContent.Icons.RegisterPng(ExampleIds.LumenCatalystIcon, iconPath);
        }
    }
}

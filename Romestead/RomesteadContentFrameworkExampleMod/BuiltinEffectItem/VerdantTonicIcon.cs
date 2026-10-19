using System.IO;
using RomesteadContentFramework.Api;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class VerdantTonicIcon
    {
        // This sample intentionally allows the PNG to be omitted: the vanilla icon
        // keeps the item usable while authors experiment with their own artwork.
        public const string FallbackIcon = "pouch_healthy";

        public static string ItemIcon { get; private set; } = FallbackIcon;

        public static void Register()
        {
            string path = ExampleAssetPaths.Asset("BuiltinEffectItem", "verdant_tonic.png");
            if (!File.Exists(path))
            {
                return;
            }

            // Select the custom ID before VerdantTonicItem copies it into ItemData.
            // This checks file presence, not whether the PNG will decode successfully.
            RomesteadContent.Icons.RegisterPng(ExampleIds.VerdantTonicIcon, path);
            ItemIcon = ExampleIds.VerdantTonicIcon;
        }
    }
}

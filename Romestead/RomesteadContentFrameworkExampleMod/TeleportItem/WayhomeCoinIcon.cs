using System.IO;
using RomesteadContentFramework.Api;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class WayhomeCoinIcon
    {
        // The vanilla icon is intentional when the optional custom PNG is absent.
        // Register this icon before the item so the selected ID reaches ItemData.
        public const string FallbackIcon = "sell";

        public static string ItemIcon { get; private set; } = FallbackIcon;

        public static void Register()
        {
            string path = ExampleAssetPaths.Asset("TeleportItem", "wayhome_coin.png");
            if (!File.Exists(path))
            {
                return;
            }

            // Loose PNG registration is handled by the client graphics registry.
            // A dedicated-server content build must exclude this visual registration.
            RomesteadContent.Icons.RegisterPng(ExampleIds.WayhomeCoinIcon, path);
            ItemIcon = ExampleIds.WayhomeCoinIcon;
        }
    }
}

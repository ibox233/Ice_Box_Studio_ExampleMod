using RomesteadContentFramework.Api;
using Shared.Models.Items;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class WayhomeCoinItem
    {
        public static void Register()
        {
            // An item can reuse a known vanilla spell by ID without a framework
            // callback. This sample changes item data, not teleport implementation.
            // Native eligibility, targeting and destination selection still apply.
            RomesteadContent.Items.Register(
                ExampleIds.WayhomeCoinItem,
                "Wayhome Coin",
                "Uses the game's original Traveler's Coin teleport spell.",
                item =>
                {
                    item.Icon = WayhomeCoinIcon.ItemIcon;
                    item.MaxStackSize = 5;
                    item.Tier = 1;
                    item.Tradable = new TradableItem
                    {
                        Price = 200UL
                    };
                    // Optional support for the existing offering system; this merely
                    // references Mercury and does not add a new god or progression API.
                    item.Worship = new ItemWorshipData
                    {
                        AssociatedGodId = "mercury",
                        WorshipTier = 1f
                    };
                    item.Usable = new UsableItem
                    {
                        Type = UsableItem.UsableType.Special,
                        UsesMax = 1,
                        Cooldown = 30f,
                        // The native Traveler's Coin entry spell needs no custom
                        // SpellTypeArgs here. Do not replace it with a guessed ID.
                        SpellId = "item:teleport_home:start"
                    };
                });
        }
    }
}

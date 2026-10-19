using System.Collections.Generic;
using RomesteadContentFramework.Api;
using Shared.Models.Items;
using Shared.Models.Stats;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class LumenCatalystItem
    {
        public static void Register()
        {
            // Item categories are optional components on ItemData, not subclasses.
            // This deliberately combines several components to demonstrate the API;
            // a production item should include only the capabilities it needs.
            // The framework registers the supplied name and description as game strings.
            RomesteadContent.Items.Register(
                ExampleIds.LumenCatalystItem,
                "Lumen Catalyst",
                "A custom-icon test item that glows, burns as fuel, and invokes a custom framework effect.",
                item =>
                {
                    item.Icon = ExampleIds.LumenCatalystIcon;
                    item.MaxStackSize = 1;
                    item.Tier = 1;
                    item.Flags = ItemFlag.Material;
                    // Price enables vanilla trading data. A shop entry is registered
                    // separately in LumenCatalystShop; this component alone adds no stock.
                    item.Tradable = new TradableItem
                    {
                        Price = 300UL
                    };
                    // Reference an existing god instead of defining a new faith system.
                    item.Worship = new ItemWorshipData
                    {
                        AssociatedGodId = "vulcan",
                        WorshipTier = 1f
                    };
                    // Fuel amount is passed to the native fuel-consumption system.
                    item.Fuel = new FuelItem
                    {
                        Amount = 200f
                    };
                    // The equipment component selects the light-source slot and a stat
                    // bonus; the LightSource component below supplies the light settings.
                    item.Equippable = new EquippableItem
                    {
                        EquipmentType = EquipmentType.LightSource,
                        StatBonuses = new Dictionary<string, StatModificationData>
                        {
                            {
                                "LightSource",
                                new StatModificationData
                                {
                                    Additive = 1.5f
                                }
                            }
                        }
                    };
                    item.LightSource = new LightSourceItem
                    {
                        Radius = 96f,
                        Strength = 1.4f
                    };
                    // A usable component invokes a spell by ID. Registering an item does
                    // not define that spell; LumenCatalystEffect provides it beforehand.
                    item.Usable = new UsableItem
                    {
                        Type = UsableItem.UsableType.Special,
                        UsesMax = 1,
                        SpellId = ExampleIds.LumenCatalystEffect,
                        Cooldown = 3f
                    };
                });
        }
    }
}

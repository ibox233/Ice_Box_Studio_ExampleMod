using RomesteadContentFramework.Api;
using Shared;
using Shared.Combat.Spells.Parameters;
using Shared.Models.Items;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class VerdantTonicItem
    {
        public static void Register()
        {
            // Reuse the game's healing spell rather than registering another spell.
            // SpellTypeArgs must match the parameter type expected by the referenced
            // native spell; an arbitrary object with similar fields is not equivalent.
            RomesteadContent.Items.Register(
                ExampleIds.VerdantTonicItem,
                "Verdant Tonic",
                "Uses the game's built-in healing spell with mod-provided HealSpellArgs.",
                item =>
                {
                    item.Icon = VerdantTonicIcon.ItemIcon;
                    item.MaxStackSize = 5;
                    item.Tier = 1;
                    // Classification and use behavior are separate: the Food flag is
                    // data categorization, while Usable below defines how it is used.
                    item.Flags = ItemFlag.Food;
                    item.Tradable = new TradableItem
                    {
                        Price = 75UL
                    };
                    item.Usable = new UsableItem
                    {
                        Type = UsableItem.UsableType.Healing,
                        UsesMax = 1,
                        Cooldown = 1f,
                        SpellId = SpellIds.ConsumeItemHealthPotion,
                        // Keep the built-in healing behavior and configure its instant
                        // and gradual healing through the game's own argument model.
                        // No client/server effect callback is needed in this example.
                        SpellTypeArgs = new HealSpellArgs
                        {
                            InstantHeal = 35f,
                            SlowHeal = 30f,
                            SlowHealTime = 6f
                        }
                    };
                });
        }
    }
}

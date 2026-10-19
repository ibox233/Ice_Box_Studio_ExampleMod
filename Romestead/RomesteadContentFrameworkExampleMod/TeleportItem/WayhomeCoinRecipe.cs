using RomesteadContentFramework.Api;
using Shared.Data;
using Shared.Models.Items;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class WayhomeCoinRecipe
    {
        public static void Register()
        {
            // This is a player backpack recipe, not a worker recipe. The wood-stick
            // cost is deliberately trivial for experimentation; tune it for a real mod.
            RomesteadContent.Recipes.Register(new ItemRecipe
            {
                Id = ExampleIds.WayhomeCoinRecipe,
                CraftedItemId = ExampleIds.WayhomeCoinItem,
                CraftedAmount = 1,
                ItemsAmounts = new[]
                {
                    new ItemAmount("material:wood_stick", 1)
                }
            }, recipe =>
            {
                // The framework includes this recipe in the player's unlocked list.
                // Registration itself does not grant an item to an existing inventory.
                recipe.UnlockedByDefault = true;
            });
        }
    }
}

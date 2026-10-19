using RomesteadContentFramework.Api;
using Shared.Data;
using Shared.Models.Items;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class VerdantTonicRecipe
    {
        public static void Register()
        {
            // Register the output item first. This separate declaration adds a simple
            // backpack recipe using an existing ingredient ID; it creates no work order
            // and requires no citizen or crafting-station registration.
            RomesteadContent.Recipes.Register(new ItemRecipe
            {
                Id = ExampleIds.VerdantTonicRecipe,
                CraftedItemId = ExampleIds.VerdantTonicItem,
                CraftedAmount = 1,
                ItemsAmounts = new[]
                {
                    new ItemAmount("material:wood_stick", 1)
                }
            }, recipe =>
            {
                // Make the teaching sample visible without requiring story progress.
                recipe.UnlockedByDefault = true;
            });
        }
    }
}

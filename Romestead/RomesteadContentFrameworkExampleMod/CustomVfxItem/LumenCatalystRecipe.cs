using RomesteadContentFramework.Api;
using Shared.Data;
using Shared.Models.Items;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class LumenCatalystRecipe
    {
        public static void Register()
        {
            // A recipe has its own ID, separate from the item it produces. Omitting
            // RequiredCraftingStation makes this a backpack recipe. Cheap vanilla
            // ingredients keep the sample easy to try; they are not balance guidance.
            RomesteadContent.Recipes.Register(new ItemRecipe
            {
                Id = ExampleIds.LumenCatalystRecipe,
                CraftedItemId = ExampleIds.LumenCatalystItem,
                CraftedAmount = 1,
                ItemsAmounts = new[]
                {
                    new ItemAmount("material:wood_stick", 1)
                }
            }, recipe =>
            {
                // Alternatively start locked and add UnlockWhenWorldFlag(...) or
                // another supported condition. Unlock state affects the player's list.
                recipe.UnlockedByDefault = true;
            });
        }
    }
}

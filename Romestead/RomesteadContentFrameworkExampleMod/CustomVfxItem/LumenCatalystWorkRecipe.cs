using RomesteadContentFramework.Api;
using Shared.Constants;
using Shared.Data.DataModels;
using Shared.Models.Items;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class LumenCatalystWorkRecipe
    {
        public static void Register()
        {
            // A work recipe belongs to the citizen job/order system, not the player's
            // backpack crafting list. Native worker processing consumes ingredients,
            // advances work progress, awards experience and handles the output.
            RomesteadContent.WorkRecipes.Register(new WorkRecipeModel
            {
                Id = ExampleIds.LumenCatalystWorkRecipe,
                ItemIngredients = new[]
                {
                    new ItemAmount("material:leather", 1),
                    new ItemAmount("material:wood_stick", 1)
                },
                // Progress is native work progress, not a number of in-game days.
                RequiredProgress = 20f,
                Experience = 5f,
                Output = new WorkOutputModel
                {
                    CraftedId = ExampleIds.LumenCatalystItem,
                    Type = WorkOrderType.Item,
                    Amount = 1
                }
            }, recipe =>
            {
                recipe.Name = "Stitch Lumen Catalyst";
                recipe.Description = "A leatherworker work order that crafts a custom framework item.";
                // The worker must reach rank 2. groupLevel selects the native recipe
                // group bucket; it is separate from the worker's rank requirement.
                recipe.LevelRequirement = 2;
                recipe.AddToJobGroup(
                    JobIds.Leatherworker,
                    groupLevel: 1,
                    // Position this entry relative to an existing vanilla recipe.
                    RomesteadContent.WorkRecipePlacement.Before("recipe:thick_leather_head_armor"));
            });
        }
    }
}

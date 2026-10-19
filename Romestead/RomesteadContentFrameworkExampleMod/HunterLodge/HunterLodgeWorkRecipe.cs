using RomesteadContentFramework.Api;
using Shared.Data.DataModels;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class HunterLodgeWorkRecipe
    {
        public static void Register()
        {
            // An ingredient-free worker order models hunting without daily grants or
            // a spawned-enemy system. Work progress and output are still handled by
            // the normal worker pipeline, including its output-inventory checks.
            RomesteadContent.WorkRecipes.Register(new WorkRecipeModel
            {
                Id = ExampleIds.HunterMeatWorkRecipe,
                // Native work progress, not elapsed days. Experience is awarded by
                // the work-order flow; DefaultRepeatableSelected sets the recipe's
                // repeat-selection default rather than spawning a worker or an order.
                RequiredProgress = 20f,
                Experience = 5f,
                DefaultRepeatableSelected = true,
                Output = new WorkOutputModel
                {
                    CraftedId = "material:meat_small",
                    Type = WorkOrderType.Item,
                    Amount = 2
                }
            }, options =>
            {
                options.Name = "Hunt Small Game";
                options.Description = "Send the assigned hunter out to gather raw small game meat.";
                options.LevelRequirement = 1;
                // Connect the recipe to the new profession; registering the model
                // without a job-group entry would not place it in that job's list.
                options.AddToJobGroup(ExampleIds.HunterJob, groupLevel: 1);
            });
        }
    }
}

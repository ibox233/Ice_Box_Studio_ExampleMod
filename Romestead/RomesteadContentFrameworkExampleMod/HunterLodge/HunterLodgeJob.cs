using RomesteadContentFramework.Api;
using Shared.Data.DataModels;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class HunterLodgeJob
    {
        public static void Register()
        {
            // The building slot and work-recipe group must reference this same job ID.
            // The framework also integrates registered jobs with the vanilla generic
            // citizen controller; adding a job row alone is not custom citizen AI.
            RomesteadContent.Jobs.Register(new JobData
            {
                Id = ExampleIds.HunterJob,
                MName = ExampleIds.HunterJob + "*job:name",
                FName = ExampleIds.HunterJob + "*job:name:feminine",
                // Reuse an existing job-control UI layout rather than adding a new UI.
                UiJobControlFlags = UiJobControls.LumberjackControls,
                // Caps correspond to the building's zero-based levels 0, 1 and 2.
                // These are maximum profession ranks, not additional worker slots.
                MaxLevelUpgradeSteps = new[] { 4, 7, 10 },
                AssociatedGodId = "diana",
                WorshipPerLevel = 10,
                // Allow assignment to the building without adding this profession
                // to the native random-citizen generation pool.
                PartOfCitizenGeneration = false,
                Icon = HunterLodgeIcon.ConstructionIcon
            }, "Hunter");
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RomesteadContentFramework.Api;
using Shared.Models.Buildings;
using Shared.Models.Construction;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class HunterLodgeBuilding
    {
        public static void Register()
        {
            // All levels share one behavior type but have separate placed-building
            // and construction definitions. Keep the complete upgrade chain together
            // so each target ID and the preceding building ID are easy to compare.
            RegisterBehavior();
            RegisterLevel1();
            RegisterLevel2();
            RegisterLevel3();
        }

        private static void RegisterBehavior()
        {
            // Use the native worker/order path. No meat is granted on a daily timer:
            // a player must assign a Hunter and queue a recipe. The standard behavior
            // supplies the framework's fixed workshop storage/output inventories and
            // the matching logistics defaults, so a custom controller is unnecessary.
            RomesteadContent.Buildings.RegisterStandardType(ExampleIds.HunterLodgeType, "Hunter Lodge", behavior =>
            {
                behavior.UseWorkerJobs();
            });
        }

        private static void RegisterLevel1()
        {
            // BuildingData describes the placed result: maps, profession slots and
            // appeal cap. It does not define the material cost or placement recipe.
            RomesteadContent.Buildings.RegisterBuilding(new BuildingData
            {
                Id = ExampleIds.HunterLodgeBuildingLevel1,
                BuildingTypeId = ExampleIds.HunterLodgeType,
                IconId = HunterLodgeIcon.ConstructionIcon,
                ExteriorMap = ExampleIds.HunterLodgeExteriorMapLevel1,
                InteriorMap = ExampleIds.HunterLodgeInteriorMap,
                CitizenSlots =
                {
                    // The position is a world-space point in the reused interior,
                    // not a screen pixel or a sprite layer. This one slot is retained
                    // at every upgrade level; upgrades increase the job's rank cap.
                    new CitizenSlotData(ExampleIds.HunterJob, new Vector3(52f, 105f, 0f), SpawnLocation.Inside)
                },
                MaxBuildingAppeal = 6f
            });

            // ConstructionModel defines how the player builds that result. Material
            // keys are native construction-resource IDs, not ordinary item IDs.
            RomesteadContent.Buildings.RegisterConstruction(new ConstructionModel
            {
                Id = ExampleIds.HunterLodgeConstructionLevel1,
                Name = ExampleIds.HunterLodgeConstructionLevel1 + "*construction:name",
                RequiredConstructionProgress = 120f,
                ConstructionMaterialsRequirement = new ConstructionMaterialsAggregate
                {
                    MaterialAmounts = new Dictionary<string, int>
                    {
                        { "resource:lumber", 6 },
                        { "resource:stone", 2 }
                    }
                },
                IconId = HunterLodgeIcon.ConstructionIcon,
                // The first displayed level uses the native zero-based upgrade index.
                UpgradeLevel = 0,
                ConstructionSpaceInfo = CreateConstructionSpace(),
                Type = ConstructionType.Building,
                SpawnedId = ExampleIds.HunterLodgeBuildingLevel1,
                ConstructionUiCategory = ConstructionUiCategory.Buildings
            }, options =>
            {
                // Registration options provide localized display strings and unlock
                // visibility; the model's SpawnedId still selects the actual building.
                options.Name = "Hunter Lodge";
                options.Description = "Assign a hunter and queue Hunt Small Game to produce raw meat. Maximum Hunter rank: 4.";
                options.UnlockedByDefault = true;
            });
        }

        private static void RegisterLevel2()
        {
            // Keep the behavior type, interior and worker count consistent. Only the
            // exterior map and level-specific data change for this placed definition.
            RomesteadContent.Buildings.RegisterBuilding(new BuildingData
            {
                Id = ExampleIds.HunterLodgeBuildingLevel2,
                BuildingTypeId = ExampleIds.HunterLodgeType,
                IconId = HunterLodgeIcon.ConstructionIcon,
                ExteriorMap = ExampleIds.HunterLodgeExteriorMapLevel2,
                InteriorMap = ExampleIds.HunterLodgeInteriorMap,
                CitizenSlots =
                {
                    new CitizenSlotData(ExampleIds.HunterJob, new Vector3(52f, 105f, 0f), SpawnLocation.Inside)
                },
                MaxBuildingAppeal = 10f
            });

            RomesteadContent.Buildings.RegisterConstruction(new ConstructionModel
            {
                Id = ExampleIds.HunterLodgeConstructionLevel2,
                Name = ExampleIds.HunterLodgeConstructionLevel2 + "*construction:name",
                RequiredConstructionProgress = 150f,
                ConstructionMaterialsRequirement = new ConstructionMaterialsAggregate
                {
                    MaterialAmounts = new Dictionary<string, int>
                    {
                        { "resource:lumber", 4 },
                        { "resource:stone", 3 }
                    }
                },
                IconId = HunterLodgeIcon.ConstructionIcon,
                UpgradeLevel = 1,
                // UpgradeForId refers to a BUILDING ID, not its construction recipe.
                // SpawnedId below selects the replacement level. This delegates the
                // upgrade to the native flow instead of deleting/recreating inventories.
                UpgradeForId = ExampleIds.HunterLodgeBuildingLevel1,
                ConstructionSpaceInfo = CreateConstructionSpace(),
                Type = ConstructionType.Building,
                SpawnedId = ExampleIds.HunterLodgeBuildingLevel2,
                ConstructionUiCategory = ConstructionUiCategory.Buildings
            }, options =>
            {
                options.Name = "Hunter Lodge II";
                options.Description = "Raises the maximum Hunter rank from 4 to 7 while preserving the lodge's inventories.";
                options.UnlockedByDefault = true;
            });
        }

        private static void RegisterLevel3()
        {
            // Extend the same chain: level III upgrades the placed level-II building.
            // MaxLevelUpgradeSteps in HunterLodgeJob provides the corresponding cap.
            RomesteadContent.Buildings.RegisterBuilding(new BuildingData
            {
                Id = ExampleIds.HunterLodgeBuildingLevel3,
                BuildingTypeId = ExampleIds.HunterLodgeType,
                IconId = HunterLodgeIcon.ConstructionIcon,
                ExteriorMap = ExampleIds.HunterLodgeExteriorMapLevel3,
                InteriorMap = ExampleIds.HunterLodgeInteriorMap,
                CitizenSlots =
                {
                    new CitizenSlotData(ExampleIds.HunterJob, new Vector3(52f, 105f, 0f), SpawnLocation.Inside)
                },
                MaxBuildingAppeal = 14f
            });

            RomesteadContent.Buildings.RegisterConstruction(new ConstructionModel
            {
                Id = ExampleIds.HunterLodgeConstructionLevel3,
                Name = ExampleIds.HunterLodgeConstructionLevel3 + "*construction:name",
                RequiredConstructionProgress = 180f,
                ConstructionMaterialsRequirement = new ConstructionMaterialsAggregate
                {
                    MaterialAmounts = new Dictionary<string, int>
                    {
                        { "resource:lumber", 6 },
                        { "resource:stone", 5 }
                    }
                },
                IconId = HunterLodgeIcon.ConstructionIcon,
                UpgradeLevel = 2,
                UpgradeForId = ExampleIds.HunterLodgeBuildingLevel2,
                ConstructionSpaceInfo = CreateConstructionSpace(),
                Type = ConstructionType.Building,
                SpawnedId = ExampleIds.HunterLodgeBuildingLevel3,
                ConstructionUiCategory = ConstructionUiCategory.Buildings
            }, options =>
            {
                options.Name = "Hunter Lodge III";
                options.Description = "Raises the maximum Hunter rank from 7 to 10 while preserving the lodge's inventories.";
                options.UnlockedByDefault = true;
            });
        }

        private static ConstructionSpaceInfo CreateConstructionSpace()
        {
            // This 8x8 rectangle is the building's construction footprint in tiles.
            // It does not define doodad collision or roof depth. Doorway collision and
            // visual layering belong to the CMX/CDX assets, not this placement area.
            return new ConstructionSpaceInfo
            {
                Areas = new[]
                {
                    new ConstructionTileArea
                    {
                        TileBounds = new Rectangle(0, 0, 8, 8),
                        Requirements = ConstructionSpaceInfo.PlacementRequirements.FreeTilesAndNoFurniture
                    }
                },
                PlacingStyle = ConstructionSpaceInfo.PlacementStyle.Single
            };
        }
    }
}

namespace RomesteadContentFrameworkExampleMod
{
    internal static class ExampleIds
    {
        // IDs are persistent content identities, not display text or file paths.
        // Replace the rcf.example namespace when adapting this code to your own mod.
        // Keep each chosen ID stable once players save worlds using the content.
        public const string LumenCatalystItem = "rcf.example:material:lumen_catalyst";
        public const string LumenCatalystIcon = "rcf.example:icon:lumen_catalyst";
        public const string LumenCatalystVfx = "rcf.example:vfx:lumen_catalyst_burst";
        public const string LumenCatalystEffect = "rcf.example:spell:lumen_catalyst_burst";
        public const string LumenCatalystRecipe = "rcf.example:recipe:lumen_catalyst";
        public const string LumenCatalystWorkRecipe = "rcf.example:work_recipe:lumen_catalyst";
        public const string VerdantTonicItem = "rcf.example:consumable:verdant_tonic";
        public const string VerdantTonicIcon = "rcf.example:icon:verdant_tonic";
        public const string VerdantTonicRecipe = "rcf.example:recipe:verdant_tonic";
        public const string WayhomeCoinItem = "rcf.example:consumable:wayhome_coin";
        public const string WayhomeCoinIcon = "rcf.example:icon:wayhome_coin";
        public const string WayhomeCoinRecipe = "rcf.example:recipe:wayhome_coin";
        public const string HunterLodgeIcon = "rcf.example:icon:hunter_lodge";
        public const string HunterLodgeType = "rcf.example:building_type:hunter_lodge";
        public const string HunterJob = "rcf.example:job:hunter";
        public const string HunterMeatWorkRecipe = "rcf.example:work_recipe:hunt_small_game";
        // The suffixes are zero-based internal identifiers; the UI labels use I/II/III.
        // Building IDs identify placed objects. Construction IDs identify the recipes
        // that create or upgrade those objects, so they must remain separate.
        public const string HunterLodgeBuildingLevel1 = "rcf.example:building:hunter_lodge:0";
        public const string HunterLodgeBuildingLevel2 = "rcf.example:building:hunter_lodge:1";
        public const string HunterLodgeBuildingLevel3 = "rcf.example:building:hunter_lodge:2";
        public const string HunterLodgeConstructionLevel1 = "rcf.example:construction:hunter_lodge:0";
        public const string HunterLodgeConstructionLevel2 = "rcf.example:construction:hunter_lodge:1";
        public const string HunterLodgeConstructionLevel3 = "rcf.example:construction:hunter_lodge:2";
        // Map IDs are extensionless lookup keys. HunterLodgeMaps connects these keys
        // to actual CMX/TMX files; a map ID is not a relative filesystem path.
        public const string HunterLodgeExteriorMapLevel1 = "rcf.example:maps/hunter_lodge/exterior/1";
        public const string HunterLodgeExteriorMapLevel2 = "rcf.example:maps/hunter_lodge/exterior/2";
        public const string HunterLodgeExteriorMapLevel3 = "rcf.example:maps/hunter_lodge/exterior/3";
        public const string HunterLodgeInteriorMap = "rcf.example:maps/hunter_lodge/interior";
    }
}

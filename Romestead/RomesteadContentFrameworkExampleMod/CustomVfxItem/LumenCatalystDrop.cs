using RomesteadContentFramework.Api;
using Shared.Models.DropTables;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class LumenCatalystDrop
    {
        public static void Register()
        {
            // Add to an existing native drop table. Referencing start:poi does not
            // register a POI or change world generation. Existing generated loot is
            // not retroactively filled just because the table gains another entry.
            RomesteadContent.Drops.AddItem("start:poi", new DropTableItemSpawnModel
            {
                ItemId = ExampleIds.LumenCatalystItem,
                MinAmount = 1,
                MaxAmount = 1,
                // The framework forwards this chance value to the native evaluator.
                SpawnChance = 20f
            });
        }
    }
}

# Romestead Content Framework Example Mod

A commented client-side example for Romestead Content Framework 1.0.0. Read the
plugin's `Load()` method first, then follow the `Register()` calls for each feature.
This is a teaching mod with deliberately inexpensive recipes, not balanced content.

## Examples

| Folder | What it demonstrates |
|---|---|
| `CustomVfxItem` | An item with a PNG icon, light/fuel/equipment components, a custom spell with separate client/server callbacks, an animated PNG effect, backpack and worker recipes, and additions to native shop/drop tables. |
| `BuiltinEffectItem` | A consumable reusing the native healing spell with `HealSpellArgs`, plus an optional icon and a backpack recipe. |
| `TeleportItem` | A consumable reusing the native Traveler's Coin spell without a custom handler, plus an optional icon and a backpack recipe. |
| `HunterLodge` | One player building with three upgrade levels, a custom profession, a worker recipe, CMX/TMX exteriors, a reused native interior, and standard workshop inventories. |

Each folder separates definitions by responsibility so a mod author can copy only
the relevant registration calls. `ExampleIds.cs` keeps all content references in
one place. Give your own plugin a new GUID and a new content-ID namespace; do not
ship another mod using the example IDs unchanged.

## Registration Flow

1. Declare icons, animations and custom effects before the items that use them.
2. Register item definitions before their recipes, shop entries and drop entries.
3. For buildings, register the job, work recipe and maps before the building and
   construction models that reference them.

The framework stores these declarations and reapplies them after native database
setup. The example does not need to patch database setup or call it itself.

Item components opt into native capabilities. A `TradableItem` does not create a
merchant, a `UsableItem` needs a valid spell ID, and registering a recipe does not
grant its output to the player. Comments beside each declaration explain these
relationships and distinguish native IDs from IDs introduced by the example.

## Buildings And Workers

`HunterLodgeBuilding.cs` keeps levels I, II and III in one file. Each level has a
separate placed-building ID and construction ID. `UpgradeForId` points to the
previous **building**, while `SpawnedId` identifies the next building definition.
The native upgrade index is zero-based.

All levels retain one Hunter slot. The profession caps are 4, 7 and 10; upgrading
does not add more workers. `UseWorkerJobs()` uses work orders and the framework's
fixed workshop storage/output inventories. It does not grant meat once per day.
Assign a Hunter and queue Hunt Small Game to exercise the production example.

Construction footprints describe placement in tiles. Doodad collision, doorway
gaps and roof depth belong to CMX/CDX resources. The three exterior CMX files share
one TMX and include two imported custom props alongside native game resources.

## Assets And Installation Layout

The build copies the example DLL to `BepInEx/plugins` and runtime assets to:

```text
BepInEx/plugins/RomesteadContentFrameworkExampleMod/Assets/
```

`ExampleAssetPaths` expects that layout. Distribute the DLL and the asset directory
together. The project explicitly lists the standalone PNGs and includes only
runtime CMX/TMX/TSX/CDB/CDX/PNG files from the map Content tree. Generation prompts,
intermediate artwork and documentation are not deployed.

The CMX authoring UUID marker stays in the source tree to preserve import paths
when editing maps. Do not rename referenced database/texture paths independently.
See `Assets/HunterLodge/README.txt` for the map resource layout.

## Build And Runtime Boundary

```powershell
dotnet build RomesteadContentFrameworkExampleMod/RomesteadContentFrameworkExampleMod.csproj -c Release
```

Run the command from the workspace root. The project reference builds the matching
framework configuration automatically. `GamePath` defaults to the local Romestead
installation; override it as an MSBuild property for another installation. Builds
copy the framework, example DLL and runtime assets into an existing BepInEx plugin
directory, but do not launch the game.

This example targets the regular client framework, including client-hosted games.
It is not a dedicated-server package. Client callbacks play visuals; authoritative
gameplay belongs in server callbacks. The Lumen Catalyst server callback only logs
the cast and does not apply a gameplay effect. A dedicated content build must omit
client graphics code and target the server framework; the separate
`HunterLodgeServer` project demonstrates that arrangement for the standalone lodge.

Build success confirms compilation only. It does not establish multiplayer,
resource rendering, production or save/reload correctness in-game.

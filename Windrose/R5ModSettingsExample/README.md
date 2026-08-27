# R5ModSettingsExample

Small UE4SS Lua mod that demonstrates how another mod can register settings with `R5ModSettings`.

The mod follows the same pattern as `SN2DrainRates` for SN2:

- writes its own manifest to `R5ModSettings\registrations`
- optionally ensures `R5ModSettings\saved\R5ModSettingsExample.lua` exists with defaults so gameplay code can read config before the player opens Settings > Mods
- reads primitive shared variables from `R5ModSettings/<ModName>/<Key>`
- reads keybind shared variables from `R5ModSettings/<ModName>/<Key>/primary` and `/secondary`
- falls back to `R5ModSettings\saved\<ModName>.lua`
- polls shared variables so runtime setting changes are noticed while the game is running

It adds one mod block named `R5 Mod Settings Example - 0.1.0` with:

- `Open Nexus Mods` action from `nexus_id = "442"`
- Toggle setting
- Slider setting
- Dropdown setting
- Keybind setting saved as primary/secondary Unreal key name strings, for example `F8` and `None`

Saved values are stored at:

```text
ue4ss\Mods\R5ModSettings\saved\R5ModSettingsExample.lua
```

Install with:

```powershell
.\Mods\R5ModSettingsExample\Install.ps1 -GameWin64Dir "F:\SteamLibrary\steamapps\common\Windrose\R5\Binaries\Win64"
```

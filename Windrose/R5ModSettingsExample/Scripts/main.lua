--[[
    R5ModSettingsExample
    R5 Mod Settings 示例模组

    中文：
    这是一个给其他 UE4SS 模组作者参考的最小完整示例。
    它演示的不是具体 gameplay 功能，而是第三方模组应该如何接入
    R5 Mod Settings。

    English:
    This is a small complete example for other UE4SS mod authors.
    It does not implement real gameplay behavior. It demonstrates how a
    third-party mod can integrate with R5 Mod Settings.

    中文：
    本示例展示：
    1. 启动时写入自己的注册 manifest。
    2. 可选：启动时确保默认 saved 配置已经存在。
    3. 优先从 UE4SS shared variables 读取运行时配置。
    4. 如果 shared variables 还没准备好，则 fallback 到 saved Lua 文件。
    5. 轮询 shared variables，让游戏内设置变更能被模组实时感知。

    English:
    This example shows how to:
    1. Write this mod's registration manifest on startup.
    2. Optionally ensure the default saved config exists on startup.
    3. Prefer UE4SS shared variables for runtime config reads.
    4. Fall back to the saved Lua file when shared variables are not ready.
    5. Poll shared variables so in-game setting changes can be applied live.

    中文：
    真实模组可以直接复制这个文件的结构，然后把 Config、WriteManifest()
    和 ApplyConfig() 替换成自己的设置项和实际功能。

    English:
    Real mods can copy this file structure, then replace Config,
    WriteManifest(), and ApplyConfig() with their own settings and behavior.
]]

-- 中文：
-- 这个名字同时用于：
-- - R5ModSettings/registrations/<ModName>.lua
-- - R5ModSettings/saved/<ModName>.lua
-- - shared variable 前缀 R5ModSettings/<ModName>/<Key>
-- 建议和你的 UE4SS 模组文件夹名保持一致，避免多个模组互相覆盖配置。
--
-- English:
-- This name is used for:
-- - R5ModSettings/registrations/<ModName>.lua
-- - R5ModSettings/saved/<ModName>.lua
-- - shared variable prefix R5ModSettings/<ModName>/<Key>
-- Keep it the same as your UE4SS mod folder name to avoid config conflicts.
local ModName = "R5ModSettingsExample"

-- 中文：
-- R5 Mod Settings 数据目录。第三方模组通常只需要写入 registrations
-- 并读取或维护 saved 配置文件。
-- 路径使用 ue4ss 运行目录下的相对路径，和 UE4SS Lua 模组常见写法一致。
--
-- English:
-- Data directory used by R5 Mod Settings.
-- Third-party mods usually only need to write registrations and read or maintain saved values.
-- The path is relative to the UE4SS runtime directory, matching common UE4SS Lua usage.
local LIB_ROOT = "./ue4ss/Mods/R5ModSettings/"

-- 中文：
-- 注册文件。把 manifest 写到这里后，它会作为一个模组配置页显示在 Settings > Mods。
--
-- English:
-- Registration file. A manifest written here appears as one mod config page under Settings > Mods.
local MANIFEST_PATH = LIB_ROOT .. "registrations/" .. ModName .. ".lua"

-- 中文：
-- 保存文件。玩家改过的值会保存在这里。
-- 本示例在首次启动时也会主动确保默认值存在，方便 gameplay 代码
-- 在玩家没打开设置前读取默认配置。这是推荐做法，但不是强制要求。
--
-- English:
-- Saved config file. Player-edited values are stored here.
-- This example also ensures defaults exist on first startup, so gameplay code can read
-- defaults before the player opens the settings menu. This is recommended, not required.
local SAVED_PATH = LIB_ROOT .. "saved/" .. ModName .. ".lua"

-- 中文：
-- shared variables 用于运行时读取最新配置。
-- 本示例会读取这些名字：
-- R5ModSettings/R5ModSettingsExample/Enabled
-- R5ModSettings/R5ModSettingsExample/Intensity
-- R5ModSettings/R5ModSettingsExample/Mode
-- R5ModSettings/R5ModSettingsExample/OpenMenuKey/primary
-- R5ModSettings/R5ModSettingsExample/OpenMenuKey/secondary
--
-- English:
-- Shared variables are used to read the latest runtime config.
-- This example reads these names:
-- R5ModSettings/R5ModSettingsExample/Enabled
-- R5ModSettings/R5ModSettingsExample/Intensity
-- R5ModSettings/R5ModSettingsExample/Mode
-- R5ModSettings/R5ModSettingsExample/OpenMenuKey/primary
-- R5ModSettings/R5ModSettingsExample/OpenMenuKey/secondary
local SHARED_PREFIX = "R5ModSettings/" .. ModName .. "/"

local function log(msg)
    print(string.format("[%s] %s\n", ModName, tostring(msg)))
end

-- 中文：
-- 模组自己的默认配置。
-- 真实模组应该让 gameplay 逻辑读取这个 Config 表，而不是到处直接读文件。
-- 这样启动、保存、运行时更新都只需要维护一份状态。
--
-- English:
-- This mod's default config.
-- Real gameplay logic should read this Config table instead of reading files everywhere.
-- This keeps startup, saving, and live updates using one authoritative state.
local Config = {
    Enabled = true,
    Intensity = 0.5,
    Mode = "balanced",

    -- 中文：
    -- keybind 设置必须使用 table，包含 primary 和 secondary 两个字段。
    -- primary 是主要按键，secondary 是备用按键；"None" 表示没有绑定备用按键。
    -- 玩家可以在 Mods 页面编辑按键；没有绑定的槽位会保存为 "None"。
    -- 读取 keybind 时请分别读取：
    -- R5ModSettings/<ModName>/OpenMenuKey/primary
    -- R5ModSettings/<ModName>/OpenMenuKey/secondary
    --
    -- English:
    -- Keybind settings must use a table with primary and secondary fields.
    -- primary is the main key, secondary is the optional backup key. "None" means unbound.
    -- Players can edit keybinds on the Mods page. Unbound slots are saved as "None".
    -- Read keybinds from:
    -- R5ModSettings/<ModName>/OpenMenuKey/primary
    -- R5ModSettings/<ModName>/OpenMenuKey/secondary
    OpenMenuKey = {
        primary = "F8",
        secondary = "None",
    },
}

-- 中文：
-- 固定 key 顺序，避免 pairs() 的无序遍历让日志或保存结果不稳定。
-- 新增设置时，同时在 Config、ConfigKeys 和 WriteManifest() 里添加。
--
-- English:
-- Fixed key order keeps logs and saved output stable instead of relying on unordered pairs().
-- When adding a new setting, add it to Config, ConfigKeys, and WriteManifest().
local ConfigKeys = {
    "Enabled",
    "Intensity",
    "Mode",
    "OpenMenuKey",
}

-- 中文：
-- 创建目录的轻量工具函数。
-- 为了避免每次启动都拉起 Windows shell，先尝试写入一个临时探针文件。
-- 只有目录真的不存在时，示例模组才调用 mkdir 创建目录。
-- 这里只处理本示例使用的固定路径，不建议把玩家输入直接拼进 os.execute。
--
-- English:
-- Lightweight directory creation helper.
-- To avoid launching the Windows shell on every startup, it first tries to write a probe file.
-- The example mod only calls mkdir when the directory is actually missing.
-- This only handles fixed paths used by this example. Do not concatenate player input into os.execute.
local EnsuredDirs = {}

local function directory_exists(path)
    local probe = path .. ".r5modsettingsexample_probe"
    local f = io.open(probe, "w")
    if not f then return false end
    f:close()
    os.remove(probe)
    return true
end

local function mkdir(path)
    if not path or path == "" or EnsuredDirs[path] then return end

    if directory_exists(path) then
        EnsuredDirs[path] = true
        return
    end

    os.execute('mkdir "' .. path:gsub("/", "\\") .. '" 2>nul')
    EnsuredDirs[path] = true
end

-- 中文：
-- 读取 Lua table 配置文件。
-- R5 Mod Settings 的 registrations/*.lua 和 saved/*.lua 都是 "return { ... }" 格式。
-- 注意：load() 会执行 Lua 代码。真实模组只应读取自己写入或信任的配置文件。
--
-- English:
-- Reads a Lua table config file.
-- R5 Mod Settings registrations/*.lua and saved/*.lua files use the "return { ... }" shape.
-- Note: load() executes Lua code. Real mods should only load files they wrote or trust.
local function read_lua_table(path)
    local f = io.open(path, "r")
    if not f then return nil end

    local content = f:read("*all")
    f:close()
    if not content or content == "" then return nil end

    -- 中文：
    -- 有些编辑器会写入 UTF-8 BOM。load() 前移除 BOM，避免解析失败。
    --
    -- English:
    -- Some editors write a UTF-8 BOM. Strip it before load() to avoid parse failures.
    if content:sub(1, 3) == "\239\187\191" then
        content = content:sub(4)
    end

    local loader = load(content)
    if not loader then return nil end

    local ok, data = pcall(loader)
    return ok and type(data) == "table" and data or nil
end

-- 中文：
-- 写文本文件，并在写入前确保父目录存在。
--
-- English:
-- Writes a text file and ensures the parent directory exists first.
local function write_text(path, body)
    mkdir(path:match("(.*[/\\])"))

    local existing = io.open(path, "r")
    if existing then
        local content = existing:read("*all")
        existing:close()
        if content == body then
            return true
        end
    end

    local f = io.open(path, "w")
    if not f then return false end
    f:write(body)
    f:close()
    return true
end

local function is_identifier(value)
    return type(value) == "string" and value:match("^[A-Za-z_][A-Za-z0-9_]*$") ~= nil
end

local function serialize_lua_value(value, indent)
    indent = indent or "    "
    local vt = type(value)
    if vt == "number" or vt == "boolean" then
        return tostring(value)
    end
    if vt == "string" then
        return string.format("%q", value)
    end
    if vt ~= "table" then
        return "nil"
    end

    local parts = { "{\n" }
    local keys = {}
    for key, _ in pairs(value) do keys[#keys + 1] = key end
    table.sort(keys, function(a, b) return tostring(a) < tostring(b) end)

    for _, key in ipairs(keys) do
        local key_text = is_identifier(key) and key or ("[" .. serialize_lua_value(key, indent .. "    ") .. "]")
        parts[#parts + 1] = indent .. key_text .. " = " .. serialize_lua_value(value[key], indent .. "    ") .. ",\n"
    end

    parts[#parts + 1] = indent:sub(1, math.max(#indent - 4, 0)) .. "}"
    return table.concat(parts)
end

-- 中文：
-- 规范化 keybind table。
-- keybind 配置应保持这个格式：
-- {
--     primary = "F8",
--     secondary = "None",
-- }
--
-- 本示例不接受旧式字符串快捷键。真实模组也建议只支持这个 table 格式，
-- 这样读取 saved 文件和 shared variables 时都更简单。
-- 如果传入值不是 table，返回 nil，让调用方忽略该值或写回默认配置。
--
-- English:
-- Normalizes a keybind table.
-- Keybind config should keep this shape:
-- {
--     primary = "F8",
--     secondary = "None",
-- }
--
-- This example does not accept old string keybind values. Real mods should also
-- use only this table shape so saved files and shared variables are easy to read.
-- If the incoming value is not a table, return nil so callers can ignore it or
-- write the default config back.
local function keybind_table(value)
    if type(value) ~= "table" then
        return nil
    end

    return {
        primary = type(value.primary) == "string" and value.primary or "None",
        secondary = type(value.secondary) == "string" and value.secondary or "None",
    }
end

local function config_values_equal(a, b)
    if type(a) ~= type(b) then return false end
    if type(a) ~= "table" then return a == b end

    for key, value in pairs(a) do
        if not config_values_equal(value, b[key]) then return false end
    end
    for key, value in pairs(b) do
        if not config_values_equal(value, a[key]) then return false end
    end
    return true
end

local function assign_config_value(key, value)
    if Config[key] == nil then return false end

    local normalized = value
    if key == "OpenMenuKey" then
        -- 中文：
        -- keybind 从 saved 文件读取时是 table，从 shared variables 读取时会先组装成 table。
        -- 这里统一走 keybind_table()，确保 Config.OpenMenuKey 始终是规范格式。
        --
        -- English:
        -- Keybinds come from saved files as tables, and are assembled into a table
        -- after reading shared variables. Use keybind_table() here so Config.OpenMenuKey
        -- always keeps the normalized shape.
        normalized = keybind_table(value)
        if not normalized then
            return false
        end
    elseif type(value) ~= type(Config[key]) then
        return false
    end

    if config_values_equal(Config[key], normalized) then
        return false
    end

    Config[key] = normalized
    return true
end

-- 中文：
-- 写入本模组的注册 manifest。
--
-- R5 Mod Settings 只需要第三方模组提供一个返回 table 的 Lua 文件。
-- 字段说明：
-- - name: 唯一名称，建议等于 UE4SS 模组文件夹名。
-- - display: 游戏内 Mods 页面显示名。
-- - version: 显示在模组名右侧，例如 "R5 Mod Settings Example - 0.1.0"。
-- - nexus_id: 可选。填写后会出现 Open Nexus Mods 按钮。
-- - settings: 设置项列表，目前支持 toggle、slider、dropdown、keybind。
--
-- English:
-- Writes this mod's registration manifest.
--
-- R5 Mod Settings only needs a Lua file that returns a table.
-- Field notes:
-- - name: Unique name. Prefer matching the UE4SS mod folder name.
-- - display: Display name shown on the in-game Mods page.
-- - version: Displayed beside the mod name, such as "R5 Mod Settings Example - 0.1.0".
-- - nexus_id: Optional. Adds an Open Nexus Mods button.
-- - settings: List of setting entries. Currently supports toggle, slider, dropdown, and keybind.
local function WriteManifest()
    local body = [=[-- Auto-generated by R5ModSettingsExample. Do not edit; will be regenerated.
-- 自动生成文件，请勿手动编辑；本示例模组启动时会重新生成。
--
-- 中文：
-- 如果你的模组想注册设置，可以复制这个文件结构到自己的模组里。
--
-- English:
-- Copy this file shape into your own mod if you want to register settings.
return {
    -- 中文：
    -- 唯一 id。发布后尽量保持不变，因为它会用于 saved 文件名。
    --
    -- English:
    -- Unique id. Keep it stable after release, because it is used for saved file names.
    name = "R5ModSettingsExample",

    -- 中文：
    -- Settings > Mods 页面里显示的模组名。
    --
    -- English:
    -- Display name shown in Settings > Mods.
    display = "R5 Mod Settings Example",

    -- 中文：
    -- 可选显示版本号。
    --
    -- English:
    -- Optional display version.
    version = "0.1.0",

    -- 中文：
    -- 可选 Nexus Mods id。填写后会生成 Open Nexus Mods 按钮。
    -- 最终 URL 是 https://www.nexusmods.com/windrose/mods/442
    --
    -- English:
    -- Optional Nexus Mods id. This creates an Open Nexus Mods button.
    -- The final URL is https://www.nexusmods.com/windrose/mods/442
    nexus_id = "442",

    settings = {
        {
            -- 中文：
            -- key 是保存文件和 shared variable 使用的名字。发布后尽量保持不变。
            --
            -- English:
            -- Key is the saved/shared variable name. Keep it stable after release.
            key = "Enabled",
            title = "Example Toggle",
            description = "Demonstrates an on/off setting saved as true or false.",
            type = "toggle",
            default = true,
        },
        {
            key = "Intensity",
            title = "Example Slider",
            description = "Demonstrates a slider saved as a numeric value.",
            type = "slider",
            default = 0.5,

            -- 中文：
            -- 滑条范围和每次移动的步进。
            --
            -- English:
            -- Slider range and movement step.
            min = 0.0,
            max = 1.0,
            step = 0.05,
        },
        {
            key = "Mode",
            title = "Example Dropdown",
            description = "Demonstrates a dropdown saved as a string value.",
            type = "dropdown",
            default = "balanced",

            -- 中文：
            -- value 是你的模组读取的真实值，label 是玩家看到的文本。
            --
            -- English:
            -- Values are what your mod reads. Labels are what the player sees.
            options = {
                { value = "balanced", label = "Balanced" },
                { value = "quality", label = "Quality" },
                { value = "performance", label = "Performance" },
            },
        },
        {
            key = "OpenMenuKey",
            title = "Example Keybind",
            description = "Demonstrates primary and secondary keybind values saved as Unreal key name strings.",
            type = "keybind",

            -- 中文：
            -- keybind 默认值必须是 primary/secondary table。
            -- 玩家可以在 Settings > Mods 中编辑 primary 和 secondary。
            -- 对已分配按键点击右键会取消对应槽位绑定，并保存为 "None"。
            -- 保存后，saved 文件仍然保持同样的 table 结构，方便你的 gameplay 逻辑直接读取。
            --
            -- English:
            -- Keybind defaults must be a primary/secondary table.
            -- Players can edit primary and secondary under Settings > Mods.
            -- Right-clicking an assigned key clears that slot and saves "None".
            -- Saved values keep the same table shape so your gameplay code can read them directly.
            default = {
                primary = "F8",
                secondary = "None",
            },
        },
    },
}
]=]

    if write_text(MANIFEST_PATH, body) then
        log("Manifest written to " .. MANIFEST_PATH)
    else
        log("Failed to write manifest at " .. MANIFEST_PATH .. " (is R5ModSettings installed?)")
    end
end

-- 中文：
-- 保存 Lua table。
-- 这里只保存适合作为配置的基础类型，以及 keybind 使用的 primary/secondary table。
--
-- English:
-- Saves a Lua table.
-- This only writes config-friendly primitive values and the primary/secondary table used by keybinds.
local function SaveLuaTable(path, data)
    local parts = { "-- Generated by R5ModSettingsExample. Editable; reloaded automatically.\nreturn {\n" }
    local keys = {}
    for key, _ in pairs(data) do keys[#keys + 1] = key end
    table.sort(keys)

    for _, key in ipairs(keys) do
        local value = data[key]
        if type(value) == "number" or type(value) == "boolean" or type(value) == "string" or type(value) == "table" then
            parts[#parts + 1] = string.format("    %s = %s,\n", key, serialize_lua_value(value, "        "))
        end
    end

    parts[#parts + 1] = "}\n"
    if not write_text(path, table.concat(parts)) then
        return false
    end

    -- 中文：
    -- 保存后也发布到 shared variables。
    -- 这样同一局游戏里，读取 shared variables 的逻辑能立即看到默认值。
    --
    -- English:
    -- Also publish saved values to shared variables.
    -- This lets runtime code read defaults immediately in the same session.
    if ModRef then
        for key, value in pairs(data) do
            local vt = type(value)
            if vt == "number" or vt == "boolean" or vt == "string" then
                pcall(function()
                    ModRef:SetSharedVariable(SHARED_PREFIX .. key, value)
                end)
            elseif vt == "table" and key == "OpenMenuKey" then
                local keys = keybind_table(value)
                if keys then
                    -- 中文：
                    -- keybind 请拆成两个独立变量发布。读取方应分别读取 /primary 和 /secondary。
                    --
                    -- English:
                    -- Publish keybinds as two independent values.
                    -- Readers should fetch /primary and /secondary separately.
                    pcall(function()
                        ModRef:SetSharedVariable(SHARED_PREFIX .. key .. "/primary", keys.primary)
                        ModRef:SetSharedVariable(SHARED_PREFIX .. key .. "/secondary", keys.secondary)
                    end)
                end
            end
        end
    end
    return true
end

-- 中文：
-- 确保 saved/<ModName>.lua 至少包含所有默认值。
--
-- 为什么要这么做：
-- R5 Mod Settings 会在加载 manifest 后自动生成或补齐 saved 默认值。
-- 但是 gameplay 模组可能在玩家打开 Settings > Mods 之前就需要读取配置。
-- 所以本示例推荐模组自己在启动时也确保默认 saved 文件存在。
--
-- 这不是接入 R5 Mod Settings 的强制步骤；如果你的模组只从 shared variables
-- 读取，或者不需要启动时立即读取 saved 文件，可以不复制这段。
--
-- English:
-- Ensures saved/<ModName>.lua contains all default values.
--
-- Why this matters:
-- R5 Mod Settings automatically creates or fills saved defaults after it loads a manifest.
-- However, a gameplay mod may need to read config before the player opens Settings > Mods.
-- For that reason, this example recommends that the owning mod also ensures its default
-- saved file exists on startup.
--
-- This is not required for every R5 Mod Settings integration. If your mod only reads shared
-- variables, or does not need to read saved values immediately on startup, you can skip this part.
local function EnsureSavedDefaults()
    local saved = read_lua_table(SAVED_PATH) or {}
    local changed = false

    -- 中文：
    -- 只补齐缺失项，不覆盖玩家已经保存过的值。
    --
    -- English:
    -- Only fill missing keys. Do not overwrite values already saved by the player.
    for _, key in ipairs(ConfigKeys) do
        if saved[key] == nil or (key == "OpenMenuKey" and type(saved[key]) ~= "table") then
            -- 中文：
            -- 如果 OpenMenuKey 被手动编辑成非 table，这里会写回默认 table。
            -- 这样 saved 文件始终能被 gameplay 逻辑直接读取，不需要每个功能点都做额外判断。
            --
            -- English:
            -- If OpenMenuKey was manually edited into a non-table value, write the default
            -- table back here. This keeps the saved file directly readable by gameplay code
            -- without adding extra checks everywhere.
            saved[key] = Config[key]
            changed = true
        end
    end

    if changed then
        return SaveLuaTable(SAVED_PATH, saved)
    end

    -- 中文：
    -- 文件本来就存在时，也把现有值发布到 shared variables。
    -- 这样 gameplay 逻辑可以统一优先读 shared variables。
    --
    -- English:
    -- If the file already exists, publish its current values to shared variables too.
    -- This lets gameplay logic consistently prefer shared variables.
    if ModRef then
        for key, value in pairs(saved) do
            local vt = type(value)
            if vt == "number" or vt == "boolean" or vt == "string" then
                pcall(function()
                    ModRef:SetSharedVariable(SHARED_PREFIX .. key, value)
                end)
            elseif vt == "table" and key == "OpenMenuKey" then
                local keys = keybind_table(value)
                if keys then
                    -- 中文：
                    -- 即使 saved 文件本来就存在，也要在启动时发布 keybind。
                    -- 这样你的模组进入 gameplay 前就可以优先读取 shared variables。
                    --
                    -- English:
                    -- Publish keybinds on startup even when the saved file already exists.
                    -- This lets your mod prefer shared variables before gameplay code runs.
                    pcall(function()
                        ModRef:SetSharedVariable(SHARED_PREFIX .. key .. "/primary", keys.primary)
                        ModRef:SetSharedVariable(SHARED_PREFIX .. key .. "/secondary", keys.secondary)
                    end)
                end
            end
        end
    end
    return true
end

-- 中文：
-- 加载当前配置。
--
-- 推荐读取顺序：
-- 1. 先读 shared variables，因为这是运行中改动后的最新值。
-- 2. 如果 shared variables 还没有值，再读 saved 文件。
--
-- 返回值表示 Config 是否发生变化，方便调用方决定是否重新应用设置。
--
-- English:
-- Loads the current config.
--
-- Recommended read order:
-- 1. Read shared variables first, because they contain the latest runtime changes.
-- 2. If shared variables are not available yet, read the saved file.
--
-- The return value tells the caller whether Config changed, so the mod can decide
-- whether it needs to re-apply settings.
local function LoadSavedConfig()
    local changed = false
    local got_any_shared = false

    if ModRef then
        for _, key in ipairs(ConfigKeys) do
            local value
            if key == "OpenMenuKey" then
                -- 中文：
                -- keybind 的 shared variables 是两个独立值。
                -- 如果只读到了其中一个，就用当前 Config 里的另一个值补齐，避免玩家只改一个按键时
                -- 把另一个按键错误重置。
                --
                -- English:
                -- Keybind shared variables are two separate values.
                -- If only one side is present, keep the other side from the current Config
                -- so changing one key does not accidentally reset the other key.
                local primary = ModRef:GetSharedVariable(SHARED_PREFIX .. key .. "/primary")
                local secondary = ModRef:GetSharedVariable(SHARED_PREFIX .. key .. "/secondary")
                if primary ~= nil or secondary ~= nil then
                    value = {
                        primary = type(primary) == "string" and primary or Config.OpenMenuKey.primary,
                        secondary = type(secondary) == "string" and secondary or Config.OpenMenuKey.secondary,
                    }
                end
            else
                value = ModRef:GetSharedVariable(SHARED_PREFIX .. key)
            end
            if value ~= nil then
                got_any_shared = true

                -- 中文：
                -- 类型检查可以避免手动编辑错误导致模组逻辑拿到异常值。
                --
                -- English:
                -- Type checks prevent manual edit mistakes from feeding invalid values into mod logic.
                if assign_config_value(key, value) then
                    changed = true
                end
            end
        end
    end

    if not got_any_shared then
        local saved = read_lua_table(SAVED_PATH)
        if saved then
            for key, value in pairs(saved) do
                if assign_config_value(key, value) then
                    changed = true
                end
            end
        end
    end

    return changed
end

local function ConfigSummary()
    return string.format("Enabled=%s Intensity=%.2f Mode=%s OpenMenuKey=%s",
        tostring(Config.Enabled),
        Config.Intensity,
        tostring(Config.Mode),
        tostring(Config.OpenMenuKey.primary) .. "/" .. tostring(Config.OpenMenuKey.secondary))
end

-- 中文：
-- 这里是实际模组应该应用配置的地方。
--
-- 示例只打印日志；真实模组可以在这里：
-- - 开启/关闭功能；
-- - 更新倍率、冷却、距离、数量等参数；
-- - 根据 dropdown 模式切换不同逻辑；
-- - 根据 keybind 保存的 primary/secondary Unreal KeyName 字符串实现自己的热键逻辑。
--
-- English:
-- This is where a real mod should apply its config.
--
-- This example only logs values. A real mod could:
-- - Enable or disable features.
-- - Update multipliers, cooldowns, distances, amounts, and similar parameters.
-- - Switch behavior based on the dropdown mode.
-- - Use the primary/secondary Unreal KeyName strings saved by keybind settings for your own hotkey logic.
local function ApplyConfig(reason)
    log(string.format("ApplyConfig (%s): %s", reason or "unknown", ConfigSummary()))
end

-- 中文：
-- 轮询 shared variables，检测玩家是否在游戏内改了设置。
--
-- 你的模组不需要每帧读取；通常 500-1000ms 检查一次已经足够。
--
-- English:
-- Poll shared variables to detect in-game setting changes.
--
-- Your mod does not need to read every frame. Checking every 500-1000ms is usually enough.
local function PollSharedConfig()
    if not ModRef then return false end

    local changed = false
    for _, key in ipairs(ConfigKeys) do
        if key == "OpenMenuKey" then
            -- 中文：
            -- 轮询时只检查 shared variables，不直接重读文件。
            -- 低频检查 /primary 和 /secondary，就可以发现玩家是否改了快捷键。
            --
            -- English:
            -- Poll shared variables instead of rereading the file directly.
            -- A low-frequency check of /primary and /secondary is enough to detect keybind changes.
            local primary = ModRef:GetSharedVariable(SHARED_PREFIX .. key .. "/primary")
            local secondary = ModRef:GetSharedVariable(SHARED_PREFIX .. key .. "/secondary")
            if primary ~= nil or secondary ~= nil then
                local value = {
                    primary = type(primary) == "string" and primary or Config.OpenMenuKey.primary,
                    secondary = type(secondary) == "string" and secondary or Config.OpenMenuKey.secondary,
                }
                local keys = keybind_table(value)
                if keys and not config_values_equal(Config.OpenMenuKey, keys) then
                    changed = true
                    break
                end
            end
        else
            local value = ModRef:GetSharedVariable(SHARED_PREFIX .. key)
            if value ~= nil and type(value) == type(Config[key]) and Config[key] ~= value then
                changed = true
                break
            end
        end
    end

    if not changed then return false end

    if LoadSavedConfig() then
        ApplyConfig("R5ModSettings shared variable update")
    end
    return true
end

-- 中文：
-- 启动流程：
-- 1. 写入注册 manifest，让 R5 Mod Settings 知道本模组有哪些设置。
-- 2. 可选但推荐：确保 saved 文件存在，并补齐缺失默认值。
-- 3. 读取当前配置到 Config。
-- 4. 应用配置。
--
-- English:
-- Startup flow:
-- 1. Write the registration manifest so R5 Mod Settings knows this mod's settings.
-- 2. Optional but recommended: ensure the saved file exists and fill missing default values.
-- 3. Load the current values into Config.
-- 4. Apply the config.
WriteManifest()
EnsureSavedDefaults()
LoadSavedConfig()
ApplyConfig("startup")

-- 中文：
-- 每秒检查一次设置变化。
-- pcall 可以避免轮询过程中一次异常直接终止整个 Lua 回调。
--
-- English:
-- Check for setting changes once per second.
-- pcall prevents one polling error from permanently stopping the Lua callback.
LoopAsync(1000, function()
    local ok, err = pcall(PollSharedConfig)
    if not ok then
        log("Config polling failed: " .. tostring(err))
    end
end)

log("Loaded. Open Settings > Mods to test toggle, slider, dropdown, keybind, and Nexus button.")

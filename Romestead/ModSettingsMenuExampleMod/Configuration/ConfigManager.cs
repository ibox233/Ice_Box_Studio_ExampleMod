using BepInEx.Configuration;
using Microsoft.Xna.Framework.Input;
using ModSettingsMenu.Api;

namespace ModSettingsMenuExampleMod.Configuration
{
    internal static class ConfigManager
    {
        public static ConfigEntry<string> ExampleMultiHotkey { get; private set; }

        public static void Init(ConfigFile config)
        {
            /*
             * Config.Bind(section, key, defaultValue, description)
             * section: BepInEx .cfg section and Mod Settings Menu category.
             * key: BepInEx .cfg key. It is also used to match saved config values.
             * defaultValue: value used when the .cfg does not already contain this key.
             * ConfigDescription(description, acceptableValues, params tags):
             *   description: BepInEx .cfg comment text. Mod Settings Menu also shows it unless Entry sets a custom description.
             *   acceptableValues: BepInEx validation metadata. Use null for unrestricted values.
             *   tags: optional extra objects after acceptableValues. Mod Settings Menu reads ModSettingsTags here.
             * ModSettingsTags.Section(section, displayName, order, hidden): category metadata. Put it on one entry in that section.
             * ModSettingsTags.Entry(displayName, description, order, sliderStep, hidden): config entry display metadata.
             * ModKeybindTags.Keybind(displayName, description, order, hidden): show ConfigEntry<Keys> or ConfigEntry<string> in the game's Settings > Bindings page.
             *   Keys entries use the first keyboard slot.
             *   String entries use K:key,key,key;M:mouse,mouse,mouse;B:button,button,button.
             *   K = keyboard Keys, M = mouse MouseButtons, B = gamepad/controller Buttons.
             *
             * ModKeybindValue helper API:
             *   ModKeybindValue.FromEntry(entry): parse a ConfigEntry<Keys> or ConfigEntry<string>.
             *   ModKeybindValue.Parse(raw): parse a raw K/M/B config string.
             *   value.Keys / value.MouseButtons / value.Buttons: read the three binding slots.
             *   value.ToSingleKey(): convert slot 0 to a Keys value for ConfigEntry<Keys>.
             *   value.ToConfigString(): serialize back to K/M/B string format.
             *   value.ToDisplayString(): format for logs or custom UI.
             */
            config.Bind("General", "Enabled", true, new ConfigDescription(
                // BepInEx description. Mod Settings Menu shows it unless ModSettingsTags.Entry sets a custom description.
                "Master switch for this example mod.",
                // Acceptable values. Null means any value of the config type is accepted.
                null,
                // Optional section metadata. Put it on one entry in the section to rename or sort the category.
                ModSettingsTags.Section("General", "General", 10),
                // Optional entry metadata. This controls display name, description, order, slider step, and hidden state.
                ModSettingsTags.Entry("Enable Example Mod", order: 1)));
            config.Bind("General", "Preset", ExamplePreset.Balanced, new ConfigDescription(
                "Enum setting example.",
                null,
                ModSettingsTags.Entry("Preset", order: 2)));
            config.Bind("General", "DebugMode", false, new ConfigDescription(
                "Boolean setting example.",
                null,
                ModSettingsTags.Entry("Debug Mode", order: 3)));

            config.Bind("Display", "Theme", "Classic", new ConfigDescription(
                "String list setting example.",
                // AcceptableValueList creates a left/right selector with the listed values.
                new AcceptableValueList<string>("Classic", "Copper", "Olive", "Ruby"),
                ModSettingsTags.Section("Display", "Display", 20),
                ModSettingsTags.Entry("Theme", order: 1)));
            config.Bind("Display", "Scale", 100, new ConfigDescription(
                "Integer range setting example.",
                // AcceptableValueRange creates a slider for supported numeric types.
                new AcceptableValueRange<int>(50, 200),
                ModSettingsTags.Entry(
                    displayName: "Scale",
                    description: "Custom description supplied through ConfigDescription tags.",
                    order: 2,
                    sliderStep: 5)));

            config.Bind("Controls", "ExampleHotkey", Keys.F7, new ConfigDescription(
                "Keyboard keybind setting example.",
                null,
                ModSettingsTags.Section("Controls", "Controls", 30),
                ModKeybindTags.Keybind("Example Hotkey", order: 1)));
            // K = keyboard Keys, M = mouse MouseButtons, B = gamepad/controller Buttons.
            ExampleMultiHotkey = config.Bind("Controls", "ExampleMultiHotkey", "K:F8,-,-;M:-,-,-;B:-,-,-", new ConfigDescription(
                "Three-slot keybind string example.",
                null,
                ModKeybindTags.Keybind("Example Multi Hotkey", order: 2)));

            config.Bind("Advanced", "Notes", "This text is editable in Mod Settings Menu.", new ConfigDescription(
                "String setting example.",
                null,
                ModSettingsTags.Section("Advanced", "Advanced", 40),
                ModSettingsTags.Entry("Notes", order: 1)));
            config.Bind("Advanced", "HiddenToken", "internal-value", new ConfigDescription(
                "Hidden by ModSettingsTags.Entry metadata.",
                null,
                ModSettingsTags.Entry(hidden: true)));
        }
    }

    public enum ExamplePreset
    {
        Relaxed,
        Balanced,
        Strict
    }
}

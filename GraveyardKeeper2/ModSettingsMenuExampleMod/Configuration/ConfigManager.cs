using BepInEx.Configuration;
using ModSettingsMenu.Api;
using UnityEngine;

namespace ModSettingsMenuExampleMod.Configuration
{
    // Keep all config bindings here so the entry point does not need to know each setting type or display rule.
    internal static class ConfigManager
    {
        // These values are both BepInEx config sections and the stable section keys used by the settings page.
        private const string GeneralSection = "General";
        private const string DisplaySection = "Display";
        private const string ControlsSection = "Controls";
        private const string InputSection = "Input";

        internal static void Bind(ConfigFile config)
        {
            /*
             * Each config binding demonstrates three layers of metadata:
             * - BepInEx section, key, and defaultValue define the stable config file format.
             * - ConfigDescription text and acceptable values define descriptions and validation rules.
             * - ModSettingsTags define menu display names, descriptions, ordering, and custom slider steps.
             *
             * Settings are discovered automatically without registration. Lower order values appear first;
             * unspecified orders use 1000. Put a section tag on any one entry in that section.
             * The menu uses native left/right selectors for bool, enum, and list values, sliders for ranged
             * numbers, key capture for KeyCode and KeyboardShortcut, and text input for other values.
             */

            // The section tag defines section text and order; the entry tag defines setting text and order.
            // A bool uses the menu's built-in localized on/off text, so no value translations are needed.
            config.Bind(GeneralSection, "Enabled", true, new ConfigDescription("Master switch for this example mod.", null, ModSettingsTags.Section(GeneralSection, "General", 10), ModSettingsTags.Entry("Enabled", "Enable the example mod.", 1)));

            // Enum values use a left/right selector while the stable enum member name remains stored in the config file.
            config.Bind(GeneralSection, "Preset", ExamplePreset.Balanced, new ConfigDescription("Enum setting example.", null, ModSettingsTags.Entry("Preset", "Select an enum value.", 2)));

            // A second bool demonstrates ordering within the same section without repeating its section tag.
            config.Bind(GeneralSection, "DebugMode", false, new ConfigDescription("Boolean setting example.", null, ModSettingsTags.Entry("Debug Mode", "Enable the debug mode example.", 3)));

            // AcceptableValueList<string> restricts the stored strings and creates a selector instead of a text input.
            config.Bind(DisplaySection, "Theme", "Classic", new ConfigDescription("String list setting example.", new AcceptableValueList<string>("Classic", "Copper", "Olive", "Ruby"), ModSettingsTags.Section(DisplaySection, "Display", 20), ModSettingsTags.Entry("Theme", "Select a string list value.", 1)));

            // AcceptableValueRange<int> creates an integer slider, and sliderStep: 5 moves it in units of five.
            // Steps are relative to the range minimum, while the range maximum of 200 remains directly reachable.
            config.Bind(DisplaySection, "Scale", 100, new ConfigDescription("Integer range setting example.", new AcceptableValueRange<int>(50, 200), ModSettingsTags.Entry("Scale", "Integer slider with a custom step of 5.", 2, sliderStep: 5d)));

            // Floating-point ranges also use sliders; 0.05 demonstrates a custom step instead of the 0.1 default.
            // The menu shows the actual config value, rather than the slider's normalized position.
            config.Bind(DisplaySection, "Opacity", 0.8f, new ConfigDescription("Floating-point range setting example.", new AcceptableValueRange<float>(0f, 1f), ModSettingsTags.Entry("Opacity", "Floating-point slider with a custom step of 0.05.", 3, sliderStep: 0.05d)));

            // UnityEngine.KeyCode creates a capture button for a single key; Escape cancels capture.
            config.Bind(ControlsSection, "ExampleHotkey", KeyCode.F7, new ConfigDescription("Keyboard key binding setting example.", null, ModSettingsTags.Section(ControlsSection, "Controls", 30), ModSettingsTags.Entry("Example Hotkey", "Capture a KeyCode value.", 1)));

            // KeyboardShortcut stores a main key together with modifier keys; the default is LeftControl + F8.
            // During capture, hold the modifiers and press the main key. Escape cancels without changing the value.
            config.Bind(ControlsSection, "ExampleShortcut", new KeyboardShortcut(KeyCode.F8, KeyCode.LeftControl), new ConfigDescription("Keyboard shortcut setting example.", null, ModSettingsTags.Entry("Example Shortcut", "Hold Ctrl, Shift or Alt and press a main key. Escape cancels capture.", 2)));

            // A string without AcceptableValueList uses a text input; its contents are the user's value, not a UI label.
            config.Bind(InputSection, "PlayerName", "Keeper", new ConfigDescription("String input setting example.", null, ModSettingsTags.Section(InputSection, "Input", 40), ModSettingsTags.Entry("Player Name", "Edit a string value.", 1)));

            // An unrestricted integer has no slider constraint and uses a text input that must parse as an integer.
            config.Bind(InputSection, "RetryCount", 3, new ConfigDescription("Unrestricted integer input example.", null, ModSettingsTags.Entry("Retry Count", "Edit an integer without a range.", 2)));
        }
    }

    // The config stores Relaxed, Balanced, or Strict; the localized example translates only their display text.
    internal enum ExamplePreset
    {
        Relaxed,
        Balanced,
        Strict
    }
}

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
             * - ConfigDescription text and acceptable values define validation rules.
             * - ModSettingsTags define only menu display names, descriptions, and ordering.
             *
             * ModSettingsMenu chooses controls by value type: bool uses a checkbox, enums and lists use
             * dropdowns, ranged numbers use sliders, KeyCode uses key capture, and other values use text input.
             */

            // A bool appears as a native-style checkbox in ModSettingsMenu.
            // The section tag defines section text and order; the entry tag defines setting text and order.
            config.Bind(GeneralSection, "Enabled", true, new ConfigDescription("Master switch for this example mod.", null, ModSettingsTags.Section(GeneralSection, "General", 10), ModSettingsTags.Entry("Enabled", "Enable the example mod.", 1)));

            // Enum values become a dropdown while the stable enum member name remains stored in the config file.
            config.Bind(GeneralSection, "Preset", ExamplePreset.Balanced, new ConfigDescription("Enum setting example.", null, ModSettingsTags.Entry("Preset", "Select an enum value.", 2)));

            // A second bool demonstrates layout and ordering when one section contains multiple checkboxes.
            config.Bind(GeneralSection, "DebugMode", false, new ConfigDescription("Boolean setting example.", null, ModSettingsTags.Entry("Debug Mode", "Enable the debug mode example.", 3)));

            // AcceptableValueList<string> turns string values into a dropdown instead of a text input.
            config.Bind(DisplaySection, "Theme", "Classic", new ConfigDescription("String list setting example.", new AcceptableValueList<string>("Classic", "Copper", "Olive", "Ruby"), ModSettingsTags.Section(DisplaySection, "Display", 20), ModSettingsTags.Entry("Theme", "Select a string list value.", 1)));

            // AcceptableValueRange<int> creates an integer slider, and sliderStep: 5 moves it in units of five.
            // Steps are relative to the range minimum, while the range maximum of 200 remains directly reachable.
            config.Bind(DisplaySection, "Scale", 100, new ConfigDescription("Integer range setting example.", new AcceptableValueRange<int>(50, 200), ModSettingsTags.Entry("Scale", "Integer slider with a custom step of 5.", 2, sliderStep: 5d)));

            // Floating-point ranges also use sliders; 0.05 demonstrates a custom step instead of the 0.1 default.
            config.Bind(DisplaySection, "Opacity", 0.8f, new ConfigDescription("Floating-point range setting example.", new AcceptableValueRange<float>(0f, 1f), ModSettingsTags.Entry("Opacity", "Floating-point slider with a custom step of 0.05.", 3, sliderStep: 0.05d)));

            // UnityEngine.KeyCode appears as a key capture button on the mod settings page.
            config.Bind(ControlsSection, "ExampleHotkey", KeyCode.F7, new ConfigDescription("Keyboard key binding setting example.", null, ModSettingsTags.Section(ControlsSection, "Controls", 30), ModSettingsTags.Entry("Example Hotkey", "Capture a KeyCode value.", 1)));

            // A string without AcceptableValueRange or AcceptableValueList uses a text input.
            config.Bind(InputSection, "PlayerName", "Survivor", new ConfigDescription("String input setting example.", null, ModSettingsTags.Section(InputSection, "Input", 40), ModSettingsTags.Entry("Player Name", "Edit a string value.", 1)));

            // An unrestricted integer has no slider constraint and uses an editable text input.
            config.Bind(InputSection, "RetryCount", 3, new ConfigDescription("Unrestricted integer input example.", null, ModSettingsTags.Entry("Retry Count", "Edit an integer without a range.", 2)));
        }
    }

    // The enum dropdown stores Relaxed, Balanced, or Strict; the localized example translates their display text.
    internal enum ExamplePreset
    {
        Relaxed,
        Balanced,
        Strict
    }
}

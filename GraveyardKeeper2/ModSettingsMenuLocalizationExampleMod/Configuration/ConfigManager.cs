using BepInEx.Configuration;
using ModSettingsMenu.Api;
using ModSettingsMenuLocalizationExampleMod.Localization;
using UnityEngine;

namespace ModSettingsMenuLocalizationExampleMod.Configuration
{
    // Keep stable section and key values in the config file and localize only the text shown in the menu.
    internal static class ConfigManager
    {
        // These section names never change with the language, so existing config files remain readable.
        private const string GeneralSection = "General";
        private const string DisplaySection = "Display";
        private const string ControlsSection = "Controls";
        private const string InputSection = "Input";

        internal static void Bind(ConfigFile config)
        {
            /*
             * Localizer.Config creates a ConfigDescription and attaches two dynamic tags:
             * - The section tag resolves its display name from sectionKey and carries the section order.
             * - The entry tag resolves its name, description, selector values, and custom slider step.
             *
             * For a base key such as config.enabled, JSON provides config.enabled.name and
             * config.enabled.description. Section names use the explicit config.section.* keys.
             * Lower order values appear first. JSON changes only menu text, not section, key, defaults,
             * or stored values. Missing translations use en; keep the en section in the example JSON.
             */

            // The first numeric argument is entry order; sectionKey and the next number define section text and order.
            // Bool values use built-in localized on/off text; JSON supplies only this entry's name and description.
            config.Bind(GeneralSection, "Enabled", true, I18n.Localizer.Config("config.enabled", 1, GeneralSection, "config.section.general", 10));

            // Enum display text uses config.preset.relaxed, config.preset.balanced, and config.preset.strict.
            // The config file still stores the original enum member, regardless of the selected game language.
            config.Bind(GeneralSection, "Preset", ExamplePreset.Balanced, I18n.Localizer.Config("config.preset", 2, GeneralSection, "config.section.general", 10));

            // Entry ordering in the same section remains stable when the language refreshes.
            config.Bind(GeneralSection, "DebugMode", false, I18n.Localizer.Config("config.debug_mode", 3, GeneralSection, "config.section.general", 10));

            // AcceptableValueList keeps Classic, Copper, Olive, and Ruby as stored values; JSON translates selector text.
            // The matching keys are config.theme.classic, config.theme.copper, config.theme.olive, and config.theme.ruby.
            config.Bind(DisplaySection, "Theme", "Classic", I18n.Localizer.Config("config.theme", 1, DisplaySection, "config.section.display", 20, new AcceptableValueList<string>("Classic", "Copper", "Olive", "Ruby")));

            // sliderStep: 5 moves the integer slider in units of five without changing the stored integer type.
            // Steps start at the range minimum of 50; the displayed value is the actual config value.
            config.Bind(DisplaySection, "Scale", 100, I18n.Localizer.Config("config.scale", 2, DisplaySection, "config.section.display", 20, new AcceptableValueRange<int>(50, 200), sliderStep: 5d));

            // The 0.05 step demonstrates decimal values; only the slider's name and description are localized.
            config.Bind(DisplaySection, "Opacity", 0.8f, I18n.Localizer.Config("config.opacity", 3, DisplaySection, "config.section.display", 20, new AcceptableValueRange<float>(0f, 1f), sliderStep: 0.05d));

            // KeyCode creates a single-key capture control. Translate its name and description, not key identifiers.
            config.Bind(ControlsSection, "ExampleHotkey", KeyCode.F7, I18n.Localizer.Config("config.example_hotkey", 1, ControlsSection, "config.section.controls", 30));

            // KeyboardShortcut captures a main key with modifiers; LeftControl + F8 is the default.
            // Hold Ctrl, Shift, or Alt and press the main key; Escape cancels. Key identifiers are not JSON translations.
            config.Bind(ControlsSection, "ExampleShortcut", new KeyboardShortcut(KeyCode.F8, KeyCode.LeftControl), I18n.Localizer.Config("config.example_shortcut", 2, ControlsSection, "config.section.controls", 30));

            // A string without an acceptable value list uses a text input; JSON does not translate the user's input.
            config.Bind(InputSection, "PlayerName", "Keeper", I18n.Localizer.Config("config.player_name", 1, InputSection, "config.section.input", 40));

            // The unrestricted integer demonstrates write-back through a text input with a localized name and description.
            config.Bind(InputSection, "RetryCount", 3, I18n.Localizer.Config("config.retry_count", 2, InputSection, "config.section.input", 40));
        }
    }

    // The config file stores English enum values while matching JSON keys provide localized display text.
    internal enum ExamplePreset
    {
        Relaxed,
        Balanced,
        Strict
    }
}

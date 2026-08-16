using BepInEx.Configuration;
using ModSettingsMenu.Api;
using ModSettingsMenuLocalizationExampleMod.Localization;
using UnityEngine;

namespace ModSettingsMenuLocalizationExampleMod.Configuration
{
    // The localized example keeps stable section and key values in the config file and localizes only UI text.
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
             * - The entry tag resolves its name, description, dropdown values, and custom slider step.
             *
             * JSON changes only text displayed on the settings page. It does not change section, key,
             * default values, or config serialization, so language changes require no config migration.
             */

            // Config creates BepInEx description text together with dynamic section and entry tags.
            // The first numeric argument is entry order; sectionKey and the final number define section text and order.
            config.Bind(GeneralSection, "Enabled", true, I18n.Localizer.Config("config.enabled", 1, GeneralSection, "config.section.general", 10));

            // Enum display text is read from JSON through the preset.relaxed, preset.balanced, and preset.strict keys.
            config.Bind(GeneralSection, "Preset", ExamplePreset.Balanced, I18n.Localizer.Config("config.preset", 2, GeneralSection, "config.section.general", 10));

            // Entry ordering in the same section remains stable when the language refreshes.
            config.Bind(GeneralSection, "DebugMode", false, I18n.Localizer.Config("config.debug_mode", 3, GeneralSection, "config.section.general", 10));

            // AcceptableValueList keeps Classic, Copper, Olive, and Ruby as stored values; JSON localizes display text only.
            config.Bind(DisplaySection, "Theme", "Classic", I18n.Localizer.Config("config.theme", 1, DisplaySection, "config.section.display", 20, new AcceptableValueList<string>("Classic", "Copper", "Olive", "Ruby")));

            // sliderStep: 5 moves the integer slider in units of five without changing the stored integer type.
            config.Bind(DisplaySection, "Scale", 100, I18n.Localizer.Config("config.scale", 2, DisplaySection, "config.section.display", 20, new AcceptableValueRange<int>(50, 200), sliderStep: 5d));

            // The 0.05 floating-point step demonstrates decimal display, config writes, and rebuilding after language changes.
            config.Bind(DisplaySection, "Opacity", 0.8f, I18n.Localizer.Config("config.opacity", 3, DisplaySection, "config.section.display", 20, new AcceptableValueRange<float>(0f, 1f), sliderStep: 0.05d));

            // KeyCode uses the standard Unity key type, so the menu creates a key capture control.
            config.Bind(ControlsSection, "ExampleHotkey", KeyCode.F7, I18n.Localizer.Config("config.example_hotkey", 1, ControlsSection, "config.section.controls", 30));

            // A string without an acceptable value list appears as a text input.
            config.Bind(InputSection, "PlayerName", "Survivor", I18n.Localizer.Config("config.player_name", 1, InputSection, "config.section.input", 40));

            // The unrestricted integer demonstrates serialization and write-back through a text input.
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

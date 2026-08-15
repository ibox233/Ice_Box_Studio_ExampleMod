using BepInEx.Configuration;
using UnityEngine;

namespace ModSettingsMenuLocalizationExampleMod.Configuration
{
    // This enum demonstrates the native left/right selector used for enum
    // configuration entries.
    internal enum ExampleMode
    {
        First,
        Second,
        Third
    }

    internal static class ConfigManager
    {
        internal static void Bind(ConfigFile config)
        {
            // Each Config.Bind call stays on one line to match the game mod
            // examples. ModLocalizer.Config adds the display name,
            // description, section label, and ordering metadata to the entry.

            // A bool is rendered as an on/off control. The entry order is 20,
            // while the General section itself starts at order 10.
            config.Bind("General", "Enabled", true, I18n.Localizer.Config("config.enabled", 20, "General", "config.section.general", 10));

            // The enum default is Second. Its three enum values are localized
            // through config.mode.first, config.mode.second, and
            // config.mode.third in the JSON file.
            config.Bind("General", "Mode", ExampleMode.Second, I18n.Localizer.Config("config.mode", 10, "General", "config.section.general", 10));

            // AcceptableValueList creates a finite left/right selector even
            // though the stored value is a string. The list values are
            // localized by the config.quality.* keys.
            config.Bind("Values", "Quality", "Medium", I18n.Localizer.Config("config.quality", 10, "Values", "config.section.values", 20, new AcceptableValueList<string>("Low", "Medium", "High")));

            // This integer slider overrides the default step of 1 and moves in
            // increments of 2 across the declared range.
            config.Bind("Values", "Count", 6, I18n.Localizer.Config("config.count", 30, "Values", "config.section.values", 20, new AcceptableValueRange<int>(0, 12), sliderStep: 2d));

            // This floating-point slider demonstrates a custom 0.25 step.
            config.Bind("Values", "Speed", 1.5f, I18n.Localizer.Config("config.speed", 20, "Values", "config.section.values", 20, new AcceptableValueRange<float>(0.5f, 3f), sliderStep: 0.25d));

            // KeyCode entries use the native three-column rebind row. The menu
            // captures a Legacy Input key and writes it back to this entry.
            config.Bind("Input", "Shortcut", KeyCode.F8, I18n.Localizer.Config("config.shortcut", 20, "Input", "config.section.input", 30));

            // A string without an acceptable-value constraint is rendered as
            // the custom text editor. This entry also verifies long text and
            // submit/cancel behavior in the example menu.
            config.Bind("Input", "Label", "Big Walk", I18n.Localizer.Config("config.label", 10, "Input", "config.section.input", 30));
        }
    }
}

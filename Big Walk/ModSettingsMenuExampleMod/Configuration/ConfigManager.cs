using BepInEx.Configuration;
using ModSettingsMenu.Api;
using UnityEngine;

namespace ModSettingsMenuExampleMod.Configuration
{
    // Enum entries are rendered as native left/right selectors.
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
            /*
             * Config.Bind stores the setting in the normal BepInEx config file.
             * ConfigDescription contains the config comment, acceptable values,
             * and any Mod Settings Menu metadata tags.
             *
             * ModSettingsTags.Section(section, order) controls category order.
             * Add it to one entry in each section. Lower values appear first.
             * ModSettingsTags.Entry(order, sliderStep) controls entry order and
             * optionally overrides the numeric slider step.
             *
             * This project does not reference BigWalkLocalizationAPI. The menu
             * therefore displays the original section names and config keys.
             */

            // The enum is ordered before the bool even though Enabled is bound first.
            config.Bind("General", "Enabled", true, new ConfigDescription("Enable this example mod.", null, ModSettingsTags.Section("General", order: 10), ModSettingsTags.Entry(order: 20)));
            config.Bind("General", "Mode", ExampleMode.Second, new ConfigDescription("Choose an enum value.", null, ModSettingsTags.Entry(order: 10)));

            // AcceptableValueList creates a finite left/right selector.
            config.Bind("Values", "Quality", "Medium", new ConfigDescription("Choose a value from a fixed string list.", new AcceptableValueList<string>("Low", "Medium", "High"), ModSettingsTags.Section("Values", order: 20), ModSettingsTags.Entry(order: 10)));

            // AcceptableValueRange creates a slider. These entries demonstrate
            // custom steps instead of the default integer 1 and decimal 0.1.
            config.Bind("Values", "Count", 6, new ConfigDescription("Adjust an integer value in steps of 2.", new AcceptableValueRange<int>(0, 12), ModSettingsTags.Entry(order: 30, sliderStep: 2d)));
            config.Bind("Values", "Speed", 1.5f, new ConfigDescription("Adjust a decimal value in steps of 0.25.", new AcceptableValueRange<float>(0.5f, 3f), ModSettingsTags.Entry(order: 20, sliderStep: 0.25d)));

            // KeyCode entries use the native-style key binding row.
            config.Bind("Input", "Shortcut", KeyCode.F8, new ConfigDescription("Choose the example keyboard shortcut.", null, ModSettingsTags.Section("Input", order: 30), ModSettingsTags.Entry(order: 20)));

            // Unrestricted strings use the custom text editor.
            config.Bind("Input", "Label", "Big Walk", new ConfigDescription("Edit a serialized text value.", null, ModSettingsTags.Entry(order: 10)));
        }
    }
}

using System.Collections.Generic;
using BepInEx.Configuration;
using Microsoft.Xna.Framework.Input;
using ModSettingsMenu.Api;

namespace ModSettingsMenuLocalizationExampleMod.Configuration
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
             *
             * This localized example uses Description(...) instead of the static ModSettingsTags helpers.
             * The custom tags keep the same public property names, but resolve their display text through I18n.L.
             */
            config.Bind("General", "Enabled", true, Description(
                // BepInEx description. Mod Settings Menu shows it unless the localized entry tag sets a custom description.
                "config.enabled.description",
                // Optional entry metadata. The localized tag controls display name, description, order, slider step, and hidden state.
                "entry.enabled", 1,
                // Optional section metadata. Put it on one entry in the section to rename or sort the category.
                "General", "section.general", 10));
            config.Bind("General", "Preset", ExamplePreset.Balanced, Description(
                "config.preset.description", "entry.preset", 2));
            config.Bind("General", "DebugMode", false, Description(
                "config.debug_mode.description", "entry.debug_mode", 3));

            // Stored values stay stable, so changing language never changes the .cfg format.
            config.Bind("Display", "Theme", "Classic", Description(
                "config.theme.description", "entry.theme", 1,
                "Display", "section.display", 20,
                // AcceptableValueList creates a left/right selector with the listed values.
                new AcceptableValueList<string>("Classic", "Copper", "Olive", "Ruby")));
            config.Bind("Display", "Scale", 100, Description(
                "config.scale.description", "entry.scale", 2,
                // AcceptableValueRange creates a slider for supported numeric types.
                acceptableValues: new AcceptableValueRange<int>(50, 200),
                sliderStep: 5));

            config.Bind("Controls", "ExampleHotkey", Keys.F7, Description(
                "config.hotkey.description", "entry.hotkey", 1,
                "Controls", "section.controls", 30,
                keybind: true));
            // K = keyboard Keys, M = mouse MouseButtons, B = gamepad/controller Buttons.
            ExampleMultiHotkey = config.Bind("Controls", "ExampleMultiHotkey", "K:F8,-,-;M:-,-,-;B:-,-,-", Description(
                "config.multi_hotkey.description", "entry.multi_hotkey", 2,
                keybind: true));

            config.Bind("Advanced", "Notes", "This text is editable in Mod Settings Menu.", Description(
                "config.notes.description", "entry.notes", 1,
                "Advanced", "section.advanced", 40));
            config.Bind("Advanced", "HiddenToken", "internal-value", Description(
                "config.hidden_token.description", entryOrder: 2, hidden: true));
        }

        private static ConfigDescription Description(
            string descriptionKey,
            string displayNameKey = null,
            int? entryOrder = null,
            string section = null,
            string sectionNameKey = null,
            int? sectionOrder = null,
            AcceptableValueBase acceptableValues = null,
            double? sliderStep = null,
            bool keybind = false,
            bool hidden = false)
        {
            var tags = new List<object>();
            if (!string.IsNullOrWhiteSpace(section))
            {
                tags.Add(new LocalizedSectionTag(section, sectionNameKey, sectionOrder));
            }

            tags.Add(new LocalizedEntryTag(displayNameKey, descriptionKey, entryOrder, sliderStep, keybind, hidden));
            return new ConfigDescription(Text(descriptionKey), acceptableValues, tags.ToArray());
        }

        private static string Text(string key)
        {
            return I18n.L.Text(key);
        }

        // Mod Settings Menu recognizes public properties on custom ConfigDescription tags.
        // The property names match ModSettingsSectionTag and ModSettingsEntryTag.
        private sealed class LocalizedSectionTag
        {
            private readonly string _displayNameKey;

            public LocalizedSectionTag(string section, string displayNameKey, int? order)
            {
                Section = section;
                _displayNameKey = displayNameKey;
                Order = order;
            }

            public string Section { get; }

            public string DisplayName => TextOrNull(_displayNameKey);

            public int? Order { get; }
        }

        private sealed class LocalizedEntryTag
        {
            private readonly string _displayNameKey;
            private readonly string _descriptionKey;

            public LocalizedEntryTag(string displayNameKey, string descriptionKey, int? order, double? sliderStep, bool keybind, bool hidden)
            {
                _displayNameKey = displayNameKey;
                _descriptionKey = descriptionKey;
                Order = order;
                SliderStep = sliderStep;
                Keybind = keybind ? true : null;
                Hidden = hidden ? true : null;
            }

            public string DisplayName => TextOrNull(_displayNameKey);

            public string Description => TextOrNull(_descriptionKey);

            public int? Order { get; }

            public double? SliderStep { get; }

            public bool? Keybind { get; }

            public bool? Hidden { get; }
        }

        private static string TextOrNull(string key)
        {
            return string.IsNullOrWhiteSpace(key) ? null : Text(key);
        }
    }

    public enum ExamplePreset
    {
        Relaxed,
        Balanced,
        Strict
    }
}

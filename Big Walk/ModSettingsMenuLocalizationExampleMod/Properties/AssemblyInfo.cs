using System.Reflection;
using System.Runtime.InteropServices;
using ModSettingsMenuLocalizationExampleMod;

// These attributes provide the assembly-level metadata used by BepInEx tools,
// release archives, and Windows file properties.
[assembly: AssemblyTitle(PluginInfo.PLUGIN_NAME)]
[assembly: AssemblyDescription("Localized Mod Settings Menu example for Big Walk")]
[assembly: AssemblyCompany(PluginInfo.PLUGIN_AUTHOR)]
[assembly: AssemblyProduct(PluginInfo.PLUGIN_NAME)]
[assembly: AssemblyCopyright("Copyright © 2026 " + PluginInfo.PLUGIN_AUTHOR + " All rights reserved.")]

// The plugin does not need COM exposure. The GUID below is an assembly identity
// and is intentionally different from the BepInEx plugin GUID above.
[assembly: ComVisible(false)]
[assembly: Guid("d9fc82a0-e34a-49c5-9317-3b5fff44b56c")]

// The assembly version follows the plugin version and is kept at four numeric
// components for the .NET assembly metadata format.
[assembly: AssemblyVersion(PluginInfo.PLUGIN_VERSION + ".0")]
[assembly: AssemblyFileVersion(PluginInfo.PLUGIN_VERSION + ".0")]

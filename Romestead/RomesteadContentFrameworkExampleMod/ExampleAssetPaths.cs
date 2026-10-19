using System.IO;
using BepInEx;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class ExampleAssetPaths
    {
        public static string Asset(string folder, string fileName)
        {
            // Match the deployment layout in the project file. The example DLL sits
            // directly in plugins, while its loose assets live in this named folder.
            // Resolve from BepInEx's plugin directory rather than the process's current
            // working directory, which is not an asset-location contract.
            return Path.Combine(Paths.PluginPath, "RomesteadContentFrameworkExampleMod", "Assets", folder, fileName);
        }
    }
}

using RomesteadContentFramework.Api;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class LumenCatalystShop
    {
        public static void Register()
        {
            // Extend an existing shop by its game ID. This does not create a merchant
            // or replace the shop; the item's Tradable component supplies its price.
            RomesteadContent.Shops.AddItem("all:1", ExampleIds.LumenCatalystItem);
        }
    }
}

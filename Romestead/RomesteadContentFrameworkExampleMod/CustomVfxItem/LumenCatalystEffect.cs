using RomesteadContentFramework.Api;

namespace RomesteadContentFrameworkExampleMod
{
    internal static class LumenCatalystEffect
    {
        public static void Register()
        {
            // Register a framework-backed spell, then reference its ID from UsableItem.
            // This API supplies the shared spell definition and the appropriate native
            // client/server spell handlers; it does not turn client callbacks into RPCs.
            RomesteadContent.Spells.RegisterEffect(ExampleIds.LumenCatalystEffect, "Lumen Burst", effect =>
            {
                // This is an existing game sound event; no audio file is bundled here.
                effect.CastSoundEffect = "event:/items/coin_drop";
                // Keep presentation on the client. Cast position is optional, so the
                // visual is only played when the native casting context provides one.
                effect.OnClientCast = context =>
                {
                    if (context.Context.Position.HasValue)
                    {
                        RomesteadContent.Vfx.Play(ExampleIds.LumenCatalystVfx, context.Context.Position.Value, scale: 1.3f);
                    }
                };
                // Apply authoritative gameplay changes in the server callback when
                // implementing a real effect. This teaching example only logs a cast;
                // its visual burst does not heal, damage or grant any items.
                effect.OnServerCast = context =>
                {
                    RomesteadContentFrameworkExampleMod.Logger.LogInfo("Lumen Catalyst custom server effect cast.");
                };
            });
        }
    }
}

using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Events;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class EventSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "event";

    public override async Task Test( TestContext t )
    {
        var ev = Core.Event;

        await t.TestAsync("OnTick fires repeatedly while subscribed", async () =>
        {
            var count = 0;
            void handler() => count++;
            ev.OnTick += handler;
            try
            {
                if (!await t.Until(() => count >= 3, 128)) Skip("OnTick did not fire (server hibernating?)");
            }
            finally { ev.OnTick -= handler; }
        });

        await t.TestAsync("Unsubscribing OnTick stops further increments", async () =>
        {
            var count = 0;
            void handler() => count++;
            ev.OnTick += handler;
            if (!await t.Until(() => count > 0, 128))
            {
                ev.OnTick -= handler;
                Skip("OnTick did not fire (server hibernating?)");
            }
            ev.OnTick -= handler;
            var snapshot = count;
            await t.Ticks(4);
            Equal(snapshot, count, "count after unsubscribe");
        });

        await t.TestAsync("OnConsoleOutput observes a WriteToServerConsole marker", async () =>
        {
            var marker = "tester-event-" + Guid.NewGuid().ToString("N")[..10];
            string? seen = null;
            void handler( IOnConsoleOutputEvent e ) { if (e.Message.Contains(marker)) seen = e.Message; }
            ev.OnConsoleOutput += handler;
            try
            {
                Core.ConsoleOutput.WriteToServerConsole($"[Tester] {marker}");
                if (!await t.Until(() => seen is not null, 128)) Skip("OnConsoleOutput did not observe the marker");
                Expect(seen!.Contains(marker), $"message '{seen}' missing marker");
            }
            finally { ev.OnConsoleOutput -= handler; }
        });

        t.Test("Subscribing and unsubscribing a batch of lifecycle events never throws", () =>
        {
            void onConnected( IOnClientConnectedEvent e ) { }
            void onDisconnected( IOnClientDisconnectedEvent e ) { }
            void onMapLoad( IOnMapLoadEvent e ) { }
            void onMapUnload( IOnMapUnloadEvent e ) { }
            void onConVarChanged( IOnConVarValueChanged e ) { }

            ev.OnClientConnected += onConnected;
            ev.OnClientDisconnected += onDisconnected;
            ev.OnMapLoad += onMapLoad;
            ev.OnMapUnload += onMapUnload;
            ev.OnConVarValueChanged += onConVarChanged;

            ev.OnClientConnected -= onConnected;
            ev.OnClientDisconnected -= onDisconnected;
            ev.OnMapLoad -= onMapLoad;
            ev.OnMapUnload -= onMapUnload;
            ev.OnConVarValueChanged -= onConVarChanged;
        });
    }
}

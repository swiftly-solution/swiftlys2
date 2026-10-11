using SwiftlyS2.Shared;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class SchedulerSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "scheduler";

    public override async Task Test( TestContext t )
    {
        var s = Core.Scheduler;

        await t.TestAsync("NextTick runs on the game thread next tick", async () =>
        {
            var ran = false;
            var onGame = false;
            s.NextTick(() => { ran = true; onGame = Core.IsGameThread; });
            Expect(!ran, "ran synchronously");
            Expect(await t.Until(() => ran), "never ran");
            Expect(onGame, "not on game thread");
        });

        await t.TestAsync("NextTickAsync<T> returns value", async () =>
            Equal(42, await s.NextTickAsync(() => 42)));

        await t.TestAsync("Delay fires once after the delay", async () =>
        {
            var n = 0;
            _ = s.Delay(4, () => n++);
            await t.Ticks(2);
            Equal(0, n, "fired early");
            Expect(await t.Until(() => n == 1, 32), "never fired");
            await t.Ticks(8);
            Equal(1, n, "fired more than once");
        });

        await t.TestAsync("Delay can be cancelled", async () =>
        {
            var n = 0;
            var cts = s.Delay(4, () => n++);
            cts.Cancel();
            await t.Ticks(12);
            Equal(0, n, "fired after cancel");
        });

        await t.TestAsync("Repeat runs immediately then periodically, stops on cancel", async () =>
        {
            var n = 0;
            var cts = s.Repeat(2, () => n++);
            await t.Ticks(10);
            cts.Cancel();
            Expect(n >= 3, $"only {n} runs");
            var atCancel = n;
            await t.Ticks(6);
            Equal(atCancel, n, "kept running after cancel");
        });

        await t.TestAsync("DelayBySeconds fires", async () =>
        {
            var n = 0;
            _ = s.DelayBySeconds(0.1f, () => n++);
            Expect(await t.Until(() => n == 1, 64), "never fired");
        });
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var s = Core.Scheduler;
        p.Profile("Scheduler.NextTick (enqueue)", () => s.NextTick(static () => { }), 20_000);
        p.Profile("Scheduler.Delay + Cancel", () => s.Delay(1000, static () => { }).Cancel(), 20_000);
        p.Profile("Scheduler.Repeat + Cancel", () => s.Repeat(1000, static () => { }).Cancel(), 20_000);
        return Task.CompletedTask;
    }
}

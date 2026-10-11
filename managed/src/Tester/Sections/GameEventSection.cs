using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Misc;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class GameEventSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "gameevent";

    private IGameEventService Ge => Core.GameEvent;

    public override async Task Test( TestContext t )
    {
        var ge = Ge;

        t.Test("HookPre observes FireToServer and Stop prevents HookPost", () =>
        {
            var pre = false;
            var post = false;
            var preGuid = ge.HookPre<EventRoundFreezeEnd>(_ => { pre = true; return HookResult.Stop; });
            var postGuid = ge.HookPost<EventRoundFreezeEnd>(_ => { post = true; return HookResult.Continue; });
            try
            {
                ge.FireToServer<EventRoundFreezeEnd>();
                if (!pre) Skip("hook did not observe FireToServer (engine-specific dispatch)");
                Expect(!post, "HookResult.Stop from a pre hook did not prevent the post hook");
            }
            finally { ge.Unhook(preGuid); ge.Unhook(postGuid); }
        });

        t.Test("Unhook stops further callbacks", () =>
        {
            var count = 0;
            var guid = ge.HookPost<EventRoundFreezeEnd>(_ => { count++; return HookResult.Continue; });
            ge.FireToServer<EventRoundFreezeEnd>();
            if (count == 0) { ge.Unhook(guid); Skip("hook did not observe FireToServer (engine-specific dispatch)"); }
            ge.Unhook(guid);
            ge.FireToServer<EventRoundFreezeEnd>();
            Equal(1, count, "count after Unhook");
        });

        t.Test("UnhookPre<T> / UnhookPost<T> remove every hook of that type", () =>
        {
            var pre1 = false;
            var pre2 = false;
            var post1 = false;
            _ = ge.HookPre<EventRoundFreezeEnd>(_ => { pre1 = true; return HookResult.Continue; });
            _ = ge.HookPre<EventRoundFreezeEnd>(_ => { pre2 = true; return HookResult.Continue; });
            _ = ge.HookPost<EventRoundFreezeEnd>(_ => { post1 = true; return HookResult.Continue; });
            ge.UnhookPre<EventRoundFreezeEnd>();
            ge.FireToServer<EventRoundFreezeEnd>();
            Expect(!pre1 && !pre2, "a pre hook still fired after UnhookPre<T>");
            post1 = false;
            ge.UnhookPost<EventRoundFreezeEnd>();
            ge.FireToServer<EventRoundFreezeEnd>();
            Expect(!post1, "post hook still fired after UnhookPost<T>");
        });

        t.Test("Fire with configureEvent delivers the configured fields to a pre hook", () =>
        {
            EventRoundStart? seen = null;
            var guid = ge.HookPre<EventRoundStart>(e => { seen = e; return HookResult.Continue; });
            try
            {
                ge.Fire<EventRoundStart>(e => { e.TimeLimit = 123; e.FragLimit = 45; e.Objective = "tester_objective"; });
                if (seen is null) Skip("hook did not observe Fire (engine-specific dispatch)");
                Equal(123, seen!.TimeLimit, "TimeLimit");
                Equal(45, seen!.FragLimit, "FragLimit");
                Equal("tester_objective", seen!.Objective, "Objective");
            }
            finally { ge.Unhook(guid); }
        });

        t.Test("Fire / FireToPlayer / FireToServer variants (with and without configureEvent) never throw", () =>
        {
            ge.Fire<EventRoundFreezeEnd>();
            ge.Fire<EventRoundStart>(e => { e.TimeLimit = 1; e.FragLimit = 1; e.Objective = "tester"; });
            ge.FireToPlayer<EventRoundFreezeEnd>(t.Caller.Slot);
            ge.FireToPlayer<EventRoundStart>(t.Caller.Slot, e => { e.TimeLimit = 1; e.FragLimit = 1; e.Objective = "tester"; });
            ge.FireToServer<EventRoundFreezeEnd>();
            ge.FireToServer<EventRoundStart>(e => { e.TimeLimit = 1; e.FragLimit = 1; e.Objective = "tester"; });
        });

        await t.TestAsync("Async Fire / FireToPlayer / FireToServer variants complete", async () =>
        {
            await ge.FireAsync<EventRoundFreezeEnd>();
            await ge.FireAsync<EventRoundStart>(e => { e.TimeLimit = 1; e.FragLimit = 1; e.Objective = "tester"; });
            await ge.FireToPlayerAsync<EventRoundFreezeEnd>(t.Caller.Slot);
            await ge.FireToPlayerAsync<EventRoundStart>(t.Caller.Slot, e => { e.TimeLimit = 1; e.FragLimit = 1; e.Objective = "tester"; });
            await ge.FireToServerAsync<EventRoundFreezeEnd>();
            await ge.FireToServerAsync<EventRoundStart>(e => { e.TimeLimit = 1; e.FragLimit = 1; e.Objective = "tester"; });
        });

        t.Test("IsListeningToEvent<T> agrees with the string overload and never throws for a bogus slot", () =>
        {
            var slot = t.Caller.Slot;
            Equal(ge.IsListeningToEvent(slot, "round_start"), ge.IsListeningToEvent<EventRoundStart>(slot), "typed vs string overload");
            Expect(!ge.IsListeningToEvent(9999, "round_start"), "bogus slot listening (string overload)");
            Expect(!ge.IsListeningToEvent<EventRoundStart>(9999), "bogus slot listening (typed overload)");
        });
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var ge = Ge;

        p.ProfileOnGame("GameEvent.Fire<T>()", ge.Fire<EventRoundFreezeEnd>, 2_000);
        p.ProfileOnGame("GameEvent.Fire<T>(configureEvent)", () => ge.Fire<EventRoundStart>(e => { e.TimeLimit = 1; e.FragLimit = 1; e.Objective = "tester"; }), 2_000);
        p.ProfileOnGame("GameEventService.IsListeningToEvent<T>", () => _ = ge.IsListeningToEvent<EventRoundStart>(p.Caller.Slot), 50_000);
        return Task.CompletedTask;
    }
}

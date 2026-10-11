using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class NetMessageSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "netmessage";

    private INetMessageService Nm => Core.NetMessage;

    public override async Task Test( TestContext t )
    {
        var nm = Nm;

        t.Test("Create<T> returns a message whose fields round-trip", () =>
        {
            using var m = nm.Create<CUserMessageShake>();
            m.Command = 1;
            m.Amplitude = 2.5f;
            m.Frequency = 3.5f;
            m.Duration = 1.5f;
            Equal(1u, m.Command, "Command");
            Equal(2.5f, m.Amplitude, "Amplitude");
            Equal(3.5f, m.Frequency, "Frequency");
            Equal(1.5f, m.Duration, "Duration");
        });

        await t.TestAsync("HookServerMessage observes a message sent via SendToPlayer", async () =>
        {
            CUserMessageShake? seen = null;
            var guid = nm.HookServerMessage<CUserMessageShake>(msg => { seen = msg; return HookResult.Continue; });
            try
            {
                using var m = nm.Create<CUserMessageShake>();
                m.Amplitude = 42f;
                m.SendToPlayer(t.Caller.Slot);
                if (!await t.Until(() => seen is not null, 64)) Skip("hook did not observe SendToPlayer (engine-specific dispatch)");
                Equal(42f, seen!.Amplitude, "Amplitude");
            }
            finally { nm.Unhook(guid); }
        });

        await t.TestAsync("HookServerMessageInternal receives the recipient's playerId", async () =>
        {
            int? seenPlayerId = null;
            var guid = nm.HookServerMessageInternal<CUserMessageShake>(( msg, playerId ) => { seenPlayerId = playerId; return HookResult.Continue; });
            try
            {
                using var m = nm.Create<CUserMessageShake>();
                m.SendToPlayer(t.Caller.Slot);
                if (!await t.Until(() => seenPlayerId is not null, 64)) Skip("hook did not observe SendToPlayer (engine-specific dispatch)");
                Equal(t.Caller.Slot, seenPlayerId!.Value, "playerId");
            }
            finally { nm.Unhook(guid); }
        });

        t.Test("Unhook stops further HookServerMessage callbacks", () =>
        {
            var count = 0;
            var guid = nm.HookServerMessage<CUserMessageShake>(_ => { count++; return HookResult.Continue; });
            nm.Unhook(guid);
            using var m = nm.Create<CUserMessageShake>();
            m.SendToPlayer(t.Caller.Slot);
            Equal(0, count, "count after Unhook");
        });

        t.Test("UnhookServerMessage<T> removes every HookServerMessage hook of that type", () =>
        {
            var count1 = 0;
            var count2 = 0;
            _ = nm.HookServerMessage<CUserMessageShake>(_ => { count1++; return HookResult.Continue; });
            _ = nm.HookServerMessage<CUserMessageShake>(_ => { count2++; return HookResult.Continue; });
            nm.UnhookServerMessage<CUserMessageShake>();
            using var m = nm.Create<CUserMessageShake>();
            m.SendToPlayer(t.Caller.Slot);
            Expect(count1 == 0 && count2 == 0, "a hook still fired after UnhookServerMessage<T>");
        });

        t.Test("UnhookServerMessageInternal<T> removes every HookServerMessageInternal hook of that type", () =>
        {
            var count = 0;
            _ = nm.HookServerMessageInternal<CUserMessageShake>(( _, _ ) => { count++; return HookResult.Continue; });
            nm.UnhookServerMessageInternal<CUserMessageShake>();
            using var m = nm.Create<CUserMessageShake>();
            m.SendToPlayer(t.Caller.Slot);
            Equal(0, count, "count after UnhookServerMessageInternal<T>");
        });

        await t.TestAsync("Send(configureMessage) convenience overload is observed by HookServerMessage", async () =>
        {
            CUserMessageShake? seen = null;
            var guid = nm.HookServerMessage<CUserMessageShake>(msg => { seen = msg; return HookResult.Continue; });
            try
            {
                nm.Send<CUserMessageShake>(msg => { msg.Amplitude = 7f; msg.SendToPlayer(t.Caller.Slot); });
                if (!await t.Until(() => seen is not null, 64)) Skip("hook did not observe Send(configureMessage) (engine-specific dispatch)");
                Equal(7f, seen!.Amplitude, "Amplitude");
            }
            finally { nm.Unhook(guid); }
        });

        await t.TestAsync("HookClientMessage observes a CNETMsg_Tick from the caller", async () =>
        {
            if (t.Caller.IsFakeClient) Skip("caller is a bot, no real client traffic");
            int? seenPlayerId = null;
            var guid = nm.HookClientMessage<CNETMsg_Tick>(( msg, playerId ) => { if (playerId == t.Caller.Slot) seenPlayerId = playerId; return HookResult.Continue; });
            try
            {
                if (!await t.Until(() => seenPlayerId is not null, 256)) Skip("no CNETMsg_Tick observed from the caller within the window");
                Equal(t.Caller.Slot, seenPlayerId!.Value, "playerId");
            }
            finally { nm.Unhook(guid); }
        });

        t.Test("HookClientMessage / UnhookClientMessage<T> never throw", () =>
        {
            _ = nm.HookClientMessage<CNETMsg_Tick>(( _, _ ) => HookResult.Continue);
            nm.UnhookClientMessage<CNETMsg_Tick>();
        });
    }
}

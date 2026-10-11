using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Misc;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class RegistratorSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "registrator";

    private const string Command = "tester_registrator_probe";
    private const string Alias = "tester_registrator_alias";
    private const string RawCommand = "tester_registrator_raw";
    private const string StaticCommand = "tester_registrator_static";

    public sealed class ValidProbe
    {
        public int Ticks;
        public ICommandContext? LastContext;
        public int CommandRuns;
        public int ClientCommandHooks;
        public int ClientChatHooks;

        [Command(Command, helpText: "Tester registrator probe")]
        [CommandAlias(Alias)]
        public void OnCommand( ICommandContext context )
        {
            LastContext = context;
            _ = Interlocked.Increment(ref CommandRuns);
        }

        [Command(RawCommand, registerRaw: true)]
        private void OnRawCommand( ICommandContext context ) => LastContext = context;

        [Command(StaticCommand)]
        public static void OnStaticCommand( ICommandContext context ) { }

        [EventListener<EventDelegates.OnTick>]
        private void OnTick() => Interlocked.Increment(ref Ticks);

        [ClientCommandHookHandler]
        public HookResult OnClientCommand( int playerId, string commandLine )
        {
            _ = Interlocked.Increment(ref ClientCommandHooks);
            return HookResult.Continue;
        }

        [ClientChatHookHandler]
        public HookResult OnClientChat( int playerId, string text, bool teamOnly )
        {
            _ = Interlocked.Increment(ref ClientChatHooks);
            return HookResult.Continue;
        }

        [GameEventHandler(HookMode.Pre)]
        public HookResult OnRoundStartPre( EventRoundStart @event ) => HookResult.Continue;

        [GameEventHandler(HookMode.Post)]
        public HookResult OnRoundStartPost( EventRoundStart @event ) => HookResult.Continue;
    }

    public sealed class BadEventTypeProbe
    {
        [EventListener<Action>]
        public void OnSomething() { }
    }

    public sealed class BadCommandSignatureProbe
    {
        [Command("tester_registrator_badsig")]
        public void OnCommand( int notAContext ) { }
    }

    public sealed class NoAttributeProbe
    {
        public void A() { }
        public int B( int x ) => x;
        private string C() => "c";
    }

    private static readonly object Gate = new();
    private static ValidProbe? valid;

    private ValidProbe EnsureRegistered()
    {
        lock (Gate)
        {
            if (valid is not null) return valid;
            var probe = new ValidProbe();
            Core.Registrator.Register(probe);
            valid = probe;
            return probe;
        }
    }

    private bool IsRegistered( string name ) => Core.Command.IsCommandRegistered(name) || Core.Command.IsCommandRegistered("sw_" + name);

    public override async Task Test( TestContext t )
    {
        var reg = Core.Registrator;
        ValidProbe? probe = null;

        t.Test("Register accepts an object with every supported attribute", () => probe = EnsureRegistered());

        t.Test("Register(null) throws ArgumentNullException", () => Throws<ArgumentNullException>(() => reg.Register(null!)));

        t.Test("Register of an object without attributes does nothing", () =>
        {
            var before = Core.Command.GetAllCommands().Count;
            reg.Register(new NoAttributeProbe());
            reg.Register(new object());
            Equal(before, Core.Command.GetAllCommands().Count, "command count");
        });

        t.Test("[Command] on a public method registers the command with the sw_ prefix", () =>
        {
            _ = EnsureRegistered();
            Expect(Core.Command.IsCommandRegistered("sw_" + Command), $"sw_{Command} not registered");
        });

        t.Test("[Command] on a private method registers too", () =>
        {
            _ = EnsureRegistered();
            Expect(IsRegistered(RawCommand), $"{RawCommand} not registered");
        });

        t.Test("[Command(registerRaw: true)] registers without the sw_ prefix", () =>
        {
            _ = EnsureRegistered();
            Expect(Core.Command.IsCommandRegistered(RawCommand), $"{RawCommand} not registered");
            Expect(!Core.Command.IsCommandRegistered("sw_" + RawCommand), $"sw_{RawCommand} registered as well");
        });

        t.Test("[CommandAlias] registers the alias", () =>
        {
            _ = EnsureRegistered();
            Expect(IsRegistered(Alias), $"{Alias} not registered");
        });

        t.Test("Static methods are ignored", () =>
        {
            _ = EnsureRegistered();
            Expect(!IsRegistered(StaticCommand), "static [Command] method was registered");
        });

        t.Test("Registered command help text and plugin are reported", () =>
        {
            _ = EnsureRegistered();
            var info = Core.Command.GetAllCommandsInfo().FirstOrDefault(c => c.CommandName == Command);
            NotNull(info, "command info");
            Equal("Tester registrator probe", info!.HelpText, "HelpText");
        });

        await t.TestAsync("Registered command runs when executed from the server console", async () =>
        {
            var p = EnsureRegistered();
            var before = p.CommandRuns;
            Core.Engine.ExecuteCommand($"sw_{Command} first second");
            Expect(await t.Until(() => p.CommandRuns > before), "handler never ran");
            var ctx = p.LastContext;
            NotNull(ctx, "context");
            Expect(!ctx!.IsSentByPlayer && ctx.Sender is null, "console command reported a player sender");
            Expect(ctx.Args.SequenceEqual(["first", "second"]), $"args: [{string.Join(", ", ctx.Args)}]");
        });

        await t.TestAsync("Alias runs the same handler", async () =>
        {
            var p = EnsureRegistered();
            var before = p.CommandRuns;
            Core.Engine.ExecuteCommand($"sw_{Alias} viaalias");
            Expect(await t.Until(() => p.CommandRuns > before), "handler never ran through the alias");
            Expect(p.LastContext!.Args.SequenceEqual(["viaalias"]), $"args: [{string.Join(", ", p.LastContext.Args)}]");
        });

        await t.TestAsync("[EventListener<OnTick>] is called every tick (also on a private method)", async () =>
        {
            var p = EnsureRegistered();
            var before = p.Ticks;
            Expect(await t.Until(() => p.Ticks >= before + 5, 64), $"only {p.Ticks - before} ticks seen");
        });

        t.Test("Handlers for client commands, chat and game events register without error", () =>
        {
            _ = EnsureRegistered();
            Expect(valid is not null, "probe missing");
        });

        t.Test("[EventListener] with a delegate that is not an EventDelegates type is rejected", () =>
            Throws<InvalidOperationException>(() => reg.Register(new BadEventTypeProbe())));

        t.Test("[Command] on a method with the wrong signature is rejected and registers nothing", () =>
        {
            Throws<ArgumentException>(() => reg.Register(new BadCommandSignatureProbe()));
            Expect(!IsRegistered("tester_registrator_badsig"), "bad command was registered");
        });

        t.Test("A failed registration does not break later ones", () =>
        {
            try { reg.Register(new BadEventTypeProbe()); } catch (InvalidOperationException) { }
            reg.Register(new NoAttributeProbe());
            Expect(IsRegistered(Command), "earlier registration vanished");
        });

        t.Test("Concurrent Register of attribute-free objects from pool threads", () =>
        {
            var failures = new System.Collections.Concurrent.ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 4).Select(w => Task.Run(() =>
            {
                try { for (var i = 0; i < 50; i++) reg.Register(new NoAttributeProbe()); }
                catch (Exception e) { failures.Add($"worker {w}: {e.Message}"); }
            })).ToArray());
            Expect(failures.IsEmpty, string.Join("; ", failures));
        });
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var reg = Core.Registrator;
        var plain = new NoAttributeProbe();

        p.Profile("Registrator.Register (empty object)", () => reg.Register(new object()), 5_000);
        p.Profile("Registrator.Register (3 methods, no attributes)", () => reg.Register(plain), 5_000);
        p.Profile("Registrator.Register (the probe's type scan, already registered)", () => _ = typeof(ValidProbe).GetMethods(), 20_000);
        return Task.CompletedTask;
    }
}

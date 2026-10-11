using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Misc;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class CommandSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "command";

    private ICommandService Cmd => Core.Command;

    private static string UniqueName() => "tester_cmd_" + Guid.NewGuid().ToString("N")[..10];

    private sealed class Recorder
    {
        private readonly object gate = new();
        public int Runs;
        public ICommandContext? Last;
        public List<ICommandContext> All { get; } = [];

        public void Handle( ICommandContext context )
        {
            lock (gate) { All.Add(context); }
            Last = context;
            Interlocked.Increment(ref Runs);
        }
    }

    private async Task WithCommand( TestContext t, Func<string, Recorder, Guid, Task> body, bool raw = false, string permission = "", string help = "Tester probe" )
    {
        var name = UniqueName();
        var rec = new Recorder();
        var guid = Cmd.RegisterCommand(name, rec.Handle, raw, permission, help);
        try { await body(name, rec, guid); }
        finally { Cmd.UnregisterCommand(guid); }
    }

    private static string Console( string name, bool raw = false ) => raw ? name : "sw_" + name;

    private async Task<bool> Run( TestContext t, Recorder rec, string commandLine, int expectedRuns = 1 )
    {
        var before = rec.Runs;
        Core.Engine.ExecuteCommand(commandLine);
        return await t.Until(() => rec.Runs >= before + expectedRuns, 64);
    }

    public override async Task Test( TestContext t )
    {
        var cmd = Cmd;

        await t.TestAsync("RegisterCommand returns a unique, non-empty guid", () => WithCommand(t, ( name, rec, guid ) =>
        {
            Expect(guid != Guid.Empty, "empty guid");
            var other = cmd.RegisterCommand(UniqueName(), rec.Handle);
            try { Expect(other != Guid.Empty && other != guid, "guids not unique"); }
            finally { cmd.UnregisterCommand(other); }
            return Task.CompletedTask;
        }));

        await t.TestAsync("A registered command is known with the sw_ prefix", () => WithCommand(t, ( name, rec, guid ) =>
        {
            Expect(cmd.IsCommandRegistered("sw_" + name), $"sw_{name} not registered");
            return Task.CompletedTask;
        }));

        await t.TestAsync("registerRaw registers without the sw_ prefix", () => WithCommand(t, ( name, rec, guid ) =>
        {
            Expect(cmd.IsCommandRegistered(name), $"{name} not registered");
            Expect(!cmd.IsCommandRegistered("sw_" + name), $"sw_{name} registered too");
            return Task.CompletedTask;
        }, raw: true));

        t.Test("An unknown command is not registered", () =>
        {
            Expect(!cmd.IsCommandRegistered("tester_cmd_no_such_command"), "bogus command registered");
            Expect(!cmd.IsCommandRegistered(""), "empty name registered");
        });

        await t.TestAsync("Unregistering by guid removes the command", async () =>
        {
            var name = UniqueName();
            var guid = cmd.RegisterCommand(name, new Recorder().Handle);
            Expect(cmd.IsCommandRegistered("sw_" + name), "not registered");
            cmd.UnregisterCommand(guid);
            await t.Ticks(2);
            Expect(!cmd.IsCommandRegistered("sw_" + name), "still registered");
        });

        await t.TestAsync("Unregistering by name removes every handler with that name", async () =>
        {
            var name = UniqueName();
            var rec = new Recorder();
            _ = cmd.RegisterCommand(name, rec.Handle);
            _ = cmd.RegisterCommand(name, rec.Handle);
            cmd.UnregisterCommand(name);
            await t.Ticks(2);
            Expect(!cmd.IsCommandRegistered("sw_" + name), "still registered");
            Core.Engine.ExecuteCommand("sw_" + name);
            await t.Ticks(4);
            Equal(0, rec.Runs, "handler ran after unregistering");
        });

        t.Test("Unregistering an unknown guid or name does not throw", () =>
        {
            cmd.UnregisterCommand(Guid.NewGuid());
            cmd.UnregisterCommand("tester_cmd_never_registered");
        });

        await t.TestAsync("A command runs from the console with its arguments", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            Expect(await Run(t, rec, $"sw_{name} alpha beta gamma"), "handler never ran");
            var ctx = rec.Last!;
            Expect(!ctx.IsSentByPlayer && ctx.Sender is null, "console command reported a player");
            Expect(ctx.Args.SequenceEqual(["alpha", "beta", "gamma"]), $"args: [{string.Join(", ", ctx.Args)}]");
        }));

        await t.TestAsync("A command without arguments gets an empty Args array", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            Expect(await Run(t, rec, "sw_" + name), "handler never ran");
            Equal(0, rec.Last!.Args.Length, "Args.Length");
        }));

        await t.TestAsync("Quoted arguments stay together", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            Expect(await Run(t, rec, $"sw_{name} \"two words\" single"), "handler never ran");
            Expect(rec.Last!.Args.SequenceEqual(["two words", "single"]), $"args: [{string.Join(" | ", rec.Last.Args)}]");
        }));

        await t.TestAsync("The context reports the command that was typed", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            Expect(await Run(t, rec, "sw_" + name), "handler never ran");
            Expect(rec.Last!.CommandName.Contains(name), $"CommandName '{rec.Last.CommandName}'");
            Equal("sw_", rec.Last.Prefix, "Prefix");
        }));

        await t.TestAsync("A raw command runs without the prefix", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            Expect(await Run(t, rec, name + " x"), "handler never ran");
            Expect(rec.Last!.Args.SequenceEqual(["x"]), $"args: [{string.Join(", ", rec.Last.Args)}]");
        }, raw: true));

        await t.TestAsync("Command names are case-insensitive", () => WithCommand(t, async ( name, rec, guid ) =>
            Expect(await Run(t, rec, "SW_" + name.ToUpperInvariant()), "upper-case command did not run")));

        await t.TestAsync("Two registrations of one name both run", async () =>
        {
            var name = UniqueName();
            var a = new Recorder();
            var b = new Recorder();
            var ga = cmd.RegisterCommand(name, a.Handle);
            var gb = cmd.RegisterCommand(name, b.Handle);
            try
            {
                Core.Engine.ExecuteCommand("sw_" + name);
                Expect(await t.Until(() => a.Runs >= 1 && b.Runs >= 1, 64), $"runs: a={a.Runs}, b={b.Runs}");
            }
            finally { cmd.UnregisterCommand(ga); cmd.UnregisterCommand(gb); }
        });

        await t.TestAsync("Unregistering one of two handlers leaves the other", async () =>
        {
            var name = UniqueName();
            var a = new Recorder();
            var b = new Recorder();
            var ga = cmd.RegisterCommand(name, a.Handle);
            var gb = cmd.RegisterCommand(name, b.Handle);
            try
            {
                cmd.UnregisterCommand(ga);
                Core.Engine.ExecuteCommand("sw_" + name);
                Expect(await t.Until(() => b.Runs >= 1, 64), "remaining handler never ran");
                Equal(0, a.Runs, "removed handler ran");
            }
            finally { cmd.UnregisterCommand(gb); }
        });

        await t.TestAsync("A handler that throws does not stop later commands", async () =>
        {
            var name = UniqueName();
            var good = new Recorder();
            var bad = cmd.RegisterCommand(name + "_bad", _ => throw new InvalidOperationException("tester: expected exception from a command handler"));
            var ok = cmd.RegisterCommand(name + "_ok", good.Handle);
            try
            {
                Core.Engine.ExecuteCommand($"sw_{name}_bad");
                await t.Ticks(2);
                Expect(await Run(t, good, $"sw_{name}_ok"), "command after the throwing handler never ran");
            }
            finally { cmd.UnregisterCommand(bad); cmd.UnregisterCommand(ok); }
        });

        await t.TestAsync("Reply from a console command does not throw", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            var replied = false;
            var g2 = cmd.RegisterCommand(name + "_r", c => { c.Reply("[Tester] Reply from a console command"); _ = c.ReplyAsync("[Tester] ReplyAsync from a console command"); replied = true; });
            try
            {
                Core.Engine.ExecuteCommand($"sw_{name}_r");
                Expect(await t.Until(() => replied, 64), "handler never ran");
            }
            finally { cmd.UnregisterCommand(g2); }
        }));

        await t.TestAsync("A command run by a player carries that player as Sender", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            t.Caller.ExecuteCommand($"sw_{name} from_player");
            if (!await t.Until(() => rec.Runs >= 1, 64)) Skip("Player.ExecuteCommand does not reach the command dispatcher");
            var ctx = rec.Last!;
            Expect(ctx.IsSentByPlayer && ctx.Sender is not null, "Sender missing");
            Expect(ReferenceEquals(ctx.Sender, t.Caller), "Sender is not the caller");
            Expect(ctx.Args.SequenceEqual(["from_player"]), $"args: [{string.Join(", ", ctx.Args)}]");
        }));

        await t.TestAsync("RegisterCommandAlias makes the alias run the same handler", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            var alias = UniqueName();
            cmd.RegisterCommandAlias(name, alias);
            Expect(cmd.IsCommandRegistered("sw_" + alias), $"sw_{alias} not registered");
            Expect(await Run(t, rec, $"sw_{alias} viaalias"), "handler never ran through the alias");
            Expect(rec.Last!.Args.SequenceEqual(["viaalias"]), $"args: [{string.Join(", ", rec.Last.Args)}]");
        }));

        await t.TestAsync("RegisterCommandAlias(registerRaw) registers the alias without a prefix", () => WithCommand(t, async ( name, rec, guid ) =>
        {
            var alias = UniqueName();
            cmd.RegisterCommandAlias(name, alias, registerRaw: true);
            Expect(cmd.IsCommandRegistered(alias), $"{alias} not registered");
            Expect(await Run(t, rec, alias), "handler never ran through the raw alias");
        }));

        await t.TestAsync("GetAllCommandsInfo reports name, raw flag, permission and help text", () => WithCommand(t, ( name, rec, guid ) =>
        {
            var info = cmd.GetAllCommandsInfo().FirstOrDefault(c => c.CommandName == name);
            NotNull(info, "command info");
            Equal(false, info!.RegisterRaw, "RegisterRaw");
            Equal("tester.cmd.permission", info.Permission, "Permission");
            Equal("Tester help text", info.HelpText, "HelpText");
            return Task.CompletedTask;
        }, permission: "tester.cmd.permission", help: "Tester help text"));

        await t.TestAsync("GetAllCommandsInfo reports registerRaw", () => WithCommand(t, ( name, rec, guid ) =>
        {
            Equal(true, cmd.GetAllCommandsInfo().First(c => c.CommandName == name).RegisterRaw, "RegisterRaw");
            return Task.CompletedTask;
        }, raw: true));

        await t.TestAsync("GetAllCommands lists the command", () => WithCommand(t, ( name, rec, guid ) =>
        {
            Expect(cmd.GetAllCommands().Contains(name, StringComparer.OrdinalIgnoreCase), "not listed");
            Equal(cmd.GetAllCommands().Count, cmd.GetAllCommands().Distinct(StringComparer.OrdinalIgnoreCase).Count(), "duplicates in GetAllCommands");
            return Task.CompletedTask;
        }));

        await t.TestAsync("GetAllCommandsByPlugin groups the command under this plugin, and GetCommandsByPlugin agrees", () => WithCommand(t, ( name, rec, guid ) =>
        {
            var groups = cmd.GetAllCommandsByPlugin();
            var plugin = groups.FirstOrDefault(g => g.Value.Any(c => c.CommandName == name)).Key;
            NotNull(plugin, "plugin name");
            Expect(cmd.GetCommandsByPlugin(plugin!).Any(c => c.CommandName == name), "GetCommandsByPlugin misses the command");
            Expect(cmd.GetCommandsByPlugin("tester_no_such_plugin").Count == 0, "unknown plugin has commands");
            return Task.CompletedTask;
        }));

        await t.TestAsync("Commands disappear from the lists after unregistering", async () =>
        {
            var name = UniqueName();
            var guid = cmd.RegisterCommand(name, new Recorder().Handle);
            cmd.UnregisterCommand(guid);
            await t.Ticks(2);
            Expect(!cmd.GetAllCommands().Contains(name, StringComparer.OrdinalIgnoreCase), "still in GetAllCommands");
            Expect(!cmd.GetAllCommandsInfo().Any(c => c.CommandName == name), "still in GetAllCommandsInfo");
        });

        t.Test("HookClientCommand / UnhookClientCommand return and accept a guid", () =>
        {
            var g = cmd.HookClientCommand(( id, line ) => HookResult.Continue);
            Expect(g != Guid.Empty, "empty guid");
            cmd.UnhookClientCommand(g);
            cmd.UnhookClientCommand(g);
            cmd.UnhookClientCommand(Guid.NewGuid());
        });

        t.Test("HookClientChat / UnhookClientChat return and accept a guid", () =>
        {
            var g = cmd.HookClientChat(( id, text, team ) => HookResult.Continue);
            Expect(g != Guid.Empty, "empty guid");
            cmd.UnhookClientChat(g);
            cmd.UnhookClientChat(g);
            cmd.UnhookClientChat(Guid.NewGuid());
        });

        await t.TestAsync("A client command hook sees commands sent by the caller", async () =>
        {
            var seen = new List<(int Id, string Line)>();
            var marker = "tester_cc_" + Guid.NewGuid().ToString("N")[..8];
            var g = cmd.HookClientCommand(( id, line ) => { if (line.Contains(marker)) lock (seen) seen.Add((id, line)); return HookResult.Continue; });
            try
            {
                t.Caller.ExecuteCommand(marker + " arg");
                if (!await t.Until(() => { lock (seen) return seen.Count > 0; }, 64)) Skip("the hook did not fire for Player.ExecuteCommand (needs real client input)");
                Equal(t.Caller.Slot, seen[0].Id, "player id");
                Expect(seen[0].Line.Contains("arg"), $"line '{seen[0].Line}'");
            }
            finally { cmd.UnhookClientCommand(g); }
        });

        await t.TestAsync("An unhooked client command hook stops firing", async () =>
        {
            var count = 0;
            var marker = "tester_cc_" + Guid.NewGuid().ToString("N")[..8];
            var g = cmd.HookClientCommand(( id, line ) => { if (line.Contains(marker)) Interlocked.Increment(ref count); return HookResult.Continue; });
            cmd.UnhookClientCommand(g);
            t.Caller.ExecuteCommand(marker);
            await t.Ticks(8);
            Equal(0, count, "calls after unhook");
        });

        t.Test("Client chat hooks cannot be driven from the server", () => Skip("needs a real chat message from a client"));

        t.Test("Concurrent IsCommandRegistered / GetAllCommands from pool threads", () =>
        {
            var failures = new System.Collections.Concurrent.ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 4).Select(w => Task.Run(() =>
            {
                try { for (var i = 0; i < 200; i++) { _ = cmd.IsCommandRegistered("sw_tester"); _ = cmd.GetAllCommands(); } }
                catch (Exception e) { failures.Add($"worker {w}: {e.Message}"); }
            })).ToArray());
            Expect(failures.IsEmpty, string.Join("; ", failures.Take(5)));
        });

        t.Test("Concurrent register / unregister from pool threads", () =>
        {
            var failures = new System.Collections.Concurrent.ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 4).Select(w => Task.Run(() =>
            {
                try
                {
                    for (var i = 0; i < 25; i++)
                    {
                        var g = cmd.RegisterCommand(UniqueName(), new Recorder().Handle);
                        cmd.UnregisterCommand(g);
                    }
                }
                catch (Exception e) { failures.Add($"worker {w}: {e.Message}"); }
            })).ToArray());
            Expect(failures.IsEmpty, string.Join("; ", failures.Take(5)));
        });
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var cmd = Cmd;
        var rec = new Recorder();
        var known = UniqueName();
        var guid = cmd.RegisterCommand(known, rec.Handle);
        try
        {
            p.ProfileOnGame("Command.IsCommandRegistered (known)", () => _ = cmd.IsCommandRegistered("sw_" + known), 100_000);
            p.ProfileOnGame("Command.IsCommandRegistered (unknown)", () => _ = cmd.IsCommandRegistered("sw_tester_cmd_unknown"), 100_000);
            p.ProfileOnGame("Command.GetAllCommands", () => _ = cmd.GetAllCommands(), 20_000);
            p.ProfileOnGame("Command.GetAllCommandsInfo", () => _ = cmd.GetAllCommandsInfo(), 20_000);
            p.ProfileOnGame("Command.GetAllCommandsByPlugin", () => _ = cmd.GetAllCommandsByPlugin(), 10_000);
            p.ProfileOnGame("Command.RegisterCommand + UnregisterCommand(guid)", () => cmd.UnregisterCommand(cmd.RegisterCommand("tester_cmd_profile", rec.Handle)), 5_000);
            p.ProfileOnGame("Command.HookClientCommand + Unhook", () => cmd.UnhookClientCommand(cmd.HookClientCommand(( id, line ) => HookResult.Continue)), 10_000);
            p.ProfileOnGame("Command.HookClientChat + Unhook", () => cmd.UnhookClientChat(cmd.HookClientChat(( id, text, team ) => HookResult.Continue)), 10_000);
            p.ProfileOnGame("Engine.ExecuteCommand (queues a no-op command)", () => Core.Engine.ExecuteCommand("sw_" + known), 2_000);
        }
        finally { cmd.UnregisterCommand(guid); }
        return Task.CompletedTask;
    }
}

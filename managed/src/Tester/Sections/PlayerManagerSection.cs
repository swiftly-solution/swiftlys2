using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Players;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class PlayerManagerSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "playermanager";

    private IPlayerManagerService Pm => Core.PlayerManager;

    private static string Ids( IEnumerable<IPlayer> players ) => string.Join(",", players.Select(p => p.Slot).Order());

    private bool CallerIsAlone( TestContext t ) => Pm.GetAllValidPlayers().All(p => p.IsFakeClient || p.Slot == t.Caller.Slot);

    private void NeedAlone( TestContext t )
    {
        if (!CallerIsAlone(t)) Skip("other human players are connected; they would see the message");
    }

    private static void NeedPawn( IPlayer p )
    {
        if (p.Pawn is not { IsValid: true }) Skip("caller has no valid pawn (spectating / not spawned)");
    }

    public override async Task Test( TestContext t )
    {
        var pm = Pm;
        var me = t.Caller;

        t.Test("PlayerCount, PlayerCap and MaxPlayers are sane", () =>
        {
            Expect(pm.PlayerCount >= 1, $"PlayerCount={pm.PlayerCount}");
            Expect(pm.MaxPlayers >= 1 && pm.MaxPlayers <= 64, $"MaxPlayers={pm.MaxPlayers}");
            Expect(pm.PlayerCap >= 1, $"PlayerCap={pm.PlayerCap}");
            Expect(pm.PlayerCount <= pm.MaxPlayers, $"PlayerCount {pm.PlayerCount} > MaxPlayers {pm.MaxPlayers}");
        });

        t.Test("IsPlayerOnline: caller yes, invalid slots no", () =>
        {
            Expect(pm.IsPlayerOnline(me.Slot), "caller offline");
            foreach (var bad in new[] { -1, -100, 64, 9999, int.MinValue, int.MaxValue })
                Expect(!pm.IsPlayerOnline(bad), $"slot {bad} online");
        });

        t.Test("GetPlayer(slot) returns the caller, invalid slots return null", () =>
        {
            Expect(ReferenceEquals(me, pm.GetPlayer(me.Slot)), "caller instance differs");
            foreach (var bad in new[] { -1, 64, 9999, int.MinValue, int.MaxValue })
                Expect(pm.GetPlayer(bad) is null, $"slot {bad} resolved");
        });

        t.Test("GetPlayer agrees with IsPlayerOnline for every slot", () =>
        {
            for (var slot = 0; slot < pm.MaxPlayers; slot++)
                Equal(pm.IsPlayerOnline(slot), pm.GetPlayer(slot) is not null, $"slot {slot}");
        });

        t.Test("GetAllPlayers and GetAllValidPlayers contain the caller", () =>
        {
            Expect(pm.GetAllPlayers().Contains(me), "GetAllPlayers");
            Expect(pm.GetAllValidPlayers().Contains(me), "GetAllValidPlayers");
        });

        t.Test("GetAllValidPlayers is a subset of GetAllPlayers and every entry is valid", () =>
        {
            var all = pm.GetAllPlayers().ToList();
            var valid = pm.GetAllValidPlayers().ToList();
            Expect(valid.All(all.Contains), "valid player missing from GetAllPlayers");
            Expect(valid.All(p => p.IsValid), "invalid player in GetAllValidPlayers");
        });

        t.Test("GetAllPlayers has no duplicates and every slot is online", () =>
        {
            var all = pm.GetAllPlayers().ToList();
            Equal(all.Count, all.Select(p => p.Slot).Distinct().Count(), "distinct slots");
            Expect(all.All(p => pm.IsPlayerOnline(p.Slot)), "listed player not online");
        });

        t.Test("PlayerCount does not exceed the number of known players by more than the bots", () =>
            Expect(pm.PlayerCount >= pm.GetAllValidPlayers().Count(p => !p.IsFakeClient), "fewer than the human players"));

        t.Test("GetBots returns only valid bots", () =>
        {
            var bots = pm.GetBots().ToList();
            Expect(bots.All(b => b.IsFakeClient && b.IsValid), "non-bot in GetBots");
            Expect(!bots.Contains(me), "caller listed as bot");
        });

        t.Test("GetInTeam(team) matches GetCT / GetT / GetSpectators", () =>
        {
            Equal(Ids(pm.GetCT()), Ids(pm.GetInTeam(Team.CT)), "CT");
            Equal(Ids(pm.GetT()), Ids(pm.GetInTeam(Team.T)), "T");
            Equal(Ids(pm.GetSpectators()), Ids(pm.GetInTeam(Team.Spectator)), "Spectators");
        });

        t.Test("Team lists are disjoint and made of valid players", () =>
        {
            var ct = pm.GetCT().Select(p => p.Slot).ToHashSet();
            var tt = pm.GetT().Select(p => p.Slot).ToHashSet();
            var sp = pm.GetSpectators().Select(p => p.Slot).ToHashSet();
            Expect(!ct.Overlaps(tt) && !ct.Overlaps(sp) && !tt.Overlaps(sp), "a player is in two teams");
            var valid = pm.GetAllValidPlayers().Select(p => p.Slot).ToHashSet();
            Expect(ct.IsSubsetOf(valid) && tt.IsSubsetOf(valid) && sp.IsSubsetOf(valid), "team lists contain an invalid player");
        });

        t.Test("Alive lists are subsets of the team lists", () =>
        {
            var alive = pm.GetAlive().Select(p => p.Slot).ToHashSet();
            Expect(pm.GetCTAlive().Select(p => p.Slot).ToHashSet().IsSubsetOf(alive), "GetCTAlive not within GetAlive");
            Expect(pm.GetTAlive().Select(p => p.Slot).ToHashSet().IsSubsetOf(alive), "GetTAlive not within GetAlive");
            Expect(pm.GetCTAlive().Select(p => p.Slot).ToHashSet().IsSubsetOf(pm.GetCT().Select(p => p.Slot).ToHashSet()), "GetCTAlive not within GetCT");
            Expect(pm.GetTAlive().Select(p => p.Slot).ToHashSet().IsSubsetOf(pm.GetT().Select(p => p.Slot).ToHashSet()), "GetTAlive not within GetT");
        });

        t.Test("GetInTeam(None) does not throw", () => _ = pm.GetInTeam(Team.None).ToList());

        t.Test("Caller is listed in the list of their team", () =>
        {
            NeedPawn(me);
            var team = (Team)me.Pawn!.TeamNum;
            Expect(pm.GetInTeam(team).Contains(me), $"caller missing from team {team}");
        });

        t.Test("GetPlayerFromController / GetPlayerFromPawn resolve the caller", () =>
        {
            Expect(me.Controller.IsValid, "controller invalid");
            Expect(ReferenceEquals(me, pm.GetPlayerFromController(me.Controller)), "from controller");
            if (me.PlayerPawn is { IsValid: true } pawn) Expect(ReferenceEquals(me, pm.GetPlayerFromPawn(pawn)), "from pawn");
        });

        t.Test("Session id lookups", () =>
        {
            Expect(pm.IsSessionIdValid(me.SessionId), "caller session invalid");
            Expect(ReferenceEquals(me, pm.GetPlayerFromSessionId(me.SessionId)), "resolve");
            Expect(!pm.IsSessionIdValid(0) && !pm.IsSessionIdValid(ulong.MaxValue), "bogus session valid");
            Expect(pm.GetPlayerFromSessionId(0) is null && pm.GetPlayerFromSessionId(ulong.MaxValue) is null, "bogus session resolved");
        });

        t.Test("Steam id lookups", () =>
        {
            if (me.IsFakeClient) Skip("caller is a bot");
            Expect(ReferenceEquals(me, pm.GetPlayerFromSteamId(me.SteamID)), "default (allow unauthorized)");
            Expect(ReferenceEquals(me, pm.GetPlayerFromSteamId(me.SteamID, allowUnauthorized: true)), "allow unauthorized");
            if (me.IsAuthorized) Expect(ReferenceEquals(me, pm.GetPlayerFromSteamId(me.SteamID, allowUnauthorized: false)), "authorized only");
            Expect(pm.GetPlayerFromSteamId(76561190000000099UL) is null, "unknown steam id resolved");
        });

        t.Test("FindTargettedPlayers(@me)", () =>
        {
            NeedPawn(me);
            var r = pm.FindTargettedPlayers(me, "@me", TargetSearchMode.IncludeSelf).ToList();
            Equal(1, r.Count, "count");
            Expect(r[0].Equals(me), "not the caller");
            Expect(!pm.FindTargettedPlayers(me, "@me", TargetSearchMode.None).Any(), "@me without IncludeSelf returned the caller");
        });

        t.Test("FindTargettedPlayers(@all) with IncludeSelf contains the caller; without it does not", () =>
        {
            NeedPawn(me);
            Expect(pm.FindTargettedPlayers(me, "@all", TargetSearchMode.IncludeSelf).Contains(me), "missing with IncludeSelf");
            Expect(!pm.FindTargettedPlayers(me, "@all", TargetSearchMode.None).Contains(me), "present without IncludeSelf");
        });

        t.Test("FindTargettedPlayers(@bots / @!bots / @human) split by IsFakeClient", () =>
        {
            NeedPawn(me);
            var mode = TargetSearchMode.IncludeSelf;
            Expect(pm.FindTargettedPlayers(me, "@bots", mode).All(p => p.IsFakeClient), "@bots has a human");
            Expect(pm.FindTargettedPlayers(me, "@!bots", mode).All(p => !p.IsFakeClient), "@!bots has a bot");
            Expect(pm.FindTargettedPlayers(me, "@human", mode).All(p => !p.IsFakeClient), "@human has a bot");
            Expect(pm.FindTargettedPlayers(me, "@!human", mode).All(p => p.IsFakeClient), "@!human has a human");
        });

        t.Test("FindTargettedPlayers(@ct / @t / @spec / @alive / @dead) filter by pawn state", () =>
        {
            NeedPawn(me);
            var mode = TargetSearchMode.IncludeSelf;
            Expect(pm.FindTargettedPlayers(me, "@ct", mode).All(p => p.Pawn!.TeamNum == (int)Team.CT), "@ct");
            Expect(pm.FindTargettedPlayers(me, "@t", mode).All(p => p.Pawn!.TeamNum == (int)Team.T), "@t");
            Expect(pm.FindTargettedPlayers(me, "@spec", mode).All(p => p.Pawn!.TeamNum == (int)Team.Spectator), "@spec");
            Expect(pm.FindTargettedPlayers(me, "@alive", mode).All(p => p.IsAlive), "@alive");
            Expect(pm.FindTargettedPlayers(me, "@dead", mode).All(p => !p.IsAlive), "@dead");
        });

        t.Test("FindTargettedPlayers(#slot) and (#userid) find the caller", () =>
        {
            NeedPawn(me);
            Expect(pm.FindTargettedPlayers(me, $"#{me.PlayerID}", TargetSearchMode.IncludeSelf).Contains(me), "#slot");
            Expect(pm.FindTargettedPlayers(me, $"#{me.UserID}", TargetSearchMode.IncludeSelf).Contains(me), "#userid");
            Expect(!pm.FindTargettedPlayers(me, "#9999", TargetSearchMode.IncludeSelf).Any(), "#9999 matched");
        });

        t.Test("FindTargettedPlayers by (partial, any case) name", () =>
        {
            NeedPawn(me);
            var name = me.Controller.PlayerName;
            if (string.IsNullOrWhiteSpace(name) || name.StartsWith('@') || name.StartsWith('#')) Skip($"name '{name}' is not searchable");
            var part = name.Length > 3 ? name[1..^1] : name;
            Expect(pm.FindTargettedPlayers(me, part, TargetSearchMode.IncludeSelf).Contains(me), $"'{part}' did not match '{name}'");
            Expect(pm.FindTargettedPlayers(me, part.ToUpperInvariant(), TargetSearchMode.IncludeSelf).Contains(me), "upper-case");
            Expect(!pm.FindTargettedPlayers(me, part.ToUpperInvariant(), TargetSearchMode.IncludeSelf, StringComparison.Ordinal).Contains(me) || part.ToUpperInvariant() == part, "ordinal comparison ignored case");
        });

        t.Test("FindTargettedPlayers by Steam id string", () =>
        {
            NeedPawn(me);
            if (me.IsFakeClient) Skip("caller is a bot");
            Expect(pm.FindTargettedPlayers(me, me.SteamID.ToString(), TargetSearchMode.IncludeSelf).Contains(me), "steamid64");
        });

        t.Test("FindTargettedPlayers(@aim) never throws", () =>
        {
            NeedPawn(me);
            _ = pm.FindTargettedPlayers(me, "@aim", TargetSearchMode.IncludeSelf).ToList();
        });

        t.Test("FindTargettedPlayers with NoMultipleTargets returns at most one", () =>
        {
            NeedPawn(me);
            Expect(pm.FindTargettedPlayers(me, "@all", TargetSearchMode.IncludeSelf | TargetSearchMode.NoMultipleTargets).Count() <= 1, "more than one");
        });

        t.Test("FindTargettedPlayers with NoBots never returns bots", () =>
        {
            NeedPawn(me);
            Expect(pm.FindTargettedPlayers(me, "@all", TargetSearchMode.IncludeSelf | TargetSearchMode.NoBots).All(p => !p.IsFakeClient), "bot returned");
        });

        t.Test("FindTargettedPlayers with Alive / Dead only returns that state", () =>
        {
            NeedPawn(me);
            Expect(pm.FindTargettedPlayers(me, "@all", TargetSearchMode.IncludeSelf | TargetSearchMode.Alive).All(p => p.IsAlive), "Alive");
            Expect(pm.FindTargettedPlayers(me, "@all", TargetSearchMode.IncludeSelf | TargetSearchMode.Dead).All(p => !p.IsAlive), "Dead");
        });

        t.Test("FindTargettedPlayers with TeamOnly / OppositeTeamOnly compare against the caller's team", () =>
        {
            NeedPawn(me);
            var team = me.Pawn!.TeamNum;
            Expect(pm.FindTargettedPlayers(me, "@all", TargetSearchMode.IncludeSelf | TargetSearchMode.TeamOnly).All(p => p.Pawn!.TeamNum == team), "TeamOnly");
            Expect(pm.FindTargettedPlayers(me, "@all", TargetSearchMode.IncludeSelf | TargetSearchMode.OppositeTeamOnly).All(p => p.Pawn!.TeamNum != team), "OppositeTeamOnly");
        });

        t.Test("FindTargettedPlayers with unknown selector returns nothing", () =>
        {
            NeedPawn(me);
            Expect(!pm.FindTargettedPlayers(me, "@tester_no_such_selector", TargetSearchMode.IncludeSelf).Any(), "matched");
            Expect(!pm.FindTargettedPlayers(me, "zzzz_no_such_player_zzzz", TargetSearchMode.IncludeSelf).Any(), "name matched");
        });

        t.Test("ShouldBlockTransmitEntity set then cleared", () =>
        {
            var index = (int)me.Controller.Index;
            pm.ShouldBlockTransmitEntity(index, true);
            pm.ShouldBlockTransmitEntity(index, false);
        });

        t.Test("ClearAllBlockedTransmitEntities", () => Skip("would wipe transmit blocks owned by other plugins"));

        t.Test("SendConsole to everyone", () => pm.SendConsole("[Tester] PlayerManager.SendConsole"));

        t.Test("SendMessage(Console) variants", () =>
        {
            pm.SendMessage(MessageType.Console, "[Tester] SendMessage(kind, text)");
            pm.SendMessage(MessageType.Console, "[Tester] SendMessage(kind, text, duration)", 1000);
            pm.SendMessage(MessageType.Console, ( p, l ) => $"[Tester] SendMessage(kind, callback) for slot {p.Slot}");
            pm.SendMessage(MessageType.Console, ( p, l ) => $"[Tester] SendMessage(kind, callback, duration) for slot {p.Slot}", 1000);
        });

        t.Test("Message callback is called once per player and gets a localizer", () =>
        {
            var calls = new List<int>();
            pm.SendMessage(MessageType.Console, ( p, l ) => { calls.Add(p.Slot); NotNull(l, "localizer"); return "[Tester] callback probe"; });
            Equal(Ids(pm.GetAllPlayers()), string.Join(",", calls.Order()), "players called");
        });

        t.Test("Visible messages (notify, chat, center, alert, html, EOT) while alone", () =>
        {
            NeedAlone(t);
            pm.SendNotify("[Tester] SendNotify");
            pm.SendChat("[Tester] SendChat");
            pm.SendCenter("[Tester] SendCenter");
            pm.SendAlert("[Tester] SendAlert");
            pm.SendCenterHTML("[Tester] SendCenterHTML", 1000);
            pm.SendChatEOT("[Tester] SendChatEOT");
            pm.SendMessage(MessageType.Chat, "[Tester] SendMessage(Chat)");
        });

        await t.TestAsync("Async message variants while alone", async () =>
        {
            NeedAlone(t);
            await pm.SendConsoleAsync("[Tester] SendConsoleAsync");
            await pm.SendNotifyAsync("[Tester] SendNotifyAsync");
            await pm.SendChatAsync("[Tester] SendChatAsync");
            await pm.SendCenterAsync("[Tester] SendCenterAsync");
            await pm.SendAlertAsync("[Tester] SendAlertAsync");
            await pm.SendCenterHTMLAsync("[Tester] SendCenterHTMLAsync", 1000);
            await pm.SendChatEOTAsync("[Tester] SendChatEOTAsync");
            await pm.SendMessageAsync(MessageType.Chat, "[Tester] SendMessageAsync(kind, text)");
            await pm.SendMessageAsync(MessageType.Chat, "[Tester] SendMessageAsync(kind, text, duration)", 1000);
            await pm.SendMessageAsync(MessageType.Chat, ( p, l ) => "[Tester] SendMessageAsync(callback)");
            await pm.SendMessageAsync(MessageType.Chat, ( p, l ) => "[Tester] SendMessageAsync(callback, duration)", 1000);
        });

        await t.TestAsync("SendConsoleAsync from a pool thread completes", async () =>
        {
            await Task.Run(() => pm.SendConsoleAsync("[Tester] SendConsoleAsync from pool"));
        });

        t.Test("Concurrent lookups from pool threads", () =>
        {
            var failures = new System.Collections.Concurrent.ConcurrentBag<string>();
            var slot = me.Slot;
            Task.WaitAll(Enumerable.Range(0, 8).Select(w => Task.Run(() =>
            {
                try
                {
                    for (var i = 0; i < 500; i++)
                    {
                        if (!pm.IsPlayerOnline(slot)) failures.Add($"worker {w}: caller offline");
                        if (pm.GetPlayer(slot) is null) failures.Add($"worker {w}: caller null");
                        _ = pm.GetAllPlayers().Count();
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
        var pm = Pm;
        var me = p.Caller;
        var slot = me.Slot;
        var session = me.SessionId;
        var steam = p.OnGame(() => me.SteamID);
        var controller = p.OnGame(() => me.Controller);

        p.Profile("PlayerManager.IsPlayerOnline", () => _ = pm.IsPlayerOnline(slot), 200_000);
        p.Profile("PlayerManager.GetPlayer", () => _ = pm.GetPlayer(slot), 200_000);
        p.Profile("PlayerManager.IsSessionIdValid", () => _ = pm.IsSessionIdValid(session), 200_000);
        p.Profile("PlayerManager.GetPlayerFromSessionId", () => _ = pm.GetPlayerFromSessionId(session), 200_000);
        p.Profile("PlayerManager.GetAllPlayers (enumerate)", () => { foreach (var _ in pm.GetAllPlayers()) { } }, 50_000);

        p.ProfileOnGame("PlayerManager.PlayerCount", () => _ = pm.PlayerCount, 100_000);
        p.ProfileOnGame("PlayerManager.MaxPlayers", () => _ = pm.MaxPlayers, 100_000);
        p.ProfileOnGame("PlayerManager.GetAllValidPlayers (enumerate)", () => { foreach (var _ in pm.GetAllValidPlayers()) { } }, 20_000);
        p.ProfileOnGame("PlayerManager.GetPlayerFromSteamId", () => _ = pm.GetPlayerFromSteamId(steam), 50_000);
        p.ProfileOnGame("PlayerManager.GetPlayerFromController", () => _ = pm.GetPlayerFromController(controller), 50_000);
        p.ProfileOnGame("PlayerManager.GetBots (enumerate)", () => { foreach (var _ in pm.GetBots()) { } }, 20_000);
        p.ProfileOnGame("PlayerManager.GetAlive (enumerate)", () => { foreach (var _ in pm.GetAlive()) { } }, 20_000);
        p.ProfileOnGame("PlayerManager.GetCT (enumerate)", () => { foreach (var _ in pm.GetCT()) { } }, 20_000);
        p.ProfileOnGame("PlayerManager.GetInTeam(T) (enumerate)", () => { foreach (var _ in pm.GetInTeam(Team.T)) { } }, 20_000);
        p.ProfileOnGame("PlayerManager.FindTargettedPlayers(@me)", () => _ = pm.FindTargettedPlayers(me, "@me", TargetSearchMode.IncludeSelf).ToList(), 5_000);
        p.ProfileOnGame("PlayerManager.FindTargettedPlayers(@all)", () => _ = pm.FindTargettedPlayers(me, "@all", TargetSearchMode.IncludeSelf).ToList(), 2_000);
        p.ProfileOnGame("PlayerManager.FindTargettedPlayers(name)", () => _ = pm.FindTargettedPlayers(me, "zz_nobody_zz", TargetSearchMode.IncludeSelf).ToList(), 2_000);
        p.ProfileOnGame("PlayerManager.ShouldBlockTransmitEntity (set + clear)", () => { pm.ShouldBlockTransmitEntity(1, true); pm.ShouldBlockTransmitEntity(1, false); }, 20_000);
        return Task.CompletedTask;
    }
}

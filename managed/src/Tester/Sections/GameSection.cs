using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Services;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class GameSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "game";

    private IGameService Game => Core.Game;

    private bool HasGameRules => Core.EntitySystem.GetGameRules() is { IsValid: true };

    private static bool Pristine( in CCSMatch m )
        => m.ActualRoundsPlayed == 0 && m.NOvertimePlaying == 0
           && m.CTScoreTotal == 0 && m.TerroristScoreTotal == 0
           && m.CTScoreFirstHalf == 0 && m.CTScoreSecondHalf == 0 && m.CTScoreOvertime == 0
           && m.TerroristScoreFirstHalf == 0 && m.TerroristScoreSecondHalf == 0 && m.TerroristScoreOvertime == 0;

    private void Mutating( TestContext t, string name, Action body )
        => t.Test(name, () =>
        {
            if (!HasGameRules) Skip("no game rules entity (map not loaded?)");
            var original = Game.MatchData;
            if (!Pristine(original)) Skip($"scores are not zero ({original}); not rewriting a live match");
            try
            {
                Game.Reset();
                body();
            }
            finally
            {
                Game.Reset();
                Game.SetPhase(original.Phase);
            }
        });

    private CCSTeam? TeamEntity( Team team )
        => Core.EntitySystem.GetAllEntitiesByDesignerName<CCSTeam>("cs_team_manager").FirstOrDefault(x => x.TeamNum == (int)team);

    private (Vector Pos, QAngle Angle, Vector Velocity) Spawn( TestContext t )
    {
        var origin = t.Caller.PlayerPawn?.AbsOrigin ?? new Vector(0, 0, 0);
        return (new Vector(origin.X, origin.Y, origin.Z + 3000f), new QAngle(0, 0, 0), new Vector(0, 0, 0));
    }

    public override async Task Test( TestContext t )
    {
        var g = Game;

        t.Test("MatchData is readable and prints", () =>
        {
            if (!HasGameRules) Skip("no game rules entity");
            var m = g.MatchData;
            Expect(m.ToString().StartsWith("Match [Round"), $"ToString: {m}");
            Expect(m.ActualRoundsPlayed >= 0 && m.CTScoreTotal >= 0 && m.TerroristScoreTotal >= 0, "negative counters");
            Expect(Enum.IsDefined(m.Phase), $"unknown phase {(short)m.Phase}");
        });

        t.Test("MatchData matches the game rules entity", () =>
        {
            var rules = Core.EntitySystem.GetGameRules();
            if (rules is not { IsValid: true }) Skip("no game rules entity");
            Equal((int)g.MatchData.ActualRoundsPlayed, rules!.TotalRoundsPlayed, "TotalRoundsPlayed");
            Equal((int)g.MatchData.NOvertimePlaying, rules.OvertimePlaying, "OvertimePlaying");
        });

        t.Test("GetWinningTeam returns a team id", () =>
        {
            if (!HasGameRules) Skip("no game rules entity");
            Expect(g.GetWinningTeam() is 0 or 2 or 3, $"got {g.GetWinningTeam()}");
        });

        Mutating(t, "Reset zeroes the match and returns to the standard phase", () =>
        {
            g.AddTerroristScore(3);
            g.AddCTScore(2);
            g.IncrementRound(4);
            g.GoToOvertime(2);
            g.SetPhase(GamePhase.GAMEPHASE_HALFTIME);
            g.Reset();
            var m = g.MatchData;
            Expect(Pristine(m), $"not zeroed: {m}");
            Equal(GamePhase.GAMEPHASE_PLAYING_STANDARD, m.Phase, "Phase");
            Equal(0, Core.EntitySystem.GetGameRules()!.TotalRoundsPlayed, "rules TotalRoundsPlayed");
        });

        Mutating(t, "SetPhase updates match data and game rules", () =>
        {
            foreach (var phase in new[] { GamePhase.GAMEPHASE_WARMUP_ROUND, GamePhase.GAMEPHASE_PLAYING_STANDARD, GamePhase.GAMEPHASE_PLAYING_FIRST_HALF, GamePhase.GAMEPHASE_PLAYING_SECOND_HALF, GamePhase.GAMEPHASE_HALFTIME })
            {
                g.SetPhase(phase);
                Equal(phase, g.MatchData.Phase, $"MatchData.Phase after {phase}");
                Equal(phase, Core.EntitySystem.GetGameRules()!.GamePhaseEnum, $"GamePhaseEnum after {phase}");
            }
        });

        Mutating(t, "Scores outside a half or overtime only change the total", () =>
        {
            g.AddTerroristScore(3);
            g.AddCTScore(2);
            var m = g.MatchData;
            Equal((short)3, m.TerroristScoreTotal, "T total");
            Equal((short)2, m.CTScoreTotal, "CT total");
            Expect(m.TerroristScoreFirstHalf == 0 && m.TerroristScoreSecondHalf == 0 && m.TerroristScoreOvertime == 0, "T half buckets touched");
            Expect(m.CTScoreFirstHalf == 0 && m.CTScoreSecondHalf == 0 && m.CTScoreOvertime == 0, "CT half buckets touched");
        });

        Mutating(t, "Scores land in the first-half bucket during the first half", () =>
        {
            g.SetPhase(GamePhase.GAMEPHASE_PLAYING_FIRST_HALF);
            g.AddTerroristScore(2);
            g.AddCTScore(1);
            var m = g.MatchData;
            Equal((short)2, m.TerroristScoreFirstHalf, "T first half");
            Equal((short)1, m.CTScoreFirstHalf, "CT first half");
            Expect(m.TerroristScoreSecondHalf == 0 && m.CTScoreSecondHalf == 0, "second half touched");
        });

        Mutating(t, "Scores land in the second-half bucket during the second half", () =>
        {
            g.SetPhase(GamePhase.GAMEPHASE_PLAYING_SECOND_HALF);
            g.AddTerroristScore(4);
            g.AddCTScore(5);
            var m = g.MatchData;
            Equal((short)4, m.TerroristScoreSecondHalf, "T second half");
            Equal((short)5, m.CTScoreSecondHalf, "CT second half");
            Expect(m.TerroristScoreFirstHalf == 0 && m.CTScoreFirstHalf == 0, "first half touched");
        });

        Mutating(t, "Scores land in the overtime bucket while overtime is playing", () =>
        {
            g.SetPhase(GamePhase.GAMEPHASE_PLAYING_FIRST_HALF);
            g.GoToOvertime();
            g.AddTerroristScore(2);
            g.AddCTScore(3);
            var m = g.MatchData;
            Equal((short)2, m.TerroristScoreOvertime, "T overtime");
            Equal((short)3, m.CTScoreOvertime, "CT overtime");
            Expect(m.TerroristScoreFirstHalf == 0 && m.CTScoreFirstHalf == 0, "first half touched during overtime");
        });

        Mutating(t, "Bonus points count as score", () =>
        {
            g.AddTerroristBonusPoints(2);
            g.AddCTBonusPoints(3);
            Equal((short)2, g.MatchData.TerroristScoreTotal, "T total");
            Equal((short)3, g.MatchData.CTScoreTotal, "CT total");
        });

        Mutating(t, "Wins add a round and a point", () =>
        {
            g.AddTerroristWins(2);
            g.AddCTWins(1);
            var m = g.MatchData;
            Equal((short)3, m.ActualRoundsPlayed, "rounds");
            Equal((short)2, m.TerroristScoreTotal, "T total");
            Equal((short)1, m.CTScoreTotal, "CT total");
            Equal(3, Core.EntitySystem.GetGameRules()!.TotalRoundsPlayed, "rules TotalRoundsPlayed");
        });

        Mutating(t, "IncrementRound defaults to one and accepts a count", () =>
        {
            g.IncrementRound();
            Equal((short)1, g.MatchData.ActualRoundsPlayed, "after default");
            g.IncrementRound(4);
            Equal((short)5, g.MatchData.ActualRoundsPlayed, "after +4");
            Equal(5, Core.EntitySystem.GetGameRules()!.TotalRoundsPlayed, "rules TotalRoundsPlayed");
        });

        Mutating(t, "GoToOvertime defaults to one and accepts a count", () =>
        {
            g.GoToOvertime();
            Equal((short)1, g.MatchData.NOvertimePlaying, "after default");
            g.GoToOvertime(2);
            Equal((short)3, g.MatchData.NOvertimePlaying, "after +2");
            Equal(3, Core.EntitySystem.GetGameRules()!.OvertimePlaying, "rules OvertimePlaying");
        });

        Mutating(t, "Negative amounts subtract", () =>
        {
            g.AddTerroristScore(5);
            g.AddTerroristScore(-2);
            Equal((short)3, g.MatchData.TerroristScoreTotal, "T total");
        });

        Mutating(t, "SwapTeamScores exchanges every bucket", () =>
        {
            g.SetPhase(GamePhase.GAMEPHASE_PLAYING_FIRST_HALF);
            g.AddTerroristScore(3);
            g.AddCTScore(1);
            g.SetPhase(GamePhase.GAMEPHASE_PLAYING_SECOND_HALF);
            g.AddTerroristScore(2);
            g.AddCTScore(4);
            var before = g.MatchData;
            g.SwapTeamScores();
            var after = g.MatchData;
            Equal(before.CTScoreTotal, after.TerroristScoreTotal, "T total <- CT total");
            Equal(before.TerroristScoreTotal, after.CTScoreTotal, "CT total <- T total");
            Equal(before.CTScoreFirstHalf, after.TerroristScoreFirstHalf, "T first half");
            Equal(before.TerroristScoreSecondHalf, after.CTScoreSecondHalf, "CT second half");
        });

        Mutating(t, "Score changes reach the team entities", () =>
        {
            g.AddTerroristScore(4);
            g.AddCTScore(2);
            var tt = TeamEntity(Team.T);
            var ct = TeamEntity(Team.CT);
            if (tt is null || ct is null) Skip("cs_team_manager entities not found");
            Equal(4, tt!.Score, "T team Score");
            Equal(2, ct!.Score, "CT team Score");
        });

        Mutating(t, "GetWinningTeam follows the score", () =>
        {
            Equal(0, g.GetWinningTeam(), "tie");
            g.AddTerroristScore(2);
            Equal((int)Team.T, g.GetWinningTeam(), "T ahead");
            g.AddCTScore(5);
            Equal((int)Team.CT, g.GetWinningTeam(), "CT ahead");
        });

        Mutating(t, "Reset is idempotent", () =>
        {
            g.Reset();
            g.Reset();
            Expect(Pristine(g.MatchData), "not zero after repeated Reset");
        });

        t.Test("TerminateRound / GoToIntermission", () => Skip("end the live round / match"));

        // ---- projectiles ----
        await ProjectileTest(t, "EmitHEGrenade", "hegrenade_projectile", s => g.EmitHEGrenade(s.Pos, s.Angle, s.Velocity, null));
        await ProjectileTest(t, "EmitFlashbang", "flashbang_projectile", s => g.EmitFlashbang(s.Pos, s.Angle, s.Velocity, null));
        await ProjectileTest(t, "EmitSmokeGrenade", "smokegrenade_projectile", s => g.EmitSmokeGrenade(s.Pos, s.Angle, s.Velocity, Team.T, null));
        await ProjectileTest(t, "EmitMolotov", "molotov_projectile", s => g.EmitMolotov(s.Pos, s.Angle, s.Velocity, Team.T, null));
        await ProjectileTest(t, "EmitDecoy", "decoy_projectile", s => g.EmitDecoy(s.Pos, s.Angle, s.Velocity, null));

        await ProjectileTestAsync(t, "EmitHEGrenadeAsync", "hegrenade_projectile", s => g.EmitHEGrenadeAsync(s.Pos, s.Angle, s.Velocity, null));
        await ProjectileTestAsync(t, "EmitFlashbangAsync", "flashbang_projectile", s => g.EmitFlashbangAsync(s.Pos, s.Angle, s.Velocity, null));
        await ProjectileTestAsync(t, "EmitSmokeGrenadeAsync", "smokegrenade_projectile", s => g.EmitSmokeGrenadeAsync(s.Pos, s.Angle, s.Velocity, Team.T, null));
        await ProjectileTestAsync(t, "EmitMolotovAsync", "molotov_projectile", s => g.EmitMolotovAsync(s.Pos, s.Angle, s.Velocity, Team.T, null));
        await ProjectileTestAsync(t, "EmitDecoyAsync", "decoy_projectile", s => g.EmitDecoyAsync(s.Pos, s.Angle, s.Velocity, null));
    }

    private async Task ProjectileTest( TestContext t, string name, string designer, Func<(Vector Pos, QAngle Angle, Vector Velocity), CBaseEntity> emit )
    {
        await t.TestAsync($"{name} creates a {designer} (despawned immediately)", () =>
        {
            var e = emit(Spawn(t));
            try
            {
                NotNull(e, "projectile");
                Expect(e.IsValid, "not valid after creation");
                Equal(designer, e.DesignerName, "DesignerName");
            }
            finally { e.Despawn(); }
            return Task.CompletedTask;
        });
    }

    private async Task ProjectileTestAsync<T>( TestContext t, string name, string designer, Func<(Vector Pos, QAngle Angle, Vector Velocity), Task<T>> emit ) where T : CBaseEntity
    {
        await t.TestAsync($"{name} creates a {designer} (despawned immediately)", async () =>
        {
            var e = await emit(Spawn(t));
            try
            {
                NotNull(e, "projectile");
                Expect(e.IsValid, "not valid after creation");
                Equal(designer, e.DesignerName, "DesignerName");
            }
            finally { e.Despawn(); }
        });
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var g = Game;
        if (!p.OnGame(() => HasGameRules)) return Task.CompletedTask;

        p.ProfileOnGame("Game.MatchData (read)", () => _ = g.MatchData.CTScoreTotal, 200_000);
        p.ProfileOnGame("Game.GetWinningTeam (entity query)", () => _ = g.GetWinningTeam(), 5_000);

        var original = p.OnGame(() => g.MatchData);
        if (!Pristine(original)) return Task.CompletedTask;

        // Each call rewrites networked state, so keep the counts small and finish with a Reset.
        try
        {
            p.ProfileOnGame("Game.AddTerroristScore(+1) then (-1)", () => { g.AddTerroristScore(1); g.AddTerroristScore(-1); }, 500);
            p.ProfileOnGame("Game.SetPhase (standard <-> first half)", () => { g.SetPhase(GamePhase.GAMEPHASE_PLAYING_FIRST_HALF); g.SetPhase(GamePhase.GAMEPHASE_PLAYING_STANDARD); }, 500);
            p.ProfileOnGame("Game.SwapTeamScores x2", () => { g.SwapTeamScores(); g.SwapTeamScores(); }, 500);
            p.ProfileOnGame("Game.Reset", () => g.Reset(), 500);
        }
        finally
        {
            p.OnGame(() =>
            {
                g.Reset();
                g.SetPhase(original.Phase);
            });
        }

        var spawn = p.OnGame(() => Spawn(new TestContext(Core, p.Caller)));
        p.ProfileOnGame("Game.EmitDecoy + Despawn", () => g.EmitDecoy(spawn.Pos, spawn.Angle, spawn.Velocity, null).Despawn(), 200);
        return Task.CompletedTask;
    }
}

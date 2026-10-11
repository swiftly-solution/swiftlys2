using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Permissions;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class PermissionsSection( ISwiftlyCore core ) : Section(core)
{
    private const ulong Fake = 76561190000000001UL;
    private const ulong Other = 76561190000000002UL;
    private const string P = "tester.perm";

    public override string Name => "permissions";

    private IPermissionManager Perm => Core.Permission;

    private void Reset()
    {
        Perm.ClearPermissions(Fake);
        Perm.ClearPermissions(Other);
        foreach (var g in new[] { "g", "a", "b", "c", "d" })
            foreach (var c in new[] { "x", "a", "b", "c", "d", "child", "wide.*" })
            {
                Perm.RemoveSubPermission($"{P}.{g}", $"{P}.{c}");
            }
    }

    private void Isolated( TestContext t, string name, Action body )
        => t.Test(name, () =>
        {
            Reset();
            try
            {
                if (Perm.PlayerHasPermission(Fake, $"{P}.probe.zzz")) Skip("default permission group grants every permission");
                body();
            }
            finally { Reset(); }
        });

    public override Task Test( TestContext t )
    {
        Isolated(t, "Player has nothing by default", () =>
        {
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.a"), "has permission without being granted");
            Expect(!Perm.PlayerHasPermissions(Fake, [$"{P}.a", $"{P}.b"]), "has permissions without being granted");
        });

        Isolated(t, "AddPermission grants, RemovePermission revokes", () =>
        {
            Perm.AddPermission(Fake, $"{P}.a");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.a"), "not granted after Add");
            Expect(!Perm.PlayerHasPermission(Other, $"{P}.a"), "leaked to another player");
            Perm.RemovePermission(Fake, $"{P}.a");
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.a"), "still granted after Remove");
        });

        Isolated(t, "Query cache is invalidated by Add and Remove", () =>
        {
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.a"), "precondition");
            Perm.AddPermission(Fake, $"{P}.a");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.a"), "stale 'false' after Add");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.a"), "cached 'true'");
            Perm.RemovePermission(Fake, $"{P}.a");
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.a"), "stale 'true' after Remove");
        });

        Isolated(t, "AddPermission twice is idempotent", () =>
        {
            Perm.AddPermission(Fake, $"{P}.a");
            Perm.AddPermission(Fake, $"{P}.a");
            Perm.RemovePermission(Fake, $"{P}.a");
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.a"), "duplicate entry survived a single Remove");
        });

        Isolated(t, "Remove of a permission never granted does not throw", () =>
        {
            Perm.RemovePermission(Fake, $"{P}.never");
            Perm.RemovePermission(9UL, $"{P}.never");
        });

        Isolated(t, "Permission names are case-insensitive", () =>
        {
            Perm.AddPermission(Fake, $"{P}.CaseTest");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.casetest"), "lower-case query missed");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.CASETEST"), "upper-case query missed");
        });

        Isolated(t, "PlayerHasPermissions requires all", () =>
        {
            Perm.AddPermission(Fake, $"{P}.a");
            Expect(!Perm.PlayerHasPermissions(Fake, [$"{P}.a", $"{P}.b"]), "true with only one of two");
            Perm.AddPermission(Fake, $"{P}.b");
            Expect(Perm.PlayerHasPermissions(Fake, [$"{P}.a", $"{P}.b"]), "false with both");
            Expect(Perm.PlayerHasPermissions(Fake, []), "empty list should be true");
        });

        Isolated(t, "Wildcard 'x.*' grants children", () =>
        {
            Perm.AddPermission(Fake, $"{P}.wild.*");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.wild.one"), "child missed");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.wild.one.two"), "nested child missed");
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.other.one"), "unrelated granted");
        });

        Isolated(t, "Wildcard 'x.*' also grants 'x' itself", () =>
        {
            Perm.AddPermission(Fake, $"{P}.wild.*");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.wild"), $"'{P}.wild' not granted by '{P}.wild.*'");
        });

        Isolated(t, "Wildcard 'x.*' does not grant a sibling that only shares the prefix", () =>
        {
            Perm.AddPermission(Fake, $"{P}.wild.*");
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.wildcat"), $"'{P}.wildcat' granted by '{P}.wild.*'");
        });

        Isolated(t, "Wildcard '*' grants everything", () =>
        {
            Perm.AddPermission(Fake, "*");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.anything"), "'*' missed");
            Expect(Perm.PlayerHasPermission(Fake, "totally.unrelated"), "'*' missed unrelated");
        });

        Isolated(t, "Sub-permission: parent grants child", () =>
        {
            Perm.AddSubPermission($"{P}.g", $"{P}.child");
            Perm.AddPermission(Fake, $"{P}.g");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.child"), "child not inherited");
            Expect(!Perm.PlayerHasPermission(Other, $"{P}.child"), "inherited by someone without the parent");
        });

        Isolated(t, "Sub-permission: added after the query invalidates the cache", () =>
        {
            Perm.AddPermission(Fake, $"{P}.g");
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.child"), "precondition");
            Perm.AddSubPermission($"{P}.g", $"{P}.child");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.child"), "stale 'false' after AddSubPermission");
            Perm.RemoveSubPermission($"{P}.g", $"{P}.child");
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.child"), "stale 'true' after RemoveSubPermission");
        });

        Isolated(t, "Sub-permission chain (a -> b -> c) is transitive", () =>
        {
            Perm.AddSubPermission($"{P}.a", $"{P}.b");
            Perm.AddSubPermission($"{P}.b", $"{P}.c");
            Perm.AddPermission(Fake, $"{P}.a");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.c"), "c not reachable from a");
        });

        Isolated(t, "Sub-permission loop (a -> b -> a) terminates", () =>
        {
            Perm.AddSubPermission($"{P}.a", $"{P}.b");
            Perm.AddSubPermission($"{P}.b", $"{P}.a");
            Perm.AddPermission(Fake, $"{P}.a");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.b"), "b not reachable");
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.d"), "unrelated granted");
        });

        Isolated(t, "Sub-permission with wildcard child", () =>
        {
            Perm.AddSubPermission($"{P}.g", $"{P}.wide.*");
            Perm.AddPermission(Fake, $"{P}.g");
            Expect(Perm.PlayerHasPermission(Fake, $"{P}.wide.x"), "wildcard child not honoured");
        });

        Isolated(t, "GetPlayerPermissions lists granted and inherited, without duplicates", () =>
        {
            Perm.AddSubPermission($"{P}.g", $"{P}.child");
            Perm.AddPermission(Fake, $"{P}.g");
            Perm.AddPermission(Fake, $"{P}.a");
            var list = Perm.GetPlayerPermissions(Fake).ToList();
            Expect(list.Contains($"{P}.g") && list.Contains($"{P}.a") && list.Contains($"{P}.child"), $"missing entries: {string.Join(",", list.Where(x => x.StartsWith(P)))}");
            Equal(list.Count, list.Distinct().Count(), "distinct count");
        });

        Isolated(t, "ClearPermissions removes everything for that player only", () =>
        {
            Perm.AddPermission(Fake, $"{P}.a");
            Perm.AddPermission(Fake, $"{P}.b");
            Perm.AddPermission(Other, $"{P}.a");
            Perm.ClearPermissions(Fake);
            Expect(!Perm.PlayerHasPermission(Fake, $"{P}.a") && !Perm.PlayerHasPermission(Fake, $"{P}.b"), "not cleared");
            Expect(Perm.PlayerHasPermission(Other, $"{P}.a"), "cleared another player");
        });

        t.Test("Caller's own 'tester.run' permission is untouched by these tests", () =>
            Expect(Perm.PlayerHasPermission(t.Caller.SteamID, "tester.run"), "caller lost tester.run"));

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var perm = Perm;
        Reset();
        try
        {
            perm.AddPermission(Fake, $"{P}.hit");
            perm.AddPermission(Fake, $"{P}.wild.*");
            perm.AddSubPermission($"{P}.a", $"{P}.b");
            perm.AddSubPermission($"{P}.b", $"{P}.c");
            perm.AddSubPermission($"{P}.c", $"{P}.d");
            perm.AddPermission(Other, $"{P}.a");

            _ = perm.PlayerHasPermission(Fake, $"{P}.hit");
            _ = perm.PlayerHasPermission(Fake, $"{P}.miss");
            _ = perm.PlayerHasPermission(Fake, $"{P}.wild.x");
            _ = perm.PlayerHasPermission(Other, $"{P}.d");

            p.Profile("PlayerHasPermission (cached, granted)", () => _ = perm.PlayerHasPermission(Fake, $"{P}.hit"), 200_000);
            p.Profile("PlayerHasPermission (cached, denied)", () => _ = perm.PlayerHasPermission(Fake, $"{P}.miss"), 200_000);
            p.Profile("PlayerHasPermission (cached, wildcard)", () => _ = perm.PlayerHasPermission(Fake, $"{P}.wild.x"), 200_000);
            p.Profile("PlayerHasPermission (cached, 3-deep sub-permission)", () => _ = perm.PlayerHasPermission(Other, $"{P}.d"), 200_000);

            var perms = new[] { $"{P}.hit", $"{P}.wild.x" };
            p.Profile("PlayerHasPermissions (2 entries, cached)", () => _ = perm.PlayerHasPermissions(Fake, perms), 100_000);
            p.Profile("GetPlayerPermissions (enumerate)", () => { foreach (var _ in perm.GetPlayerPermissions(Fake)) { } }, 20_000);

            p.Profile("AddPermission + RemovePermission", () => { perm.AddPermission(Other, $"{P}.cycle"); perm.RemovePermission(Other, $"{P}.cycle"); }, 20_000);
            p.Profile("Uncached query (flush via Add/Remove, then granted)", () =>
            {
                perm.AddPermission(Other, $"{P}.cycle");
                _ = perm.PlayerHasPermission(Other, $"{P}.cycle");
                perm.RemovePermission(Other, $"{P}.cycle");
            }, 20_000);
            p.Profile("Uncached query (3-deep sub-permission)", () =>
            {
                perm.AddSubPermission($"{P}.g", $"{P}.child");
                _ = perm.PlayerHasPermission(Other, $"{P}.d");
                perm.RemoveSubPermission($"{P}.g", $"{P}.child");
            }, 20_000);
        }
        finally { Reset(); }

        return Task.CompletedTask;
    }
}

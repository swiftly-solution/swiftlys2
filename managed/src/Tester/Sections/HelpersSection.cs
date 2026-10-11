using System.Reflection;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Helpers;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class HelpersSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "helpers";

    private IHelpers H => Core.Helpers;

    private static void ClearVDataCache()
        => typeof(IHelpers).Assembly.GetType("SwiftlyS2.Core.Services.HelpersService")!
            .GetMethod("RenewVDataCache", BindingFlags.Public | BindingFlags.Static)!
            .Invoke(null, null);

    public override Task Test( TestContext t )
    {
        var h = H;
        var all = Enum.GetValues<ItemDefinitionIndex>();

        t.Test("ItemDefinitionIndex values are unique", () =>
        {
            var dupes = all.GroupBy(v => (int)v).Where(g => g.Count() > 1).Select(g => string.Join("/", g)).ToList();
            Expect(dupes.Count == 0, $"duplicate values: {string.Join(", ", dupes)}");
        });

        t.Test("Every ItemDefinitionIndex has a classname", () =>
        {
            var missing = all.Where(v => h.GetClassnameByDefinitionIndex(v) is null).ToList();
            Expect(missing.Count == 0, $"no classname for: {string.Join(", ", missing)}");
        });

        t.Test("Classname -> index -> classname round trip for every item", () =>
        {
            foreach (var v in all)
            {
                var name = h.GetClassnameByDefinitionIndex(v);
                if (name is null) continue;
                Equal((int)v, h.GetDefinitionIndexByClassname(name) ?? int.MinValue, $"index of {name}");
                Equal(name, h.GetClassnameByDefinitionIndex((int)v), $"classname of {(int)v}");
            }
        });

        t.Test("Enum and int overloads agree", () =>
        {
            foreach (var v in all) Equal(h.GetClassnameByDefinitionIndex((int)v), h.GetClassnameByDefinitionIndex(v), v.ToString());
        });

        t.Test("Well-known classnames", () =>
        {
            Equal(7, h.GetDefinitionIndexByClassname("weapon_ak47") ?? -1, "weapon_ak47");
            Equal(9, h.GetDefinitionIndexByClassname("weapon_awp") ?? -1, "weapon_awp");
            Equal(42, h.GetDefinitionIndexByClassname("weapon_knife") ?? -1, "weapon_knife");
            Equal(49, h.GetDefinitionIndexByClassname("weapon_c4") ?? -1, "weapon_c4");
            Equal("weapon_deagle", h.GetClassnameByDefinitionIndex(ItemDefinitionIndex.Deagle) ?? "", "Deagle");
            Equal("item_kevlar", h.GetClassnameByDefinitionIndex(ItemDefinitionIndex.ItemKevlar) ?? "", "Kevlar");
        });

        t.Test("Unknown index and classname resolve to null", () =>
        {
            Expect(h.GetClassnameByDefinitionIndex(99_999) is null, "unknown index");
            Expect(h.GetClassnameByDefinitionIndex(-1) is null, "negative index");
            Expect(h.GetDefinitionIndexByClassname("weapon_tester_nope") is null, "unknown classname");
            Expect(h.GetDefinitionIndexByClassname("") is null, "empty classname");
        });

        t.Test("Classname lookup is case-sensitive", () =>
            Expect(h.GetDefinitionIndexByClassname("WEAPON_AK47") is null, "upper-case name resolved"));

        t.Test("Null classname throws", () => Throws<ArgumentNullException>(() => h.GetDefinitionIndexByClassname(null!)));

        t.Test("GetWeaponCSDataFromKey returns data for a known weapon", () =>
        {
            ClearVDataCache();
            var data = h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Ak47);
            NotNull(data, "AK-47 data");
            Expect(data!.Address != 0, "zero address");
        });

        t.Test("VData lookups are cached (same instance)", () =>
        {
            ClearVDataCache();
            var a = h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Awp);
            NotNull(a, "first");
            Expect(ReferenceEquals(a, h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Awp)), "enum overload not cached");
            Expect(ReferenceEquals(a, h.GetWeaponCSDataFromKey((int)ItemDefinitionIndex.Awp)), "int overload differs");
        });

        t.Test("Different weapons give different VData", () =>
        {
            ClearVDataCache();
            var ak = h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Ak47);
            var awp = h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Awp);
            NotNull(ak, "ak47");
            NotNull(awp, "awp");
            Expect(ak!.Address != awp!.Address, "same address for two weapons");
        });

        t.Test("Unknown item index returns null, repeatedly", () =>
        {
            ClearVDataCache();
            Expect(h.GetWeaponCSDataFromKey(99_999) is null, "unknown index");
            Expect(h.GetWeaponCSDataFromKey(99_999) is null, "unknown index (cached)");
        });

        t.Test("String-key lookup by classname describes the same weapon as the index lookup", () =>
        {
            ClearVDataCache();
            var byName = h.GetWeaponCSDataFromKey(-1, "weapon_ak47");
            NotNull(byName, "by name");
            var byIndex = h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Ak47);
            NotNull(byIndex, "by index");
            Equal(byName!.Price, byIndex!.Price, "Price");
            Equal(byName.KillAward, byIndex.KillAward, "KillAward");
            Equal(byName.WeaponType, byIndex.WeaponType, "WeaponType");
            Equal(byName.PrimaryReserveAmmoMax, byIndex.PrimaryReserveAmmoMax, "PrimaryReserveAmmoMax");
        });

        t.Test("A classname lookup does not leak into the cache entry of item index 0", () =>
        {
            ClearVDataCache();
            var ak = h.GetWeaponCSDataFromKey(-1, "weapon_ak47");
            NotNull(ak, "ak47");
            var zero = h.GetWeaponCSDataFromKey(0);
            Expect(zero is null || zero.Address != ak!.Address, "index 0 resolved to the AK-47 data");
        });

        t.Test("Concurrent cached reads from pool threads", () =>
        {
            ClearVDataCache();
            var warm = h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Ak47);
            NotNull(warm, "warm-up");
            var bad = new System.Collections.Concurrent.ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 8).Select(w => Task.Run(() =>
            {
                for (var i = 0; i < 1000; i++)
                    if (!ReferenceEquals(warm, h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Ak47))) { bad.Add($"worker {w}"); return; }
            })).ToArray());
            Expect(bad.IsEmpty, string.Join(", ", bad));
        });

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var h = H;
        ClearVDataCache();
        _ = p.OnGame(() => h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Ak47));

        p.Profile("Helpers.GetClassnameByDefinitionIndex (early entry)", () => _ = h.GetClassnameByDefinitionIndex(ItemDefinitionIndex.Deagle), 200_000);
        p.Profile("Helpers.GetClassnameByDefinitionIndex (late entry)", () => _ = h.GetClassnameByDefinitionIndex(ItemDefinitionIndex.KnifeKukri), 200_000);
        p.Profile("Helpers.GetClassnameByDefinitionIndex (unknown)", () => _ = h.GetClassnameByDefinitionIndex(99_999), 200_000);
        p.Profile("Helpers.GetDefinitionIndexByClassname (hit)", () => _ = h.GetDefinitionIndexByClassname("weapon_ak47"), 200_000);
        p.Profile("Helpers.GetDefinitionIndexByClassname (miss)", () => _ = h.GetDefinitionIndexByClassname("weapon_nope"), 200_000);
        p.Profile("Helpers.GetWeaponCSDataFromKey (cached, enum)", () => _ = h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Ak47), 200_000);
        p.Profile("Helpers.GetWeaponCSDataFromKey (cached, int)", () => _ = h.GetWeaponCSDataFromKey(7), 200_000);

        p.ProfileOnGame("Helpers.GetWeaponCSDataFromKey (uncached, native)", () =>
        {
            ClearVDataCache();
            _ = p.OnGame(() => h.GetWeaponCSDataFromKey(ItemDefinitionIndex.Ak47));
        }, 5_000);
        return Task.CompletedTask;
    }
}

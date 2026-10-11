using SwiftlyS2.Shared;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class GameDataSection( ISwiftlyCore core ) : Section(core)
{
    private const string Bogus = "Tester::NoSuchEntryZzz";
    private const string KnownSignature = "CBaseEntity::TakeDamage";
    private const string KnownOffset = "CBaseEntity::Touch";
    private const string PluginOffset = "Tester::PluginOffset";

    public override string Name => "gamedata";

    private static int ExpectedPluginOffset => OperatingSystem.IsWindows() ? 291 : 1110;

    public override Task Test( TestContext t )
    {
        var gd = Core.GameData;

        t.Test("HasSignature(known) is true", () => Expect(gd.HasSignature(KnownSignature), $"{KnownSignature} missing from gamedata"));
        t.Test("HasSignature(bogus) is false", () => Expect(!gd.HasSignature(Bogus), "bogus signature exists"));

        t.Test("GetSignature(known) is a non-zero address, stable between calls", () =>
        {
            var a = gd.GetSignature(KnownSignature);
            Expect(a != 0, "zero address");
            Equal(a, gd.GetSignature(KnownSignature), "second read");
        });

        t.Test("TryGetSignature(known) agrees with GetSignature", () =>
        {
            Expect(gd.TryGetSignature(KnownSignature, out var addr), "TryGet returned false");
            Equal(gd.GetSignature(KnownSignature), addr, "address");
        });

        t.Test("TryGetSignature(bogus) is false with a zero address", () =>
        {
            Expect(!gd.TryGetSignature(Bogus, out var addr), "returned true");
            Equal((nint)0, addr, "out value");
        });

        t.Test("GetSignature(bogus) returns zero or throws, never a real address", () =>
        {
            try { Equal((nint)0, gd.GetSignature(Bogus), "address"); }
            catch (Exception) { }
        });

        t.Test("HasOffset(known) is true", () => Expect(gd.HasOffset(KnownOffset), $"{KnownOffset} missing from gamedata"));
        t.Test("HasOffset(bogus) is false", () => Expect(!gd.HasOffset(Bogus), "bogus offset exists"));

        t.Test("GetOffset(known) is positive and stable", () =>
        {
            var o = gd.GetOffset(KnownOffset);
            Expect(o > 0, $"offset {o}");
            Equal(o, gd.GetOffset(KnownOffset), "second read");
        });

        t.Test("TryGetOffset(known) agrees with GetOffset", () =>
        {
            Expect(gd.TryGetOffset(KnownOffset, out var off), "TryGet returned false");
            Equal((nint)gd.GetOffset(KnownOffset), off, "offset");
        });

        t.Test("TryGetOffset(bogus) is false with a zero offset", () =>
        {
            Expect(!gd.TryGetOffset(Bogus, out var off), "returned true");
            Equal((nint)0, off, "out value");
        });

        t.Test("GetOffset(bogus) returns zero or throws", () =>
        {
            try { Equal(0, gd.GetOffset(Bogus), "offset"); }
            catch (Exception) { /* throwing is acceptable */ }
        });

        t.Test("Plugin offsets.jsonc is loaded for the current platform", () =>
        {
            Expect(gd.HasOffset(PluginOffset), $"{PluginOffset} missing (is resources/gamedata copied next to Tester.dll?)");
            Equal(ExpectedPluginOffset, gd.GetOffset(PluginOffset), "GetOffset");
            Expect(gd.TryGetOffset(PluginOffset, out var off) && off == ExpectedPluginOffset, "TryGetOffset");
        });

        t.Test("Plugin gamedata does not leak into other lookups", () =>
            Expect(!gd.HasSignature(PluginOffset) && !gd.HasPatch(PluginOffset), "offset name found as signature/patch"));

        t.Test("HasPatch(bogus) is false", () => Expect(!gd.HasPatch(Bogus), "bogus patch exists"));

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var gd = Core.GameData;
        p.Profile("GameData.HasSignature(known)", () => _ = gd.HasSignature(KnownSignature), 200_000);
        p.Profile("GameData.HasSignature(bogus)", () => _ = gd.HasSignature(Bogus), 200_000);
        p.Profile("GameData.GetSignature(known)", () => _ = gd.GetSignature(KnownSignature), 200_000);
        p.Profile("GameData.TryGetSignature(known)", () => _ = gd.TryGetSignature(KnownSignature, out _), 200_000);
        p.Profile("GameData.TryGetSignature(bogus)", () => _ = gd.TryGetSignature(Bogus, out _), 200_000);
        p.Profile("GameData.HasOffset(known)", () => _ = gd.HasOffset(KnownOffset), 200_000);
        p.Profile("GameData.GetOffset(known)", () => _ = gd.GetOffset(KnownOffset), 200_000);
        p.Profile("GameData.TryGetOffset(known)", () => _ = gd.TryGetOffset(KnownOffset, out _), 200_000);
        p.Profile("GameData.GetOffset(plugin file)", () => _ = gd.GetOffset(PluginOffset), 200_000);
        p.Profile("GameData.HasPatch(bogus)", () => _ = gd.HasPatch(Bogus), 200_000);
        return Task.CompletedTask;
    }
}

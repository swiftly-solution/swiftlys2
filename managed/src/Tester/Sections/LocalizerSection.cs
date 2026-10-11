using SwiftlyS2.Shared;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class LocalizerSection( ISwiftlyCore core ) : Section(core)
{
    private const string Missing = "tester.no_such_key_zzz";

    private static readonly string[] Hello = ["Hello", "Hallo"];
    private static readonly string[] Format = ["Player Bob has 7 points", "Spieler Bob hat 7 Punkte"];

    public override string Name => "localizer";

    public override Task Test( TestContext t )
    {
        var loc = Core.Localizer;

        t.Test("Translation files were loaded (tester.hello resolves)", () =>
        {
            var v = loc["tester.hello"];
            Expect(Hello.Contains(v), $"tester.hello = '{v}' (are resources/translations copied next to Tester.dll?)");
        });

        t.Test("Indexer is stable between calls", () => Equal(loc["tester.hello"], loc["tester.hello"], "tester.hello"));

        t.Test("Format args are applied", () =>
        {
            var v = loc["tester.format", "Bob", 7];
            Expect(Format.Contains(v), $"got '{v}'");
        });

        t.Test("Args on a key without placeholders are ignored", () =>
            Equal(loc["tester.hello"], loc["tester.hello", "unused", 1], "tester.hello with args"));

        t.Test("Key missing in the active language falls back to English", () =>
            Equal("Only in English", loc["tester.english_only"], "tester.english_only"));

        t.Test("Unknown key returns '<language>.<key>'", () =>
        {
            var v = loc[Missing];
            Expect(v.EndsWith("." + Missing, StringComparison.Ordinal) && v.Length > Missing.Length + 1, $"got '{v}'");
        });

        t.Test("Unknown key with args does not throw", () => _ = loc[Missing, 1, 2, 3]);

        t.Test("Color tags are converted to chat color codes", () =>
        {
            var v = loc["tester.colored"];
            Expect(!v.Contains("[red]") && !v.Contains("[/]"), $"tags left in '{v}'");
            Expect(v.Contains('\x07') && v.Contains('\x01'), $"color codes missing in '{v}'");
        });

        t.Test("Text starting with '[' gets a leading space", () =>
            Expect(loc["tester.bracket"].StartsWith(' '), $"got '{loc["tester.bracket"]}'"));

        t.Test("Separate Core.Localizer reads resolve the same text", () =>
        {
            var a = Core.Localizer;
            var b = Core.Localizer;
            Equal(a["tester.hello"], b["tester.hello"], "tester.hello");
            Equal(a["tester.english_only"], b["tester.english_only"], "tester.english_only");
        });

        t.Test("Translation.GetPlayerLocalizer(caller) resolves keys", () =>
        {
            var pl = Core.Translation.GetPlayerLocalizer(t.Caller);
            NotNull(pl, "player localizer");
            Expect(Hello.Contains(pl["tester.hello"]), $"got '{pl["tester.hello"]}'");
            Expect(Format.Contains(pl["tester.format", "Bob", 7]), "format");
        });

        t.Test("Player localizer falls back to English and reports unknown keys", () =>
        {
            var pl = Core.Translation.GetPlayerLocalizer(t.Caller);
            Equal("Only in English", pl["tester.english_only"], "tester.english_only");
            Expect(pl[Missing].EndsWith("." + Missing, StringComparison.Ordinal), $"got '{pl[Missing]}'");
        });

        t.Test("Caller language is readable", () =>
        {
            var lang = t.Caller.PlayerLanguage;
            NotNull(lang, "PlayerLanguage");
            Expect(!string.IsNullOrWhiteSpace(lang.Value), "empty language value");
        });

        t.Test("Player localizer works for every connected player", () =>
        {
            foreach (var player in Core.PlayerManager.GetAllValidPlayers())
                Expect(Hello.Contains(Core.Translation.GetPlayerLocalizer(player)["tester.hello"]), $"slot {player.Slot}");
        });

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var loc = Core.Localizer;
        var me = p.Caller;
        var pl = p.OnGame(() => Core.Translation.GetPlayerLocalizer(me));

        p.Profile("Localizer[key] (hit)", () => _ = loc["tester.hello"], 200_000);
        p.Profile("Localizer[key] (English fallback)", () => _ = loc["tester.english_only"], 200_000);
        p.Profile("Localizer[key] (missing)", () => _ = loc[Missing], 200_000);
        p.Profile("Localizer[key, args] (2 args)", () => _ = loc["tester.format", "Bob", 7], 100_000);
        p.ProfileOnGame("Translation.GetPlayerLocalizer(player)", () => _ = Core.Translation.GetPlayerLocalizer(me), 50_000);
        p.Profile("PlayerLocalizer[key] (hit)", () => _ = pl["tester.hello"], 200_000);
        p.ProfileOnGame("GetPlayerLocalizer + lookup (per-message cost)", () => _ = Core.Translation.GetPlayerLocalizer(me)["tester.hello"], 50_000);
        return Task.CompletedTask;
    }
}

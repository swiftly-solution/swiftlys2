using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Translation;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class TranslationSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "translation";

    private static IEnumerable<Language> AllLanguages()
        => typeof(Language).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(f => f.FieldType == typeof(Language))
            .Select(f => (Language)f.GetValue(null)!);

    public override Task Test( TestContext t )
    {
        t.Test("Every predefined language has a non-empty code", () =>
        {
            var all = AllLanguages().ToList();
            Expect(all.Count >= 30, $"only {all.Count} languages found");
            foreach (var l in all) Expect(!string.IsNullOrWhiteSpace(l.Value), "empty language code");
        });

        t.Test("Predefined language codes are unique", () =>
        {
            var codes = AllLanguages().Select(l => l.Value).ToList();
            var dupes = codes.GroupBy(c => c).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            Expect(dupes.Count == 0, $"duplicate codes: {string.Join(",", dupes)}");
        });

        t.Test("Well-known codes", () =>
        {
            Equal("en", Language.English.Value, "English");
            Equal("de", Language.German.Value, "German");
            Equal("pt-BR", Language.Brazilian.Value, "Brazilian");
            Equal("zh-CN", Language.ChineseCN.Value, "ChineseCN");
            Equal("es-419", Language.LatinAmerica.Value, "LatinAmerica");
        });

        t.Test("Equality is by value", () =>
        {
            var a = new Language("en");
            Expect(a.Equals(Language.English), "Equals(Language)");
            Expect(a.Equals((object)Language.English), "Equals(object)");
            Equal(a.GetHashCode(), Language.English.GetHashCode(), "GetHashCode");
            Expect(!a.Equals(Language.German), "en == de");
            Expect(!a.Equals((Language?)null), "Equals(null)");
            Expect(!a.Equals((object)"en"), "Equals(string)");
        });

        t.Test("ToString and implicit string conversion return the code", () =>
        {
            Equal("fr", Language.French.ToString(), "ToString");
            string s = Language.French;
            Equal("fr", s, "implicit string");
        });

        t.Test("Language works as a dictionary key", () =>
        {
            var map = new Dictionary<Language, int> { [Language.English] = 1, [Language.German] = 2 };
            Equal(1, map[new Language("en")], "en");
            Equal(2, map[new Language("de")], "de");
            Expect(!map.ContainsKey(Language.French), "fr present");
        });

        t.Test("Caller language has a code", () =>
        {
            var lang = t.Caller.PlayerLanguage;
            NotNull(lang, "PlayerLanguage");
            Expect(!string.IsNullOrWhiteSpace(lang.Value), "empty code");
        });

        t.Test("Caller language matches a known language or is a custom code", () =>
        {
            var code = t.Caller.PlayerLanguage.Value;
            if (!AllLanguages().Any(l => l.Value == code)) Skip($"custom/unknown code '{code}' (informational)");
        });

        t.Test("Players sharing a language receive identical text", () =>
        {
            var groups = Core.PlayerManager.GetAllValidPlayers()
                .Where(p => !p.IsFakeClient)
                .GroupBy(p => p.PlayerLanguage.Value)
                .ToList();
            foreach (var g in groups)
            {
                var texts = g.Select(p => Core.Translation.GetPlayerLocalizer(p)["tester.hello"]).Distinct().ToList();
                Expect(texts.Count == 1, $"language '{g.Key}' got {texts.Count} different texts");
            }
        });

        t.Test("Every player localizer falls back to English for a key only English has", () =>
        {
            foreach (var p in Core.PlayerManager.GetAllValidPlayers().Where(p => !p.IsFakeClient))
                Equal("Only in English", Core.Translation.GetPlayerLocalizer(p)["tester.english_only"], $"slot {p.Slot}");
        });

        t.Test("Per-player language switching", () => Skip("needs a second client with a different language"));

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var me = p.Caller;
        var map = new Dictionary<Language, int> { [Language.English] = 1, [Language.German] = 2, [Language.French] = 3 };
        var a = Language.English;
        var b = new Language("en");

        p.Profile("new Language(code)", () => _ = new Language("en"), 200_000);
        p.Profile("Language.Equals(Language)", () => _ = a.Equals(b), 200_000);
        p.Profile("Language.GetHashCode", () => _ = a.GetHashCode(), 200_000);
        p.Profile("Language implicit string", () => { string _ = a; }, 200_000);
        p.Profile("Dictionary<Language,T>.TryGetValue", () => _ = map.TryGetValue(b, out _), 200_000);
        p.ProfileOnGame("IPlayer.PlayerLanguage", () => _ = me.PlayerLanguage, 50_000);
        p.ProfileOnGame("GetPlayerLocalizer for all valid players", () =>
        {
            foreach (var pl in Core.PlayerManager.GetAllValidPlayers()) _ = Core.Translation.GetPlayerLocalizer(pl);
        }, 10_000);
        return Task.CompletedTask;
    }
}

using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Natives;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class ConVarSection( ISwiftlyCore core ) : Section(core)
{
    private const string IntName = "CS_WarnFriendlyDamageInterval";
    private const string FloatName = "sv_chat_proximity";
    private const string BoolName = "sv_cheats";
    private const string StringName = "sv_logsdir";
    private const string VectorName = "smoke_grenade_ct_color";

    public override string Name => "convar";

    private IConVarService Cv => Core.ConVar;

    private void Need( string name )
    {
        if (Cv.FindAsString(name) is null) Skip($"convar {name} not present on this server build");
    }

    private IConVar<T> Typed<T>( string name )
    {
        Need(name);
        return Cv.Find<T>(name) ?? throw new Exception($"Find<{typeof(T).Name}>({name}) returned null");
    }

    private static void Keep<T>( IConVar<T> cv, Action body )
    {
        var original = cv.Value;
        try { body(); }
        finally { cv.SetInternal(original); }
    }

    private static bool Near( float a, float b ) => MathF.Abs(a - b) < 1e-3f;

    private static Vector Vec( IConVar<Vector> cv ) => cv.Value;

    public override async Task Test( TestContext t )
    {
        var cv = Cv;

        t.Test("FindAsString finds every engine convar", () =>
        {
            foreach (var name in new[] { IntName, FloatName, BoolName, StringName, VectorName })
            {
                var c = cv.FindAsString(name);
                NotNull(c, name);
                Equal(name, c!.Name, "Name");
            }
        });

        t.Test("Find<T> with the matching type returns a typed convar", () =>
        {
            NotNull(cv.Find<int>(IntName), IntName);
            NotNull(cv.Find<float>(FloatName), FloatName);
            NotNull(cv.Find<bool>(BoolName), BoolName);
            NotNull(cv.Find<string>(StringName), StringName);
            NotNull(cv.Find<Vector>(VectorName), VectorName);
        });

        t.Test("Find of an unknown convar returns null", () =>
        {
            Expect(cv.Find<int>("tester_cvar_no_such_convar") is null, "Find<int>");
            Expect(cv.FindAsString("tester_cvar_no_such_convar") is null, "FindAsString");
            Expect(cv.FindAsString("") is null, "empty name");
        });

        t.Test("Find<T> with the wrong type throws", () =>
        {
            Need(BoolName);
            Throws<Exception>(() => cv.Find<int>(BoolName));
            Throws<Exception>(() => cv.Find<string>(IntName));
            Throws<Exception>(() => cv.Find<bool>(StringName));
            Throws<Exception>(() => cv.Find<float>(IntName));
        });

        t.Test("Lookup of a differently-cased name resolves to the same convar or nothing", () =>
        {
            Need(BoolName);
            var c = cv.FindAsString(BoolName.ToUpperInvariant());
            if (c is null) Skip("name lookup is case-sensitive on this build");
            Equal(cv.FindAsString(BoolName)!.ValueAsString, c!.ValueAsString, "value");
        });

        t.Test("HelpText matches the dump", () =>
        {
            Need(BoolName);
            Equal("Defines how frequently the server notifies clients that a player damaged a friend", cv.FindAsString(IntName)!.HelpText, IntName);
            Equal("Allow cheats on server", cv.FindAsString(BoolName)!.HelpText, BoolName);
            Equal("Folder in the game directory where server logs will be stored.", cv.FindAsString(StringName)!.HelpText, StringName);
            Equal("", cv.FindAsString(FloatName)!.HelpText, FloatName);
        });

        t.Test("Flags match the dump", () =>
        {
            Need(BoolName);
            void Has( string name, ConvarFlags expected )
            {
                var flags = cv.FindAsString(name)!.Flags;
                Expect((flags & expected) == expected, $"{name}: expected {expected} within {flags}");
            }

            Has(IntName, ConvarFlags.CHEAT);
            Has(FloatName, ConvarFlags.REPLICATED | ConvarFlags.RELEASE);
            var module = cv.FindAsString(FloatName)!.Flags;
            Expect((module & (ConvarFlags.CLIENTDLL | ConvarFlags.GAMEDLL)) != 0, $"{FloatName}: neither CLIENTDLL nor GAMEDLL in {module}");
            Has(BoolName, ConvarFlags.NOTIFY | ConvarFlags.REPLICATED | ConvarFlags.RELEASE);
            Has(StringName, ConvarFlags.ARCHIVE | ConvarFlags.RELEASE);
            Has(VectorName, ConvarFlags.GAMEDLL | ConvarFlags.CHEAT);
        });

        t.Test("Default values match the dump", () =>
        {
            Equal(3, Typed<int>(IntName).DefaultValue, IntName);
            Expect(Near(-1f, Typed<float>(FloatName).DefaultValue), $"{FloatName} default {Typed<float>(FloatName).DefaultValue}");
            Equal(false, Typed<bool>(BoolName).DefaultValue, BoolName);
            Equal("logs", Typed<string>(StringName).DefaultValue, StringName);
            var v = Typed<Vector>(VectorName).DefaultValue;
            Expect(Near(75, v.X) && Near(127, v.Y) && Near(155, v.Z), $"{VectorName} default {v}");
        });

        t.Test("HasDefaultValue / TryGetDefaultValue agree with DefaultValue", () =>
        {
            var c = Typed<int>(IntName);
            Expect(c.HasDefaultValue, "HasDefaultValue");
            Expect(c.TryGetDefaultValue(out var d) && d == c.DefaultValue, "TryGetDefaultValue");
            Expect(c.TryGetDefaultValueAsString(out var s) && s == c.DefaultValueAsString, "TryGetDefaultValueAsString");
        });

        t.Test("Typed Value and ValueAsString describe the same value", () =>
        {
            Equal(Typed<int>(IntName).Value.ToString(), cv.FindAsString(IntName)!.ValueAsString, IntName);
            Equal(Typed<string>(StringName).Value, cv.FindAsString(StringName)!.ValueAsString, StringName);
            Expect(float.Parse(cv.FindAsString(FloatName)!.ValueAsString, System.Globalization.CultureInfo.InvariantCulture) is var f && Near(f, Typed<float>(FloatName).Value), FloatName);
        });

        t.Test("Value of an untouched convar equals its default", () =>
        {
            var c = Typed<int>(IntName);
            if (c.Value != c.DefaultValue) Skip($"{IntName} is set to {c.Value} on this server");
        });

        t.Test("The same convar found twice reflects the same value", () =>
        {
            var a = Typed<int>(IntName);
            var b = Typed<int>(IntName);
            Keep(a, () =>
            {
                a.SetInternal(11);
                Equal(11, b.Value, "second handle");
            });
        });

        t.Test("SetInternal on an int convar round-trips and restores", () =>
        {
            var c = Typed<int>(IntName);
            var original = c.Value;
            Keep(c, () =>
            {
                c.SetInternal(12);
                Equal(12, c.Value, "Value");
                Equal("12", c.ValueAsString, "ValueAsString");
                c.SetInternal(-5);
                Equal(-5, c.Value, "negative");
            });
            Equal(original, c.Value, "restored");
        });

        t.Test("SetInternal on a float convar round-trips and restores", () =>
        {
            var c = Typed<float>(FloatName);
            var original = c.Value;
            Keep(c, () =>
            {
                c.SetInternal(2.5f);
                Expect(Near(2.5f, c.Value), $"Value {c.Value}");
                Expect(Near(2.5f, float.Parse(c.ValueAsString, System.Globalization.CultureInfo.InvariantCulture)), $"ValueAsString {c.ValueAsString}");
            });
            Expect(Near(original, c.Value), "restored");
        });

        t.Test("SetInternal on a bool convar round-trips and restores", () =>
        {
            var c = Typed<bool>(BoolName);
            var original = c.Value;
            Keep(c, () =>
            {
                c.SetInternal(!original);
                Equal(!original, c.Value, "toggled");
                c.SetInternal(original);
                Equal(original, c.Value, "back");
            });
            Equal(original, c.Value, "restored");
        });

        t.Test("SetInternal on a string convar round-trips and restores", () =>
        {
            var c = Typed<string>(StringName);
            var original = c.Value;
            Keep(c, () =>
            {
                c.SetInternal("tester_logs");
                Equal("tester_logs", c.Value, "Value");
                Equal("tester_logs", c.ValueAsString, "ValueAsString");
                c.SetInternal("");
                Equal("", c.Value, "empty string");
            });
            Equal(original, c.Value, "restored");
        });

        t.Test("SetInternal on a vector convar round-trips and restores", () =>
        {
            var c = Typed<Vector>(VectorName);
            var original = c.Value;
            Keep(c, () =>
            {
                c.SetInternal(new Vector(1, 2, 3));
                var v = c.Value;
                Expect(Near(1, v.X) && Near(2, v.Y) && Near(3, v.Z), $"Value {v}");
            });
            var back = c.Value;
            Expect(Near(original.X, back.X) && Near(original.Y, back.Y) && Near(original.Z, back.Z), $"restored {back}");
        });

        t.Test("SetInternalAsString parses into the typed value", () =>
        {
            var c = Typed<int>(IntName);
            Keep(c, () =>
            {
                c.SetInternalAsString("21");
                Equal(21, c.Value, "Value");
            });
        });

        await t.TestAsync("Value setter changes the convar and restores", async () =>
        {
            var c = Typed<int>(IntName);
            var original = c.Value;
            try
            {
                c.Value = 17;
                Expect(await t.Until(() => c.Value == 17, 64), $"Value stayed {c.Value}");
            }
            finally
            {
                c.SetInternal(original);
                await t.Ticks(2);
            }
            Equal(original, c.Value, "restored");
        });

        await t.TestAsync("ValueAsString setter changes the convar and restores", async () =>
        {
            var cvar = cv.FindAsString(IntName);
            Need(IntName);
            var original = cvar!.ValueAsString;
            try
            {
                cvar.ValueAsString = "19";
                Expect(await t.Until(() => cvar.ValueAsString == "19", 64), $"ValueAsString stayed {cvar.ValueAsString}");
            }
            finally
            {
                cvar.SetInternalAsString(original);
                await t.Ticks(2);
            }
            Equal(original, cvar.ValueAsString, "restored");
        });

        t.Test("ValueAsString with an unparsable value throws ArgumentException and keeps the value", () =>
        {
            Need(IntName);
            var cvar = cv.FindAsString(IntName)!;
            var before = cvar.ValueAsString;
            Throws<ArgumentException>(() => cvar.ValueAsString = "not_a_number");
            Equal(before, cvar.ValueAsString, "value after failed set");
        });

        t.Test("CreateOrFind creates a convar of every supported type and finds it again", () =>
        {
            void Round<T>( string suffix, T value, T other, Func<T, T, bool> same )
            {
                var name = "tester_cvar_" + suffix;
                var c = cv.CreateOrFind(name, $"Tester convar {suffix}", value, ConvarFlags.NONE);
                Equal(name, c.Name, "Name");
                Equal($"Tester convar {suffix}", c.HelpText, "HelpText");
                Expect(c.HasDefaultValue, $"{suffix}: HasDefaultValue");
                Expect(same(value, c.DefaultValue), $"{suffix}: DefaultValue {c.DefaultValue}");
                var original = c.Value;
                try
                {
                    c.SetInternal(other);
                    Expect(same(other, c.Value), $"{suffix}: Value {c.Value}");
                }
                finally { c.SetInternal(original); }
                Expect(cv.Find<T>(name) is not null, $"{suffix}: Find<T> after creation");
                Expect(cv.FindAsString(name) is not null, $"{suffix}: FindAsString after creation");
            }

            Round("bool", true, false, ( a, b ) => a == b);
            Round("short", (short)7, (short)-8, ( a, b ) => a == b);
            Round("ushort", (ushort)9, (ushort)65_000, ( a, b ) => a == b);
            Round("int", 13, -14, ( a, b ) => a == b);
            Round("uint", 15u, 4_000_000_000u, ( a, b ) => a == b);
            Round("long", 17L, -9_000_000_000_000L, ( a, b ) => a == b);
            Round("ulong", 19UL, 18_000_000_000_000_000_000UL, ( a, b ) => a == b);
            Round("float", 1.5f, -2.25f, ( a, b ) => Near(a, b));
            Round("double", 1.5d, -2.25d, ( a, b ) => Math.Abs(a - b) < 1e-9);
            Round("string", "default text", "changed text", ( a, b ) => a == b);
            Round("vector", new Vector(1, 2, 3), new Vector(4, 5, 6), ( a, b ) => Near(a.X, b.X) && Near(a.Y, b.Y) && Near(a.Z, b.Z));
            Round("qangle", new QAngle(10, 20, 30), new QAngle(40, 50, 60), ( a, b ) => Near(a.Pitch, b.Pitch) && Near(a.Yaw, b.Yaw) && Near(a.Roll, b.Roll));
        });

        t.Test("CreateOrFind twice returns a convar with the same value", () =>
        {
            var a = cv.CreateOrFind("tester_cvar_twice", "Tester twice", 5);
            var b = cv.CreateOrFind("tester_cvar_twice", "Tester twice", 99);
            var original = a.Value;
            try
            {
                a.SetInternal(6);
                Equal(6, b.Value, "second handle");
                Equal(5, b.DefaultValue, "default from the first creation");
            }
            finally { a.SetInternal(original); }
        });

        t.Test("Create on an existing convar throws", () =>
        {
            Need(BoolName);
            Throws<Exception>(() => cv.Create(BoolName, "dup", false));
            _ = cv.CreateOrFind("tester_cvar_exists", "exists", 1);
            Throws<Exception>(() => cv.Create("tester_cvar_exists", "dup", 1));
        });

        t.Test("CreateOrFind with an existing name but another type throws", () =>
        {
            _ = cv.CreateOrFind("tester_cvar_typed", "typed", 1);
            Throws<Exception>(() => cv.CreateOrFind("tester_cvar_typed", "typed", "text"));
        });

        t.Test("Created convar keeps its flags", () =>
        {
            var c = cv.CreateOrFind("tester_cvar_flags", "flags", 1, ConvarFlags.HIDDEN | ConvarFlags.DONTRECORD);
            Expect((c.Flags & (ConvarFlags.HIDDEN | ConvarFlags.DONTRECORD)) == (ConvarFlags.HIDDEN | ConvarFlags.DONTRECORD), $"flags {c.Flags}");
        });

        t.Test("Flags setter changes the flags and restores", () =>
        {
            var c = cv.CreateOrFind("tester_cvar_flags2", "flags2", 1, ConvarFlags.NONE);
            var original = c.Flags;
            try
            {
                c.Flags = original | ConvarFlags.HIDDEN;
                Expect((c.Flags & ConvarFlags.HIDDEN) != 0, "HIDDEN not set");
            }
            finally { c.Flags = original; }
            Equal(original, c.Flags, "restored");
        });

        t.Test("CreateOrFind with a range exposes min and max", () =>
        {
            var c = cv.CreateOrFind("tester_cvar_range", "range", 5, 1, 10);
            Expect(c.HasMinValue && c.HasMaxValue, "HasMin/HasMax");
            Equal(1, c.MinValue, "MinValue");
            Equal(10, c.MaxValue, "MaxValue");
            Expect(c.TryGetMinValue(out var min) && min == 1, "TryGetMinValue");
            Expect(c.TryGetMaxValue(out var max) && max == 10, "TryGetMaxValue");
            Expect(c.TryGetMinValueAsString(out var smin) && smin == "1", "TryGetMinValueAsString");
            Expect(c.TryGetMaxValueAsString(out var smax) && smax == "10", "TryGetMaxValueAsString");
        });

        t.Test("MinValue and MaxValue setters round-trip and restore", () =>
        {
            var c = cv.CreateOrFind("tester_cvar_range", "range", 5, 1, 10);
            var (min, max) = (c.MinValue, c.MaxValue);
            try
            {
                c.MinValue = 2;
                c.MaxValue = 9;
                Equal(2, c.MinValue, "MinValue");
                Equal(9, c.MaxValue, "MaxValue");
            }
            finally
            {
                c.MinValue = min;
                c.MaxValue = max;
            }
        });

        t.Test("A convar without a range has no min or max", () =>
        {
            var c = cv.CreateOrFind("tester_cvar_norange", "no range", 5);
            Expect(!c.HasMinValue && !c.HasMaxValue, "range present");
            Expect(!c.TryGetMinValue(out _) && !c.TryGetMaxValue(out _), "TryGet succeeded");
            Throws<Exception>(() => _ = c.MinValue);
            Throws<Exception>(() => _ = c.MaxValue);
        });

        t.Test("String convars have no range", () =>
        {
            Need(StringName);
            var c = Typed<string>(StringName);
            Expect(!c.TryGetMinValue(out _) && !c.TryGetMaxValue(out _), "range on a string convar");
            Throws<InvalidOperationException>(() => _ = c.MinValue);
        });

        t.Test("DefaultValue setter changes and restores the default", () =>
        {
            var c = cv.CreateOrFind("tester_cvar_default", "default", 5);
            var original = c.DefaultValue;
            try
            {
                c.DefaultValue = 8;
                Equal(8, c.DefaultValue, "DefaultValue");
            }
            finally { c.DefaultValue = original; }
        });

        t.Test("ReplicateToClient to the caller does not throw", () =>
        {
            var c = cv.CreateOrFind("tester_cvar_replicate", "replicate", 1, ConvarFlags.REPLICATED);
            if (t.Caller.IsFakeClient) Skip("caller is a bot");
            c.ReplicateToClient(t.Caller.Slot, 2);
            c.ReplicateToClientAsString(t.Caller.Slot, "1");
            cv.ReplicateToClient(t.Caller.Slot, "tester_cvar_replicate", "1");
        });

        t.Test("ReplicateToAll", () =>
        {
            var others = Core.PlayerManager.GetAllValidPlayers().Any(p => !p.IsFakeClient && p.Slot != t.Caller.Slot);
            if (others) Skip("other human players are connected");
            cv.ReplicateToAll("tester_cvar_replicate", "1");
        });

        await t.TestAsync("QueryClient returns the caller's value", async () =>
        {
            if (t.Caller.IsFakeClient) Skip("caller is a bot");
            string? answer = null;
            cv.QueryClient(t.Caller.Slot, "m_yaw", v => answer = v);
            if (!await t.Until(() => answer is not null, 256)) Skip("client did not answer a convar query");
            Expect(float.TryParse(answer, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var yaw) && yaw > 0, $"m_yaw = '{answer}'");
        });

        await t.TestAsync("Several queries for one convar all get an answer", async () =>
        {
            if (t.Caller.IsFakeClient) Skip("caller is a bot");
            var answers = new List<string>();
            for (var i = 0; i < 3; i++) cv.QueryClient(t.Caller.Slot, "m_yaw", v => { lock (answers) answers.Add(v); });
            if (!await t.Until(() => { lock (answers) return answers.Count > 0; }, 256)) Skip("client did not answer a convar query");
            Expect(await t.Until(() => { lock (answers) return answers.Count == 3; }, 64), $"{answers.Count} of 3 callbacks");
            Expect(answers.Distinct().Count() == 1, $"answers differ: {string.Join(" | ", answers)}");
        });

        await t.TestAsync("QueryClient for a second client convar", async () =>
        {
            if (t.Caller.IsFakeClient) Skip("caller is a bot");
            string? answer = null;
            cv.QueryClient(t.Caller.Slot, "sensitivity", v => answer = v);
            if (!await t.Until(() => answer is not null, 256)) Skip("client did not answer a convar query");
            Expect(!string.IsNullOrEmpty(answer), "empty answer");
        });
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var cv = Cv;
        if (cv.FindAsString(BoolName) is null) return Task.CompletedTask;

        var asInt = cv.Find<int>(IntName)!;
        var asFloat = cv.Find<float>(FloatName)!;
        var asBool = cv.Find<bool>(BoolName)!;
        var asString = cv.Find<string>(StringName)!;
        var asVector = cv.Find<Vector>(VectorName)!;
        var untyped = cv.FindAsString(IntName)!;
        var mine = cv.CreateOrFind("tester_cvar_profile", "profile", 1);
        var originalInt = p.OnGame(() => asInt.Value);

        try
        {
            p.ProfileOnGame("ConVar.FindAsString (hit)", () => _ = cv.FindAsString(IntName), 20_000);
            p.ProfileOnGame("ConVar.FindAsString (miss)", () => _ = cv.FindAsString("tester_cvar_no_such_convar"), 50_000);
            p.ProfileOnGame("ConVar.Find<int> (hit)", () => _ = cv.Find<int>(IntName), 20_000);
            p.ProfileOnGame("ConVar.Find<string> (hit)", () => _ = cv.Find<string>(StringName), 20_000);
            p.ProfileOnGame("ConVar.CreateOrFind (existing)", () => _ = cv.CreateOrFind("tester_cvar_profile", "profile", 1), 20_000);

            p.ProfileOnGame("IConVar<int>.Value (get)", () => _ = asInt.Value, 200_000);
            p.ProfileOnGame("IConVar<float>.Value (get)", () => _ = asFloat.Value, 200_000);
            p.ProfileOnGame("IConVar<bool>.Value (get)", () => _ = asBool.Value, 200_000);
            p.ProfileOnGame("IConVar<string>.Value (get)", () => _ = asString.Value, 100_000);
            p.ProfileOnGame("IConVar<Vector>.Value (get)", () => _ = asVector.Value, 200_000);
            p.ProfileOnGame("IConVar.ValueAsString (get)", () => _ = untyped.ValueAsString, 100_000);
            p.ProfileOnGame("IConVar.HelpText", () => _ = untyped.HelpText, 100_000);
            p.ProfileOnGame("IConVar.Flags (get)", () => _ = untyped.Flags, 200_000);
            p.ProfileOnGame("IConVar<int>.DefaultValue", () => _ = asInt.DefaultValue, 100_000);
            p.ProfileOnGame("IConVar.HasDefaultValue", () => _ = untyped.HasDefaultValue, 100_000);

            p.ProfileOnGame("IConVar<int>.SetInternal", () => mine.SetInternal(2), 100_000);
            p.ProfileOnGame("IConVar.SetInternalAsString", () => ((IConVar)mine).SetInternalAsString("3"), 100_000);
            p.ProfileOnGame("IConVar<int>.Value (set, queued)", () => mine.Value = 4, 20_000);
        }
        finally
        {
            p.OnGame(() =>
            {
                asInt.SetInternal(originalInt);
                mine.SetInternal(1);
            });
        }

        return Task.CompletedTask;
    }
}

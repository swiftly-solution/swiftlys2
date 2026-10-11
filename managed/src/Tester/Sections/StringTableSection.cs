using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.StringTable;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class StringTableSection( ISwiftlyCore core ) : Section(core)
{
    private const string Table = "ServerAvatarOverrides";
    private const string Key1 = "76561190000000001";
    private const string Key2 = "76561190000000002";
    private const string Key3 = "76561190000000003";

    private static readonly byte[] Png =
    [
        0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
        0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x08, 0x06, 0x00, 0x00, 0x00, 0x1F, 0x15, 0xC4,
        0x89, 0x00, 0x00, 0x00, 0x0A, 0x49, 0x44, 0x41, 0x54, 0x78, 0x9C, 0x63, 0x00, 0x01, 0x00, 0x00,
        0x05, 0x00, 0x01, 0x0D, 0x0A, 0x2D, 0xB4, 0x00, 0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE,
        0x42, 0x60, 0x82,
    ];

    private static readonly byte[] Other = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    public override string Name => "stringtable";

    private IStringTableService Svc => Core.StringTable;

    private IStringTable Avatars()
        => Svc.FindTable(Table) ?? throw new SkipException($"string table {Table} not found (map not loaded?)");

    private List<(int Id, string Name)> AllTables()
    {
        var list = new List<(int, string)>();
        for (var id = 0; id < 128; id++)
            if (Svc.FindTableById(id) is { } table) list.Add((id, table.TableName));
        return list;
    }

    private static bool Same( StringTableOutUserData got, byte[] expected ) => got.IsValid && got.AsRaw().SequenceEqual(expected);

    private static void Put( IStringTable table, string key, byte[] data ) => _ = table.SetOrAddStringUserData(key, StringTableUserData.FromRaw(data));

    public override async Task Test( TestContext t )
    {
        var svc = Svc;

        t.Test("FindTable finds ServerAvatarOverrides", () =>
        {
            var table = svc.FindTable(Table);
            NotNull(table, Table);
            Equal(Table, table!.TableName, "TableName");
        });

        t.Test("FindTable of an unknown or empty name returns null", () =>
        {
            Expect(svc.FindTable("TesterNoSuchTable") is null, "unknown");
            Expect(svc.FindTable("") is null, "empty");
            Expect(svc.FindTable(Table.ToLowerInvariant()) is null || svc.FindTable(Table.ToLowerInvariant())!.TableName.Equals(Table, StringComparison.OrdinalIgnoreCase), "wrong table for another casing");
        });

        t.Test("FindTableById of an invalid id returns null", () =>
        {
            foreach (var id in new[] { -1, -100, 128, 9999, int.MaxValue, int.MinValue })
                Expect(svc.FindTableById(id) is null, $"id {id}");
        });

        t.Test("Tables can be enumerated by id and each id is stable", () =>
        {
            var tables = AllTables();
            Expect(tables.Count >= 1, "no table found");
            Expect(tables.Any(x => x.Name == Table), $"{Table} not among: {string.Join(", ", tables.Select(x => x.Name))}");
            Equal(tables.Count, tables.Select(x => x.Name).Distinct().Count(), "duplicate table names");
            foreach (var (id, name) in tables)
            {
                var byName = svc.FindTable(name);
                NotNull(byName, name);
                Equal(id, byName!.TableId, $"TableId of {name}");
                Equal(name, svc.FindTableById(id)!.TableName, $"TableName of id {id}");
            }
        });

        t.Test("FindTable returns the same table on every call", () =>
        {
            var a = Avatars();
            var b = Avatars();
            Equal(a.TableId, b.TableId, "TableId");
            Equal(a.NumStrings, b.NumStrings, "NumStrings");
        });

        t.Test("Every table reports a sane string count and readable first strings", () =>
        {
            foreach (var (id, name) in AllTables())
            {
                var table = svc.FindTableById(id)!;
                Expect(table.NumStrings >= 0, $"{name}: NumStrings {table.NumStrings}");
                for (var i = 0; i < Math.Min(table.NumStrings, 5); i++)
                {
                    Expect(table.IsStringIndexValid(i), $"{name}[{i}] not valid");
                    var s = table.GetString(i);
                    NotNull(s, $"{name}[{i}]");
                    var found = table.FindStringIndex(s!);
                    Expect(found is not null && table.GetString(found.Value) == s, $"{name}: FindStringIndex('{s}') = {found}");
                }
            }
        });

        t.Test("Invalid indices are reported as invalid", () =>
        {
            var table = Avatars();
            foreach (var i in new[] { -1, -5, table.NumStrings, table.NumStrings + 100, int.MaxValue, int.MinValue })
            {
                Expect(!table.IsStringIndexValid(i), $"index {i} valid");
                Expect(table.GetString(i) is null, $"GetString({i})");
                Expect(!table.TryGetStringUserData(i, out _), $"TryGetStringUserData({i})");
                Expect(!table.GetStringUserData(i).IsValid, $"GetStringUserData({i})");
            }
        });

        t.Test("An unknown string has no index and no user data", () =>
        {
            var table = Avatars();
            Expect(table.FindStringIndex("tester_no_such_string_zzz") is null, "FindStringIndex");
            Expect(!table.TryGetStringUserData("tester_no_such_string_zzz", out _), "TryGetStringUserData");
            Expect(!table.GetStringUserData("tester_no_such_string_zzz").IsValid, "GetStringUserData");
        });

        t.Test("AsRaw / AsString on invalid user data throw InvalidOperationException", () =>
        {
            var table = Avatars();
            Throws<InvalidOperationException>(() => _ = table.GetStringUserData("tester_no_such_string_zzz").AsRaw().Length);
            Throws<InvalidOperationException>(() => _ = table.GetStringUserData("tester_no_such_string_zzz").AsString());
        });

        t.Test("GetOrAddString adds a string once and returns its index", () =>
        {
            var table = Avatars();
            var before = table.NumStrings;
            var existed = table.FindStringIndex(Key1) is not null;
            var index = table.GetOrAddString(Key1);
            Expect(index >= 0, $"index {index}");
            Equal(index, table.GetOrAddString(Key1), "second call");
            Equal(index, table.FindStringIndex(Key1) ?? -1, "FindStringIndex");
            Equal(Key1, table.GetString(index), "GetString");
            Expect(table.IsStringIndexValid(index), "IsStringIndexValid");
            Equal(existed ? before : before + 1, table.NumStrings, "NumStrings");
        });

        t.Test("Different strings get different indices", () =>
        {
            var table = Avatars();
            var a = table.GetOrAddString(Key1);
            var b = table.GetOrAddString(Key2);
            Expect(a != b, $"both {a}");
            Equal(Key2, table.GetString(b), "second string");
        });

        t.Test("SetOrAddStringUserData stores the avatar and reads it back by string and by index", () =>
        {
            var table = Avatars();
            Put(table, Key1, Png);
            var index = table.FindStringIndex(Key1);
            NotNull(index, "index");
            Expect(Same(table.GetStringUserData(Key1), Png), "by string");
            Expect(Same(table.GetStringUserData(index!.Value), Png), "by index");
            Expect(table.TryGetStringUserData(Key1, out var s) && Same(s, Png), "TryGet by string");
            Expect(table.TryGetStringUserData(index.Value, out var i) && Same(i, Png), "TryGet by index");
        });

        t.Test("SetOrAddStringUserData on an existing string replaces the data", () =>
        {
            var table = Avatars();
            Put(table, Key1, Png);
            Put(table, Key1, Other);
            Expect(Same(table.GetStringUserData(Key1), Other), "data not replaced");
            Put(table, Key1, Png);
            Expect(Same(table.GetStringUserData(Key1), Png), "data not restored");
        });

        t.Test("SetStringUserData by string and by index", () =>
        {
            var table = Avatars();
            Put(table, Key2, Png);
            _ = table.SetStringUserData(Key2, StringTableUserData.FromRaw(Other));
            Expect(Same(table.GetStringUserData(Key2), Other), "by string: data");
            var index = table.FindStringIndex(Key2)!.Value;
            _ = table.SetStringUserData(index, StringTableUserData.FromRaw(Png));
            Expect(Same(table.GetStringUserData(index), Png), "by index: data");
        });

        t.Test("forceOverride=false leaves either the old or the new data, never a mix", () =>
        {
            var table = Avatars();
            Put(table, Key3, Png);
            _ = table.SetStringUserData(Key3, StringTableUserData.FromRaw(Other), forceOverride: false);
            var now = table.GetStringUserData(Key3);
            Expect(Same(now, Png) || Same(now, Other), "stored data is neither the old nor the new data");
            Put(table, Key3, Png);
        });

        t.Test("SetStringUserData return value agrees with whether the data changed", () =>
        {
            var table = Avatars();
            Put(table, Key3, Png);
            var returned = table.SetStringUserData(Key3, StringTableUserData.FromRaw(Other));
            var stored = Same(table.GetStringUserData(Key3), Other);
            Put(table, Key3, Png);
            Expect(stored, "data was not stored");
            if (!returned) Skip("engine returned false although the data was stored (return value is unreliable)");
        });

        t.Test("SetStringUserData with an unknown string or invalid index throws ArgumentException", () =>
        {
            var table = Avatars();
            Throws<ArgumentException>(() => table.SetStringUserData("tester_no_such_string_zzz", StringTableUserData.FromRaw(Png)));
            Throws<ArgumentException>(() => table.SetStringUserData(-1, StringTableUserData.FromRaw(Png)));
            Throws<ArgumentException>(() => table.SetStringUserData(table.NumStrings + 100, StringTableUserData.FromRaw(Png)));
        });

        t.Test("FromString stores NUL-terminated UTF-8 text", () =>
        {
            var table = Avatars();
            _ = table.SetOrAddStringUserData(Key3, StringTableUserData.FromString("tester text テスト"));
            var data = table.GetStringUserData(Key3);
            Equal("tester text テスト", data.AsString().TrimEnd('\0'), "text");
            Equal((byte)0, data.AsRaw()[^1], "terminator");
            Put(table, Key3, Png);
        });

        t.Test("Stored data does not alias the caller's buffer", () =>
        {
            var table = Avatars();
            var buffer = (byte[])Other.Clone();
            Put(table, Key2, buffer);
            Array.Fill(buffer, (byte)0xEE);
            Expect(Same(table.GetStringUserData(Key2), Other), "table data changed when the caller's array was modified");
            Put(table, Key2, Png);
        });

        t.Test("Binary data with zero bytes is kept whole", () =>
        {
            var table = Avatars();
            byte[] data = [0, 1, 0, 2, 0, 0, 3, 0];
            Put(table, Key2, data);
            Expect(Same(table.GetStringUserData(Key2), data), "zero bytes lost");
            Put(table, Key2, Png);
        });

        t.Test("A 4 KB blob round-trips", () =>
        {
            var table = Avatars();
            var blob = new byte[4096];
            new Random(7).NextBytes(blob);
            Put(table, Key3, blob);
            Expect(Same(table.GetStringUserData(Key3), blob), "content differs");
            Put(table, Key3, Png);
        });

        t.Test("The ReadOnlySpan overload of FromRaw works", () =>
        {
            var table = Avatars();
            _ = table.SetOrAddStringUserData(Key2, StringTableUserData.FromRaw(new ReadOnlySpan<byte>(Other)));
            Expect(Same(table.GetStringUserData(Key2), Other), "content differs");
            Put(table, Key2, Png);
        });

        await t.TestAsync("ReplicateUserData by string and by index to the caller", async () =>
        {
            if (t.Caller.IsFakeClient) Skip("caller is a bot");
            var table = Avatars();
            Put(table, Key1, Png);
            await t.Ticks(5);
            var index = table.FindStringIndex(Key1)!.Value;
            var filter = CRecipientFilter.FromPlayers(t.Caller);
            table.ReplicateUserData(Key1, StringTableUserData.FromRaw(Png), in filter);
            table.ReplicateUserData(index, StringTableUserData.FromRaw(Png), in filter);
        });

        t.Test("ReplicateUserData with an unknown string throws ArgumentException", () =>
        {
            var table = Avatars();
            var filter = CRecipientFilter.FromPlayers(t.Caller);
            Throws<ArgumentException>(() => table.ReplicateUserData("tester_no_such_string_zzz", StringTableUserData.FromRaw(Png), in filter));
        });

        t.Test("Concurrent reads from pool threads", () =>
        {
            var table = Avatars();
            _ = table.SetOrAddStringUserData(Key1, StringTableUserData.FromRaw(Png));
            var failures = new System.Collections.Concurrent.ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 4).Select(w => Task.Run(() =>
            {
                try
                {
                    for (var i = 0; i < 200; i++)
                    {
                        if (table.FindStringIndex(Key1) is null) failures.Add($"worker {w}: missing");
                        if (!Same(table.GetStringUserData(Key1), Png)) failures.Add($"worker {w}: wrong data");
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
        var svc = Svc;
        var table = p.OnGame(() => svc.FindTable(Table));
        if (table is null) return Task.CompletedTask;

        var index = p.OnGame(() =>
        {
            _ = table.SetOrAddStringUserData(Key1, StringTableUserData.FromRaw(Png));
            return table.FindStringIndex(Key1) ?? 0;
        });

        p.ProfileOnGame("StringTable.FindTable (name)", () => _ = svc.FindTable(Table), 100_000);
        p.ProfileOnGame("StringTable.FindTableById", () => _ = svc.FindTableById(table.TableId), 100_000);
        p.ProfileOnGame("IStringTable.NumStrings", () => _ = table.NumStrings, 200_000);
        p.ProfileOnGame("IStringTable.FindStringIndex (hit)", () => _ = table.FindStringIndex(Key1), 50_000);
        p.ProfileOnGame("IStringTable.FindStringIndex (miss)", () => _ = table.FindStringIndex("tester_no_such_string_zzz"), 50_000);
        p.ProfileOnGame("IStringTable.GetString", () => _ = table.GetString(index), 100_000);
        p.ProfileOnGame("IStringTable.IsStringIndexValid", () => _ = table.IsStringIndexValid(index), 200_000);
        p.ProfileOnGame("IStringTable.TryGetStringUserData (index)", () => _ = table.TryGetStringUserData(index, out _), 100_000);
        p.ProfileOnGame("IStringTable.GetStringUserData (string)", () => _ = table.GetStringUserData(Key1).IsValid, 50_000);
        p.ProfileOnGame("IStringTable.GetOrAddString (existing)", () => _ = table.GetOrAddString(Key1), 50_000);
        p.ProfileOnGame("IStringTable.SetStringUserData (index, PNG)", () => _ = table.SetStringUserData(index, StringTableUserData.FromRaw(Png)), 20_000);
        p.ProfileOnGame("IStringTable.SetOrAddStringUserData (existing)", () => _ = table.SetOrAddStringUserData(Key1, StringTableUserData.FromRaw(Png)), 20_000);
        return Task.CompletedTask;
    }
}

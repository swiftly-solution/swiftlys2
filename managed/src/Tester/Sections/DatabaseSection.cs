using System.Data;
using System.Data.Common;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Database;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class DatabaseSection( ISwiftlyCore core ) : Section(core)
{
    private const string Unknown = "tester_no_such_connection_zzz";

    public override string Name => "database";

    private IDatabaseService Db => Core.Database;

    private bool Configured => !string.IsNullOrEmpty(Db.GetConnectionInfo("").Driver);

    private void NeedDb()
    {
        if (!Configured) Skip("no database connection configured (database.jsonc)");
    }

    private static DbCommand Cmd( DbConnection c, string sql, DbTransaction? tx = null, params (string Name, object? Value)[] args )
    {
        var cmd = c.CreateCommand();
        cmd.CommandText = sql;
        cmd.Transaction = tx;
        foreach (var (name, value) in args)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(p);
        }
        return cmd;
    }

    private static async Task<long> Scalar( DbConnection c, string sql, DbTransaction? tx = null, params (string, object?)[] args )
    {
        await using var cmd = Cmd(c, sql, tx, args);
        return Convert.ToInt64(await cmd.ExecuteScalarAsync());
    }

    private static async Task<int> Exec( DbConnection c, string sql, DbTransaction? tx = null, params (string, object?)[] args )
    {
        await using var cmd = Cmd(c, sql, tx, args);
        return await cmd.ExecuteNonQueryAsync();
    }

    private static async Task ExpectThrows( Func<Task> body )
    {
        try { await body(); }
        catch (Exception) { return; }
        throw new Exception("expected an exception but nothing was thrown");
    }

    private async Task<DbConnection> OpenAsync( string name = "" )
    {
        var conn = (DbConnection)Db.GetConnection(name);
        await conn.OpenAsync();
        return conn;
    }

    private async Task<(DbConnection Conn, string Table)> OpenTemp()
    {
        NeedDb();
        var conn = await OpenAsync();
        var table = "tester_" + Guid.NewGuid().ToString("N")[..12];
        var columns = "(id INTEGER NOT NULL, name VARCHAR(64) NULL, n INTEGER NULL)";
        _ = await Exec(conn, Db.GetConnectionInfo("").Driver == "mysql" ? $"CREATE TEMPORARY TABLE {table} {columns}" : $"CREATE TEMP TABLE {table} {columns}");
        return (conn, table);
    }

    public override Task Test( TestContext t ) => Task.Run(() => RunTests(t));

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p ) => RunProfile(p);

    private async Task RunTests( TestContext t )
    {
        t.Test("ConnectionInfo sqlite string", () =>
            Equal("Data Source=data.db", new DatabaseConnectionInfo("sqlite", "", "data.db", "", "", 0, 0, "").ToString(), "sqlite"));

        t.Test("ConnectionInfo mysql string uses port 3306 by default and timeout when set", () =>
        {
            Equal("Server=h;Port=3306;Database=d;User ID=u;Password=p", new DatabaseConnectionInfo("mysql", "h", "d", "u", "p", 0, 0, "").ToString(), "default port, no timeout");
            Equal("Server=h;Port=3307;Database=d;User ID=u;Password=p;Timeout=9", new DatabaseConnectionInfo("mysql", "h", "d", "u", "p", 9, 3307, "").ToString(), "custom port + timeout");
        });

        t.Test("ConnectionInfo postgresql string uses port 5432 by default", () =>
            Equal("Server=h;Port=5432;Database=d;User ID=u;Password=p", new DatabaseConnectionInfo("postgresql", "h", "d", "u", "p", 0, 0, "").ToString(), "postgresql"));

        t.Test("ConnectionInfo RawUri wins over every other field", () =>
            Equal("raw://x", new DatabaseConnectionInfo("mysql", "h", "d", "u", "p", 5, 1, "raw://x").ToString(), "RawUri"));

        t.Test("ConnectionInfo unknown driver renders a URI", () =>
            Equal("foo://u:p@h:7/d", new DatabaseConnectionInfo("foo", "h", "d", "u", "p", 0, 7, "").ToString(), "uri"));

        t.Test("ConnectionInfo options are appended and quoted when needed", () =>
        {
            var opts = new Dictionary<string, string> { ["Pooling"] = "true", ["Application Name"] = "a b;c" };
            var s = new DatabaseConnectionInfo("sqlite", "", "x.db", "", "", 0, 0, "") { Options = opts }.ToString();
            Expect(s.StartsWith("Data Source=x.db;"), $"prefix: {s}");
            Expect(s.Contains(";Pooling=true"), $"plain option: {s}");
            Expect(s.Contains(";Application Name=\"a b;c\""), $"quoted option: {s}");
        });

        t.Test("ConnectionInfo Options defaults to empty (never null)", () =>
            Expect(new DatabaseConnectionInfo("sqlite", "", "x.db", "", "", 0, 0, "").Options.Count == 0, "not empty"));

        t.Test("Unknown connection names resolve to the default connection", () =>
        {
            var a = Db.GetConnectionInfo(Unknown);
            var b = Db.GetConnectionInfo(Unknown + "2");
            var d = Db.GetConnectionInfo("");
            Equal(d.Driver, a.Driver, "Driver");
            Equal(d.Database, a.Database, "Database");
            Equal(d.Host, a.Host, "Host");
            Equal(d.Driver, b.Driver, "Driver (2)");
        });

        t.Test("GetConnectionString equals GetConnectionInfo(...).ToString()", () =>
        {
            NeedDb();
            Equal(Db.GetConnectionInfo("").ToString(), Db.GetConnectionString(""), "string");
            Equal(Db.GetConnectionString(""), Db.GetConnectionString(Unknown), "fallback string");
        });

        t.Test("Default connection uses a supported driver", () =>
        {
            NeedDb();
            Expect(Db.GetConnectionInfo("").Driver is "sqlite" or "mysql" or "postgresql", $"driver '{Db.GetConnectionInfo("").Driver}'");
        });

        t.Test("GetConnection without a configured connection throws NotSupportedException", () =>
        {
            if (Configured) Skip("a connection is configured");
            Throws<NotSupportedException>(() => Db.GetConnection(""));
        });

        t.Test("GetConnection returns a new closed connection each call", () =>
        {
            NeedDb();
            using var a = Db.GetConnection("");
            using var b = Db.GetConnection("");
            Expect(!ReferenceEquals(a, b), "same instance");
            Equal(ConnectionState.Closed, a.State, "state");
        });

        t.Test("Unknown name connects to the same database as the default", () =>
        {
            NeedDb();
            using var a = Db.GetConnection("");
            using var b = Db.GetConnection(Unknown);
            Equal(a.ConnectionString, b.ConnectionString, "ConnectionString");
        });

        await t.TestAsync("Open / Close / reopen", async () =>
        {
            NeedDb();
            await using var c = (DbConnection)Db.GetConnection("");
            await c.OpenAsync();
            Equal(ConnectionState.Open, c.State, "after Open");
            await c.CloseAsync();
            Equal(ConnectionState.Closed, c.State, "after Close");
            await c.OpenAsync();
            Equal(ConnectionState.Open, c.State, "after reopen");
        });

        await t.TestAsync("SELECT 1 round trip", async () =>
        {
            NeedDb();
            await using var c = await OpenAsync();
            Equal(1L, await Scalar(c, "SELECT 1"), "SELECT 1");
        });

        await t.TestAsync("INSERT / SELECT round trip with parameters", async () =>
        {
            var (c, tbl) = await OpenTemp();
            await using var lease = c;
            _ = await Exec(c, $"INSERT INTO {tbl} (id, name, n) VALUES (@id, @name, @n)", null, ("@id", 1), ("@name", "alice"), ("@n", 42));
            Equal(1L, await Scalar(c, $"SELECT COUNT(*) FROM {tbl}"), "count");
            await using var cmd = Cmd(c, $"SELECT name, n FROM {tbl} WHERE id = @id", null, ("@id", 1));
            await using var r = await cmd.ExecuteReaderAsync();
            Expect(await r.ReadAsync(), "no row");
            Equal("alice", r.GetString(0), "name");
            Equal(42, Convert.ToInt32(r.GetValue(1)), "n");
            Expect(!await r.ReadAsync(), "extra row");
        });

        await t.TestAsync("UPDATE and DELETE report affected rows", async () =>
        {
            var (c, tbl) = await OpenTemp();
            await using var lease = c;
            for (var i = 0; i < 5; i++) _ = await Exec(c, $"INSERT INTO {tbl} (id, name, n) VALUES (@id, 'x', 0)", null, ("@id", i));
            Equal(3, await Exec(c, $"UPDATE {tbl} SET n = 9 WHERE id < 3"), "updated");
            Equal(3L, await Scalar(c, $"SELECT COUNT(*) FROM {tbl} WHERE n = 9"), "n = 9");
            Equal(3, await Exec(c, $"DELETE FROM {tbl} WHERE n = 9"), "deleted");
            Equal(2L, await Scalar(c, $"SELECT COUNT(*) FROM {tbl}"), "remaining");
        });

        await t.TestAsync("NULL values round trip", async () =>
        {
            var (c, tbl) = await OpenTemp();
            await using var lease = c;
            _ = await Exec(c, $"INSERT INTO {tbl} (id, name, n) VALUES (@id, @name, @n)", null, ("@id", 1), ("@name", null), ("@n", null));
            await using var cmd = Cmd(c, $"SELECT name, n FROM {tbl} WHERE id = 1");
            await using var r = await cmd.ExecuteReaderAsync();
            Expect(await r.ReadAsync(), "no row");
            Expect(r.IsDBNull(0) && r.IsDBNull(1), "values are not NULL");
        });

        await t.TestAsync("Parameters neutralise SQL injection text", async () =>
        {
            var (c, tbl) = await OpenTemp();
            await using var lease = c;
            const string evil = "x'); DROP TABLE t; -- \"; ;";
            _ = await Exec(c, $"INSERT INTO {tbl} (id, name, n) VALUES (1, @name, 0)", null, ("@name", evil));
            await using var cmd = Cmd(c, $"SELECT name FROM {tbl} WHERE id = 1");
            Equal(evil, (string)(await cmd.ExecuteScalarAsync())!, "stored text");
            Equal(1L, await Scalar(c, $"SELECT COUNT(*) FROM {tbl}"), "table intact");
        });

        await t.TestAsync("Transaction commit keeps rows", async () =>
        {
            var (c, tbl) = await OpenTemp();
            await using var lease = c;
            await using (var tx = await c.BeginTransactionAsync())
            {
                _ = await Exec(c, $"INSERT INTO {tbl} (id, name, n) VALUES (1, 'a', 0)", tx);
                _ = await Exec(c, $"INSERT INTO {tbl} (id, name, n) VALUES (2, 'b', 0)", tx);
                await tx.CommitAsync();
            }
            Equal(2L, await Scalar(c, $"SELECT COUNT(*) FROM {tbl}"), "count");
        });

        await t.TestAsync("Transaction rollback discards rows", async () =>
        {
            var (c, tbl) = await OpenTemp();
            await using var lease = c;
            await using (var tx = await c.BeginTransactionAsync())
            {
                _ = await Exec(c, $"INSERT INTO {tbl} (id, name, n) VALUES (1, 'a', 0)", tx);
                await tx.RollbackAsync();
            }
            Equal(0L, await Scalar(c, $"SELECT COUNT(*) FROM {tbl}"), "count");
        });

        await t.TestAsync("Batch insert of 500 rows in a transaction", async () =>
        {
            var (c, tbl) = await OpenTemp();
            await using var lease = c;
            await using (var tx = await c.BeginTransactionAsync())
            {
                await using var cmd = Cmd(c, $"INSERT INTO {tbl} (id, name, n) VALUES (@id, 'b', @n)", tx, ("@id", 0), ("@n", 0));
                var pid = cmd.Parameters[0];
                var pn = cmd.Parameters[1];
                for (var i = 0; i < 500; i++)
                {
                    pid.Value = i;
                    pn.Value = i * 2;
                    _ = await cmd.ExecuteNonQueryAsync();
                }
                await tx.CommitAsync();
            }
            Equal(500L, await Scalar(c, $"SELECT COUNT(*) FROM {tbl}"), "count");
            Equal(998L, await Scalar(c, $"SELECT MAX(n) FROM {tbl}"), "max");
        });

        await t.TestAsync("Bad SQL throws and the connection stays usable", async () =>
        {
            NeedDb();
            await using var c = await OpenAsync();
            await ExpectThrows(() => Exec(c, "SELEKT nonsense"));
            Equal(1L, await Scalar(c, "SELECT 1"), "after error");
        });

        await t.TestAsync("Parallel connections", async () =>
        {
            NeedDb();
            var results = await Task.WhenAll(Enumerable.Range(0, 8).Select(async _ =>
            {
                await using var c = await OpenAsync();
                return await Scalar(c, "SELECT 1");
            }));
            Expect(results.All(r => r == 1), "a worker returned a wrong result");
        });

        await t.TestAsync("Temp tables disappear with their connection", async () =>
        {
            var (c, tbl) = await OpenTemp();
            await c.DisposeAsync();
            await using var c2 = await OpenAsync();
            await ExpectThrows(() => Scalar(c2, $"SELECT COUNT(*) FROM {tbl}"));
        });
    }

    private async Task RunProfile( ProfileContext p )
    {
        var info = new DatabaseConnectionInfo("mysql", "h", "d", "u", "p", 9, 3307, "");
        var lite = new DatabaseConnectionInfo("sqlite", "", "x.db", "", "", 0, 0, "");

        p.Profile("ConnectionInfo.ToString (mysql)", () => _ = info.ToString(), 100_000);
        p.Profile("ConnectionInfo.ToString (sqlite)", () => _ = lite.ToString(), 100_000);
        p.Profile("Database.GetConnectionInfo(default)", () => _ = Db.GetConnectionInfo(""), 200_000);
        p.Profile("Database.GetConnectionInfo(unknown)", () => _ = Db.GetConnectionInfo(Unknown), 200_000);
        p.Profile("Database.GetConnectionString(default)", () => _ = Db.GetConnectionString(""), 100_000);

        if (!Configured) return;

        p.Profile("Database.GetConnection (create, not opened)", () => Db.GetConnection("").Dispose(), 20_000);

        await p.ProfileAsync("GetConnection + Open + Close", async () =>
        {
            await using var c = await OpenAsync();
        }, 500);

        var (conn, tbl) = await OpenTemp();
        await using (conn)
        {
            _ = await Exec(conn, $"INSERT INTO {tbl} (id, name, n) VALUES (1, 'row', 1)");
            await using var select1 = Cmd(conn, "SELECT 1");
            await using var selectRow = Cmd(conn, $"SELECT name, n FROM {tbl} WHERE id = @id", null, ("@id", 1));
            await using var insert = Cmd(conn, $"INSERT INTO {tbl} (id, name, n) VALUES (@id, 'p', 1)", null, ("@id", 0));
            var pid = insert.Parameters[0];
            var next = 100;

            await p.ProfileAsync("SELECT 1 (open connection, reused command)", async () => _ = await select1.ExecuteScalarAsync(), 1_000);
            await p.ProfileAsync("SELECT row by id (reader)", async () =>
            {
                await using var r = await selectRow.ExecuteReaderAsync();
                _ = await r.ReadAsync();
            }, 1_000);
            await p.ProfileAsync("INSERT 1 row (autocommit)", async () =>
            {
                pid.Value = next++;
                _ = await insert.ExecuteNonQueryAsync();
            }, 500);
            await p.ProfileAsync("INSERT 100 rows in one transaction", async () =>
            {
                await using var tx = await conn.BeginTransactionAsync();
                insert.Transaction = tx;
                for (var i = 0; i < 100; i++)
                {
                    pid.Value = next++;
                    _ = await insert.ExecuteNonQueryAsync();
                }
                await tx.CommitAsync();
                insert.Transaction = null;
            }, 50);
        }
    }
}

using System.Data;
using System.Data.SQLite;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Npgsql;
using MySqlConnector;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Database;

namespace SwiftlyS2.Core.Database;

internal class DatabaseService : IDatabaseService
{
    private readonly ILogger<DatabaseService> logger;
    private readonly ConcurrentDictionary<string, Func<IDbConnection>> connectionFactories = new();

    static DatabaseService()
    {
    }

    public DatabaseService( ILogger<DatabaseService> logger )
    {
        this.logger = logger;
        this.connectionFactories.Clear();
    }

    private string ResolveConnectionName( string connectionName )
    {
        return NativeDatabase.ConnectionExists(connectionName) ? connectionName : NativeDatabase.GetDefaultConnectionName();
    }

    private Func<IDbConnection> GetOrCreateConnectionFactory( string connectionName )
    {
        var resolvedName = ResolveConnectionName(connectionName);

        if (connectionFactories.TryGetValue(resolvedName, out var cached))
        {
            return cached;
        }

        try
        {
            var factory = CreateConnectionFactory(resolvedName);
            _ = connectionFactories.TryAdd(resolvedName, factory);
            return factory;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                throw;
            }

            logger.LogError(e, "Failed to create connection factory for '{ConnectionName}'", resolvedName);
            throw;
        }
    }

    private static Func<IDbConnection> CreateConnectionFactory( string connectionName )
    {
        var driver = NativeDatabase.GetConnectionDriver(connectionName);
        var host = NativeDatabase.GetConnectionHost(connectionName);
        var database = NativeDatabase.GetConnectionDatabase(connectionName);
        var user = NativeDatabase.GetConnectionUser(connectionName);
        var pass = NativeDatabase.GetConnectionPass(connectionName);
        var timeout = NativeDatabase.GetConnectionTimeout(connectionName);
        var port = NativeDatabase.GetConnectionPort(connectionName);

        return driver switch {
            "sqlite" => CreateSqliteFactory(database),
            "mysql" => CreateMySqlFactory(host, port, database, user, pass, timeout),
            "postgresql" => CreatePostgresFactory(host, port, database, user, pass, timeout),
            _ => throw new NotSupportedException($"Unsupported database driver: {driver}")
        };
    }

    private static Func<IDbConnection> CreateSqliteFactory( string database )
    {
        var connStr = $"Data Source={database}";
        return () => new SQLiteConnection(connStr);
    }

    private static Func<IDbConnection> CreateMySqlFactory( string host, ushort port, string database, string user, string pass, uint timeout )
    {
        var builder = new MySqlConnectionStringBuilder {
            Server = host,
            Port = port > 0 ? port : 3306u,
            Database = database,
            UserID = user,
            Password = pass
        };

        if (timeout > 0)
        {
            builder.ConnectionTimeout = timeout;
        }

        var connStr = builder.ConnectionString;
        return () => new MySqlConnection(connStr);
    }

    private static Func<IDbConnection> CreatePostgresFactory( string host, ushort port, string database, string user, string pass, uint timeout )
    {
        var builder = new NpgsqlConnectionStringBuilder {
            Host = host,
            Port = port > 0 ? port : 5432,
            Database = database,
            Username = user,
            Password = pass
        };

        if (timeout > 0)
        {
            builder.Timeout = (int)timeout;
        }

        var connStr = builder.ConnectionString;
        return () => new NpgsqlConnection(connStr);
    }

    public string GetConnectionString( string connectionName )
    {
        return GetConnectionInfo(connectionName).ToString();
    }

    public DatabaseConnectionInfo GetConnectionInfo( string connectionName )
    {
        var resolvedName = ResolveConnectionName(connectionName);
        return new DatabaseConnectionInfo(
            NativeDatabase.GetConnectionDriver(resolvedName),
            NativeDatabase.GetConnectionHost(resolvedName),
            NativeDatabase.GetConnectionDatabase(resolvedName),
            NativeDatabase.GetConnectionUser(resolvedName),
            NativeDatabase.GetConnectionPass(resolvedName),
            NativeDatabase.GetConnectionTimeout(resolvedName),
            NativeDatabase.GetConnectionPort(resolvedName),
            NativeDatabase.GetConnectionRawUri(resolvedName)
        );
    }

    public IDbConnection GetConnection( string connectionName )
    {
        return GetOrCreateConnectionFactory(connectionName)();
    }
}
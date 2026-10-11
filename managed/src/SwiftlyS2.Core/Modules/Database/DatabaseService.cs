using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Npgsql;
using MySqlConnector;
using SwiftlyS2.Shared.Database;

namespace SwiftlyS2.Core.Database;

internal class DatabaseService : IDatabaseService
{
    private readonly ILogger<DatabaseService> logger;
    private readonly DatabaseConnectionManager connectionManager;
    private readonly ConcurrentDictionary<string, Func<IDbConnection>> connectionFactories = new();

    static DatabaseService()
    {
    }

    public DatabaseService(ILogger<DatabaseService> logger, DatabaseConnectionManager connectionManager)
    {
        this.logger = logger;
        this.connectionManager = connectionManager;
        this.connectionFactories.Clear();
    }

    private string ResolveConnectionName(string connectionName)
    {
        return connectionManager.ConnectionExists(connectionName) ? connectionName : connectionManager.GetDefaultConnectionName();
    }

    private Func<IDbConnection> GetOrCreateConnectionFactory(string connectionName)
    {
        var resolvedName = ResolveConnectionName(connectionName);

        try
        {
            return connectionFactories.GetOrAdd(resolvedName, name => CreateConnectionFactory(connectionManager.GetConnectionInfo(name)));
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                throw;
            }

            logger.LogError(e, "Failed to create connection factory for '{ConnectionName}'", resolvedName);
            throw;
        }
    }

    private static Func<IDbConnection> CreateConnectionFactory(DatabaseConnectionInfo info)
    {
        return info.Driver switch
        {
            "sqlite" => CreateSqliteFactory(info),
            "mysql" => CreateMySqlFactory(info),
            "postgresql" => CreatePostgresFactory(info),
            _ => throw new NotSupportedException($"Unsupported database driver: {info.Driver}")
        };
    }

    private static Func<IDbConnection> CreateSqliteFactory(DatabaseConnectionInfo info)
    {
        var builder = new SQLiteConnectionStringBuilder
        {
            DataSource = info.Database
        };

        ApplyOptions(builder, info);

        var connStr = builder.ConnectionString;
        return () => new SQLiteConnection(connStr);
    }

    private static Func<IDbConnection> CreateMySqlFactory(DatabaseConnectionInfo info)
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Server = info.Host,
            Port = info.Port > 0 ? info.Port : 3306u,
            Database = info.Database,
            UserID = info.User,
            Password = info.Pass
        };

        if (info.Timeout > 0)
        {
            builder.ConnectionTimeout = info.Timeout;
        }

        ApplyOptions(builder, info);

        var connStr = builder.ConnectionString;
        return () => new MySqlConnection(connStr);
    }

    private static Func<IDbConnection> CreatePostgresFactory(DatabaseConnectionInfo info)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = info.Host,
            Port = info.Port > 0 ? info.Port : 5432,
            Database = info.Database,
            Username = info.User,
            Password = info.Pass
        };

        if (info.Timeout > 0)
        {
            builder.Timeout = (int)info.Timeout;
        }

        ApplyOptions(builder, info);

        var connStr = builder.ConnectionString;
        return () => new NpgsqlConnection(connStr);
    }

    private static void ApplyOptions(DbConnectionStringBuilder builder, DatabaseConnectionInfo info)
    {
        foreach (var (key, value) in info.Options)
        {
            try
            {
                builder[ResolveKeyword(builder, key)] = value;
                _ = builder.ConnectionString;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException($"Invalid option '{key}' = '{value}' for the {info.Driver} driver: {e.Message}", e);
            }
        }
    }

    private static string ResolveKeyword(DbConnectionStringBuilder builder, string key)
    {
        if (builder is not SQLiteConnectionStringBuilder)
        {
            return key;
        }

        var wanted = NormalizeKeyword(key);
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(builder))
        {
            if (property.IsBrowsable && (NormalizeKeyword(property.DisplayName) == wanted || NormalizeKeyword(property.Name) == wanted))
            {
                return property.DisplayName;
            }
        }

        throw new ArgumentException($"Option '{key}' not supported.");
    }

    private static string NormalizeKeyword(string keyword) => keyword.Replace(" ", "").Replace("_", "").ToLowerInvariant();

    public string GetConnectionString(string connectionName)
    {
        return GetConnectionInfo(connectionName).ToString();
    }

    public DatabaseConnectionInfo GetConnectionInfo(string connectionName)
    {
        return connectionManager.GetConnectionInfo(ResolveConnectionName(connectionName));
    }

    public IDbConnection GetConnection(string connectionName)
    {
        return GetOrCreateConnectionFactory(connectionName)();
    }
}
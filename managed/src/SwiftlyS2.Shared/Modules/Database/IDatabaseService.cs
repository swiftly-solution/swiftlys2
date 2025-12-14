using System.Data;

namespace SwiftlyS2.Shared.Database;

public record struct DatabaseConnectionInfo( string Driver, string Host, string Database, string User, string Pass, uint Timeout, ushort Port, string RawUri )
{
    public override readonly string ToString()
    {
        // If we have a raw URI, return it directly
        if (!string.IsNullOrWhiteSpace(RawUri))
        {
            return RawUri;
        }

        var port = Port > 0 ? Port : Driver switch {
            "mysql" => (ushort)3306,
            "postgresql" => (ushort)5432,
            _ => (ushort)0
        };

        return Driver switch {
            "sqlite" => $"Data Source={Database}",
            "mysql" or "postgresql" => $"Server={Host};Port={port};Database={Database};User ID={User};Password={Pass}" + (Timeout > 0 ? $";Timeout={Timeout}" : ""),
            _ => $"{Driver}://{User}:{Pass}@{Host}:{port}/{Database}"
        };
    }
}

public interface IDatabaseService
{
    /// <summary>
    /// Get the connection string for a given connection name.
    /// </summary>
    /// <param name="connectionName">The name of the connection to get the string for.</param>
    /// <returns>The connection string. Returns the default connection string if the connection name is not found.</returns>
    public string GetConnectionString( string connectionName );

    /// <summary>
    /// Get the connection info for a given connection name.
    /// </summary>
    /// <param name="connectionName">The name of the connection to get the info for.</param>
    /// <returns>The connection info. Returns the default connection info if the connection name is not found.</returns>
    public DatabaseConnectionInfo GetConnectionInfo( string connectionName );

    /// <summary>
    /// Get a connection to the database.
    /// </summary>
    /// <param name="connectionName">The name of the connection to get the connection for.</param>
    /// <returns>A connection to the database. Return the default connection if the connection name is not found.</returns>
    public IDbConnection GetConnection( string connectionName );
}
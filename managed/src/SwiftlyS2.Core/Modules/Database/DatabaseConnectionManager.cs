using System.Collections.Frozen;
using Microsoft.Extensions.Configuration;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Database;

namespace SwiftlyS2.Core.Database;

internal class DatabaseConnectionManager
{
    private readonly RootDirService rootDirService;
    private readonly string defaultConnectionName;
    private readonly IReadOnlyDictionary<string, DatabaseConnectionInfo> connections;

    public DatabaseConnectionManager( IConfiguration configuration, RootDirService rootDirService )
    {
        this.rootDirService = rootDirService;

        var defaultConnection = configuration["default_connection"] ?? "";
        var built = new Dictionary<string, DatabaseConnectionInfo>();

        foreach (var child in configuration.GetSection("connections").GetChildren())
        {
            var conn = child.Value != null
                ? ParseUri(child.Value)
                : ParseObject(child);

            if (conn is null)
            {
                continue;
            }

            built[child.Key] = conn.Value;

            if (defaultConnection.Length == 0)
            {
                defaultConnection = child.Key;
            }
        }

        connections = built;
        defaultConnectionName = defaultConnection;
    }

    public string GetDefaultConnectionName() => defaultConnectionName;

    public bool ConnectionExists( string connectionName ) => connections.ContainsKey(connectionName);

    public DatabaseConnectionInfo GetConnectionInfo( string connectionName )
    {
        return connections.TryGetValue(connectionName, out var conn) ? conn : default;
    }

    private static DatabaseConnectionInfo? ParseObject( IConfigurationSection section )
    {
        if (!section.GetChildren().Any())
        {
            return null;
        }

        var driver = section["driver"] ?? "mysql";
        var host = section["host"] ?? "localhost";
        var database = section["database"] ?? "";
        var user = section["user"] ?? "";
        var pass = section["pass"] ?? "";
        var timeout = uint.TryParse(section["timeout"], out var t) ? t : 0u;
        var port = ushort.TryParse(section["port"], out var p) ? p : (ushort)0;

        return new DatabaseConnectionInfo(driver, host, database, user, pass, timeout, port, "") {
            Options = ParseOptions(section.GetSection("options"))
        };
    }

    private static FrozenDictionary<string, string> ParseOptions( IConfigurationSection section )
    {
        return section.GetChildren()
            .Where(option => option.Value is not null)
            .ToFrozenDictionary(option => option.Key, option => option.Value!, StringComparer.OrdinalIgnoreCase);
    }

    private static ushort GetDefaultPort( string driver ) => driver switch
    {
        "mysql" or "mariadb" => 3306,
        "postgresql" or "postgres" => 5432,
        _ => 0
    };

    private DatabaseConnectionInfo ParseUri( string uri )
    {
        var protoEnd = uri.IndexOf("://", StringComparison.Ordinal);
        if (protoEnd == -1)
        {
            return new DatabaseConnectionInfo("", "", "", "", "", 0, 0, uri);
        }

        var driver = uri[..protoEnd];
        var rest = uri[(protoEnd + 3)..];

        if (driver == "sqlite")
        {
            return new DatabaseConnectionInfo("sqlite", "", ResolveSqlitePath(rest), "", "", 0, 0, uri);
        }

        string host = "", database = "", user = "", pass = "";
        ushort port = 0;

        var atPos = rest.LastIndexOf('@');
        string hostPart;
        if (atPos == -1)
        {
            hostPart = rest;
        }
        else
        {
            var credentials = rest[..atPos];
            var colonPos = credentials.IndexOf(':');
            if (colonPos != -1)
            {
                user = credentials[..colonPos];
                pass = credentials[(colonPos + 1)..];
            }
            else
            {
                user = credentials;
            }

            hostPart = rest[(atPos + 1)..];
        }

        var slashPos = hostPart.IndexOf('/');
        if (slashPos != -1)
        {
            var hostPort = hostPart[..slashPos];
            database = hostPart[(slashPos + 1)..];

            var portColonPos = hostPort.LastIndexOf(':');
            if (portColonPos != -1)
            {
                host = hostPort[..portColonPos];
                port = ushort.TryParse(hostPort[(portColonPos + 1)..], out var parsedPort) ? parsedPort : GetDefaultPort(driver);
            }
            else
            {
                host = hostPort;
                port = GetDefaultPort(driver);
            }
        }

        return new DatabaseConnectionInfo(driver, host, database, user, pass, 0, port, uri);
    }

    private string ResolveSqlitePath( string rest )
    {
        if (!rest.Contains('/'))
        {
            return Path.Combine(rootDirService.GetDataRoot(), rest);
        }

        var gameDir = NativeEngineHelpers.GetGameDirectoryPath();
        var gameFolder = NativeEngineHelpers.GetCurrentGame() == "cs2" ? "csgo" : "unknown";
        var gameRoot = Path.Combine(gameDir, gameFolder);

        return rest.Contains(gameRoot) ? rest : Path.Combine(gameRoot, rest);
    }
}

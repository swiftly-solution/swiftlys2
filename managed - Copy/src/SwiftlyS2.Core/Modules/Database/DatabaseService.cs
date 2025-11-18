using Dapper;
using System.Data.SQLite;
using SwiftlyS2.Core.Natives;
using System.Data;
using System.Collections.Concurrent;
using MySqlConnector;
using Npgsql;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Database;

namespace SwiftlyS2.Core.Database;

internal class DatabaseService : IDatabaseService
{

  private ILogger<DatabaseService> _Logger { get; init; }
  private CoreContext _Context { get; init; }

  private ConcurrentDictionary<string, Func<IDbConnection>> _connectionStrings = new ConcurrentDictionary<string, Func<IDbConnection>>();

  public DatabaseService( ILogger<DatabaseService> logger, CoreContext context )
  {
    _Logger = logger;
    _Context = context;
  }

  public string GetConnectionString( string connectionName )
  {
    if (NativeDatabase.ConnectionExists(connectionName))
    {
      return NativeDatabase.GetCredentials(connectionName);
    }
    return NativeDatabase.GetDefaultConnectionCredentials();
  }

  private Func<IDbConnection> ResolveConnectionString( string connectionString )
  {
    try
    {

      if (_connectionStrings.TryGetValue(connectionString, out var connectionFunc))
      {
        return connectionFunc;
      }

      var protocol = connectionString.Split("://")[0];
      var rest = connectionString.Split("://")[1];

      if (protocol == "sqlite")
      {
        var path = connectionString.Split("://")[1];
        return () => new SQLiteConnection($"Data Source={path}");
      }

      var credential = rest.Split("@")[0];
      rest = rest.Split("@")[1];

      var user = credential.Split(":")[0];
      var password = credential.Split(":")[1];

      var address = rest.Split("/")[0];
      var database = rest.Split("/")[1];

      var host = address.Split(":")[0];
      var port = address.Split(":")[1];


      if (protocol == "mysql")
      {
        return () => new MySqlConnection($"Server={host};Port={port};Database={database};User ID={user};Password={password};");
      }
      else if (protocol == "postgresql")
      {
        return () => new NpgsqlConnection($"Server={host};Port={port};Database={database};User ID={user};Password={password};");
      }

      throw new Exception($"Unsupported protocol: {protocol}");
    }
    catch (Exception e)
    {
      if (!GlobalExceptionHandler.Handle(e)) throw;
      _Logger.LogError(e, "Failed to resolve database credentials for {connectionString}! Please check your connection string format.", connectionString);
      throw;
    }
  }

  public IDbConnection GetConnection( string connectionName )
  {
    var connectionString = GetConnectionString(connectionName);
    return ResolveConnectionString(connectionString)();
  }
}
using System.Data;

namespace SwiftlyS2.Shared.Database;

public interface IDatabaseService {

  /// <summary>
  /// Get the connection string for a given connection name.
  /// </summary>
  /// <param name="connectionName">The name of the connection to get the connection string for.</param>
  /// <returns>The connection string for the given connection name. Return the default connection string if the connection name is not found.</returns>
  string GetConnectionString(string connectionName);

  /// <summary>
  /// Get a connection to the database.
  /// </summary>
  /// <param name="connectionName">The name of the connection to get the connection for.</param>
  /// <returns>A connection to the database. Return the default connection if the connection name is not found.</returns>
  IDbConnection GetConnection(string connectionName);

}

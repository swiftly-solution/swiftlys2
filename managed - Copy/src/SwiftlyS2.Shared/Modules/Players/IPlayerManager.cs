namespace SwiftlyS2.Shared.Players;

public interface IPlayerManagerService
{
    /// <summary>
    /// Checks whether a specific player is currently online and connected to the server.
    /// </summary>
    /// <returns>True if the player is online, false otherwise.</returns>
    public bool IsPlayerOnline( int playerid );

    /// <summary>
    /// Gets the number of players currently in the game.
    /// </summary>
    public int PlayerCount { get; }

    /// <summary>
    /// Gets the maximum number of players allowed by the engine.
    /// </summary>
    public int PlayerCap { get; }

    /// <summary>
    /// Broadcasts a message to players using different display methods based on the message type.
    /// </summary>
    /// <param name="kind">The type of message display.</param>
    /// <param name="message">The text content to send to players.</param>
    public void SendMessage( MessageType kind, string message );
    /// <summary>
    /// Sends a message of the specified type to the players with a custom HTML duration.
    /// </summary>
    /// <param name="kind">The type of message to send. Determines how the message is processed or displayed.</param>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    /// <param name="htmlDuration">The duration, in milliseconds, for which the message should be displayed in HTML format.</param>
    public void SendMessage( MessageType kind, string message, int htmlDuration = 5000 );
    /// <summary>
    /// Sends a notify message to the players.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public void SendNotify( string message );
    /// <summary>
    /// Sends a console message to the players.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public void SendConsole( string message );
    /// <summary>
    /// Sends a chat message to the players.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public void SendChat( string message );
    /// <summary>
    /// Sends a center message to the players.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public void SendCenter( string message );
    /// <summary>
    /// Sends an alert message to the players.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public void SendAlert( string message );
    /// <summary>
    /// Sends a center HTML message to the players.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    /// <param name="duration">The duration, in milliseconds, for which the message should be displayed in HTML format.</param>
    public void SendCenterHTML( string message, int duration = 5000 );
    /// <summary>
    /// Sends an end-of-text chat message to the players.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public void SendChatEOT( string message );

    /// <summary>
    /// Controls whether a specific entity should be blocked from being transmitted/synchronized to clients.
    /// </summary>
    public void ShouldBlockTransmitEntity( int entityid, bool shouldBlockTransmit );

    /// <summary>
    /// Removes all entity transmission blocks, allowing all previously blocked entities to be transmitted to clients again.
    /// </summary>
    public void ClearAllBlockedTransmitEntities();

    /// <summary>
    /// Retrieves the player associated with the specified player ID.
    /// </summary>
    /// <param name="playerid">The unique identifier of the player to retrieve. Must be a valid player ID.</param>
    /// <returns>An <see cref="IPlayer"/> instance representing the player with the specified ID, or <c>null</c> if no such
    /// player exists.</returns>
    public IPlayer GetPlayer( int playerid );

    /// <summary>
    /// Retrieves all players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all online players.</returns>
    public IEnumerable<IPlayer> GetAllPlayers();

    /// <summary>
    /// Finds targetted players based on the provided search criteria.
    /// </summary>
    /// <param name="player">The player initiating the search.</param>
    /// <param name="target">The target player name or identifier.</param>
    /// <param name="searchMode">The search mode to apply.</param>
    /// <returns>A collection of players matching the search criteria.</returns>
    public IEnumerable<IPlayer> FindTargettedPlayers( IPlayer player, string target, TargetSearchMode searchMode );
}
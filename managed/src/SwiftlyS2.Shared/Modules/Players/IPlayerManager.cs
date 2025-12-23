using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Translation;

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
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="kind">The type of message display.</param>
    /// <param name="message">The text content to send to players.</param>
    [ThreadUnsafe]
    public void SendMessage( MessageType kind, string message );

    /// <summary>
    /// Sends a message of the specified type to the players with a custom HTML duration.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="kind">The type of message to send. Determines how the message is processed or displayed.</param>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    /// <param name="htmlDuration">The duration, in milliseconds, for which the message should be displayed in HTML format.</param>
    [ThreadUnsafe]
    public void SendMessage( MessageType kind, string message, int htmlDuration = 5000 );

    /// <summary>
    /// Broadcasts a message to players using different display methods based on the message type.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="kind">The type of message display.</param>
    /// <param name="messageCallback">The text callback to send to players.</param>
    [ThreadUnsafe]
    public void SendMessage( MessageType kind, Func<IPlayer, ILocalizer, string> messageCallback );

    /// <summary>
    /// Sends a message of the specified type to the players with a custom HTML duration.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="kind">The type of message to send. Determines how the message is processed or displayed.</param>
    /// <param name="messageCallback">The callback of the message to send. Cannot be null.</param>
    /// <param name="htmlDuration">The duration, in milliseconds, for which the message should be displayed in HTML format.</param>
    [ThreadUnsafe]
    public void SendMessage( MessageType kind, Func<IPlayer, ILocalizer, string> messageCallback, int htmlDuration = 5000 );

    /// <summary>
    /// Sends a message of the specified type to the players asynchronously.
    /// </summary>
    /// <param name="kind">The type of message to send. Determines how the message is processed or displayed.</param>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public Task SendMessageAsync( MessageType kind, string message );

    /// <summary>
    /// Sends a message of the specified type to the players asynchronously with a custom HTML duration.
    /// </summary>
    /// <param name="kind">The type of message to send. Determines how the message is processed or displayed.</param>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    /// <param name="htmlDuration">The duration, in milliseconds, for which the message should be displayed in HTML format.</param>
    public Task SendMessageAsync( MessageType kind, string message, int htmlDuration = 5000 );

    /// <summary>
    /// Broadcasts a message to players using different display methods based on the message type.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="kind">The type of message display.</param>
    /// <param name="messageCallback">The text callback to send to players.</param>
    public Task SendMessageAsync( MessageType kind, Func<IPlayer, ILocalizer, string> messageCallback );

    /// <summary>
    /// Sends a message of the specified type to the players with a custom HTML duration.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="kind">The type of message to send. Determines how the message is processed or displayed.</param>
    /// <param name="messageCallback">The callback of the message to send. Cannot be null.</param>
    /// <param name="htmlDuration">The duration, in milliseconds, for which the message should be displayed in HTML format.</param>
    public Task SendMessageAsync( MessageType kind, Func<IPlayer, ILocalizer, string> messageCallback, int htmlDuration = 5000 );

    /// <summary>
    /// Sends a notify message to the players.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    [ThreadUnsafe]
    public void SendNotify( string message );

    /// <summary>
    /// Sends a notify message to the players asynchronously.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public Task SendNotifyAsync( string message );

    /// <summary>
    /// Sends a console message to the players.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    [ThreadUnsafe]
    public void SendConsole( string message );

    /// <summary>
    /// Sends a console message to the players asynchronously.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public Task SendConsoleAsync( string message );

    /// <summary>
    /// Sends a chat message to the players.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    [ThreadUnsafe]
    public void SendChat( string message );

    /// <summary>
    /// Sends a chat message to the players asynchronously.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public Task SendChatAsync( string message );

    /// <summary>
    /// Sends a center message to the players.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    [ThreadUnsafe]
    public void SendCenter( string message );

    /// <summary>
    /// Sends a center message to the players asynchronously.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public Task SendCenterAsync( string message );

    /// <summary>
    /// Sends an alert message to the players.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    [ThreadUnsafe]
    public void SendAlert( string message );

    /// <summary>
    /// Sends an alert message to the players asynchronously.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public Task SendAlertAsync( string message );

    /// <summary>
    /// Sends a center HTML message to the players.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    /// <param name="duration">The duration, in milliseconds, for which the message should be displayed in HTML format.</param>
    [ThreadUnsafe]
    public void SendCenterHTML( string message, int duration = 5000 );

    /// <summary>
    /// Sends a center HTML message to the players asynchronously.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    /// <param name="duration">The duration, in milliseconds, for which the message should be displayed in HTML format.</param>
    public Task SendCenterHTMLAsync( string message, int duration = 5000 );

    /// <summary>
    /// Sends an end-of-text chat message to the players.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    [ThreadUnsafe]
    public void SendChatEOT( string message );

    /// <summary>
    /// Sends an end-of-text chat message to the players asynchronously.
    /// </summary>
    /// <param name="message">The content of the message to send. Cannot be null.</param>
    public Task SendChatEOTAsync( string message );

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
    public IPlayer? GetPlayer( int playerid );

    /// <summary>
    /// Retrieves the player associated with the specified controller.
    /// </summary>
    /// <param name="controller">The controller to retrieve the player from.</param>
    /// <returns>An <see cref="IPlayer"/> instance representing the player with the specified controller, or <c>null</c> if no such
    /// player exists.</returns>
    public IPlayer? GetPlayerFromController( CBasePlayerController controller );

/// <summary>
/// Retrieves the player associated with the specified pawn.
/// </summary>
/// <param name="pawn">The pawn to retrieve the player from.</param>
/// <returns>An <see cref="IPlayer"/> instance representing the player with the specified pawn, or <c>null</c> if no such
/// player exists.</returns>
    public IPlayer? GetPlayerFromPawn( CBasePlayerPawn pawn );

    /// <summary>
    /// Retrieves all players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all online players.</returns>
    public IEnumerable<IPlayer> GetAllPlayers();

    /// <summary>
    /// Retrieves all bot players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all online bot players.</returns>
    public IEnumerable<IPlayer> GetBots();

    /// <summary>
    /// Retrieves all alive players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all alive players currently online.</returns>
    public IEnumerable<IPlayer> GetAlive();

    /// <summary>
    /// Retrieves all CT players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all CT players currently online.</returns>
    public IEnumerable<IPlayer> GetCT();

    /// <summary>
    /// Retrieves all T players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all T players currently online.</returns>
    public IEnumerable<IPlayer> GetT();

    /// <summary>
    /// Retrieves all spectator players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all spectator players currently online.</returns>
    public IEnumerable<IPlayer> GetSpectators();

    /// <summary>
    /// Retrieves all players in the specified team.
    /// </summary>
    /// <param name="team">The team for which to retrieve players.</param>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all players in the specified team.</returns>
    public IEnumerable<IPlayer> GetInTeam( Team team );

    /// <summary>
    /// Retrieves all alive T players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all alive T players currently online.</returns>
    public IEnumerable<IPlayer> GetTAlive();

    /// <summary>
    /// Retrieves all alive CT players currently online.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="IPlayer"/> instances representing all alive CT players currently online.</returns>
    public IEnumerable<IPlayer> GetCTAlive();

    /// <summary>
    /// Finds targetted players based on the provided search criteria.
    /// </summary>
    /// <param name="player">The player initiating the search.</param>
    /// <param name="target">The target player name or identifier.</param>
    /// <param name="searchMode">The search mode to apply.</param>
    /// <param name="nameComparison">The string comparison mode for name matching. Defaults to <see cref="StringComparison.OrdinalIgnoreCase"/>.</param>
    /// <returns>A collection of players matching the search criteria.</returns>
    public IEnumerable<IPlayer> FindTargettedPlayers( IPlayer player, string target, TargetSearchMode searchMode,
        StringComparison nameComparison = StringComparison.OrdinalIgnoreCase );
}
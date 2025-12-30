using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.GameEvents;

/// <summary>
/// Plugin-scoped service for managing game events.
/// </summary>
public interface IGameEventService
{
    /// <summary>
    /// The delegate type for game event callbacks.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="eventObj">The event object.</param>
    /// <returns>The hook result.</returns>
    delegate HookResult GameEventHandler<T>( T eventObj ) where T : IGameEvent<T>;

    /// <summary>
    /// Hooks a pre-event callback.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="callback">The callback to hook.</param>
    /// <returns>A GUID representing the hook. You can use this to unhook the callback later.</returns>
    Guid HookPre<T>( GameEventHandler<T> callback ) where T : IGameEvent<T>;

    /// <summary>
    /// Hooks a post-event callback.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="callback">The callback to hook.</param>
    /// <returns>A GUID representing the hook. You can use this to unhook the callback later.</returns>
    Guid HookPost<T>( GameEventHandler<T> callback ) where T : IGameEvent<T>;

    /// <summary>
    /// Unhooks a callback.
    /// </summary>
    /// <param name="guid">The GUID of the hook to unhook.</param>
    void Unhook( Guid guid );

    /// <summary>
    /// Unhooks all pre-event callbacks.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    void UnhookPre<T>() where T : IGameEvent<T>;

    /// <summary>
    /// Unhooks all post-event callbacks.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    void UnhookPost<T>() where T : IGameEvent<T>;


    /// <summary>
    /// Fires an event to all players.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    [ThreadUnsafe]
    void Fire<T>() where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to all players with a configured event.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// <param name="configureEvent">The action to configure the event.</param>
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    [ThreadUnsafe]
    void Fire<T>( Action<T> configureEvent ) where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to all players asynchronously.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    Task FireAsync<T>() where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to all players with a configured event asynchronously.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    Task FireAsync<T>( Action<T> configureEvent ) where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to a player.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="slot">The player slot.</param>
    [ThreadUnsafe]
    void FireToPlayer<T>( int slot ) where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to a player with a configured event.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="slot">The player slot.</param>
    /// <param name="configureEvent">The action to configure the event.</param>
    [ThreadUnsafe]
    void FireToPlayer<T>( int slot, Action<T> configureEvent ) where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to a player asynchronously.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="slot">The player slot.</param>
    Task FireToPlayerAsync<T>( int slot ) where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to a player with a configured event asynchronously.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="slot">The player slot.</param>
    /// <param name="configureEvent">The action to configure the event.</param>
    Task FireToPlayerAsync<T>( int slot, Action<T> configureEvent ) where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to the server.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ThreadUnsafe]
    void FireToServer<T>() where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to the server with a configured event.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="configureEvent">The action to configure the event.</param>
    [ThreadUnsafe]
    void FireToServer<T>( Action<T> configureEvent ) where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to the server asynchronously.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    Task FireToServerAsync<T>() where T : IGameEvent<T>;

    /// <summary>
    /// Fires an event to the server with a configured event asynchronously.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="configureEvent">The action to configure the event.</param>
    Task FireToServerAsync<T>( Action<T> configureEvent ) where T : IGameEvent<T>;

    /// <summary>
    /// Check if the player is Listening this event.
    /// </summary>
    /// <param name="slot">The player slot.</param>
    /// <param name="eventName">The event name.</param>
    bool IsListeningToEvent( int slot, string eventName );

    /// <summary>
    /// Check if the player is Listening this event.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <param name="slot">The player slot.</param>
    bool IsListeningToEvent<T>( int slot ) where T : IGameEvent<T>;
}

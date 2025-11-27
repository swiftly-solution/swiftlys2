namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Event delegates.
/// </summary>
public class EventDelegates
{

    /// <summary>
    /// Called when game has processed a tick. Won't be called if the server is in hibernation.
    /// This callback is a hot path, be careful with it and don't do anything expensive.
    /// </summary>
    public delegate void OnTick();

    /// <summary>
    /// Called when the world is updated. This happens even in hibernation.
    /// This callback is a hot path, be careful with it and don't do anything expensive.
    /// </summary>
    public delegate void OnWorldUpdate();

    /// <summary>
    /// Called when Steam API is activated.
    /// </summary>
    public delegate void OnSteamAPIActivated();

    /// <summary>
    /// Called when a client connects to the server.
    /// </summary>
    public delegate void OnClientConnected( IOnClientConnectedEvent @event );

    /// <summary>
    /// Called when a client disconnects from the server.
    /// </summary>
    public delegate void OnClientDisconnected( IOnClientDisconnectedEvent @event );

    /// <summary>
    /// Called when a client's key state changes.
    /// </summary>
    public delegate void OnClientKeyStateChanged( IOnClientKeyStateChangedEvent @event );

    /// <summary>
    /// Called when a client is fully put in server.
    /// </summary>
    public delegate void OnClientPutInServer( IOnClientPutInServerEvent @event );

    /// <summary>
    /// Called when a client is authorized by Steam.
    /// </summary>
    public delegate void OnClientSteamAuthorize( IOnClientSteamAuthorizeEvent @event );

    /// <summary>
    /// Called when a client's Steam authorization fails.
    /// </summary>
    public delegate void OnClientSteamAuthorizeFail( IOnClientSteamAuthorizeFailEvent @event );

    /// <summary>
    /// Called when an entity is created.
    /// </summary>
    public delegate void OnEntityCreated( IOnEntityCreatedEvent @event );

    /// <summary>
    /// Called when an entity is deleted.
    /// </summary>
    public delegate void OnEntityDeleted( IOnEntityDeletedEvent @event );

    /// <summary>
    /// Called when an entity's parent changes.
    /// </summary>
    public delegate void OnEntityParentChanged( IOnEntityParentChangedEvent @event );

    /// <summary>
    /// Called when an entity is spawned.
    /// </summary>
    public delegate void OnEntitySpawned( IOnEntitySpawnedEvent @event );

    /// <summary>
    /// Called when a map is loaded.
    /// </summary>
    public delegate void OnMapLoad( IOnMapLoadEvent @event );

    /// <summary>
    /// Called when a map is unloaded.
    /// </summary>
    public delegate void OnMapUnload( IOnMapUnloadEvent @event );

    /// <summary>
    /// Called when a client processes user commands.
    /// This callback is a hot path, be careful with it and don't do anything expensive.
    /// </summary>
    public delegate void OnClientProcessUsercmds( IOnClientProcessUsercmdsEvent @event );

    /// <summary>
    /// Called when a ConVar value is changed.
    /// </summary>
    public delegate void OnConVarValueChanged( IOnConVarValueChanged @event );

    /// <summary>
    /// Called when a ConCommand is created.
    /// </summary>
    public delegate void OnConCommandCreated( IOnConCommandCreated @event );

    /// <summary>
    /// Called when a ConVar is created.
    /// </summary>
    public delegate void OnConVarCreated( IOnConVarCreated @event );

    /// <summary>
    /// Called when an entity takes damage.
    /// </summary>
    public delegate void OnEntityTakeDamage( IOnEntityTakeDamageEvent @event );

    /// <summary>
    /// Called when the game is precaching resources.
    /// </summary>
    public delegate void OnPrecacheResource( IOnPrecacheResourceEvent @event );

    [Obsolete("OnEntityTouchHook is deprecated. Use OnEntityStartTouch, OnEntityTouch, or OnEntityEndTouch instead.")]
    public delegate void OnEntityTouchHook( IOnEntityTouchHookEvent @event );

    /// <summary>
    /// Called when an entity starts touching another entity.
    /// </summary>
    public delegate void OnEntityStartTouch( IOnEntityStartTouchEvent @event );

    /// <summary>
    /// Called when an entity is touching another entity.
    /// </summary>
    public delegate void OnEntityTouch( IOnEntityTouchEvent @event );

    /// <summary>
    /// Called when an entity ends touching another entity.
    /// </summary>
    public delegate void OnEntityEndTouch( IOnEntityEndTouchEvent @event );

    /// <summary>
    /// Called when an item services can acquire hook is triggered.
    /// </summary>
    public delegate void OnItemServicesCanAcquireHook( IOnItemServicesCanAcquireHookEvent @event );

    /// <summary>
    /// Called when a weapon services can use hook is triggered.
    /// </summary>
    public delegate void OnWeaponServicesCanUseHook( IOnWeaponServicesCanUseHookEvent @event );

    /// <summary>
    /// Called when a console output is received.
    /// </summary>
    public delegate void OnConsoleOutput( IOnConsoleOutputEvent @event );

    /// <summary>
    /// Called when a command is executed.
    /// </summary>
    public delegate void OnCommandExecuteHook( IOnCommandExecuteHookEvent @event );

    /// <summary>
    /// Called when the movement services run command hook is triggered.
    /// </summary>
    public delegate void OnMovementServicesRunCommandHook( IOnMovementServicesRunCommandHookEvent @event );

    /// <summary>
    /// Called when the player pawn post think hook is triggered.
    /// </summary>
    public delegate void OnPlayerPawnPostThink( IOnPlayerPawnPostThinkHookEvent @event );

    /// <summary>
    /// Called when an entity identity accept input hook is triggered.
    /// </summary>
    public delegate void OnEntityIdentityAcceptInputHook( IOnEntityIdentityAcceptInputHookEvent @event );
}
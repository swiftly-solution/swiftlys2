namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Custom event subscriber interface.
/// </summary>
public interface IEventSubscriber
{

    /// <summary>
    /// Called when game has processed a tick. Won't be called if the server is in hibernation.
    /// This callback is a hot path, be careful with it and don't do anything expensive.
    /// </summary>
    public event EventDelegates.OnTick? OnTick;

    /// <summary>
    /// Called when the world is updated. This happens even in hibernation.
    /// This callback is a hot path, be careful with it and don't do anything expensive.
    /// </summary>
    public event EventDelegates.OnWorldUpdate? OnWorldUpdate;

    /// <summary>
    /// Called when Steam API is activated.
    /// </summary>
    public event EventDelegates.OnSteamAPIActivated? OnSteamAPIActivated;

    /// <summary>
    /// Called when a client connects to the server.
    /// </summary>
    public event EventDelegates.OnClientConnected? OnClientConnected;

    /// <summary>
    /// Called when a client disconnects from the server.
    /// </summary>
    public event EventDelegates.OnClientDisconnected? OnClientDisconnected;

    /// <summary>
    /// Called when a client's key state changes.
    /// </summary>
    public event EventDelegates.OnClientKeyStateChanged? OnClientKeyStateChanged;

    /// <summary>
    /// Called when a client is fully put in server.
    /// </summary>
    public event EventDelegates.OnClientPutInServer? OnClientPutInServer;

    /// <summary>
    /// Called when a client is authorized by Steam.
    /// </summary>
    public event EventDelegates.OnClientSteamAuthorize? OnClientSteamAuthorize;

    /// <summary>
    /// Called when a client's Steam authorization fails.
    /// </summary>
    public event EventDelegates.OnClientSteamAuthorizeFail? OnClientSteamAuthorizeFail;

    /// <summary>
    /// Called when an entity is created.
    /// </summary>
    public event EventDelegates.OnEntityCreated? OnEntityCreated;

    /// <summary>
    /// Called when an entity is deleted.
    /// </summary>
    public event EventDelegates.OnEntityDeleted? OnEntityDeleted;

    /// <summary>
    /// Called when an entity's parent changes.
    /// </summary>
    public event EventDelegates.OnEntityParentChanged? OnEntityParentChanged;

    /// <summary>
    /// Called when an entity is spawned.
    /// </summary>
    public event EventDelegates.OnEntitySpawned? OnEntitySpawned;

    /// <summary>
    /// Called when a map is loaded.
    /// </summary>
    public event EventDelegates.OnMapLoad? OnMapLoad;

    /// <summary>
    /// Called when a map is unloaded.
    /// </summary>
    public event EventDelegates.OnMapUnload? OnMapUnload;

    /// <summary>
    /// Called when the game process user's input.
    /// This callback is a hot path, be careful with it and don't do anything expensive.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Controller.ProcessUsercmds instead.")]
    public event EventDelegates.OnClientProcessUsercmds? OnClientProcessUsercmds;

    /// <summary>
    /// Called when a ConVar value is changed.
    /// </summary>
    public event EventDelegates.OnConVarValueChanged? OnConVarValueChanged;

    /// <summary>
    /// Called when a ConCommand is created.
    /// </summary>
    public event EventDelegates.OnConCommandCreated? OnConCommandCreated;

    /// <summary>
    /// Called when a ConVar is created.
    /// </summary>
    public event EventDelegates.OnConVarCreated? OnConVarCreated;

    /// <summary>
    /// Called when an entity takes damage.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.TakeDamage instead.")]
    public event EventDelegates.OnEntityTakeDamage? OnEntityTakeDamage;

    /// <summary>
    /// Called when the game is precaching resources.
    /// </summary>
    public event EventDelegates.OnPrecacheResource? OnPrecacheResource;

    /// <summary>
    /// Called when an item services can acquire hook is triggered.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Items.CanAcquire instead.")]
    public event EventDelegates.OnItemServicesCanAcquireHook? OnItemServicesCanAcquireHook;

    /// <summary>
    /// Called when a weapon services can use hook is triggered.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Weapons.CanUse instead.")]
    public event EventDelegates.OnWeaponServicesCanUseHook? OnWeaponServicesCanUseHook;

    /// <summary>
    /// Called when a weapon services drop weapon hook is triggered.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Weapons.Drop instead.")]
    public event EventDelegates.OnWeaponServicesDropWeaponHook? OnWeaponServicesDropWeaponHook;

    /// <summary>
    /// Called when the game outputs a console message. Callbacks run on the dedicated
    /// console dispatch thread; schedule work before using game-thread-only APIs.
    /// </summary>
    public event EventDelegates.OnConsoleOutput? OnConsoleOutput;

    /// <summary>
    /// Called when a server command (ConCommand) is executed.
    /// </summary>
    public event EventDelegates.OnCommandExecuteHook? OnCommandExecuteHook;

    /// <summary>
    /// Called when an entity starts touching another entity.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.StartTouch instead.")]
    public event EventDelegates.OnEntityStartTouch? OnEntityStartTouch;

    /// <summary>
    /// Called when an entity is touching another entity.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.Touch instead.")]
    public event EventDelegates.OnEntityTouch? OnEntityTouch;

    /// <summary>
    /// Called when an entity ends touching another entity.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.EndTouch instead.")]
    public event EventDelegates.OnEntityEndTouch? OnEntityEndTouch;

    /// <summary>
    /// Called when the movement services run command hook is triggered.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Movement.RunCommand instead.")]
    public event EventDelegates.OnMovementServicesRunCommandHook? OnMovementServicesRunCommandHook;

    /// <summary>
    /// Called when the player pawn post think hook is triggered.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Pawn.PostThink instead.")]
    public event EventDelegates.OnPlayerPawnPostThink? OnPlayerPawnPostThink;

    /// <summary>
    /// Called when an entity identity accept input hook is triggered.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.AcceptInput instead.")]
    public event EventDelegates.OnEntityIdentityAcceptInputHook? OnEntityIdentityAcceptInputHook;

    /// <summary>
    /// Called when an entity fires output.
    /// </summary>
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.FireOutput instead.")]
    public event EventDelegates.OnEntityFireOutputHookEvent? OnEntityFireOutputHook;

    /// <summary>
    /// Called when the server is started.
    /// </summary>
    public event EventDelegates.OnStartupServer? OnStartupServer;

    /// <summary>
    /// Called when a client sends a voice packet.
    /// </summary>
    public event EventDelegates.OnClientVoice? OnClientVoice;

    /// <summary>
    /// Called when a custom hud element is clicked.
    /// </summary>    
    public event EventDelegates.OnCustomHudClicked? OnCustomHudClicked;
}

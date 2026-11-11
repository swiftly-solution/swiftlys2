using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.Events;

/// <summary>
/// Plugin-scoped custom event subscriber.
/// </summary>
internal class EventSubscriber : IEventSubscriber, IDisposable
{
    private readonly ILogger<EventSubscriber> logger;

    private readonly Lock sync = new();

    private bool disposed;

    public bool Disposed => disposed;

    public EventSubscriber( IContextedProfilerService profiler, ILogger<EventSubscriber> logger )
    {
        this.logger = logger;
    }

    ~EventSubscriber()
    {
        Dispose();
    }

    private void Add<T>( CoreEvent kind, ref T? handlers, T? value ) where T : Delegate
    {
        if (value == null) return;

        lock (sync)
        {
            if (disposed) return;

            if (handlers == null) EventPublisher.Router.Add(kind, this);
            handlers = (T)Delegate.Combine(handlers, value);
        }
    }

    private void Remove<T>( CoreEvent kind, ref T? handlers, T? value ) where T : Delegate
    {
        if (value == null) return;

        lock (sync)
        {
            if (handlers == null) return;

            handlers = (T?)Delegate.Remove(handlers, value);
            if (handlers == null) _ = EventPublisher.Router.Remove(kind, this);
        }
    }

    private void LogError( Exception e, string eventName )
    {
        if (GlobalExceptionHandler.Handle(ref e)) logger.LogError(e, "Error invoking {Event}.", eventName);
    }

    private EventDelegates.OnTick? _OnTick;
    public event EventDelegates.OnTick? OnTick {
        add => Add(CoreEvent.Tick, ref _OnTick, value);
        remove => Remove(CoreEvent.Tick, ref _OnTick, value);
    }

    private EventDelegates.OnWorldUpdate? _OnWorldUpdate;
    public event EventDelegates.OnWorldUpdate? OnWorldUpdate {
        add => Add(CoreEvent.WorldUpdate, ref _OnWorldUpdate, value);
        remove => Remove(CoreEvent.WorldUpdate, ref _OnWorldUpdate, value);
    }

    private EventDelegates.OnClientConnected? _OnClientConnected;
    public event EventDelegates.OnClientConnected? OnClientConnected {
        add => Add(CoreEvent.ClientConnected, ref _OnClientConnected, value);
        remove => Remove(CoreEvent.ClientConnected, ref _OnClientConnected, value);
    }

    private EventDelegates.OnClientDisconnected? _OnClientDisconnected;
    public event EventDelegates.OnClientDisconnected? OnClientDisconnected {
        add => Add(CoreEvent.ClientDisconnected, ref _OnClientDisconnected, value);
        remove => Remove(CoreEvent.ClientDisconnected, ref _OnClientDisconnected, value);
    }

    private EventDelegates.OnClientKeyStateChanged? _OnClientKeyStateChanged;
    public event EventDelegates.OnClientKeyStateChanged? OnClientKeyStateChanged {
        add => Add(CoreEvent.ClientKeyStateChanged, ref _OnClientKeyStateChanged, value);
        remove => Remove(CoreEvent.ClientKeyStateChanged, ref _OnClientKeyStateChanged, value);
    }

    private EventDelegates.OnClientPutInServer? _OnClientPutInServer;
    public event EventDelegates.OnClientPutInServer? OnClientPutInServer {
        add => Add(CoreEvent.ClientPutInServer, ref _OnClientPutInServer, value);
        remove => Remove(CoreEvent.ClientPutInServer, ref _OnClientPutInServer, value);
    }

    private EventDelegates.OnClientSteamAuthorize? _OnClientSteamAuthorize;
    public event EventDelegates.OnClientSteamAuthorize? OnClientSteamAuthorize {
        add => Add(CoreEvent.ClientSteamAuthorize, ref _OnClientSteamAuthorize, value);
        remove => Remove(CoreEvent.ClientSteamAuthorize, ref _OnClientSteamAuthorize, value);
    }

    private EventDelegates.OnClientSteamAuthorizeFail? _OnClientSteamAuthorizeFail;
    public event EventDelegates.OnClientSteamAuthorizeFail? OnClientSteamAuthorizeFail {
        add => Add(CoreEvent.ClientSteamAuthorizeFail, ref _OnClientSteamAuthorizeFail, value);
        remove => Remove(CoreEvent.ClientSteamAuthorizeFail, ref _OnClientSteamAuthorizeFail, value);
    }

    private EventDelegates.OnEntityCreated? _OnEntityCreated;
    public event EventDelegates.OnEntityCreated? OnEntityCreated {
        add => Add(CoreEvent.EntityCreated, ref _OnEntityCreated, value);
        remove => Remove(CoreEvent.EntityCreated, ref _OnEntityCreated, value);
    }

    private EventDelegates.OnEntityDeleted? _OnEntityDeleted;
    public event EventDelegates.OnEntityDeleted? OnEntityDeleted {
        add => Add(CoreEvent.EntityDeleted, ref _OnEntityDeleted, value);
        remove => Remove(CoreEvent.EntityDeleted, ref _OnEntityDeleted, value);
    }

    private EventDelegates.OnEntityParentChanged? _OnEntityParentChanged;
    public event EventDelegates.OnEntityParentChanged? OnEntityParentChanged {
        add => Add(CoreEvent.EntityParentChanged, ref _OnEntityParentChanged, value);
        remove => Remove(CoreEvent.EntityParentChanged, ref _OnEntityParentChanged, value);
    }

    private EventDelegates.OnEntitySpawned? _OnEntitySpawned;
    public event EventDelegates.OnEntitySpawned? OnEntitySpawned {
        add => Add(CoreEvent.EntitySpawned, ref _OnEntitySpawned, value);
        remove => Remove(CoreEvent.EntitySpawned, ref _OnEntitySpawned, value);
    }

    private EventDelegates.OnMapLoad? _OnMapLoad;
    public event EventDelegates.OnMapLoad? OnMapLoad {
        add => Add(CoreEvent.MapLoad, ref _OnMapLoad, value);
        remove => Remove(CoreEvent.MapLoad, ref _OnMapLoad, value);
    }

    private EventDelegates.OnMapUnload? _OnMapUnload;
    public event EventDelegates.OnMapUnload? OnMapUnload {
        add => Add(CoreEvent.MapUnload, ref _OnMapUnload, value);
        remove => Remove(CoreEvent.MapUnload, ref _OnMapUnload, value);
    }

    private EventDelegates.OnClientProcessUsercmds? _OnClientProcessUsercmds;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Controller.ProcessUsercmds instead.")]
    public event EventDelegates.OnClientProcessUsercmds? OnClientProcessUsercmds {
        add => Add(CoreEvent.ClientProcessUsercmds, ref _OnClientProcessUsercmds, value);
        remove => Remove(CoreEvent.ClientProcessUsercmds, ref _OnClientProcessUsercmds, value);
    }

    private EventDelegates.OnConVarValueChanged? _OnConVarValueChanged;
    public event EventDelegates.OnConVarValueChanged? OnConVarValueChanged {
        add => Add(CoreEvent.ConVarValueChanged, ref _OnConVarValueChanged, value);
        remove => Remove(CoreEvent.ConVarValueChanged, ref _OnConVarValueChanged, value);
    }

    private EventDelegates.OnConCommandCreated? _OnConCommandCreated;
    public event EventDelegates.OnConCommandCreated? OnConCommandCreated {
        add => Add(CoreEvent.ConCommandCreated, ref _OnConCommandCreated, value);
        remove => Remove(CoreEvent.ConCommandCreated, ref _OnConCommandCreated, value);
    }

    private EventDelegates.OnConVarCreated? _OnConVarCreated;
    public event EventDelegates.OnConVarCreated? OnConVarCreated {
        add => Add(CoreEvent.ConVarCreated, ref _OnConVarCreated, value);
        remove => Remove(CoreEvent.ConVarCreated, ref _OnConVarCreated, value);
    }

    private EventDelegates.OnEntityTakeDamage? _OnEntityTakeDamage;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.TakeDamage instead.")]
    public event EventDelegates.OnEntityTakeDamage? OnEntityTakeDamage {
        add => Add(CoreEvent.EntityTakeDamage, ref _OnEntityTakeDamage, value);
        remove => Remove(CoreEvent.EntityTakeDamage, ref _OnEntityTakeDamage, value);
    }

    private EventDelegates.OnPrecacheResource? _OnPrecacheResource;
    public event EventDelegates.OnPrecacheResource? OnPrecacheResource {
        add => Add(CoreEvent.PrecacheResource, ref _OnPrecacheResource, value);
        remove => Remove(CoreEvent.PrecacheResource, ref _OnPrecacheResource, value);
    }

    private EventDelegates.OnEntityStartTouch? _OnEntityStartTouch;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.StartTouch instead.")]
    public event EventDelegates.OnEntityStartTouch? OnEntityStartTouch {
        add => Add(CoreEvent.EntityStartTouch, ref _OnEntityStartTouch, value);
        remove => Remove(CoreEvent.EntityStartTouch, ref _OnEntityStartTouch, value);
    }

    private EventDelegates.OnEntityTouch? _OnEntityTouch;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.Touch instead.")]
    public event EventDelegates.OnEntityTouch? OnEntityTouch {
        add => Add(CoreEvent.EntityTouch, ref _OnEntityTouch, value);
        remove => Remove(CoreEvent.EntityTouch, ref _OnEntityTouch, value);
    }

    private EventDelegates.OnEntityEndTouch? _OnEntityEndTouch;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.EndTouch instead.")]
    public event EventDelegates.OnEntityEndTouch? OnEntityEndTouch {
        add => Add(CoreEvent.EntityEndTouch, ref _OnEntityEndTouch, value);
        remove => Remove(CoreEvent.EntityEndTouch, ref _OnEntityEndTouch, value);
    }

    private EventDelegates.OnItemServicesCanAcquireHook? _OnItemServicesCanAcquireHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Items.CanAcquire instead.")]
    public event EventDelegates.OnItemServicesCanAcquireHook? OnItemServicesCanAcquireHook {
        add => Add(CoreEvent.CanAcquire, ref _OnItemServicesCanAcquireHook, value);
        remove => Remove(CoreEvent.CanAcquire, ref _OnItemServicesCanAcquireHook, value);
    }

    private EventDelegates.OnWeaponServicesCanUseHook? _OnWeaponServicesCanUseHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Weapons.CanUse instead.")]
    public event EventDelegates.OnWeaponServicesCanUseHook? OnWeaponServicesCanUseHook {
        add => Add(CoreEvent.CanUseWeapon, ref _OnWeaponServicesCanUseHook, value);
        remove => Remove(CoreEvent.CanUseWeapon, ref _OnWeaponServicesCanUseHook, value);
    }

    private EventDelegates.OnWeaponServicesDropWeaponHook? _OnWeaponServicesDropWeaponHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Weapons.Drop instead.")]
    public event EventDelegates.OnWeaponServicesDropWeaponHook? OnWeaponServicesDropWeaponHook {
        add => Add(CoreEvent.WeaponDrop, ref _OnWeaponServicesDropWeaponHook, value);
        remove => Remove(CoreEvent.WeaponDrop, ref _OnWeaponServicesDropWeaponHook, value);
    }

    private EventDelegates.OnConsoleOutput? _OnConsoleOutput;
    public event EventDelegates.OnConsoleOutput? OnConsoleOutput {
        add => Add(CoreEvent.ConsoleOutput, ref _OnConsoleOutput, value);
        remove => Remove(CoreEvent.ConsoleOutput, ref _OnConsoleOutput, value);
    }

    private EventDelegates.OnCommandExecuteHook? _OnCommandExecuteHook;
    public event EventDelegates.OnCommandExecuteHook? OnCommandExecuteHook {
        add => Add(CoreEvent.CommandExecuteHook, ref _OnCommandExecuteHook, value);
        remove => Remove(CoreEvent.CommandExecuteHook, ref _OnCommandExecuteHook, value);
    }

    private EventDelegates.OnSteamAPIActivated? _OnSteamAPIActivated;
    public event EventDelegates.OnSteamAPIActivated? OnSteamAPIActivated {
        add => Add(CoreEvent.SteamAPIActivated, ref _OnSteamAPIActivated, value);
        remove => Remove(CoreEvent.SteamAPIActivated, ref _OnSteamAPIActivated, value);
    }

    private EventDelegates.OnMovementServicesRunCommandHook? _OnMovementServicesRunCommandHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Movement.RunCommand instead.")]
    public event EventDelegates.OnMovementServicesRunCommandHook? OnMovementServicesRunCommandHook {
        add => Add(CoreEvent.RunCommand, ref _OnMovementServicesRunCommandHook, value);
        remove => Remove(CoreEvent.RunCommand, ref _OnMovementServicesRunCommandHook, value);
    }

    private EventDelegates.OnPlayerPawnPostThink? _OnPlayerPawnPostThink;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Pawn.PostThink instead.")]
    public event EventDelegates.OnPlayerPawnPostThink? OnPlayerPawnPostThink {
        add => Add(CoreEvent.PostThink, ref _OnPlayerPawnPostThink, value);
        remove => Remove(CoreEvent.PostThink, ref _OnPlayerPawnPostThink, value);
    }

    private EventDelegates.OnEntityIdentityAcceptInputHook? _OnEntityIdentityAcceptInputHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.AcceptInput instead.")]
    public event EventDelegates.OnEntityIdentityAcceptInputHook? OnEntityIdentityAcceptInputHook {
        add => Add(CoreEvent.AcceptInput, ref _OnEntityIdentityAcceptInputHook, value);
        remove => Remove(CoreEvent.AcceptInput, ref _OnEntityIdentityAcceptInputHook, value);
    }

    private EventDelegates.OnEntityFireOutputHookEvent? _OnEntityFireOutputHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.FireOutput instead.")]
    public event EventDelegates.OnEntityFireOutputHookEvent? OnEntityFireOutputHook {
        add => Add(CoreEvent.FireOutput, ref _OnEntityFireOutputHook, value);
        remove => Remove(CoreEvent.FireOutput, ref _OnEntityFireOutputHook, value);
    }

    private EventDelegates.OnStartupServer? _OnStartupServer;
    public event EventDelegates.OnStartupServer? OnStartupServer {
        add => Add(CoreEvent.StartupServer, ref _OnStartupServer, value);
        remove => Remove(CoreEvent.StartupServer, ref _OnStartupServer, value);
    }

    private EventDelegates.OnClientVoice? _OnClientVoice;
    public event EventDelegates.OnClientVoice? OnClientVoice {
        add => Add(CoreEvent.ClientVoice, ref _OnClientVoice, value);
        remove => Remove(CoreEvent.ClientVoice, ref _OnClientVoice, value);
    }

    private EventDelegates.OnClientSettingsChanged? _OnClientSettingsChanged;
    public event EventDelegates.OnClientSettingsChanged? OnClientSettingsChanged {
        add => Add(CoreEvent.ClientSettingsChanged, ref _OnClientSettingsChanged, value);
        remove => Remove(CoreEvent.ClientSettingsChanged, ref _OnClientSettingsChanged, value);
    }

    private EventDelegates.OnCustomHudClicked? _OnCustomHudClicked;
    public event EventDelegates.OnCustomHudClicked? OnCustomHudClicked {
        add => Add(CoreEvent.CustomHudClicked, ref _OnCustomHudClicked, value);
        remove => Remove(CoreEvent.CustomHudClicked, ref _OnCustomHudClicked, value);
    }

    public void Dispose()
    {
        lock (sync)
        {
            if (disposed) return;
            disposed = true;

            _OnTick = null;
            _OnWorldUpdate = null;
            _OnClientConnected = null;
            _OnClientDisconnected = null;
            _OnClientKeyStateChanged = null;
            _OnClientPutInServer = null;
            _OnClientSteamAuthorize = null;
            _OnClientSteamAuthorizeFail = null;
            _OnEntityCreated = null;
            _OnEntityDeleted = null;
            _OnEntityParentChanged = null;
            _OnEntitySpawned = null;
            _OnMapLoad = null;
            _OnMapUnload = null;
            _OnClientProcessUsercmds = null;
            _OnConVarValueChanged = null;
            _OnConCommandCreated = null;
            _OnConVarCreated = null;
            _OnEntityTakeDamage = null;
            _OnPrecacheResource = null;
            _OnEntityStartTouch = null;
            _OnEntityTouch = null;
            _OnEntityEndTouch = null;
            _OnItemServicesCanAcquireHook = null;
            _OnWeaponServicesCanUseHook = null;
            _OnWeaponServicesDropWeaponHook = null;
            _OnConsoleOutput = null;
            _OnCommandExecuteHook = null;
            _OnSteamAPIActivated = null;
            _OnMovementServicesRunCommandHook = null;
            _OnPlayerPawnPostThink = null;
            _OnEntityIdentityAcceptInputHook = null;
            _OnEntityFireOutputHook = null;
            _OnStartupServer = null;
            _OnClientVoice = null;
            _OnClientSettingsChanged = null;
            _OnCustomHudClicked = null;

            foreach (var kind in Enum.GetValues<CoreEvent>()) _ = EventPublisher.Router.Remove(kind, this);
        }

        GC.SuppressFinalize(this);
    }

    public void InvokeOnTick()
    {
        var handler = _OnTick;
        if (handler == null) return;
        try { handler.Invoke(); }
        catch (Exception e) { LogError(e, nameof(OnTick)); }
    }

    public void InvokeOnWorldUpdate()
    {
        var handler = _OnWorldUpdate;
        if (handler == null) return;
        try { handler.Invoke(); }
        catch (Exception e) { LogError(e, nameof(OnWorldUpdate)); }
    }

    public void InvokeOnClientConnected( ref OnClientConnectedEvent @event )
    {
        var handler = _OnClientConnected;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientConnected)); }
    }

    public void InvokeOnClientDisconnected( ref OnClientDisconnectedEvent @event )
    {
        var handler = _OnClientDisconnected;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientDisconnected)); }
    }

    public void InvokeOnClientKeyStateChanged( ref OnClientKeyStateChangedEvent @event )
    {
        var handler = _OnClientKeyStateChanged;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientKeyStateChanged)); }
    }

    public void InvokeOnClientPutInServer( ref OnClientPutInServerEvent @event )
    {
        var handler = _OnClientPutInServer;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientPutInServer)); }
    }

    public void InvokeOnClientSteamAuthorize( ref OnClientSteamAuthorizeEvent @event )
    {
        var handler = _OnClientSteamAuthorize;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientSteamAuthorize)); }
    }

    public void InvokeOnClientSteamAuthorizeFail( ref OnClientSteamAuthorizeFailEvent @event )
    {
        var handler = _OnClientSteamAuthorizeFail;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientSteamAuthorizeFail)); }
    }

    public void InvokeOnEntityCreated( ref OnEntityCreatedEvent @event )
    {
        var handler = _OnEntityCreated;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityCreated)); }
    }

    public void InvokeOnClientVoice( ref OnClientVoiceEvent @event )
    {
        var handler = _OnClientVoice;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientVoice)); }
    }

    public void InvokeOnClientSettingsChanged( ref OnClientSettingsChangedEvent @event )
    {
        var handler = _OnClientSettingsChanged;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientSettingsChanged)); }
    }

    public void InvokeOnEntityDeleted( ref OnEntityDeletedEvent @event )
    {
        var handler = _OnEntityDeleted;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityDeleted)); }
    }

    public void InvokeOnEntityParentChanged( ref OnEntityParentChangedEvent @event )
    {
        var handler = _OnEntityParentChanged;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityParentChanged)); }
    }

    public void InvokeOnEntitySpawned( ref OnEntitySpawnedEvent @event )
    {
        var handler = _OnEntitySpawned;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntitySpawned)); }
    }

    public void InvokeOnMapLoad( ref OnMapLoadEvent @event )
    {
        var handler = _OnMapLoad;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnMapLoad)); }
    }

    public void InvokeOnMapUnload( ref OnMapUnloadEvent @event )
    {
        var handler = _OnMapUnload;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnMapUnload)); }
    }

    public void InvokeOnClientProcessUsercmds( ref OnClientProcessUsercmdsEvent @event )
    {
        var handler = _OnClientProcessUsercmds;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnClientProcessUsercmds)); }
    }

    public void InvokeOnEntityTakeDamage( ref OnEntityTakeDamageEvent @event )
    {
        var handler = _OnEntityTakeDamage;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityTakeDamage)); }
    }

    public void InvokeOnPrecacheResource( ref OnPrecacheResourceEvent @event )
    {
        var handler = _OnPrecacheResource;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnPrecacheResource)); }
    }

    public void InvokeOnEntityStartTouch( ref OnEntityStartTouchEvent @event )
    {
        var handler = _OnEntityStartTouch;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityStartTouch)); }
    }

    public void InvokeOnEntityTouch( ref OnEntityTouchEvent @event )
    {
        var handler = _OnEntityTouch;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityTouch)); }
    }

    public void InvokeOnEntityEndTouch( ref OnEntityEndTouchEvent @event )
    {
        var handler = _OnEntityEndTouch;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityEndTouch)); }
    }

    public void InvokeOnSteamAPIActivatedHook()
    {
        var handler = _OnSteamAPIActivated;
        if (handler == null) return;
        try { handler.Invoke(); }
        catch (Exception e) { LogError(e, nameof(OnSteamAPIActivated)); }
    }

    public void InvokeOnItemServicesCanAcquireHook( ref OnItemServicesCanAcquireHookEvent @event )
    {
        var handler = _OnItemServicesCanAcquireHook;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnItemServicesCanAcquireHook)); }
    }

    public void InvokeOnWeaponServicesCanUseHook( ref OnWeaponServicesCanUseHookEvent @event )
    {
        var handler = _OnWeaponServicesCanUseHook;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnWeaponServicesCanUseHook)); }
    }

    public void InvokeOnConsoleOutput( ref OnConsoleOutputEvent @event )
    {
        var handler = _OnConsoleOutput;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnConsoleOutput)); }
    }

    public void InvokeOnConVarValueChanged( ref OnConVarValueChanged @event )
    {
        var handler = _OnConVarValueChanged;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnConVarValueChanged)); }
    }

    public void InvokeOnConCommandCreated( ref OnConCommandCreated @event )
    {
        var handler = _OnConCommandCreated;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnConCommandCreated)); }
    }

    public void InvokeOnConVarCreated( ref OnConVarCreated @event )
    {
        var handler = _OnConVarCreated;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnConVarCreated)); }
    }

    public void InvokeOnCommandExecuteHook( ref OnCommandExecuteHookEvent @event )
    {
        var handler = _OnCommandExecuteHook;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnCommandExecuteHook)); }
    }

    public void InvokeOnMovementServicesRunCommandHook( ref OnMovementServicesRunCommandHookEvent @event )
    {
        var handler = _OnMovementServicesRunCommandHook;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnMovementServicesRunCommandHook)); }
    }

    public void InvokeOnPlayerPawnPostThinkHook( ref OnPlayerPawnPostThinkHookEvent @event )
    {
        var handler = _OnPlayerPawnPostThink;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnPlayerPawnPostThink)); }
    }

    public void InvokeOnEntityIdentityAcceptInputHook( ref OnEntityIdentityAcceptInputHookEvent @event )
    {
        var handler = _OnEntityIdentityAcceptInputHook;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityIdentityAcceptInputHook)); }
    }

    public void InvokeOnWeaponServicesDropWeaponHook( ref OnWeaponServicesDropWeaponHook @event )
    {
        var handler = _OnWeaponServicesDropWeaponHook;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnWeaponServicesDropWeaponHook)); }
    }

    public void InvokeOnEntityFireOutputHook( ref OnEntityFireOutputHookEvent @event )
    {
        var handler = _OnEntityFireOutputHook;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnEntityFireOutputHook)); }
    }

    public void InvokeOnStartupServer()
    {
        var handler = _OnStartupServer;
        if (handler == null) return;
        try { handler.Invoke(); }
        catch (Exception e) { LogError(e, nameof(OnStartupServer)); }
    }

    public void InvokeOnCustomHudClicked( ref OnCustomHudClickedEvent @event )
    {
        var handler = _OnCustomHudClicked;
        if (handler == null) return;
        try { handler.Invoke(@event); }
        catch (Exception e) { LogError(e, nameof(OnCustomHudClicked)); }
    }
}

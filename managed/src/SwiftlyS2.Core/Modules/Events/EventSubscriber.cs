using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.GameHooks;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.Events;

/// <summary>
/// Plugin-scoped custom event subscriber.
/// </summary>
internal class EventSubscriber : IEventSubscriber, IDisposable
{
    private readonly IContextedProfilerService profiler;
    private readonly ILogger<EventSubscriber> logger;

    private volatile bool disposed;

    public bool Disposed => disposed;

    public EventSubscriber( IContextedProfilerService profiler, ILogger<EventSubscriber> logger )
    {
        this.profiler = profiler;
        this.logger = logger;
        this.disposed = false;
        EventPublisher.Subscribe(this);
    }

    ~EventSubscriber()
    {
        Dispose();
    }

    public event EventDelegates.OnTick? OnTick;
    public event EventDelegates.OnWorldUpdate? OnWorldUpdate;
    public event EventDelegates.OnClientConnected? OnClientConnected;
    public event EventDelegates.OnClientDisconnected? OnClientDisconnected;
    public event EventDelegates.OnClientKeyStateChanged? OnClientKeyStateChanged;
    public event EventDelegates.OnClientPutInServer? OnClientPutInServer;
    public event EventDelegates.OnClientSteamAuthorize? OnClientSteamAuthorize;
    public event EventDelegates.OnClientSteamAuthorizeFail? OnClientSteamAuthorizeFail;
    public event EventDelegates.OnEntityCreated? OnEntityCreated;
    public event EventDelegates.OnEntityDeleted? OnEntityDeleted;
    public event EventDelegates.OnEntityParentChanged? OnEntityParentChanged;
    public event EventDelegates.OnEntitySpawned? OnEntitySpawned;
    public event EventDelegates.OnMapLoad? OnMapLoad;
    public event EventDelegates.OnMapUnload? OnMapUnload;
    private EventDelegates.OnClientProcessUsercmds? _OnClientProcessUsercmds;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Controller.ProcessUsercmds instead.")]
    public event EventDelegates.OnClientProcessUsercmds? OnClientProcessUsercmds {
        add {
            if (_OnClientProcessUsercmds == null) GameHooksPublisher.AddHookListener(HookListener.ProcessUsercmds);
            _OnClientProcessUsercmds += value;
        }
        remove {
            _OnClientProcessUsercmds -= value;
            if (_OnClientProcessUsercmds == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
        }
    }
    public event EventDelegates.OnConVarValueChanged? OnConVarValueChanged;
    public event EventDelegates.OnConCommandCreated? OnConCommandCreated;
    public event EventDelegates.OnConVarCreated? OnConVarCreated;
    private EventDelegates.OnEntityTakeDamage? _OnEntityTakeDamage;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.TakeDamage instead.")]
    public event EventDelegates.OnEntityTakeDamage? OnEntityTakeDamage {
        add {
            if (_OnEntityTakeDamage == null) GameHooksPublisher.AddHookListener(HookListener.TakeDamage);
            _OnEntityTakeDamage += value;
        }
        remove {
            _OnEntityTakeDamage -= value;
            if (_OnEntityTakeDamage == null) GameHooksPublisher.RemoveHookListener(HookListener.TakeDamage);
        }
    }
    public event EventDelegates.OnPrecacheResource? OnPrecacheResource;
    private EventDelegates.OnEntityStartTouch? _OnEntityStartTouch;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.StartTouch instead.")]
    public event EventDelegates.OnEntityStartTouch? OnEntityStartTouch {
        add {
            if (_OnEntityStartTouch == null) GameHooksPublisher.AddHookListener(HookListener.StartTouch);
            _OnEntityStartTouch += value;
        }
        remove {
            _OnEntityStartTouch -= value;
            if (_OnEntityStartTouch == null) GameHooksPublisher.RemoveHookListener(HookListener.StartTouch);
        }
    }
    private EventDelegates.OnEntityTouch? _OnEntityTouch;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.Touch instead.")]
    public event EventDelegates.OnEntityTouch? OnEntityTouch {
        add {
            if (_OnEntityTouch == null) GameHooksPublisher.AddHookListener(HookListener.Touch);
            _OnEntityTouch += value;
        }
        remove {
            _OnEntityTouch -= value;
            if (_OnEntityTouch == null) GameHooksPublisher.RemoveHookListener(HookListener.Touch);
        }
    }
    private EventDelegates.OnEntityEndTouch? _OnEntityEndTouch;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.EndTouch instead.")]
    public event EventDelegates.OnEntityEndTouch? OnEntityEndTouch {
        add {
            if (_OnEntityEndTouch == null) GameHooksPublisher.AddHookListener(HookListener.EndTouch);
            _OnEntityEndTouch += value;
        }
        remove {
            _OnEntityEndTouch -= value;
            if (_OnEntityEndTouch == null) GameHooksPublisher.RemoveHookListener(HookListener.EndTouch);
        }
    }
    private EventDelegates.OnItemServicesCanAcquireHook? _OnItemServicesCanAcquireHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Items.CanAcquire instead.")]
    public event EventDelegates.OnItemServicesCanAcquireHook? OnItemServicesCanAcquireHook {
        add {
            if (_OnItemServicesCanAcquireHook == null) GameHooksPublisher.AddHookListener(HookListener.CanAcquire);
            _OnItemServicesCanAcquireHook += value;
        }
        remove {
            _OnItemServicesCanAcquireHook -= value;
            if (_OnItemServicesCanAcquireHook == null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
        }
    }
    private EventDelegates.OnWeaponServicesCanUseHook? _OnWeaponServicesCanUseHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Weapons.CanUse instead.")]
    public event EventDelegates.OnWeaponServicesCanUseHook? OnWeaponServicesCanUseHook {
        add {
            if (_OnWeaponServicesCanUseHook == null) GameHooksPublisher.AddHookListener(HookListener.CanUse);
            _OnWeaponServicesCanUseHook += value;
        }
        remove {
            _OnWeaponServicesCanUseHook -= value;
            if (_OnWeaponServicesCanUseHook == null) GameHooksPublisher.RemoveHookListener(HookListener.CanUse);
        }
    }
    private EventDelegates.OnWeaponServicesDropWeaponHook? _OnWeaponServicesDropWeaponHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Weapons.Drop instead.")]
    public event EventDelegates.OnWeaponServicesDropWeaponHook? OnWeaponServicesDropWeaponHook {
        add {
            if (_OnWeaponServicesDropWeaponHook == null) GameHooksPublisher.AddHookListener(HookListener.WeaponDrop);
            _OnWeaponServicesDropWeaponHook += value;
        }
        remove {
            _OnWeaponServicesDropWeaponHook -= value;
            if (_OnWeaponServicesDropWeaponHook == null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop);
        }
    }
    private readonly Lock consoleOutputLock = new();
    private EventDelegates.OnConsoleOutput? _OnConsoleOutput;
    public event EventDelegates.OnConsoleOutput? OnConsoleOutput {
        add {
            if (value == null) return;

            bool addedFirstListener;
            lock (consoleOutputLock)
            {
                addedFirstListener = _OnConsoleOutput == null;
                _OnConsoleOutput += value;
                if (addedFirstListener) EventPublisher.AddConsoleOutputListener();
            }
        }
        remove {
            if (value == null) return;

            bool removedLastListener;
            lock (consoleOutputLock)
            {
                var hadListeners = _OnConsoleOutput != null;
                _OnConsoleOutput -= value;
                removedLastListener = hadListeners && _OnConsoleOutput == null;
                if (removedLastListener) EventPublisher.RemoveConsoleOutputListener();
            }
        }
    }
    public event EventDelegates.OnCommandExecuteHook? OnCommandExecuteHook;
    public event EventDelegates.OnSteamAPIActivated? OnSteamAPIActivated;
    private EventDelegates.OnMovementServicesRunCommandHook? _OnMovementServicesRunCommandHook;

    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Movement.RunCommand instead.")]
    public event EventDelegates.OnMovementServicesRunCommandHook? OnMovementServicesRunCommandHook {
        add {
            if (_OnMovementServicesRunCommandHook == null) GameHooksPublisher.AddHookListener(HookListener.RunCommand);
            _OnMovementServicesRunCommandHook += value;
        }
        remove {
            _OnMovementServicesRunCommandHook -= value;
            if (_OnMovementServicesRunCommandHook == null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
        }
    }
    private EventDelegates.OnPlayerPawnPostThink? _OnPlayerPawnPostThink;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Pawn.PostThink instead.")]
    public event EventDelegates.OnPlayerPawnPostThink? OnPlayerPawnPostThink {
        add {
            if (_OnPlayerPawnPostThink == null) GameHooksPublisher.AddHookListener(HookListener.PostThink);
            _OnPlayerPawnPostThink += value;
        }
        remove {
            _OnPlayerPawnPostThink -= value;
            if (_OnPlayerPawnPostThink == null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink);
        }
    }
    private EventDelegates.OnEntityIdentityAcceptInputHook? _OnEntityIdentityAcceptInputHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.AcceptInput instead.")]
    public event EventDelegates.OnEntityIdentityAcceptInputHook? OnEntityIdentityAcceptInputHook {
        add {
            if (_OnEntityIdentityAcceptInputHook == null) GameHooksPublisher.AddHookListener(HookListener.AcceptInput);
            _OnEntityIdentityAcceptInputHook += value;
        }
        remove {
            _OnEntityIdentityAcceptInputHook -= value;
            if (_OnEntityIdentityAcceptInputHook == null) GameHooksPublisher.RemoveHookListener(HookListener.AcceptInput);
        }
    }
    private EventDelegates.OnEntityFireOutputHookEvent? _OnEntityFireOutputHook;
    [Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks.Entities.FireOutput instead.")]
    public event EventDelegates.OnEntityFireOutputHookEvent? OnEntityFireOutputHook {
        add {
            if (_OnEntityFireOutputHook == null) GameHooksPublisher.AddHookListener(HookListener.FireOutput);
            _OnEntityFireOutputHook += value;
        }
        remove {
            _OnEntityFireOutputHook -= value;
            if (_OnEntityFireOutputHook == null) GameHooksPublisher.RemoveHookListener(HookListener.FireOutput);
        }
    }
    public event EventDelegates.OnStartupServer? OnStartupServer;
    public event EventDelegates.OnClientVoice? OnClientVoice;
    public event EventDelegates.OnCustomHudClicked? OnCustomHudClicked;

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }
        disposed = true;

        if (_OnClientProcessUsercmds != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
        if (_OnEntityTakeDamage != null) GameHooksPublisher.RemoveHookListener(HookListener.TakeDamage);
        if (_OnEntityStartTouch != null) GameHooksPublisher.RemoveHookListener(HookListener.StartTouch);
        if (_OnEntityTouch != null) GameHooksPublisher.RemoveHookListener(HookListener.Touch);
        if (_OnEntityEndTouch != null) GameHooksPublisher.RemoveHookListener(HookListener.EndTouch);
        if (_OnItemServicesCanAcquireHook != null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
        if (_OnWeaponServicesCanUseHook != null) GameHooksPublisher.RemoveHookListener(HookListener.CanUse);
        if (_OnWeaponServicesDropWeaponHook != null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop);
        if (_OnPlayerPawnPostThink != null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink);
        if (_OnEntityIdentityAcceptInputHook != null) GameHooksPublisher.RemoveHookListener(HookListener.AcceptInput);
        if (_OnEntityFireOutputHook != null) GameHooksPublisher.RemoveHookListener(HookListener.FireOutput);
        if (_OnMovementServicesRunCommandHook != null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);

        lock (consoleOutputLock)
        {
            if (_OnConsoleOutput != null)
            {
                _OnConsoleOutput = null;
                EventPublisher.RemoveConsoleOutputListener();
            }
        }

        EventPublisher.Unsubscribe(this);
        GC.SuppressFinalize(this);
    }

    public bool ListensToTick => OnTick != null;

    public void InvokeOnTick()
    {
        if (OnTick == null)
        {
            return;
        }

        try
        {

            OnTick.Invoke();
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnTick.");
            }
        }

    }

    public bool ListensToWorldUpdate => OnWorldUpdate != null;

    public void InvokeOnWorldUpdate()
    {
        if (OnWorldUpdate == null)
        {
            return;
        }
        try
        {

            OnWorldUpdate.Invoke();
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnWorldUpdate.");
            }
        }

    }

    public bool ListensToClientConnected => OnClientConnected != null;

    public void InvokeOnClientConnected( ref OnClientConnectedEvent @event )
    {
        if (OnClientConnected == null)
        {
            return;
        }

        try
        {

            OnClientConnected.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnClientConnected.");
            }
        }

    }

    public bool ListensToClientDisconnected => OnClientDisconnected != null;

    public void InvokeOnClientDisconnected( ref OnClientDisconnectedEvent @event )
    {
        if (OnClientDisconnected == null)
        {
            return;
        }
        try
        {

            OnClientDisconnected.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnClientDisconnected.");
            }
        }

    }

    public bool ListensToClientKeyStateChanged => OnClientKeyStateChanged != null;

    public void InvokeOnClientKeyStateChanged( ref OnClientKeyStateChangedEvent @event )
    {
        if (OnClientKeyStateChanged == null)
        {
            return;
        }
        try
        {

            OnClientKeyStateChanged.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnClientKeyStateChanged.");
            }
        }

    }

    public bool ListensToClientPutInServer => OnClientPutInServer != null;

    public void InvokeOnClientPutInServer( ref OnClientPutInServerEvent @event )
    {
        if (OnClientPutInServer == null)
        {
            return;
        }
        try
        {

            OnClientPutInServer.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnClientPutInServer.");
            }
        }

    }

    public bool ListensToClientSteamAuthorize => OnClientSteamAuthorize != null;

    public void InvokeOnClientSteamAuthorize( ref OnClientSteamAuthorizeEvent @event )
    {
        if (OnClientSteamAuthorize == null)
        {
            return;
        }
        try
        {

            OnClientSteamAuthorize.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnClientSteamAuthorize.");
            }
        }

    }

    public bool ListensToClientSteamAuthorizeFail => OnClientSteamAuthorizeFail != null;

    public void InvokeOnClientSteamAuthorizeFail( ref OnClientSteamAuthorizeFailEvent @event )
    {
        if (OnClientSteamAuthorizeFail == null)
        {
            return;
        }
        try
        {

            OnClientSteamAuthorizeFail.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnClientSteamAuthorizeFail.");
            }
        }

    }

    public bool ListensToEntityCreated => OnEntityCreated != null;

    public void InvokeOnEntityCreated( ref OnEntityCreatedEvent @event )
    {
        if (OnEntityCreated == null)
        {
            return;
        }
        try
        {

            OnEntityCreated.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityCreated.");
            }
        }

    }

    public bool ListensToClientVoice => OnClientVoice != null;

    public void InvokeOnClientVoice( ref OnClientVoiceEvent @event )
    {
        if (OnClientVoice == null)
        {
            return;
        }
        try
        {

            OnClientVoice.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnClientVoice.");
            }
        }

    }

    public bool ListensToEntityDeleted => OnEntityDeleted != null;

    public void InvokeOnEntityDeleted( ref OnEntityDeletedEvent @event )
    {
        if (OnEntityDeleted == null)
        {
            return;
        }
        try
        {

            OnEntityDeleted.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityDeleted.");
            }
        }

    }

    public bool ListensToEntityParentChanged => OnEntityParentChanged != null;

    public void InvokeOnEntityParentChanged( ref OnEntityParentChangedEvent @event )
    {
        if (OnEntityParentChanged == null)
        {
            return;
        }
        try
        {

            OnEntityParentChanged.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityParentChanged.");
            }
        }

    }

    public bool ListensToEntitySpawned => OnEntitySpawned != null;

    public void InvokeOnEntitySpawned( ref OnEntitySpawnedEvent @event )
    {
        if (OnEntitySpawned == null)
        {
            return;
        }
        try
        {

            OnEntitySpawned.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntitySpawned.");
            }
        }

    }

    public bool ListensToMapLoad => OnMapLoad != null;

    public void InvokeOnMapLoad( ref OnMapLoadEvent @event )
    {
        if (OnMapLoad == null)
        {
            return;
        }
        try
        {

            OnMapLoad.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnMapLoad.");
            }
        }

    }

    public bool ListensToMapUnload => OnMapUnload != null;

    public void InvokeOnMapUnload( ref OnMapUnloadEvent @event )
    {
        if (OnMapUnload == null)
        {
            return;
        }
        try
        {

            OnMapUnload.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnMapUnload.");
            }
        }

    }

    public bool ListensToProcessUsercmds => _OnClientProcessUsercmds != null;

    public bool ListensToTakeDamage => _OnEntityTakeDamage != null;

    public void InvokeOnClientProcessUsercmds( ref OnClientProcessUsercmdsEvent @event )
    {
        if (_OnClientProcessUsercmds == null)
        {
            return;
        }
        try
        {

            _OnClientProcessUsercmds.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnClientProcessUsercmds.");
            }
        }

    }

    public void InvokeOnEntityTakeDamage( ref OnEntityTakeDamageEvent @event )
    {
        if (_OnEntityTakeDamage == null)
        {
            return;
        }
        try
        {

            _OnEntityTakeDamage.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityTakeDamage.");
            }
        }

    }

    public bool ListensToPrecacheResource => OnPrecacheResource != null;

    public void InvokeOnPrecacheResource( ref OnPrecacheResourceEvent @event )
    {
        if (OnPrecacheResource == null)
        {
            return;
        }
        try
        {

            OnPrecacheResource.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnPrecacheResource.");
            }
        }

    }

    public bool ListensToEntityStartTouch => _OnEntityStartTouch != null;

    public void InvokeOnEntityStartTouch( ref OnEntityStartTouchEvent @event )
    {
        if (_OnEntityStartTouch == null)
        {
            return;
        }
        try
        {

            _OnEntityStartTouch.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityStartTouch.");
            }
        }

    }

    public bool ListensToEntityTouch => _OnEntityTouch != null;

    public void InvokeOnEntityTouch( ref OnEntityTouchEvent @event )
    {
        if (_OnEntityTouch == null)
        {
            return;
        }
        try
        {

            _OnEntityTouch.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityTouch.");
            }
        }

    }

    public bool ListensToEntityEndTouch => _OnEntityEndTouch != null;

    public void InvokeOnEntityEndTouch( ref OnEntityEndTouchEvent @event )
    {
        if (_OnEntityEndTouch == null)
        {
            return;
        }
        try
        {

            _OnEntityEndTouch.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityEndTouch.");
            }
        }

    }

    public void InvokeOnSteamAPIActivatedHook()
    {
        if (OnSteamAPIActivated == null)
        {
            return;
        }
        try
        {

            OnSteamAPIActivated.Invoke();
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnSteamAPIActivatedHook.");
            }
        }

    }

    public bool ListensToCanAcquire => _OnItemServicesCanAcquireHook != null;

    public void InvokeOnItemServicesCanAcquireHook( ref OnItemServicesCanAcquireHookEvent @event )
    {
        if (_OnItemServicesCanAcquireHook == null)
        {
            return;
        }
        try
        {

            _OnItemServicesCanAcquireHook.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnItemServicesCanAcquireHook.");
            }
        }

    }

    public bool ListensToCanUseWeapon => _OnWeaponServicesCanUseHook != null;

    public void InvokeOnWeaponServicesCanUseHook( ref OnWeaponServicesCanUseHookEvent @event )
    {
        if (_OnWeaponServicesCanUseHook == null)
        {
            return;
        }
        try
        {

            _OnWeaponServicesCanUseHook.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnWeaponServicesCanUseHook.");
            }
        }

    }

    public bool ListensToConsoleOutput => System.Threading.Volatile.Read(ref _OnConsoleOutput) != null;

    public void InvokeOnConsoleOutput( ref OnConsoleOutputEvent @event )
    {
        var handler = System.Threading.Volatile.Read(ref _OnConsoleOutput);
        if (handler == null)
        {
            return;
        }
        try
        {

            handler.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnConsoleOutput.");
            }
        }

    }

    public bool ListensToConVarValueChanged => OnConVarValueChanged != null;

    public void InvokeOnConVarValueChanged( ref OnConVarValueChanged @event )
    {
        if (OnConVarValueChanged == null)
        {
            return;
        }
        try
        {

            OnConVarValueChanged.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnConVarValueChanged.");
            }
        }

    }

    public bool ListensToConCommandCreated => OnConCommandCreated != null;

    public void InvokeOnConCommandCreated( ref OnConCommandCreated @event )
    {
        if (OnConCommandCreated == null)
        {
            return;
        }
        try
        {

            OnConCommandCreated.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnConCommandCreated.");
            }
        }

    }

    public bool ListensToConVarCreated => OnConVarCreated != null;

    public void InvokeOnConVarCreated( ref OnConVarCreated @event )
    {
        if (OnConVarCreated == null)
        {
            return;
        }
        try
        {

            OnConVarCreated.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnConVarCreated.");
            }
        }

    }

    public void InvokeOnCommandExecuteHook( ref OnCommandExecuteHookEvent @event )
    {
        if (OnCommandExecuteHook == null)
        {
            return;
        }
        try
        {

            OnCommandExecuteHook.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnCommandExecuteHook.");
            }
        }

    }

    public bool ListensToRunCommand => _OnMovementServicesRunCommandHook != null;

    public void InvokeOnMovementServicesRunCommandHook( ref OnMovementServicesRunCommandHookEvent @event )
    {
        if (_OnMovementServicesRunCommandHook == null)
        {
            return;
        }
        try
        {

            _OnMovementServicesRunCommandHook.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnMovementServicesRunCommandHook.");
            }
        }

    }

    public bool ListensToPostThink => _OnPlayerPawnPostThink != null;

    public void InvokeOnPlayerPawnPostThinkHook( ref OnPlayerPawnPostThinkHookEvent @event )
    {
        if (_OnPlayerPawnPostThink == null)
        {
            return;
        }
        try
        {

            _OnPlayerPawnPostThink.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnPlayerPawnPostThink.");
            }
        }

    }

    public bool ListensToAcceptInput => _OnEntityIdentityAcceptInputHook != null;

    public void InvokeOnEntityIdentityAcceptInputHook( ref OnEntityIdentityAcceptInputHookEvent @event )
    {
        if (_OnEntityIdentityAcceptInputHook == null)
        {
            return;
        }
        try
        {

            _OnEntityIdentityAcceptInputHook.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityIdentityAcceptInput.");
            }
        }

    }

    public bool ListensToWeaponDrop => _OnWeaponServicesDropWeaponHook != null;

    public void InvokeOnWeaponServicesDropWeaponHook( ref OnWeaponServicesDropWeaponHook @event )
    {
        if (_OnWeaponServicesDropWeaponHook == null)
        {
            return;
        }
        try
        {

            _OnWeaponServicesDropWeaponHook.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnWeaponServicesDropWeaponHook.");
            }
        }

    }

    public bool ListensToFireOutput => _OnEntityFireOutputHook != null;

    public void InvokeOnEntityFireOutputHook( ref OnEntityFireOutputHookEvent @event )
    {
        if (_OnEntityFireOutputHook == null)
        {
            return;
        }
        try
        {

            _OnEntityFireOutputHook.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnEntityFireOutputHook.");
            }
        }

    }

    public bool ListensToStartupServer => OnStartupServer != null;

    public void InvokeOnStartupServer()
    {
        if (OnStartupServer == null)
        {
            return;
        }

        try
        {

            OnStartupServer.Invoke();
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnStartupServer.");
            }
        }

    }

    public bool ListensToCustomHudClicked => OnCustomHudClicked != null;

    public void InvokeOnCustomHudClicked( ref OnCustomHudClickedEvent @event )
    {
        if (OnCustomHudClicked == null)
        {
            return;
        }

        try
        {
            OnCustomHudClicked.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking OnCustomHudClicked.");
            }
        }

    }
}

using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.Events;

/// <summary>
/// Plugin-scoped custom event subscriber.
/// </summary>
internal class EventSubscriber : IEventSubscriber, IDisposable
{

    private CoreContext _Id { get; init; }
    private IContextedProfilerService _Profiler { get; init; }
    private ILogger<EventSubscriber> _Logger { get; init; }

    public EventSubscriber( CoreContext id, IContextedProfilerService profiler, ILogger<EventSubscriber> logger )
    {
        _Id = id;
        _Profiler = profiler;
        _Logger = logger;
        EventPublisher.Subscribe(this);
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
    public event EventDelegates.OnClientProcessUsercmds? OnClientProcessUsercmds;
    public event EventDelegates.OnConVarValueChanged? OnConVarValueChanged;
    public event EventDelegates.OnConCommandCreated? OnConCommandCreated;
    public event EventDelegates.OnConVarCreated? OnConVarCreated;
    public event EventDelegates.OnEntityTakeDamage? OnEntityTakeDamage;
    public event EventDelegates.OnPrecacheResource? OnPrecacheResource;
    public event EventDelegates.OnEntityTouchHook? OnEntityTouchHook;
    public event EventDelegates.OnEntityStartTouch? OnEntityStartTouch;
    public event EventDelegates.OnEntityTouch? OnEntityTouch;
    public event EventDelegates.OnEntityEndTouch? OnEntityEndTouch;
    public event EventDelegates.OnItemServicesCanAcquireHook? OnItemServicesCanAcquireHook;
    public event EventDelegates.OnWeaponServicesCanUseHook? OnWeaponServicesCanUseHook;
    public event EventDelegates.OnConsoleOutput? OnConsoleOutput;
    public event EventDelegates.OnCommandExecuteHook? OnCommandExecuteHook;
    public event EventDelegates.OnSteamAPIActivated? OnSteamAPIActivated;
    public event EventDelegates.OnMovementServicesRunCommandHook? OnMovementServicesRunCommandHook;
    public event EventDelegates.OnPlayerPawnPostThink? OnPlayerPawnPostThink;
    public event EventDelegates.OnEntityIdentityAcceptInputHook? OnEntityIdentityAcceptInputHook;

    public void Dispose()
    {
        EventPublisher.Unsubscribe(this);
    }

    public void InvokeOnTick()
    {
        try
        {
            _Profiler.StartRecording("Event::OnTick");
            OnTick?.Invoke();
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnTick.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnTick");
        }
    }

    public void InvokeOnWorldUpdate()
    {
        try
        {
            _Profiler.StartRecording("Event::OnWorldUpdate");
            OnWorldUpdate?.Invoke();
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnWorldUpdate.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnWorldUpdate");
        }
    }

    public void InvokeOnClientConnected( OnClientConnectedEvent @event )
    {
        try
        {
            if (OnClientConnected == null) return;
            _Profiler.StartRecording("Event::OnClientConnected");
            OnClientConnected?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnClientConnected.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnClientConnected");
        }
    }

    public void InvokeOnClientDisconnected( OnClientDisconnectedEvent @event )
    {
        try
        {
            if (OnClientDisconnected == null) return;
            _Profiler.StartRecording("Event::OnClientDisconnected");
            OnClientDisconnected?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnClientDisconnected.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnClientDisconnected");
        }
    }

    public void InvokeOnClientKeyStateChanged( OnClientKeyStateChangedEvent @event )
    {
        try
        {
            if (OnClientKeyStateChanged == null) return;
            _Profiler.StartRecording("Event::OnClientKeyStateChanged");
            OnClientKeyStateChanged?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnClientKeyStateChanged.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnClientKeyStateChanged");
        }
    }

    public void InvokeOnClientPutInServer( OnClientPutInServerEvent @event )
    {
        try
        {
            if (OnClientPutInServer == null) return;
            _Profiler.StartRecording("Event::OnClientPutInServer");
            OnClientPutInServer?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnClientPutInServer.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnClientPutInServer");
        }
    }

    public void InvokeOnClientSteamAuthorize( OnClientSteamAuthorizeEvent @event )
    {
        try
        {
            if (OnClientSteamAuthorize == null) return;
            _Profiler.StartRecording("Event::OnClientSteamAuthorize");
            OnClientSteamAuthorize?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnClientSteamAuthorize.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnClientSteamAuthorize");
        }
    }

    public void InvokeOnClientSteamAuthorizeFail( OnClientSteamAuthorizeFailEvent @event )
    {
        try
        {
            if (OnClientSteamAuthorizeFail == null) return;
            _Profiler.StartRecording("Event::OnClientSteamAuthorizeFail");
            OnClientSteamAuthorizeFail?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnClientSteamAuthorizeFail.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnClientSteamAuthorizeFail");
        }
    }

    public void InvokeOnEntityCreated( OnEntityCreatedEvent @event )
    {
        try
        {
            if (OnEntityCreated == null) return;
            _Profiler.StartRecording("Event::OnEntityCreated");
            OnEntityCreated?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityCreated.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityCreated");
        }
    }

    public void InvokeOnEntityDeleted( OnEntityDeletedEvent @event )
    {
        try
        {
            if (OnEntityDeleted == null) return;
            _Profiler.StartRecording("Event::OnEntityDeleted");
            OnEntityDeleted?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityDeleted.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityDeleted");
        }
    }

    public void InvokeOnEntityParentChanged( OnEntityParentChangedEvent @event )
    {
        try
        {
            if (OnEntityParentChanged == null) return;
            _Profiler.StartRecording("Event::OnEntityParentChanged");
            OnEntityParentChanged?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityParentChanged.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityParentChanged");
        }
    }

    public void InvokeOnEntitySpawned( OnEntitySpawnedEvent @event )
    {
        try
        {
            if (OnEntitySpawned == null) return;
            _Profiler.StartRecording("Event::OnEntitySpawned");
            OnEntitySpawned?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntitySpawned.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntitySpawned");
        }
    }

    public void InvokeOnMapLoad( OnMapLoadEvent @event )
    {
        try
        {
            if (OnMapLoad == null) return;
            _Profiler.StartRecording("Event::OnMapLoad");
            OnMapLoad?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnMapLoad.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnMapLoad");
        }
    }

    public void InvokeOnMapUnload( OnMapUnloadEvent @event )
    {
        try
        {
            if (OnMapUnload == null) return;
            _Profiler.StartRecording("Event::OnMapUnload");
            OnMapUnload?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnMapUnload.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnMapUnload");
        }
    }

    public void InvokeOnClientProcessUsercmds( OnClientProcessUsercmdsEvent @event )
    {
        try
        {
            if (OnClientProcessUsercmds == null) return;
            _Profiler.StartRecording("Event::OnClientProcessUsercmds");
            OnClientProcessUsercmds?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnClientProcessUsercmds.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnClientProcessUsercmds");
        }
    }

    public void InvokeOnEntityTakeDamage( OnEntityTakeDamageEvent @event )
    {
        try
        {
            if (OnEntityTakeDamage == null) return;
            _Profiler.StartRecording("Event::OnEntityTakeDamage");
            OnEntityTakeDamage?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityTakeDamage.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityTakeDamage");
        }
    }

    public void InvokeOnPrecacheResource( OnPrecacheResourceEvent @event )
    {
        try
        {
            if (OnPrecacheResource == null) return;
            _Profiler.StartRecording("Event::OnPrecacheResource");
            OnPrecacheResource?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnPrecacheResource.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnPrecacheResource");
        }
    }

    [Obsolete("InvokeOnEntityTouchHook is deprecated. Use InvokeOnEntityStartTouch, InvokeOnEntityTouch, or InvokeOnEntityEndTouch instead.")]
    public void InvokeOnEntityTouchHook( OnEntityTouchHookEvent @event )
    {
        try
        {
            if (OnEntityTouchHook == null) return;
            _Profiler.StartRecording("Event::OnEntityTouchHook");
            OnEntityTouchHook?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityTouchHook.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityTouchHook");
        }
    }

    public void InvokeOnEntityStartTouch( OnEntityStartTouchEvent @event )
    {
        try
        {
            if (OnEntityStartTouch == null) return;
            _Profiler.StartRecording("Event::OnEntityStartTouch");
            OnEntityStartTouch?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityStartTouch.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityStartTouch");
        }
    }

    public void InvokeOnEntityTouch( OnEntityTouchEvent @event )
    {
        try
        {
            if (OnEntityTouch == null) return;
            _Profiler.StartRecording("Event::OnEntityTouch");
            OnEntityTouch?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityTouch.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityTouch");
        }
    }

    public void InvokeOnEntityEndTouch( OnEntityEndTouchEvent @event )
    {
        try
        {
            if (OnEntityEndTouch == null) return;
            _Profiler.StartRecording("Event::OnEntityEndTouch");
            OnEntityEndTouch?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityEndTouch.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityEndTouch");
        }
    }

    public void InvokeOnSteamAPIActivatedHook()
    {
        try
        {
            _Profiler.StartRecording("Event::OnSteamAPIActivatedHook");
            OnSteamAPIActivated?.Invoke();
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnSteamAPIActivatedHook.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnSteamAPIActivatedHook");
        }
    }

    public void InvokeOnItemServicesCanAcquireHook( OnItemServicesCanAcquireHookEvent @event )
    {
        try
        {
            if (OnItemServicesCanAcquireHook == null) return;
            _Profiler.StartRecording("Event::OnItemServicesCanAcquireHook");
            OnItemServicesCanAcquireHook?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnItemServicesCanAcquireHook.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnItemServicesCanAcquireHook");
        }
    }

    public void InvokeOnWeaponServicesCanUseHook( OnWeaponServicesCanUseHookEvent @event )
    {
        try
        {
            if (OnWeaponServicesCanUseHook == null) return;
            _Profiler.StartRecording("Event::OnWeaponServicesCanUseHook");
            OnWeaponServicesCanUseHook?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnWeaponServicesCanUseHook.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnWeaponServicesCanUseHook");
        }
    }

    public void InvokeOnConsoleOutput( OnConsoleOutputEvent @event )
    {
        try
        {
            if (OnConsoleOutput == null) return;
            _Profiler.StartRecording("Event::OnConsoleOutput");
            OnConsoleOutput?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnConsoleOutput.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnConsoleOutput");
        }
    }

    public void InvokeOnConVarValueChanged( OnConVarValueChanged @event )
    {
        try
        {
            if (OnConVarValueChanged == null) return;
            _Profiler.StartRecording("Event::OnConVarValueChanged");
            OnConVarValueChanged?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnConVarValueChanged.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnConVarValueChanged");
        }
    }

    public void InvokeOnConCommandCreated( OnConCommandCreated @event )
    {
        try
        {
            if (OnConCommandCreated == null) return;
            _Profiler.StartRecording("Event::OnConCommandCreated");
            OnConCommandCreated?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnConCommandCreated.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnConCommandCreated");
        }
    }

    public void InvokeOnConVarCreated( OnConVarCreated @event )
    {
        try
        {
            if (OnConVarCreated == null) return;
            _Profiler.StartRecording("Event::OnConVarCreated");
            OnConVarCreated?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnConVarCreated.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnConVarCreated");
        }
    }

    public void InvokeOnCommandExecuteHook( OnCommandExecuteHookEvent @event )
    {
        try
        {
            if (OnCommandExecuteHook == null) return;
            _Profiler.StartRecording("Event::OnCommandExecuteHook");
            OnCommandExecuteHook?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnCommandExecuteHook.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnCommandExecuteHook");
        }
    }

    public void InvokeOnMovementServicesRunCommandHook( OnMovementServicesRunCommandHookEvent @event )
    {
        try
        {
            if (OnMovementServicesRunCommandHook == null) return;
            _Profiler.StartRecording("Event::OnMovementServicesRunCommandHook");
            OnMovementServicesRunCommandHook?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnMovementServicesRunCommandHook.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnMovementServicesRunCommandHook");
        }
    }

    public void InvokeOnPlayerPawnPostThinkHook( OnPlayerPawnPostThinkHookEvent @event )
    {
        try
        {
            if (OnPlayerPawnPostThink == null) return;
            _Profiler.StartRecording("Event::OnPlayerPawnPostThink");
            OnPlayerPawnPostThink?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnPlayerPawnPostThink.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnPlayerPawnPostThink");
        }
    }

    public void InvokeOnEntityIdentityAcceptInputHook( OnEntityIdentityAcceptInputHookEvent @event )
    {
        try
        {
            if (OnEntityIdentityAcceptInputHook == null) return;
            _Profiler.StartRecording("Event::OnEntityIdentityAcceptInput");
            OnEntityIdentityAcceptInputHook?.Invoke(@event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Error invoking OnEntityIdentityAcceptInput.");
        }
        finally
        {
            _Profiler.StopRecording("Event::OnEntityIdentityAcceptInput");
        }
    }
}
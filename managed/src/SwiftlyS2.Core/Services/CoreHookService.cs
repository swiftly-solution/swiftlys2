
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using SwiftlyS2.Core.Datamaps;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.SteamAPI;

namespace SwiftlyS2.Core.Services;

internal class CoreHookService : IDisposable
{
    private readonly ISwiftlyCore core;
    private readonly ILogger<CoreHookService> logger;
    private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public CoreHookService( ILogger<CoreHookService> logger, ISwiftlyCore core )
    {
        this.logger = logger;
        this.core = core;

        HookExecuteCommand();
        HookICvarFindConCommandTemplate();
        HookCCSPlayerItemServicesCanAcquire();
        HookCCSPlayerWeaponServicesCanUse();
        HookCBaseEntityTouchTemplate();
        HookSteamServerAPIActivated();
        HookCPlayerMovementServicesRunCommand();
        HookCCSPlayerPawnPostThink();
        HookEntityIdentityAcceptInput();
        HookEntityIOOutputFireOutputInternal();
        HookDispatchDatamapFunction();
        HookWeaponServicesDropWeapon();
        HookOnClientProcessUsercmds();
    }

    /*
        Original function in engine2.dll: __int64 sub_1C0CD0(__int64 a1, int a2, unsigned int a3, ...)
        This is a variadic function, but we only need the first two variable arguments (v55, v57)

        __int64 sub_1C0CD0(__int64 a1, int a2, unsigned int a3, ...)
        {
            ...

            va_list va; // [rsp+D28h] [rbp+D28h]
            __int64 v55; // [rsp+E28h] [rbp+D28h] BYREF
            va_list va1; // [rsp+E28h] [rbp+D28h]

            ...

            va_start(va1, a3);
            va_start(va, a3);
            v55 = va_arg(va1, _QWORD);
            v57 = va_arg(va1, _QWORD);

            ...
        }

        So we model it as a fixed 5-parameter function for interop purposes
    */
    private delegate nint ExecuteCommand( nint a1, int a2, uint a3, nint a4, nint a5 );
    private delegate nint ICvarFindConCommandWindows( nint pICvar, nint pRet, nint pConCommandName, int unk1 );
    private delegate nint ICvarFindConCommandLinux( nint pICvar, nint pConCommandName, int unk1 );
    private delegate nint CBaseEntityTouchTemplate( nint pBaseEntity, nint pOtherEntity );
    private delegate void SteamServerAPIActivated( nint pServer );
    private delegate void CEntityIdentityAcceptInput( nint pEntityIdentity, nint inputName, nint activator, nint caller, nint variant, int outputId, nint unk1, nint unk2 );
    private delegate void CEntityIOOutputFireOutputInternal( nint pEntityIO, nint pActivator, nint pCaller, nint pVariant, float flDelay, nint unk1, nint unk2 );
    private delegate void DispatchDatamapFunction( nint a1, nint pDatamapFunc, nint a3, uint a4, nint a5, double a6 /* unknown */ );

    private IUnmanagedFunction<ExecuteCommand>? executeCommand;
    private Guid executeCommandGuid;
    private IUnmanagedFunction<ICvarFindConCommandWindows>? findConCommandWindows;
    private IUnmanagedFunction<ICvarFindConCommandLinux>? findConCommandLinux;
    private Guid findConCommandGuid;
    private IUnmanagedFunction<CBaseEntityTouchTemplate>? entityStartTouch;
    private Guid entityStartTouchGuid;
    private IUnmanagedFunction<CBaseEntityTouchTemplate>? entityTouch;
    private Guid entityTouchGuid;
    private IUnmanagedFunction<CBaseEntityTouchTemplate>? entityEndTouch;
    private Guid entityEndTouchGuid;
    private IUnmanagedFunction<SteamServerAPIActivated>? steamServerAPIActivated;
    private Guid steamServerAPIActivatedGuid;
    private IUnmanagedFunction<CEntityIdentityAcceptInput>? entityIdentityAcceptInput;
    private Guid entityIdentityAcceptInputGuid;
    private IUnmanagedFunction<CEntityIOOutputFireOutputInternal>? entityIOOutputFireOutputInternal;
    private Guid entityIOOutputFireOutputInternalGuid;
    private IUnmanagedFunction<DispatchDatamapFunction>? dispatchDatamapFunction;
    private Guid dispatchDatamapFunctionGuid;

    internal void HookEntityIdentityAcceptInput()
    {
        var address = core.GameData.GetSignature("CEntityIdentity::AcceptInput");

        logger.LogInformation("Hooking CEntityIdentity::AcceptInput at {Address:X}", address);

        entityIdentityAcceptInput = core.Memory.GetUnmanagedFunctionByAddress<CEntityIdentityAcceptInput>(address);
        entityIdentityAcceptInputGuid = entityIdentityAcceptInput.AddHook(next =>
        {
            return ( pEntityIdentity, pInputName, pActivator, pCaller, pVariant, outputId, unk1, unk2 ) =>
            {
                unsafe
                {
                    var entityIdentity = core.Memory.ToSchemaClass<CEntityIdentity>(pEntityIdentity);
                    if (!entityIdentity.IsValid || !entityIdentity.EntityInstance.IsValid)
                    {
                        next()(pEntityIdentity, pInputName, pActivator, pCaller, pVariant, outputId, unk1, unk2);
                        return;
                    }
                    var inputName = pInputName != nint.Zero ? pInputName.AsRef<CUtlSymbolLarge>().Value : "";
                    var activator = pActivator != nint.Zero ? EntityManager.GetEntityByAddress(pActivator) : null;
                    var caller = pCaller != nint.Zero ? EntityManager.GetEntityByAddress(pCaller) : null;

                    var @event = new OnEntityIdentityAcceptInputHookEvent {
                        Identity = entityIdentity,
                        EntityInstance = EntityManager.GetEntityByIndex(entityIdentity.EntityInstance.Index)!,
                        DesignerName = entityIdentity.DesignerName,
                        InputName = inputName,
                        Activator = activator,
                        Caller = caller,
                        _variant = (CVariant<CVariantDefaultAllocator>*)pVariant,
                        OutputId = outputId,
                        Result = HookResult.Continue
                    };
                    EventPublisher.InvokeOnEntityIdentityAcceptInputHook(@event);

                    if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal)
                    {
                        return;
                    }

                    next()(pEntityIdentity, pInputName, pActivator, pCaller, pVariant, outputId, unk1, unk2);
                }
            };
        });
    }

    internal void UnhookEntityIdentityAcceptInput()
    {
        if (entityIdentityAcceptInput == null) return;
        entityIdentityAcceptInput.RemoveHook(entityIdentityAcceptInputGuid);
        entityIdentityAcceptInput = null;
    }

    internal unsafe void HookEntityIOOutputFireOutputInternal()
    {
        var address = core.GameData.GetSignature("CEntityIOOutput::FireOutputInternal");

        logger.LogInformation("Hooking CEntityIOOutput_FireOutputInternal at {Address:X}", address);

        entityIOOutputFireOutputInternal = core.Memory.GetUnmanagedFunctionByAddress<CEntityIOOutputFireOutputInternal>(address);
        entityIOOutputFireOutputInternalGuid = entityIOOutputFireOutputInternal.AddHook(next =>
        {
            return ( pEntityIO, pActivator, pCaller, pVariant, flDelay, unk1, unk2 ) =>
            {
                var entityIO = pEntityIO.AsRef<CEntityIOOutput>();

                var outputName = entityIO.Desc.Name.Value;
                var activator = pActivator != nint.Zero ? EntityManager.GetEntityByAddress(pActivator) : null;
                var caller = pCaller != nint.Zero ? EntityManager.GetEntityByAddress(pCaller) : null;

                var variant = pVariant.AsRef<CVariant<CVariantDefaultAllocator>>();

                var @event = new OnEntityFireOutputHookEvent {
                    _entityIO = (CEntityIOOutput*)pEntityIO,
                    _variant = (CVariant<CVariantDefaultAllocator>*)pVariant,
                    DesignerName = caller?.DesignerName ?? string.Empty,
                    OutputName = outputName,
                    Activator = activator,
                    Caller = caller,
                    VariantValue = variant,
                    Delay = flDelay,
                    Result = HookResult.Continue
                };
                EventPublisher.InvokeEntityFireOutputHook(@event);

                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal)
                {
                    return;
                }

                next()(pEntityIO, pActivator, pCaller, pVariant, flDelay, unk1, unk2);
            };
        });
    }

    internal void UnhookEntityIOOutputFireOutputInternal()
    {
        if (entityIOOutputFireOutputInternal == null) return;
        entityIOOutputFireOutputInternal.RemoveHook(entityIOOutputFireOutputInternalGuid);
        entityIOOutputFireOutputInternal = null;
    }

    internal void HookExecuteCommand()
    {
        var address = core.GameData.GetSignature("Cmd_ExecuteCommand");

        logger.LogInformation("Hooking Cmd_ExecuteCommand at {Address:X}", address);

        executeCommand = core.Memory.GetUnmanagedFunctionByAddress<ExecuteCommand>(address);
        executeCommandGuid = executeCommand.AddHook(( next ) =>
        {
            return ( a1, a2, a3, a4, a5 ) =>
            {
                unsafe
                {
                    if (a5 != nint.Zero)
                    {
                        ref var command = ref Unsafe.AsRef<CCommand>((void*)a5);
                        var @eventPre = new OnCommandExecuteHookEvent(ref command, HookMode.Pre);
                        EventPublisher.InvokeOnCommandExecuteHook(@eventPre);

                        if (@eventPre.Result == HookResult.Stop || @eventPre.Result == HookResult.CancelOriginal)
                        {
                            return 0;
                        }

                        var result = next()(a1, a2, a3, a4, a5);

                        var @eventPost = new OnCommandExecuteHookEvent(ref command, HookMode.Post);
                        EventPublisher.InvokeOnCommandExecuteHook(@eventPost);
                        return result;
                    }
                    return next()(a1, a2, a3, a4, a5);
                }
            };
        });
    }

    internal void UnhookExecuteCommand()
    {
        if (executeCommand == null) return;
        executeCommand.RemoveHook(executeCommandGuid);
        executeCommand = null;
    }

    internal void WeaponDropPre( ref WeaponDropPreContext @event )
    {
        var @e = new OnWeaponServicesDropWeaponHook {
            WeaponServices = @event.Params.Player.PlayerPawn!.WeaponServices!,
            Weapon = @event.Params.Weapon,
            SwappingWeapon = @event.Params.SwappingWeapon,
            Result = @event.HookResult
        };
        EventPublisher.InvokeOnWeaponServicesDropWeaponHook(@e);
        @event.SetHookResult(@e.Result);
    }

    internal void HookWeaponServicesDropWeapon()
    {
        core.GameHooks.Weapons.Drop.Pre += WeaponDropPre;
    }

    internal void UnhookWeaponServicesDropWeapon()
    {
        core.GameHooks.Weapons.Drop.Pre -= WeaponDropPre;
    }

    internal void HookICvarFindConCommandTemplate()
    {
        var offset = core.GameData.GetOffset("ICvar::FindConCommand");
        if (IsWindows)
        {
            findConCommandWindows = core.Memory.GetUnmanagedFunctionByVTable<ICvarFindConCommandWindows>(core.Memory.GetVTableAddress(Library.Tier0, "CCvar")!.Value, offset);
            logger.LogInformation("Hooking ICvar::FindConCommand at {Address:X}", findConCommandWindows.Address);
            findConCommandGuid = findConCommandWindows.AddHook(( next ) =>
            {
                return ( pICvar, pRet, pConCommandName, unk1 ) =>
                {
                    var commandName = Marshal.PtrToStringAnsi(pConCommandName)!;
                    if (commandName.StartsWith("ecwb", StringComparison.OrdinalIgnoreCase))
                    {
                        commandName = commandName.Substring(4);
                        var bytes = Encoding.UTF8.GetBytes(commandName);
                        unsafe
                        {
                            var pStr = (nint)NativeMemory.AllocZeroed((nuint)bytes.Length);
                            pStr.CopyFrom(bytes);
                            var result = next()(pICvar, pRet, pStr, unk1);
                            NativeMemory.Free((void*)pStr);
                            return result;
                        }
                    }
                    return next()(pICvar, pRet, pConCommandName, unk1);
                };
            });
        }
        else
        {
            findConCommandLinux = core.Memory.GetUnmanagedFunctionByVTable<ICvarFindConCommandLinux>(core.Memory.GetVTableAddress(Library.Tier0, "CCvar")!.Value, offset);
            logger.LogInformation("Hooking ICvar::FindConCommand at {Address:X}", findConCommandLinux.Address);
            findConCommandGuid = findConCommandLinux.AddHook(( next ) =>
            {
                return ( pICvar, pConCommandName, unk1 ) =>
                {
                    var commandName = Marshal.PtrToStringUTF8(pConCommandName)!;
                    if (commandName.StartsWith("ecwb", StringComparison.OrdinalIgnoreCase))
                    {
                        commandName = commandName.Substring(4);
                        var bytes = Encoding.UTF8.GetBytes(commandName);
                        unsafe
                        {
                            var pStr = (nint)NativeMemory.AllocZeroed((nuint)bytes.Length);
                            pStr.CopyFrom(bytes);
                            var result = next()(pICvar, pStr, unk1);
                            NativeMemory.Free((void*)pStr);
                            return result;
                        }
                    }
                    return next()(pICvar, pConCommandName, unk1);
                };
            });
        }
    }

    internal void UnhookICvarFindConCommandTemplate()
    {
        if (IsWindows)
        {
            if (findConCommandWindows == null) return;
            findConCommandWindows.RemoveHook(findConCommandGuid);
            findConCommandWindows = null;
        }
        else
        {
            if (findConCommandLinux == null) return;
            findConCommandLinux.RemoveHook(findConCommandGuid);
            findConCommandLinux = null;
        }
    }

    internal void CanAcquireEventPost( ref CanAcquireItemPostContext @event )
    {
        var @e = new OnItemServicesCanAcquireHookEvent {
            ItemServices = @event.Params.Player.PlayerPawn!.ItemServices!,
            EconItemView = @event.Params.EconItemView,
            WeaponVData = @event.Params.WeaponVData,
            AcquireMethod = @event.Params.AcquireMethod,
            OriginalResult = @event.Return
        };

        EventPublisher.InvokeOnCanAcquireHook(@e);
        @event.Return = @e.OriginalResult;
    }

    internal void HookCCSPlayerItemServicesCanAcquire()
    {
        core.GameHooks.Items.CanAcquire.Post += CanAcquireEventPost;
    }

    internal void UnhookCCSPlayerItemServicesCanAcquire()
    {
        core.GameHooks.Items.CanAcquire.Post -= CanAcquireEventPost;
    }

    internal void CanUseEventPost( ref CanUseWeaponPostContext @event )
    {
        var @e = new OnWeaponServicesCanUseHookEvent {
            WeaponServices = @event.Params.Player.PlayerPawn!.WeaponServices!,
            Weapon = @event.Params.Weapon,
            OriginalResult = @event.Return
        };

        EventPublisher.InvokeOnWeaponServicesCanUseHook(@e);
        @event.Return = @e.OriginalResult;
    }

    internal void HookCCSPlayerWeaponServicesCanUse()
    {
        core.GameHooks.Weapons.CanUse.Post += CanUseEventPost;
    }

    internal void UnhookCCSPlayerWeaponServicesCanUse()
    {
        core.GameHooks.Weapons.CanUse.Post -= CanUseEventPost;
    }

    internal void HookCBaseEntityTouchTemplate()
    {
        var touchOffset = core.GameData.GetOffset("CBaseEntity::Touch");
        var startTouchOffset = core.GameData.GetOffset("CBaseEntity::StartTouch");
        var endTouchOffset = core.GameData.GetOffset("CBaseEntity::EndTouch");
        entityStartTouch = core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, startTouchOffset);
        entityTouch = core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, touchOffset);
        entityEndTouch = core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, endTouchOffset);
        logger.LogInformation("Hooking CBaseEntity::StartTouch at {Address:X}", entityStartTouch.Address);
        logger.LogInformation("Hooking CBaseEntity::Touch at {Address:X}", entityTouch.Address);
        logger.LogInformation("Hooking CBaseEntity::EndTouch at {Address:X}", entityEndTouch.Address);

        entityStartTouchGuid = entityStartTouch.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = EntityManager.GetEntityByAddress(pBaseEntity) as CBaseEntity;
                var otherEntity = EntityManager.GetEntityByAddress(pOtherEntity) as CBaseEntity;
                using var @event = new OnEntityStartTouchEvent {
                    Entity = entity ?? core.Memory.ToSchemaClass<CBaseEntity>(pBaseEntity),
                    OtherEntity = otherEntity ?? core.Memory.ToSchemaClass<CBaseEntity>(pOtherEntity)
                };
                EventPublisher.InvokeOnEntityStartTouch(@event);
                return next()(pBaseEntity, pOtherEntity);
            };
        });

        entityTouchGuid = entityTouch.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = EntityManager.GetEntityByAddress(pBaseEntity) as CBaseEntity;
                var otherEntity = EntityManager.GetEntityByAddress(pOtherEntity) as CBaseEntity;
                using var @event = new OnEntityTouchEvent {
                    Entity = entity ?? core.Memory.ToSchemaClass<CBaseEntity>(pBaseEntity),
                    OtherEntity = otherEntity ?? core.Memory.ToSchemaClass<CBaseEntity>(pOtherEntity)
                };
                EventPublisher.InvokeOnEntityTouch(@event);
                return next()(pBaseEntity, pOtherEntity);
            };
        });

        entityEndTouchGuid = entityEndTouch.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = EntityManager.GetEntityByAddress(pBaseEntity) as CBaseEntity;
                var otherEntity = EntityManager.GetEntityByAddress(pOtherEntity) as CBaseEntity;
                using var @event = new OnEntityEndTouchEvent {
                    Entity = entity ?? core.Memory.ToSchemaClass<CBaseEntity>(pBaseEntity),
                    OtherEntity = otherEntity ?? core.Memory.ToSchemaClass<CBaseEntity>(pOtherEntity)
                };
                EventPublisher.InvokeOnEntityEndTouch(@event);
                return next()(pBaseEntity, pOtherEntity);
            };
        });
    }

    internal void UnhookCBaseEntityTouchTemplate()
    {
        if (entityStartTouch != null)
        {
            entityStartTouch.RemoveHook(entityStartTouchGuid);
            entityStartTouch = null;
        }
        if (entityTouch != null)
        {
            entityTouch.RemoveHook(entityTouchGuid);
            entityTouch = null;
        }
        if (entityEndTouch != null)
        {
            entityEndTouch.RemoveHook(entityEndTouchGuid);
            entityEndTouch = null;
        }
    }

    internal void HookSteamServerAPIActivated()
    {
        var offset = core.GameData.GetOffset("IServerGameDLL::GameServerSteamAPIActivated");
        steamServerAPIActivated = core.Memory.GetUnmanagedFunctionByVTable<SteamServerAPIActivated>(core.Memory.GetVTableAddress(Library.Server, "CSource2Server")!.Value, offset);
        logger.LogInformation("Hooking IServerGameDLL::GameServerSteamAPIActivated at {Address:X}", steamServerAPIActivated.Address);
        steamServerAPIActivatedGuid = steamServerAPIActivated.AddHook(next =>
        {
            return ( pServer ) =>
            {
                if (!CSteamGameServerAPIContext.Init())
                {
                    logger.LogError("Failed to initialize Steamworks GameServer API context.");
                    return;
                }

                EventPublisher.InvokeOnSteamAPIActivatedHook();
                next()(pServer);
            };
        });
    }

    internal void UnhookSteamServerAPIActivated()
    {
        if (steamServerAPIActivated == null) return;
        steamServerAPIActivated.RemoveHook(steamServerAPIActivatedGuid);
        steamServerAPIActivated = null;
    }

    internal void MovementServicesRunCommandHookPre( ref RunCommandMovementPreContext @event )
    {
        using var @ev = new OnMovementServicesRunCommandHookEvent {
            MovementServices = @event.Params.Player.PlayerPawn!.MovementServices!,
            ButtonState = @event.Params.UserCmd.ButtonState,
            UserCmdPB = @event.Params.UserCmd.CSGOUserCmd
        };
        EventPublisher.InvokeOnMovementServicesRunCommandHook(@ev);
    }

    internal void HookCPlayerMovementServicesRunCommand()
    {
        core.GameHooks.Movement.RunCommand.Pre += MovementServicesRunCommandHookPre;
    }

    internal void UnhookCPlayerMovementServicesRunCommand()
    {
        core.GameHooks.Movement.RunCommand.Pre -= MovementServicesRunCommandHookPre;
    }

    internal void CCSPlayerPostPostThinkPre( ref PostThinkPawnPreContext @event )
    {
        using var @ev = new OnPlayerPawnPostThinkHookEvent {
            PlayerPawn = @event.Params.Player.PlayerPawn!
        };
        EventPublisher.InvokeOnPlayerPawnPostThinkHook(@ev);
    }

    internal void HookCCSPlayerPawnPostThink()
    {
        core.GameHooks.Pawn.PostThink.Pre += CCSPlayerPostPostThinkPre;
    }

    internal void UnhookCCSPlayerPawnPostThink()
    {
        core.GameHooks.Pawn.PostThink.Pre -= CCSPlayerPostPostThinkPre;
    }

    internal void HookDispatchDatamapFunction()
    {
        var address = core.GameData.GetSignature("DispatchDatamapFunction");
        dispatchDatamapFunction = core.Memory.GetUnmanagedFunctionByAddress<DispatchDatamapFunction>(address);
        dispatchDatamapFunctionGuid = dispatchDatamapFunction.AddHook(next =>
        {
            return ( a1, pDatamapFunc, a3, a4, a5, a6 ) =>
            {
                try
                {
                    var func = pDatamapFunc;
                    if (DatamapFunctionHookManager.TryGetHook(func, out var hook))
                    {
                        func = hook;
                    }
                    next()(a1, func, a3, a4, a5, a6);
                }
                catch (Exception e)
                {
                    if (!GlobalExceptionHandler.Handle(ref e)) return;
                    AnsiConsole.WriteException(e);
                }
            };
        });
    }

    internal void UnhookDispatchDatamapFunction()
    {
        if (dispatchDatamapFunction == null) return;
        dispatchDatamapFunction.RemoveHook(dispatchDatamapFunctionGuid);
        dispatchDatamapFunction = null;
    }

    internal void OnClientProcessUsercmds( ref ProcessUsercmdsPreContext @event )
    {
        var @ev = new OnClientProcessUsercmdsEvent {
            PlayerId = @event.Params.Player.PlayerID,
            Paused = @event.Params.Paused,
            Margin = @event.Params.Margin,
            Usercmds = (List<CSGOUserCmdPB>)@event.Params.Usercmds.Select(@e => @e.CSGOUserCmd)
        };
        EventPublisher.OnClientProcessUsercmds(ref @ev);
    }

    internal void HookOnClientProcessUsercmds()
    {
        core.GameHooks.Controller.ProcessUsercmds.Pre += OnClientProcessUsercmds;
    }

    internal void UnhookOnClientProcessUsercmds()
    {
        core.GameHooks.Controller.ProcessUsercmds.Pre -= OnClientProcessUsercmds;
    }

    public void Dispose()
    {
        UnhookExecuteCommand();
        UnhookICvarFindConCommandTemplate();
        UnhookCCSPlayerItemServicesCanAcquire();
        UnhookCCSPlayerWeaponServicesCanUse();
        UnhookCBaseEntityTouchTemplate();
        UnhookSteamServerAPIActivated();
        UnhookEntityIdentityAcceptInput();
        UnhookEntityIOOutputFireOutputInternal();
        UnhookWeaponServicesDropWeapon();
        UnhookDispatchDatamapFunction();
        UnhookCCSPlayerPawnPostThink();
        UnhookCPlayerMovementServicesRunCommand();
        UnhookOnClientProcessUsercmds();
    }
}

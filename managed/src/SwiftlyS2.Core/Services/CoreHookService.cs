
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using SwiftlyS2.Core.Datamaps;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Core.Schemas;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
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
    private delegate nint CCSPlayerItemServicesCanAcquire( nint pItemServices, nint pEconItemView, int acquireMethod, nint unk1 );
    private delegate byte CCSPlayerWeaponServicesCanUse( nint pWeaponServices, nint pBasePlayerWeapon );
    private delegate nint CBaseEntityTouchTemplate( nint pBaseEntity, nint pOtherEntity );
    private delegate void SteamServerAPIActivated( nint pServer );
    private delegate nint CPlayerMovementServicesRunCommand( nint pMovementServices, nint pUserCmd );
    private delegate nint CCSPlayerPawnPostThink( nint pPlayerPawn );
    private delegate void CEntityIdentityAcceptInput( nint pEntityIdentity, nint inputName, nint activator, nint caller, nint variant, int outputId, nint unk1, nint unk2 );
    private delegate void CEntityIOOutputFireOutputInternal( nint pEntityIO, nint pActivator, nint pCaller, nint pVariant, float flDelay, nint unk1, nint unk2 );
    private delegate void DispatchDatamapFunction( nint a1, nint pDatamapFunc, nint a3, uint a4, nint a5, double a6 /* unknown */ );
    private delegate byte DropWeaponWindows( nint weaponServices, nint playerWeapon, byte swapping );
    private delegate nint DropWeaponLinux( nint weaponServices, nint playerWeapon, byte swapping );

    private IUnmanagedFunction<ExecuteCommand>? executeCommand;
    private Guid executeCommandGuid;
    private IUnmanagedFunction<ICvarFindConCommandWindows>? findConCommandWindows;
    private IUnmanagedFunction<ICvarFindConCommandLinux>? findConCommandLinux;
    private Guid findConCommandGuid;
    private IUnmanagedFunction<CCSPlayerItemServicesCanAcquire>? itemServicesCanAcquire;
    private Guid itemServicesCanAcquireGuid;
    private IUnmanagedFunction<CCSPlayerWeaponServicesCanUse>? weaponServicesCanUse;
    private Guid weaponServicesCanUseGuid;
    private IUnmanagedFunction<CBaseEntityTouchTemplate>? entityStartTouch;
    private Guid entityStartTouchGuid;
    private IUnmanagedFunction<CBaseEntityTouchTemplate>? entityTouch;
    private Guid entityTouchGuid;
    private IUnmanagedFunction<CBaseEntityTouchTemplate>? entityEndTouch;
    private Guid entityEndTouchGuid;
    private IUnmanagedFunction<SteamServerAPIActivated>? steamServerAPIActivated;
    private Guid steamServerAPIActivatedGuid;
    private IUnmanagedFunction<CPlayerMovementServicesRunCommand>? movementServiceRunCommand;
    private Guid movementServiceRunCommandGuid;
    private IUnmanagedFunction<CCSPlayerPawnPostThink>? playerPawnPostThink;
    private Guid playerPawnPostThinkGuid;
    private IUnmanagedFunction<CEntityIdentityAcceptInput>? entityIdentityAcceptInput;
    private Guid entityIdentityAcceptInputGuid;
    private IUnmanagedFunction<CEntityIOOutputFireOutputInternal>? entityIOOutputFireOutputInternal;
    private Guid entityIOOutputFireOutputInternalGuid;
    private IUnmanagedFunction<DispatchDatamapFunction>? dispatchDatamapFunction;
    private Guid dispatchDatamapFunctionGuid;
    private IUnmanagedFunction<DropWeaponWindows>? dropWeaponWindows;
    private IUnmanagedFunction<DropWeaponLinux>? dropWeaponLinux;
    private Guid dropWeaponGuid;

    private void HookEntityIdentityAcceptInput()
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

    private unsafe void HookEntityIOOutputFireOutputInternal()
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

    private void HookExecuteCommand()
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

    private void HookWeaponServicesDropWeapon()
    {
        var sig = core.GameData.GetSignature("CCSPlayer_WeaponServices::DropWeapon");
        if (IsWindows)
        {
            dropWeaponWindows = core.Memory.GetUnmanagedFunctionByAddress<DropWeaponWindows>(sig);
            logger.LogInformation("Hooking CCSPlayer_WeaponServices::DropWeapon at {Address:X}", dropWeaponWindows.Address);
            dropWeaponGuid = dropWeaponWindows.AddHook(next =>
            {
                return ( pWeaponServices, pPlayerWeapon, swapping ) =>
                {
                    var weaponServices = core.Memory.ToSchemaClass<CCSPlayer_WeaponServices>(pWeaponServices);
                    var playerWeapon = pPlayerWeapon != nint.Zero ? EntityManager.GetEntityByAddress(pPlayerWeapon) as CBasePlayerWeapon ?? core.Memory.ToSchemaClass<CBasePlayerWeapon>(pPlayerWeapon) : null;

                    var @event = new OnWeaponServicesDropWeaponHook {
                        WeaponServices = weaponServices,
                        Weapon = playerWeapon,
                        SwappingWeapon = swapping != 0,
                        Result = HookResult.Continue
                    };
                    EventPublisher.InvokeOnWeaponServicesDropWeaponHook(@event);

                    return (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) ? (byte)0 : next()(pWeaponServices, pPlayerWeapon, swapping);
                };
            });
        }
        else
        {
            dropWeaponLinux = core.Memory.GetUnmanagedFunctionByAddress<DropWeaponLinux>(sig);
            logger.LogInformation("Hooking CCSPlayer_WeaponServices::DropWeapon at {Address:X}", dropWeaponLinux.Address);
            dropWeaponGuid = dropWeaponLinux.AddHook(next =>
            {
                return ( pWeaponServices, pPlayerWeapon, swapping ) =>
                {
                    var weaponServices = core.Memory.ToSchemaClass<CCSPlayer_WeaponServices>(pWeaponServices);
                    var playerWeapon = pPlayerWeapon != nint.Zero ? EntityManager.GetEntityByAddress(pPlayerWeapon) as CBasePlayerWeapon ?? core.Memory.ToSchemaClass<CBasePlayerWeapon>(pPlayerWeapon) : null;

                    var @event = new OnWeaponServicesDropWeaponHook {
                        WeaponServices = weaponServices,
                        Weapon = playerWeapon,
                        SwappingWeapon = swapping != 0,
                        Result = HookResult.Continue
                    };
                    EventPublisher.InvokeOnWeaponServicesDropWeaponHook(@event);

                    return (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) ? 0 : next()(pWeaponServices, pPlayerWeapon, swapping);
                };
            });
        }
    }

    private void HookICvarFindConCommandTemplate()
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

    private void HookCCSPlayerItemServicesCanAcquire()
    {
        var address = core.GameData.GetSignature("CCSPlayer_ItemServices::CanAcquire");

        logger.LogInformation("Hooking CCSPlayer_ItemServices::CanAcquire at {Address:X}", address);

        itemServicesCanAcquire = core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerItemServicesCanAcquire>(address);
        itemServicesCanAcquireGuid = itemServicesCanAcquire.AddHook(next =>
        {
            return ( pItemServices, pEconItemView, acquireMethod, unk1 ) =>
            {
                var result = next()(pItemServices, pEconItemView, acquireMethod, unk1);

                var itemServices = core.Memory.ToSchemaClass<CCSPlayer_ItemServices>(pItemServices);

                Schema.isFollowingServerGuidelines = false;

                var econItemView = core.Memory.ToSchemaClass<CEconItemView>(pEconItemView);

                var @event = new OnItemServicesCanAcquireHookEvent {
                    ItemServices = itemServices,
                    EconItemView = econItemView,
                    WeaponVData = core.Helpers.GetWeaponCSDataFromKey(econItemView.ItemDefinitionIndex),
                    AcquireMethod = (AcquireMethod)acquireMethod,
                    OriginalResult = (AcquireResult)result
                };

                Schema.isFollowingServerGuidelines = NativeServerHelpers.IsFollowingServerGuidelines();

                EventPublisher.InvokeOnCanAcquireHook(@event);

                if (@event.Intercepted)
                {
                    // original result is modified here.
                    return (int)@event.OriginalResult;
                }

                return result;
            };
        });
    }

    private void HookCCSPlayerWeaponServicesCanUse()
    {
        var offset = core.GameData.GetOffset("CCSPlayer_WeaponServices::CanUse");
        weaponServicesCanUse = core.Memory.GetUnmanagedFunctionByVTable<CCSPlayerWeaponServicesCanUse>(core.Memory.GetVTableAddress(Library.Server, "CCSPlayer_WeaponServices")!.Value, offset);
        logger.LogInformation("Hooking CCSPlayer_WeaponServices::CanUse at {Address:X}", weaponServicesCanUse.Address);
        weaponServicesCanUseGuid = weaponServicesCanUse.AddHook(next =>
        {
            return ( pWeaponServices, pBasePlayerWeapon ) =>
            {
                var result = next()(pWeaponServices, pBasePlayerWeapon);

                var weaponServices = core.Memory.ToSchemaClass<CCSPlayer_WeaponServices>(pWeaponServices);
                var basePlayerWeapon = EntityManager.GetEntityByAddress(pBasePlayerWeapon) as CCSWeaponBase ?? core.Memory.ToSchemaClass<CCSWeaponBase>(pBasePlayerWeapon);

                var @event = new OnWeaponServicesCanUseHookEvent {
                    WeaponServices = weaponServices,
                    Weapon = basePlayerWeapon,
                    OriginalResult = result != 0
                };
                EventPublisher.InvokeOnWeaponServicesCanUseHook(@event);

                return @event.Intercepted ? @event.OriginalResult ? (byte)1 : (byte)0 : result;
            };
        });
    }

    private void HookCBaseEntityTouchTemplate()
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

    private void HookSteamServerAPIActivated()
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

    private void HookCPlayerMovementServicesRunCommand()
    {
        var offset = core.GameData.GetOffset("CPlayer_MovementServices::RunCommand");
        movementServiceRunCommand = core.Memory.GetUnmanagedFunctionByVTable<CPlayerMovementServicesRunCommand>(core.Memory.GetVTableAddress(Library.Server, "CPlayer_MovementServices")!.Value, offset);
        logger.LogInformation("Hooking CPlayer_MovementServices::RunCommand at {Address:X}", movementServiceRunCommand.Address);
        movementServiceRunCommandGuid = movementServiceRunCommand.AddHook(( next ) =>
        {
            return ( pMovementServices, pUserCmd ) =>
            {
                var movementService = core.Memory.ToSchemaClass<CCSPlayer_MovementServices>(pMovementServices);
                var userCmdPb = new CSGOUserCmdPBImpl(pUserCmd + 0x10, false);
                var buttonState = new CInButtonStateImpl(pUserCmd + 0x58);

                using var @event = new OnMovementServicesRunCommandHookEvent {
                    MovementServices = movementService,
                    ButtonState = buttonState,
                    UserCmdPB = userCmdPb
                };
                EventPublisher.InvokeOnMovementServicesRunCommandHook(@event);

                var result = next()(pMovementServices, pUserCmd);
                return result;
            };
        });
    }

    private void HookCCSPlayerPawnPostThink()
    {
        var address = core.GameData.GetSignature("CCSPlayerPawn::PostThink");

        logger.LogInformation("Hooking CCSPlayerPawn::PostThink at {Address:X}", address);

        playerPawnPostThink = core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerPawnPostThink>(address);
        playerPawnPostThinkGuid = playerPawnPostThink.AddHook(( next ) =>
        {
            return ( pPlayerPawn ) =>
            {
                var playerPawn = EntityManager.GetEntityByAddress(pPlayerPawn) as CCSPlayerPawn;

                using var @event = new OnPlayerPawnPostThinkHookEvent {
                    PlayerPawn = playerPawn ?? core.Memory.ToSchemaClass<CCSPlayerPawn>(pPlayerPawn)
                };
                EventPublisher.InvokeOnPlayerPawnPostThinkHook(@event);

                return next()(pPlayerPawn);
            };
        });
    }

    private void HookDispatchDatamapFunction()
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

    public void Dispose()
    {
        executeCommand?.RemoveHook(executeCommandGuid);
        findConCommandWindows?.RemoveHook(findConCommandGuid);
        findConCommandLinux?.RemoveHook(findConCommandGuid);
        itemServicesCanAcquire?.RemoveHook(itemServicesCanAcquireGuid);
        weaponServicesCanUse?.RemoveHook(weaponServicesCanUseGuid);
        entityStartTouch?.RemoveHook(entityStartTouchGuid);
        entityTouch?.RemoveHook(entityTouchGuid);
        entityEndTouch?.RemoveHook(entityEndTouchGuid);
        steamServerAPIActivated?.RemoveHook(steamServerAPIActivatedGuid);
        movementServiceRunCommand?.RemoveHook(movementServiceRunCommandGuid);
        playerPawnPostThink?.RemoveHook(playerPawnPostThinkGuid);
        entityIdentityAcceptInput?.RemoveHook(entityIdentityAcceptInputGuid);
        entityIOOutputFireOutputInternal?.RemoveHook(entityIOOutputFireOutputInternalGuid);
        dispatchDatamapFunction?.RemoveHook(dispatchDatamapFunctionGuid);
        dropWeaponWindows?.RemoveHook(dropWeaponGuid);
        dropWeaponLinux?.RemoveHook(dropWeaponGuid);
    }
}
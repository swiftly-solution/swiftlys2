using System.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.SteamAPI;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Core.Schemas;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Core.Services;

internal class CoreHookService : IDisposable
{
    private readonly ILogger<CoreHookService> logger;
    private readonly ISwiftlyCore core;

    public CoreHookService( ILogger<CoreHookService> logger, ISwiftlyCore core )
    {
        this.logger = logger;
        this.core = core;

        HookExecuteCommand();
        HookFindConCommandTemplate();
        HookCCSPlayerItemServicesCanAcquire();
        HookCCSPlayerWeaponServicesCanUse();
        HookCBaseEntityTouchTemplate();
        HookSteamServerAPIActivated();
        HookCPlayerMovementServicesRunCommand();
        HookCCSPlayerPawnPostThink();
        HookEntityIdentityAcceptInput();
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
    private delegate nint FindConCommandWindows( nint pICvar, nint pRet, nint pConCommandName, int unk1 );
    private delegate nint FindConCommandLinux( nint pICvar, nint pConCommandName, int unk1 );
    private delegate int CCSPlayerItemServicesCanAcquire( nint pItemServices, nint pEconItemView, nint acquireMethod, nint unk1 );
    private delegate byte CCSPlayerWeaponServicesCanUse( nint pWeaponServices, nint pBasePlayerWeapon );
    private delegate nint CBaseEntityTouchTemplate( nint pBaseEntity, nint pOtherEntity );
    private delegate void SteamServerAPIActivated( nint pServer );
    private delegate nint CPlayerMovementServicesRunCommand( nint pMovementServices, nint pUserCmd );
    private delegate void CCSPlayerPawnPostThink( nint pPlayerPawn );
    private delegate void CEntityIdentityAcceptInput( nint pEntityIdentity, nint inputName, nint activator, nint caller, nint variant, int outputId, nint unk1, nint unk2 );

    private IUnmanagedFunction<ExecuteCommand>? executeCommand;
    private Guid executeCommandGuid;
    private IUnmanagedFunction<FindConCommandWindows>? findConCommandWindows;
    private IUnmanagedFunction<FindConCommandLinux>? findConCommandLinux;
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

    private void HookEntityIdentityAcceptInput()
    {
        var address = core.GameData.GetSignature("CEntityIdentity::AcceptInput");

        logger.LogInformation("Hooking CEntityIdentity::AcceptInput at {Address}", address);

        entityIdentityAcceptInput = core.Memory.GetUnmanagedFunctionByAddress<CEntityIdentityAcceptInput>(address);
        entityIdentityAcceptInputGuid = entityIdentityAcceptInput.AddHook(next =>
        {
            return ( pEntityIdentity, pInputName, pActivator, pCaller, pVariant, outputId, unk1, unk2 ) =>
            {
                var entityIdentity = core.Memory.ToSchemaClass<CEntityIdentity>(pEntityIdentity);
                var inputName = pInputName.AsRef<CUtlSymbolLarge>();
                var activator = pActivator != nint.Zero ? core.Memory.ToSchemaClass<CEntityInstance>(pActivator) : null;
                var caller = pCaller != nint.Zero ? core.Memory.ToSchemaClass<CEntityInstance>(pCaller) : null;

                var variant = pVariant.AsRef<CVariant>();

                var @event = new OnEntityIdentityAcceptInputHookEvent {
                    Identity = entityIdentity,
                    EntityInstance = entityIdentity.EntityInstance,
                    InputName = inputName.Value,
                    Activator = activator,
                    Caller = caller,
                    VariantValue = variant,
                    OutputId = outputId,
                    Result = HookResult.Continue
                };
                EventPublisher.InvokeOnEntityIdentityAcceptInputHook(@event);

                if (@event.Result == HookResult.Stop)
                {
                    return;
                }

                next()(pEntityIdentity, pInputName, pActivator, pCaller, pVariant, outputId, unk1, unk2);
            };
        });
    }

    private void HookExecuteCommand()
    {
        var address = core.GameData.GetSignature("Cmd_ExecuteCommand");

        logger.LogInformation("Hooking Cmd_ExecuteCommand at {Address}", address);

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

    private void HookFindConCommandTemplate()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var offset = core.GameData.GetOffset("ICvar::FindConCommand");
            findConCommandLinux = core.Memory.GetUnmanagedFunctionByVTable<FindConCommandLinux>(core.Memory.GetVTableAddress("tier0", "CCvar")!.Value, offset);

            logger.LogInformation("Hooking ICvar::FindConCommand at {Address}", findConCommandLinux.Address);

            findConCommandGuid = findConCommandLinux.AddHook(( next ) =>
            {
                return ( pICvar, pConCommandName, unk1 ) =>
                {
                    var commandName = Marshal.PtrToStringAnsi(pConCommandName)!;
                    if (commandName.StartsWith("^wb^"))
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
        else
        {
            var offset = core.GameData.GetOffset("ICvar::FindConCommand");
            findConCommandWindows = core.Memory.GetUnmanagedFunctionByVTable<FindConCommandWindows>(core.Memory.GetVTableAddress("tier0", "CCvar")!.Value, offset);

            logger.LogInformation("Hooking ICvar::FindConCommand at {Address}", findConCommandWindows.Address);

            findConCommandGuid = findConCommandWindows.AddHook(( next ) =>
            {
                return ( pICvar, pRet, pConCommandName, unk1 ) =>
                {
                    var commandName = Marshal.PtrToStringAnsi(pConCommandName)!;
                    if (commandName.StartsWith("^wb^"))
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
    }

    private void HookCCSPlayerItemServicesCanAcquire()
    {
        var address = core.GameData.GetSignature("CCSPlayer_ItemServices::CanAcquire");

        logger.LogInformation("Hooking CCSPlayer_ItemServices::CanAcquire at {Address}", address);

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
        var pVtable = core.Memory.GetVTableAddress(Library.Server, "CCSPlayer_WeaponServices");

        if (pVtable == null)
        {
            logger.LogError("Failed to get CCSPlayer_WeaponServices vtable.");
            return;
        }
        weaponServicesCanUse = core.Memory.GetUnmanagedFunctionByVTable<CCSPlayerWeaponServicesCanUse>(pVtable!.Value, offset);
        logger.LogInformation("Hooking CCSPlayer_WeaponServices::CanUse at {Address}", weaponServicesCanUse.Address);
        weaponServicesCanUseGuid = weaponServicesCanUse.AddHook(next =>
        {
            return ( pWeaponServices, pBasePlayerWeapon ) =>
            {
                var result = next()(pWeaponServices, pBasePlayerWeapon);

                var weaponServices = new CCSPlayer_WeaponServicesImpl(pWeaponServices);
                var basePlayerWeapon = new CCSWeaponBaseImpl(pBasePlayerWeapon);

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
        var pVtable = core.Memory.GetVTableAddress(Library.Server, "CBaseEntity");

        if (pVtable == null)
        {
            logger.LogError("Failed to get CBaseEntity vtable.");
            return;
        }
        entityStartTouch = core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(pVtable!.Value, startTouchOffset);
        entityTouch = core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(pVtable!.Value, touchOffset);
        entityEndTouch = core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(pVtable!.Value, endTouchOffset);
        logger.LogInformation("Hooking CBaseEntity::StartTouch at {Address}", entityStartTouch.Address);
        logger.LogInformation("Hooking CBaseEntity::Touch at {Address}", entityTouch.Address);
        logger.LogInformation("Hooking CBaseEntity::EndTouch at {Address}", entityEndTouch.Address);

        entityStartTouchGuid = entityStartTouch.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = new CBaseEntityImpl(pBaseEntity);
                var otherEntity = new CBaseEntityImpl(pOtherEntity);
                EventPublisher.InvokeOnEntityStartTouch(new OnEntityStartTouchEvent { Entity = entity, OtherEntity = otherEntity });
                EventPublisher.InvokeOnEntityTouchHook(new OnEntityTouchHookEvent { Entity = entity, OtherEntity = otherEntity, TouchType = EntityTouchType.StartTouch });
                return next()(pBaseEntity, pOtherEntity);
            };
        });

        entityTouchGuid = entityTouch.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = new CBaseEntityImpl(pBaseEntity);
                var otherEntity = new CBaseEntityImpl(pOtherEntity);
                EventPublisher.InvokeOnEntityTouch(new OnEntityTouchEvent { Entity = entity, OtherEntity = otherEntity });
                EventPublisher.InvokeOnEntityTouchHook(new OnEntityTouchHookEvent { Entity = entity, OtherEntity = otherEntity, TouchType = EntityTouchType.Touch });
                return next()(pBaseEntity, pOtherEntity);
            };
        });

        entityEndTouchGuid = entityEndTouch.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = new CBaseEntityImpl(pBaseEntity);
                var otherEntity = new CBaseEntityImpl(pOtherEntity);
                EventPublisher.InvokeOnEntityEndTouch(new OnEntityEndTouchEvent { Entity = entity, OtherEntity = otherEntity });
                EventPublisher.InvokeOnEntityTouchHook(new OnEntityTouchHookEvent { Entity = entity, OtherEntity = otherEntity, TouchType = EntityTouchType.EndTouch });
                return next()(pBaseEntity, pOtherEntity);
            };
        });
    }

    private void HookSteamServerAPIActivated()
    {
        var offset = core.GameData.GetOffset("IServerGameDLL::GameServerSteamAPIActivated");
        var pVtable = core.Memory.GetVTableAddress(Library.Server, "CSource2Server");

        if (pVtable == null)
        {
            logger.LogError("Failed to get CSource2Server vtable.");
            return;
        }

        steamServerAPIActivated = core.Memory.GetUnmanagedFunctionByVTable<SteamServerAPIActivated>(pVtable!.Value, offset);
        logger.LogInformation("Hooking IServerGameDLL::GameServerSteamAPIActivated at {Address}", steamServerAPIActivated.Address);
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
        var pVtable = core.Memory.GetVTableAddress(Library.Server, "CPlayer_MovementServices");
        if (pVtable == null)
        {
            logger.LogError("Failed to get CPlayer_MovementServices vtable.");
            return;
        }
        movementServiceRunCommand = core.Memory.GetUnmanagedFunctionByVTable<CPlayerMovementServicesRunCommand>(pVtable!.Value, offset);
        logger.LogInformation("Hooking CPlayer_MovementServices::RunCommand at {Address}", movementServiceRunCommand.Address);
        movementServiceRunCommandGuid = movementServiceRunCommand.AddHook(( next ) =>
        {
            return ( pMovementServices, pUserCmd ) =>
            {
                var movementService = new CCSPlayer_MovementServicesImpl(pMovementServices);
                var userCmdPb = new CSGOUserCmdPBImpl(pUserCmd + 0x10, false);
                var buttonState = new CInButtonStateImpl(pUserCmd + 0x58);

                var @event = new OnMovementServicesRunCommandHookEvent {
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

        logger.LogInformation("Hooking CCSPlayerPawn::PostThink at {Address}", address);

        playerPawnPostThink = core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerPawnPostThink>(address);
        playerPawnPostThinkGuid = playerPawnPostThink.AddHook(( next ) =>
        {
            return ( pPlayerPawn ) =>
            {
                var playerPawn = new CCSPlayerPawnImpl(pPlayerPawn);

                var @event = new OnPlayerPawnPostThinkHookEvent {
                    PlayerPawn = playerPawn
                };
                EventPublisher.InvokeOnPlayerPawnPostThinkHook(@event);

                next()(pPlayerPawn);
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
    }
}
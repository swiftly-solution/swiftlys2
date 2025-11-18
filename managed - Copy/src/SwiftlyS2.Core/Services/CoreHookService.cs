using System.Runtime.CompilerServices;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SteamAPI;
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Core.Services;

internal class CoreHookService : IDisposable
{
  private ILogger<CoreHookService> _Logger { get; init; }
  private ISwiftlyCore _Core { get; init; }

  public CoreHookService( ILogger<CoreHookService> logger, ISwiftlyCore core )
  {
    _Logger = logger;
    _Core = core;

    HookTouch();
    HookCanAcquire();
    HookCommandExecute();
    HookICVarFindConCommand();
    HookCCSPlayer_WeaponServices_CanUse();
    HookSteamServerAPIActivated();
    HookCPlayer_MovementServices_RunCommand();
  }

  private delegate int CanAcquireDelegate( nint pItemServices, nint pEconItemView, nint acquireMethod, nint unk1 );
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
  private delegate nint ExecuteCommandDelegate( nint a1, int a2, uint a3, nint a4, nint a5 );

  private delegate byte CCSPlayer_WeaponServices_CanUse( nint pWeaponServices, nint pBasePlayerWeapon );
  private delegate nint CBaseEntity_Touch_Template( nint pBaseEntity, nint pOtherEntity );
  private delegate void SteamServerAPIActivated( nint pServer );
  private delegate nint CPlayer_MovementServices_RunCommandDelegate( nint pMovementServices, nint pUserCmd );

  private IUnmanagedFunction<ExecuteCommandDelegate>? _ExecuteCommand;
  private Guid _ExecuteCommandGuid;
  private IUnmanagedFunction<CanAcquireDelegate>? _CanAcquire;
  private Guid _CanAcquireGuid;
  private IUnmanagedFunction<CCSPlayer_WeaponServices_CanUse>? _CCSPlayer_WeaponServices_CanUse;
  private Guid _CCSPlayer_WeaponServices_CanUseGuid;
  private IUnmanagedFunction<CBaseEntity_Touch_Template>? _CBaseEntity_StartTouch;
  private Guid _CBaseEntity_StartTouchGuid;
  private IUnmanagedFunction<CBaseEntity_Touch_Template>? _CBaseEntity_Touch;
  private Guid _CBaseEntity_TouchGuid;
  private IUnmanagedFunction<CBaseEntity_Touch_Template>? _CBaseEntity_EndTouch;
  private Guid _CBaseEntity_EndTouchGuid;
  private IUnmanagedFunction<SteamServerAPIActivated>? _SteamServerAPIActivated;
  private Guid _SteamServerAPIActivatedGuid;
  private IUnmanagedFunction<CPlayer_MovementServices_RunCommandDelegate>? _CPlayer_MovementServices_RunCommand;
  private Guid _CPlayer_MovementServices_RunCommandGuid;

  private void HookSteamServerAPIActivated()
  {
    var offset = _Core.GameData.GetOffset("IServerGameDLL::GameServerSteamAPIActivated");
    var pVtable = _Core.Memory.GetVTableAddress(Library.Server, "CSource2Server");

    if (pVtable == null)
    {
      _Logger.LogError("Failed to get CSource2Server vtable.");
      return;
    }

    _SteamServerAPIActivated = _Core.Memory.GetUnmanagedFunctionByVTable<SteamServerAPIActivated>(pVtable!.Value, offset);
    _Logger.LogInformation("Hooking IServerGameDLL::GameServerSteamAPIActivated at {Address}", _SteamServerAPIActivated.Address);
    _SteamServerAPIActivatedGuid = _SteamServerAPIActivated.AddHook(next =>
    {
      return ( pServer ) =>
      {
        if (!CSteamGameServerAPIContext.Init())
        {
          _Logger.LogError("Failed to initialize Steamworks GameServer API context.");
          return;
        }

        
        EventPublisher.InvokeOnSteamAPIActivatedHook();
        next()(pServer);
      };
    });
  }

  private void HookCCSPlayer_WeaponServices_CanUse()
  {
    var offset = _Core.GameData.GetOffset("CCSPlayer_WeaponServices::CanUse");
    var pVtable = _Core.Memory.GetVTableAddress(Library.Server, "CCSPlayer_WeaponServices");

    if (pVtable == null)
    {
      _Logger.LogError("Failed to get CCSPlayer_WeaponServices vtable.");
      return;
    }
    _CCSPlayer_WeaponServices_CanUse = _Core.Memory.GetUnmanagedFunctionByVTable<CCSPlayer_WeaponServices_CanUse>(pVtable!.Value, offset);
    _Logger.LogInformation("Hooking CCSPlayer_WeaponServices::CanUse at {Address}", _CCSPlayer_WeaponServices_CanUse.Address);
    _CCSPlayer_WeaponServices_CanUseGuid = _CCSPlayer_WeaponServices_CanUse.AddHook(next =>
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

        if (@event.Intercepted)
        {
          return @event.OriginalResult ? (byte)1 : (byte)0;
        }

        return result;
      };
    });
  }

  private void HookTouch()
  {
    var touchOffset = _Core.GameData.GetOffset("CBaseEntity::Touch");
    var startTouchOffset = _Core.GameData.GetOffset("CBaseEntity::StartTouch");
    var endTouchOffset = _Core.GameData.GetOffset("CBaseEntity::EndTouch");
    var pVtable = _Core.Memory.GetVTableAddress(Library.Server, "CBaseEntity");

    if (pVtable == null)
    {
      _Logger.LogError("Failed to get CBaseEntity vtable.");
      return;
    }
    _CBaseEntity_StartTouch = _Core.Memory.GetUnmanagedFunctionByVTable<CBaseEntity_Touch_Template>(pVtable!.Value, startTouchOffset);
    _CBaseEntity_Touch = _Core.Memory.GetUnmanagedFunctionByVTable<CBaseEntity_Touch_Template>(pVtable!.Value, touchOffset);
    _CBaseEntity_EndTouch = _Core.Memory.GetUnmanagedFunctionByVTable<CBaseEntity_Touch_Template>(pVtable!.Value, endTouchOffset);
    _Logger.LogInformation("Hooking CBaseEntity::StartTouch at {Address}", _CBaseEntity_StartTouch.Address);
    _Logger.LogInformation("Hooking CBaseEntity::Touch at {Address}", _CBaseEntity_Touch.Address);
    _Logger.LogInformation("Hooking CBaseEntity::EndTouch at {Address}", _CBaseEntity_EndTouch.Address);

    _CBaseEntity_StartTouchGuid = _CBaseEntity_StartTouch.AddHook(next =>
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

    _CBaseEntity_TouchGuid = _CBaseEntity_Touch.AddHook(next =>
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

    _CBaseEntity_EndTouchGuid = _CBaseEntity_EndTouch.AddHook(next =>
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
  private void HookCanAcquire()
  {

    var address = _Core.GameData.GetSignature("CCSPlayer_ItemServices::CanAcquire");

    _Logger.LogInformation("Hooking CCSPlayer_ItemServices::CanAcquire at {Address}", address);

    _CanAcquire = _Core.Memory.GetUnmanagedFunctionByAddress<CanAcquireDelegate>(address);
    _CanAcquireGuid = _CanAcquire.AddHook(next =>
    {

      return ( pItemServices, pEconItemView, acquireMethod, unk1 ) =>
      {
        var result = next()(pItemServices, pEconItemView, acquireMethod, unk1);

        var itemServices = _Core.Memory.ToSchemaClass<CCSPlayer_ItemServices>(pItemServices);
        var econItemView = _Core.Memory.ToSchemaClass<CEconItemView>(pEconItemView);

        var @event = new OnItemServicesCanAcquireHookEvent {
          ItemServices = itemServices,
          EconItemView = econItemView,
          WeaponVData = _Core.Helpers.GetWeaponCSDataFromKey(econItemView.ItemDefinitionIndex),
          AcquireMethod = (AcquireMethod)acquireMethod,
          OriginalResult = (AcquireResult)result
        };

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

  private void HookCommandExecute()
  {

    var address = _Core.GameData.GetSignature("Cmd_ExecuteCommand");

    _Logger.LogInformation("Hooking Cmd_ExecuteCommand at {Address}", address);

    _ExecuteCommand = _Core.Memory.GetUnmanagedFunctionByAddress<ExecuteCommandDelegate>(address);
    _ExecuteCommandGuid = _ExecuteCommand.AddHook(( next ) =>
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

  private delegate nint FindConCommandDelegate( nint pICvar, nint pRet, nint pConCommandName, int unk1 );
  private delegate nint FindConCommandDelegateLinux( nint pICvar, nint pConCommandName, int unk1 );

  private IUnmanagedFunction<FindConCommandDelegate>? _FindConCommandWindows;
  private IUnmanagedFunction<FindConCommandDelegateLinux>? _FindConCommandLinux;
  private Guid _FindConCommandGuid;

  private void HookICVarFindConCommand()
  {
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
      var offset = _Core.GameData.GetOffset("ICvar::FindConCommand");
      _FindConCommandLinux = _Core.Memory.GetUnmanagedFunctionByVTable<FindConCommandDelegateLinux>(_Core.Memory.GetVTableAddress("tier0", "CCvar")!.Value, offset);

      _Logger.LogInformation("Hooking ICvar::FindConCommand at {Address}", _FindConCommandLinux.Address);

      _FindConCommandGuid = _FindConCommandLinux.AddHook(( next ) =>
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
      var offset = _Core.GameData.GetOffset("ICvar::FindConCommand");
      _FindConCommandWindows = _Core.Memory.GetUnmanagedFunctionByVTable<FindConCommandDelegate>(_Core.Memory.GetVTableAddress("tier0", "CCvar")!.Value, offset);

      _Logger.LogInformation("Hooking ICvar::FindConCommand at {Address}", _FindConCommandWindows.Address);

      _FindConCommandGuid = _FindConCommandWindows.AddHook(( next ) =>
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


  private void HookCPlayer_MovementServices_RunCommand()
  {
    var offset = _Core.GameData.GetOffset("CPlayer_MovementServices::RunCommand");
    var pVtable = _Core.Memory.GetVTableAddress(Library.Server, "CPlayer_MovementServices");
    if (pVtable == null)
    {
      _Logger.LogError("Failed to get CPlayer_MovementServices vtable.");
      return;
    }
    _CPlayer_MovementServices_RunCommand = _Core.Memory.GetUnmanagedFunctionByVTable<CPlayer_MovementServices_RunCommandDelegate>(pVtable!.Value, offset);
    _Logger.LogInformation("Hooking CPlayer_MovementServices::RunCommand at {Address}", _CPlayer_MovementServices_RunCommand.Address);
    _CPlayer_MovementServices_RunCommandGuid = _CPlayer_MovementServices_RunCommand.AddHook(( next ) =>
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

  public void Dispose()
  {
    _CanAcquire!.RemoveHook(_CanAcquireGuid);
    _ExecuteCommand!.RemoveHook(_ExecuteCommandGuid);
    _FindConCommandWindows?.RemoveHook(_FindConCommandGuid);
    _FindConCommandLinux?.RemoveHook(_FindConCommandGuid);
    _CCSPlayer_WeaponServices_CanUse!.RemoveHook(_CCSPlayer_WeaponServices_CanUseGuid);
    _CBaseEntity_StartTouch!.RemoveHook(_CBaseEntity_StartTouchGuid);
    _CBaseEntity_Touch!.RemoveHook(_CBaseEntity_TouchGuid);
    _CBaseEntity_EndTouch!.RemoveHook(_CBaseEntity_EndTouchGuid);
    _SteamServerAPIActivated?.RemoveHook(_SteamServerAPIActivatedGuid);
    _CPlayer_MovementServices_RunCommand?.RemoveHook(_CPlayer_MovementServices_RunCommandGuid);
  }
}
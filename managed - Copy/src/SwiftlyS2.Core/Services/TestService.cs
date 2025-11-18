using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate nint DispatchSpawnHook(nint entity, nint kv);

internal class TestService {

  private ILogger<TestService> _Logger { get; init; }
  private ProfileService _ProfileService { get; init; }
  private ISwiftlyCore _Core { get; init; }
  public unsafe TestService(
    ILogger<TestService> logger,
    ProfileService profileService,
    ISwiftlyCore core
  ) {
    _ProfileService = profileService;
    _Core = core;
    _Logger = logger;

    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");

    Test2();
  }


  public void Test()
  {
    _Core.Event.OnEntityCreated += (@event) =>
    {
      var name = _Core.Memory.GetObjectPtrVtableName(@event.Entity.Address);
      Console.WriteLine("Entity spawned: " + name);
      var hasVtable = _Core.Memory.ObjectPtrHasVtable(@event.Entity.Address + 1);
      Console.WriteLine("Has vtable: " + hasVtable);
      var vtable = _Core.Memory.ObjectPtrHasBaseClass(@event.Entity.Address, "CBaseEntity");
      Console.WriteLine("Has base class: " + vtable);
      var handle = _Core.EntitySystem.GetRefEHandle(@event.Entity);
    };
    // _Core.Event.OnItemServicesCanAcquireHook += (@event) => {
    //   Console.WriteLine(@event.EconItemView.ItemDefinitionIndex);

    //   @event.SetAcquireResult(AcquireResult.NotAllowedByProhibition);
    // };


  }
  public void Test2()
  {
    _Core.Event.OnMovementServicesRunCommandHook += (@event) => {
      @event.UserCmdPB.Base.ClientTick -= 100;
    };
    // _Core.Event.OnItemServicesCanAcquireHook += (@event) => {
    //   Console.WriteLine(@event.EconItemView.ItemDefinitionIndex);

    //   @event.SetAcquireResult(AcquireResult.NotAllowedByProhibition);
    // };


  }
}
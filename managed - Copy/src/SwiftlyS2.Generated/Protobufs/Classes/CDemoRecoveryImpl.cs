
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoRecoveryImpl : TypedProtobuf<CDemoRecovery>, CDemoRecovery
{
  public CDemoRecoveryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CDemoRecovery_DemoInitialSpawnGroupEntry InitialSpawnGroup
  { get => new CDemoRecovery_DemoInitialSpawnGroupEntryImpl(NativeNetMessages.GetNestedMessage(Address, "initial_spawn_group"), false); }


  public byte[] SpawnGroupMessage
  { get => Accessor.GetBytes("spawn_group_message"); set => Accessor.SetBytes("spawn_group_message", value); }

}

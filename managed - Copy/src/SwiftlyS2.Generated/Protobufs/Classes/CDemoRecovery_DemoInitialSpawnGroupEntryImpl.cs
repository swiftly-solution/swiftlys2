
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoRecovery_DemoInitialSpawnGroupEntryImpl : TypedProtobuf<CDemoRecovery_DemoInitialSpawnGroupEntry>, CDemoRecovery_DemoInitialSpawnGroupEntry
{
  public CDemoRecovery_DemoInitialSpawnGroupEntryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Spawngrouphandle
  { get => Accessor.GetUInt32("spawngrouphandle"); set => Accessor.SetUInt32("spawngrouphandle", value); }


  public bool WasCreated
  { get => Accessor.GetBool("was_created"); set => Accessor.SetBool("was_created", value); }

}


using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_SpawnGroup_LoadCompletedImpl : NetMessage<CNETMsg_SpawnGroup_LoadCompleted>, CNETMsg_SpawnGroup_LoadCompleted
{
  public CNETMsg_SpawnGroup_LoadCompletedImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Spawngrouphandle
  { get => Accessor.GetUInt32("spawngrouphandle"); set => Accessor.SetUInt32("spawngrouphandle", value); }

}

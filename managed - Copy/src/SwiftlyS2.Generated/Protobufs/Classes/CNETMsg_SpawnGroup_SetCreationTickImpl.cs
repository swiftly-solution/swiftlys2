
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_SpawnGroup_SetCreationTickImpl : NetMessage<CNETMsg_SpawnGroup_SetCreationTick>, CNETMsg_SpawnGroup_SetCreationTick
{
  public CNETMsg_SpawnGroup_SetCreationTickImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Spawngrouphandle
  { get => Accessor.GetUInt32("spawngrouphandle"); set => Accessor.SetUInt32("spawngrouphandle", value); }


  public int Tickcount
  { get => Accessor.GetInt32("tickcount"); set => Accessor.SetInt32("tickcount", value); }


  public uint Creationsequence
  { get => Accessor.GetUInt32("creationsequence"); set => Accessor.SetUInt32("creationsequence", value); }

}


using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_SpawnGroup_UnloadImpl : NetMessage<CNETMsg_SpawnGroup_Unload>, CNETMsg_SpawnGroup_Unload
{
  public CNETMsg_SpawnGroup_UnloadImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Spawngrouphandle
  { get => Accessor.GetUInt32("spawngrouphandle"); set => Accessor.SetUInt32("spawngrouphandle", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public int Tickcount
  { get => Accessor.GetInt32("tickcount"); set => Accessor.SetInt32("tickcount", value); }

}

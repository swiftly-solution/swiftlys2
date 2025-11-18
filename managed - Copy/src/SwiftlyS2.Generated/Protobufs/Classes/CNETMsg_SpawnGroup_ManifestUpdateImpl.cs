
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_SpawnGroup_ManifestUpdateImpl : NetMessage<CNETMsg_SpawnGroup_ManifestUpdate>, CNETMsg_SpawnGroup_ManifestUpdate
{
  public CNETMsg_SpawnGroup_ManifestUpdateImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Spawngrouphandle
  { get => Accessor.GetUInt32("spawngrouphandle"); set => Accessor.SetUInt32("spawngrouphandle", value); }


  public byte[] Spawngroupmanifest
  { get => Accessor.GetBytes("spawngroupmanifest"); set => Accessor.SetBytes("spawngroupmanifest", value); }


  public bool Manifestincomplete
  { get => Accessor.GetBool("manifestincomplete"); set => Accessor.SetBool("manifestincomplete", value); }

}

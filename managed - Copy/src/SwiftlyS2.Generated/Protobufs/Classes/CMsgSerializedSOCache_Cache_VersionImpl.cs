
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSerializedSOCache_Cache_VersionImpl : TypedProtobuf<CMsgSerializedSOCache_Cache_Version>, CMsgSerializedSOCache_Cache_Version
{
  public CMsgSerializedSOCache_Cache_VersionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Service
  { get => Accessor.GetUInt32("service"); set => Accessor.SetUInt32("service", value); }


  public ulong Version
  { get => Accessor.GetUInt64("version"); set => Accessor.SetUInt64("version", value); }

}

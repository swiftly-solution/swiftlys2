
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCDirtySDOCacheImpl : TypedProtobuf<CMsgGCToGCDirtySDOCache>, CMsgGCToGCDirtySDOCache
{
  public CMsgGCToGCDirtySDOCacheImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint SdoType
  { get => Accessor.GetUInt32("sdo_type"); set => Accessor.SetUInt32("sdo_type", value); }


  public ulong KeyUint64
  { get => Accessor.GetUInt64("key_uint64"); set => Accessor.SetUInt64("key_uint64", value); }

}


using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCDirtyMultipleSDOCacheImpl : TypedProtobuf<CMsgGCToGCDirtyMultipleSDOCache>, CMsgGCToGCDirtyMultipleSDOCache
{
  public CMsgGCToGCDirtyMultipleSDOCacheImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint SdoType
  { get => Accessor.GetUInt32("sdo_type"); set => Accessor.SetUInt32("sdo_type", value); }


  public IProtobufRepeatedFieldValueType<ulong> KeyUint64
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "key_uint64"); }

}

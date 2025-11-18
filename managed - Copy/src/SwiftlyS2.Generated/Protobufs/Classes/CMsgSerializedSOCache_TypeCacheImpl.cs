
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSerializedSOCache_TypeCacheImpl : TypedProtobuf<CMsgSerializedSOCache_TypeCache>, CMsgSerializedSOCache_TypeCache
{
  public CMsgSerializedSOCache_TypeCacheImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Type
  { get => Accessor.GetUInt32("type"); set => Accessor.SetUInt32("type", value); }


  public IProtobufRepeatedFieldValueType<byte[]> Objects
  { get => new ProtobufRepeatedFieldValueType<byte[]>(Accessor, "objects"); }


  public uint ServiceId
  { get => Accessor.GetUInt32("service_id"); set => Accessor.SetUInt32("service_id", value); }

}

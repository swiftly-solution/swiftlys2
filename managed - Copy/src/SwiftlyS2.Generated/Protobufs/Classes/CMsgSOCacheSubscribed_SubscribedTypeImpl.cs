
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSOCacheSubscribed_SubscribedTypeImpl : TypedProtobuf<CMsgSOCacheSubscribed_SubscribedType>, CMsgSOCacheSubscribed_SubscribedType
{
  public CMsgSOCacheSubscribed_SubscribedTypeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int TypeId
  { get => Accessor.GetInt32("type_id"); set => Accessor.SetInt32("type_id", value); }


  public IProtobufRepeatedFieldValueType<byte[]> ObjectData
  { get => new ProtobufRepeatedFieldValueType<byte[]>(Accessor, "object_data"); }

}

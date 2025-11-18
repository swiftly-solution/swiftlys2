
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSOCacheSubscribedImpl : TypedProtobuf<CMsgSOCacheSubscribed>, CMsgSOCacheSubscribed
{
  public CMsgSOCacheSubscribedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscribed_SubscribedType> Objects
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscribed_SubscribedType>(Accessor, "objects"); }


  public ulong Version
  { get => Accessor.GetUInt64("version"); set => Accessor.SetUInt64("version", value); }


  public CMsgSOIDOwner OwnerSoid
  { get => new CMsgSOIDOwnerImpl(NativeNetMessages.GetNestedMessage(Address, "owner_soid"), false); }

}

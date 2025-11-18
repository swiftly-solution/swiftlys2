
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CQuest_PublisherAddCommunityItemsToPlayer_Request_AttributeImpl : TypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute>, CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute
{
  public CQuest_PublisherAddCommunityItemsToPlayer_Request_AttributeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Attribute
  { get => Accessor.GetUInt32("attribute"); set => Accessor.SetUInt32("attribute", value); }


  public ulong Value
  { get => Accessor.GetUInt64("value"); set => Accessor.SetUInt64("value", value); }

}

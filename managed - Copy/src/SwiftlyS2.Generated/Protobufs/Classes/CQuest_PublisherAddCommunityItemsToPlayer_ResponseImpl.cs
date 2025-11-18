
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CQuest_PublisherAddCommunityItemsToPlayer_ResponseImpl : TypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Response>, CQuest_PublisherAddCommunityItemsToPlayer_Response
{
  public CQuest_PublisherAddCommunityItemsToPlayer_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ItemsMatched
  { get => Accessor.GetUInt32("items_matched"); set => Accessor.SetUInt32("items_matched", value); }


  public uint ItemsGranted
  { get => Accessor.GetUInt32("items_granted"); set => Accessor.SetUInt32("items_granted", value); }

}

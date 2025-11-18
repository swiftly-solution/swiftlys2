
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CQuest_PublisherAddCommunityItemsToPlayer_Response : ITypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Response>
{
  static CQuest_PublisherAddCommunityItemsToPlayer_Response ITypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CQuest_PublisherAddCommunityItemsToPlayer_ResponseImpl(handle, isManuallyAllocated);


  public uint ItemsMatched { get; set; }


  public uint ItemsGranted { get; set; }

}

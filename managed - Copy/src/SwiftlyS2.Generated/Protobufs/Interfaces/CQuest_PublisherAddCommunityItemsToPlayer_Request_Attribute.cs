
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute : ITypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute>
{
  static CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute ITypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute>.Wrap(nint handle, bool isManuallyAllocated) => new CQuest_PublisherAddCommunityItemsToPlayer_Request_AttributeImpl(handle, isManuallyAllocated);


  public uint Attribute { get; set; }


  public ulong Value { get; set; }

}


using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CQuest_PublisherAddCommunityItemsToPlayer_Request : ITypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Request>
{
  static CQuest_PublisherAddCommunityItemsToPlayer_Request ITypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CQuest_PublisherAddCommunityItemsToPlayer_RequestImpl(handle, isManuallyAllocated);


  public ulong Steamid { get; set; }


  public uint Appid { get; set; }


  public uint MatchItemType { get; set; }


  public uint MatchItemClass { get; set; }


  public string PrefixItemName { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute> Attributes { get; }


  public string Note { get; set; }

}

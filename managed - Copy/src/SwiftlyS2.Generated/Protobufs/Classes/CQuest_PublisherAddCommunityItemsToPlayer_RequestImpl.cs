
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CQuest_PublisherAddCommunityItemsToPlayer_RequestImpl : TypedProtobuf<CQuest_PublisherAddCommunityItemsToPlayer_Request>, CQuest_PublisherAddCommunityItemsToPlayer_Request
{
  public CQuest_PublisherAddCommunityItemsToPlayer_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public uint MatchItemType
  { get => Accessor.GetUInt32("match_item_type"); set => Accessor.SetUInt32("match_item_type", value); }


  public uint MatchItemClass
  { get => Accessor.GetUInt32("match_item_class"); set => Accessor.SetUInt32("match_item_class", value); }


  public string PrefixItemName
  { get => Accessor.GetString("prefix_item_name"); set => Accessor.SetString("prefix_item_name", value); }


  public IProtobufRepeatedFieldSubMessageType<CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute> Attributes
  { get => new ProtobufRepeatedFieldSubMessageType<CQuest_PublisherAddCommunityItemsToPlayer_Request_Attribute>(Accessor, "attributes"); }


  public string Note
  { get => Accessor.GetString("note"); set => Accessor.SetString("note", value); }

}

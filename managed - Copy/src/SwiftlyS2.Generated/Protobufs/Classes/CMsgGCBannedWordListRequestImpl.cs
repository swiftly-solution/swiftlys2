
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCBannedWordListRequestImpl : TypedProtobuf<CMsgGCBannedWordListRequest>, CMsgGCBannedWordListRequest
{
  public CMsgGCBannedWordListRequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint BanListGroupId
  { get => Accessor.GetUInt32("ban_list_group_id"); set => Accessor.SetUInt32("ban_list_group_id", value); }


  public uint WordId
  { get => Accessor.GetUInt32("word_id"); set => Accessor.SetUInt32("word_id", value); }

}

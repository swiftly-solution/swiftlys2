
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCBannedWordListResponseImpl : TypedProtobuf<CMsgGCBannedWordListResponse>, CMsgGCBannedWordListResponse
{
  public CMsgGCBannedWordListResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint BanListGroupId
  { get => Accessor.GetUInt32("ban_list_group_id"); set => Accessor.SetUInt32("ban_list_group_id", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCBannedWord> WordList
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCBannedWord>(Accessor, "word_list"); }

}

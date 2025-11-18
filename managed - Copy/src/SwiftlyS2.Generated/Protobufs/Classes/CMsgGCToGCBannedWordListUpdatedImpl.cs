
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCBannedWordListUpdatedImpl : TypedProtobuf<CMsgGCToGCBannedWordListUpdated>, CMsgGCToGCBannedWordListUpdated
{
  public CMsgGCToGCBannedWordListUpdatedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint GroupId
  { get => Accessor.GetUInt32("group_id"); set => Accessor.SetUInt32("group_id", value); }

}

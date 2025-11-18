
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCollectItemImpl : TypedProtobuf<CMsgGCCollectItem>, CMsgGCCollectItem
{
  public CMsgGCCollectItemImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong CollectionItemId
  { get => Accessor.GetUInt64("collection_item_id"); set => Accessor.SetUInt64("collection_item_id", value); }


  public ulong SubjectItemId
  { get => Accessor.GetUInt64("subject_item_id"); set => Accessor.SetUInt64("subject_item_id", value); }

}


using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSetItemPositionsImpl : TypedProtobuf<CMsgSetItemPositions>, CMsgSetItemPositions
{
  public CMsgSetItemPositionsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgSetItemPositions_ItemPosition> ItemPositions
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSetItemPositions_ItemPosition>(Accessor, "item_positions"); }

}

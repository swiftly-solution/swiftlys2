
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSetItemPositions_ItemPosition : ITypedProtobuf<CMsgSetItemPositions_ItemPosition>
{
  static CMsgSetItemPositions_ItemPosition ITypedProtobuf<CMsgSetItemPositions_ItemPosition>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSetItemPositions_ItemPositionImpl(handle, isManuallyAllocated);


  public uint LegacyItemId { get; set; }


  public uint Position { get; set; }


  public ulong ItemId { get; set; }

}

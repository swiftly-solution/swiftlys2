
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSetItemPositions : ITypedProtobuf<CMsgSetItemPositions>
{
  static CMsgSetItemPositions ITypedProtobuf<CMsgSetItemPositions>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSetItemPositionsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgSetItemPositions_ItemPosition> ItemPositions { get; }

}

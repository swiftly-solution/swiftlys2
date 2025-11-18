
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgConsumableExhausted : ITypedProtobuf<CMsgConsumableExhausted>
{
  static CMsgConsumableExhausted ITypedProtobuf<CMsgConsumableExhausted>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgConsumableExhaustedImpl(handle, isManuallyAllocated);


  public int ItemDefId { get; set; }

}

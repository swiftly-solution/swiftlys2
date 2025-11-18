
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MLTick : ITypedProtobuf<MLTick>
{
  static MLTick ITypedProtobuf<MLTick>.Wrap(nint handle, bool isManuallyAllocated) => new MLTickImpl(handle, isManuallyAllocated);


  public int TickCount { get; set; }


  public MLGameState State { get; }


  public IProtobufRepeatedFieldSubMessageType<MLEvent> Events { get; }

}

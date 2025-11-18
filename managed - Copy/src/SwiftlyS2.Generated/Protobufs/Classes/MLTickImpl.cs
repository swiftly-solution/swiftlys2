
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MLTickImpl : TypedProtobuf<MLTick>, MLTick
{
  public MLTickImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int TickCount
  { get => Accessor.GetInt32("tick_count"); set => Accessor.SetInt32("tick_count", value); }


  public MLGameState State
  { get => new MLGameStateImpl(NativeNetMessages.GetNestedMessage(Address, "state"), false); }


  public IProtobufRepeatedFieldSubMessageType<MLEvent> Events
  { get => new ProtobufRepeatedFieldSubMessageType<MLEvent>(Accessor, "events"); }

}

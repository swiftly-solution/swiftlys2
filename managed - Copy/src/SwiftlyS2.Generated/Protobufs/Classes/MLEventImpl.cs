
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MLEventImpl : TypedProtobuf<MLEvent>, MLEvent
{
  public MLEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string EventName
  { get => Accessor.GetString("event_name"); set => Accessor.SetString("event_name", value); }


  public IProtobufRepeatedFieldSubMessageType<MLDict> Data
  { get => new ProtobufRepeatedFieldSubMessageType<MLDict>(Accessor, "data"); }

}

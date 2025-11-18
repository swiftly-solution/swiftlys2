
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgSource1LegacyGameEvent : ITypedProtobuf<CMsgSource1LegacyGameEvent>, INetMessage<CMsgSource1LegacyGameEvent>, IDisposable
{
  static int INetMessage<CMsgSource1LegacyGameEvent>.MessageId => 207;
  
  static string INetMessage<CMsgSource1LegacyGameEvent>.MessageName => "CMsgSource1LegacyGameEvent";

  static CMsgSource1LegacyGameEvent ITypedProtobuf<CMsgSource1LegacyGameEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource1LegacyGameEventImpl(handle, isManuallyAllocated);


  public string EventName { get; set; }


  public int Eventid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEvent_key_t> Keys { get; }


  public int ServerTick { get; set; }


  public int Passthrough { get; set; }

}


using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_GameEvent : ITypedProtobuf<CSVCMsg_GameEvent>
{
  static CSVCMsg_GameEvent ITypedProtobuf<CSVCMsg_GameEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_GameEventImpl(handle, isManuallyAllocated);


  public string EventName { get; set; }


  public int Eventid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_GameEvent_key_t> Keys { get; }

}

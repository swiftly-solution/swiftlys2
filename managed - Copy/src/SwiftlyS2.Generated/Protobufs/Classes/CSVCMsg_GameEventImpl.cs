
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_GameEventImpl : TypedProtobuf<CSVCMsg_GameEvent>, CSVCMsg_GameEvent
{
  public CSVCMsg_GameEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string EventName
  { get => Accessor.GetString("event_name"); set => Accessor.SetString("event_name", value); }


  public int Eventid
  { get => Accessor.GetInt32("eventid"); set => Accessor.SetInt32("eventid", value); }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_GameEvent_key_t> Keys
  { get => new ProtobufRepeatedFieldSubMessageType<CSVCMsg_GameEvent_key_t>(Accessor, "keys"); }

}

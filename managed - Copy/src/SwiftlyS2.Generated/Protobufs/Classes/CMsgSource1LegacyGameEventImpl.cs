
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource1LegacyGameEventImpl : NetMessage<CMsgSource1LegacyGameEvent>, CMsgSource1LegacyGameEvent
{
  public CMsgSource1LegacyGameEventImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string EventName
  { get => Accessor.GetString("event_name"); set => Accessor.SetString("event_name", value); }


  public int Eventid
  { get => Accessor.GetInt32("eventid"); set => Accessor.SetInt32("eventid", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEvent_key_t> Keys
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEvent_key_t>(Accessor, "keys"); }


  public int ServerTick
  { get => Accessor.GetInt32("server_tick"); set => Accessor.SetInt32("server_tick", value); }


  public int Passthrough
  { get => Accessor.GetInt32("passthrough"); set => Accessor.SetInt32("passthrough", value); }

}


using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_CustomGameEventImpl : TypedProtobuf<CUserMsg_CustomGameEvent>, CUserMsg_CustomGameEvent
{
  public CUserMsg_CustomGameEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string EventName
  { get => Accessor.GetString("event_name"); set => Accessor.SetString("event_name", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}

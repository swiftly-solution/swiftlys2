
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsgList_GameEvents_event_tImpl : TypedProtobuf<CSVCMsgList_GameEvents_event_t>, CSVCMsgList_GameEvents_event_t
{
  public CSVCMsgList_GameEvents_event_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Tick
  { get => Accessor.GetInt32("tick"); set => Accessor.SetInt32("tick", value); }


  public CSVCMsg_GameEvent Event
  { get => new CSVCMsg_GameEventImpl(NativeNetMessages.GetNestedMessage(Address, "event"), false); }

}

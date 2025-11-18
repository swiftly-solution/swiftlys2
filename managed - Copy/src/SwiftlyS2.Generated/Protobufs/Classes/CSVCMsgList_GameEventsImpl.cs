
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsgList_GameEventsImpl : TypedProtobuf<CSVCMsgList_GameEvents>, CSVCMsgList_GameEvents
{
  public CSVCMsgList_GameEventsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsgList_GameEvents_event_t> Events
  { get => new ProtobufRepeatedFieldSubMessageType<CSVCMsgList_GameEvents_event_t>(Accessor, "events"); }

}

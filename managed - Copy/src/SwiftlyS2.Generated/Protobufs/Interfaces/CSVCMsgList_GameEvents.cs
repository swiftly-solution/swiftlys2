
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsgList_GameEvents : ITypedProtobuf<CSVCMsgList_GameEvents>
{
  static CSVCMsgList_GameEvents ITypedProtobuf<CSVCMsgList_GameEvents>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsgList_GameEventsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CSVCMsgList_GameEvents_event_t> Events { get; }

}

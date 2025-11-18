
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsgList_GameEvents_event_t : ITypedProtobuf<CSVCMsgList_GameEvents_event_t>
{
  static CSVCMsgList_GameEvents_event_t ITypedProtobuf<CSVCMsgList_GameEvents_event_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsgList_GameEvents_event_tImpl(handle, isManuallyAllocated);


  public int Tick { get; set; }


  public CSVCMsg_GameEvent Event { get; }

}

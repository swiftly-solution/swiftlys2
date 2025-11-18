
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_NextMsgPredictedImpl : NetMessage<CSVCMsg_NextMsgPredicted>, CSVCMsg_NextMsgPredicted
{
  public CSVCMsg_NextMsgPredictedImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int PredictedByPlayerSlot
  { get => Accessor.GetInt32("predicted_by_player_slot"); set => Accessor.SetInt32("predicted_by_player_slot", value); }


  public uint MessageTypeId
  { get => Accessor.GetUInt32("message_type_id"); set => Accessor.SetUInt32("message_type_id", value); }

}

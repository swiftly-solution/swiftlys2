
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_NextMsgPredicted : ITypedProtobuf<CSVCMsg_NextMsgPredicted>, INetMessage<CSVCMsg_NextMsgPredicted>, IDisposable
{
  static int INetMessage<CSVCMsg_NextMsgPredicted>.MessageId => 77;
  
  static string INetMessage<CSVCMsg_NextMsgPredicted>.MessageName => "CSVCMsg_NextMsgPredicted";

  static CSVCMsg_NextMsgPredicted ITypedProtobuf<CSVCMsg_NextMsgPredicted>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_NextMsgPredictedImpl(handle, isManuallyAllocated);


  public int PredictedByPlayerSlot { get; set; }


  public uint MessageTypeId { get; set; }

}


using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGCToGCMsgMasterAck_Response : ITypedProtobuf<CGCToGCMsgMasterAck_Response>
{
  static CGCToGCMsgMasterAck_Response ITypedProtobuf<CGCToGCMsgMasterAck_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CGCToGCMsgMasterAck_ResponseImpl(handle, isManuallyAllocated);


  public int Eresult { get; set; }

}

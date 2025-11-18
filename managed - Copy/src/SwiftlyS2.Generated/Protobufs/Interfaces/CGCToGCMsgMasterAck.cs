
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGCToGCMsgMasterAck : ITypedProtobuf<CGCToGCMsgMasterAck>
{
  static CGCToGCMsgMasterAck ITypedProtobuf<CGCToGCMsgMasterAck>.Wrap(nint handle, bool isManuallyAllocated) => new CGCToGCMsgMasterAckImpl(handle, isManuallyAllocated);


  public uint DirIndex { get; set; }


  public uint GcType { get; set; }

}

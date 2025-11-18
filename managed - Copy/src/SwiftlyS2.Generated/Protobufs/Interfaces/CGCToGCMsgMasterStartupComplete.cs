
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGCToGCMsgMasterStartupComplete : ITypedProtobuf<CGCToGCMsgMasterStartupComplete>
{
  static CGCToGCMsgMasterStartupComplete ITypedProtobuf<CGCToGCMsgMasterStartupComplete>.Wrap(nint handle, bool isManuallyAllocated) => new CGCToGCMsgMasterStartupCompleteImpl(handle, isManuallyAllocated);


}

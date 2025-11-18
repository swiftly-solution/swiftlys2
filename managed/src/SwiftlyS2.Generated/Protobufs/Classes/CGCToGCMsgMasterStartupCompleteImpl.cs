using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGCToGCMsgMasterStartupCompleteImpl : TypedProtobuf<CGCToGCMsgMasterStartupComplete>, CGCToGCMsgMasterStartupComplete
{
    public CGCToGCMsgMasterStartupCompleteImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


}

using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_Unsubscribe_ResponseImpl : TypedProtobuf<CPublishedFile_Unsubscribe_Response>, CPublishedFile_Unsubscribe_Response
{
    public CPublishedFile_Unsubscribe_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}
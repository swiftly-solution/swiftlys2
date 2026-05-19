using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_Subscribe_ResponseImpl : TypedProtobuf<CPublishedFile_Subscribe_Response>, CPublishedFile_Subscribe_Response
{
    public CPublishedFile_Subscribe_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}
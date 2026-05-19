using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_Subscribe_Response : ITypedProtobuf<CPublishedFile_Subscribe_Response>
{
    static CPublishedFile_Subscribe_Response ITypedProtobuf<CPublishedFile_Subscribe_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_Subscribe_ResponseImpl(handle, isManuallyAllocated);

}
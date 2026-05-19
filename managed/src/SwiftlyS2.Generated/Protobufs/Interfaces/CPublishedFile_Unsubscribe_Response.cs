using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_Unsubscribe_Response : ITypedProtobuf<CPublishedFile_Unsubscribe_Response>
{
    static CPublishedFile_Unsubscribe_Response ITypedProtobuf<CPublishedFile_Unsubscribe_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_Unsubscribe_ResponseImpl(handle, isManuallyAllocated);

}
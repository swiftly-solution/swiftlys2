using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_Update_Response : ITypedProtobuf<CPublishedFile_Update_Response>
{
    static CPublishedFile_Update_Response ITypedProtobuf<CPublishedFile_Update_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_Update_ResponseImpl(handle, isManuallyAllocated);

}
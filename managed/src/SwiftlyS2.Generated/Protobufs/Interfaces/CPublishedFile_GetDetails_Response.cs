using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_GetDetails_Response : ITypedProtobuf<CPublishedFile_GetDetails_Response>
{
    static CPublishedFile_GetDetails_Response ITypedProtobuf<CPublishedFile_GetDetails_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_GetDetails_ResponseImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails> Publishedfiledetails { get; }
}
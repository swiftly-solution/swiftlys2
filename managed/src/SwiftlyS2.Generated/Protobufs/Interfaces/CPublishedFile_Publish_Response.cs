using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_Publish_Response : ITypedProtobuf<CPublishedFile_Publish_Response>
{
    static CPublishedFile_Publish_Response ITypedProtobuf<CPublishedFile_Publish_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_Publish_ResponseImpl(handle, isManuallyAllocated);

    public ulong Publishedfileid { get; set; }
    public string RedirectUri { get; set; }
}
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_GetUserFiles_Response : ITypedProtobuf<CPublishedFile_GetUserFiles_Response>
{
    static CPublishedFile_GetUserFiles_Response ITypedProtobuf<CPublishedFile_GetUserFiles_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_GetUserFiles_ResponseImpl(handle, isManuallyAllocated);

    public uint Total { get; set; }
    public uint Startindex { get; set; }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails> Publishedfiledetails { get; }
    public IProtobufRepeatedFieldSubMessageType<CPublishedFile_GetUserFiles_Response_App> Apps { get; }
}
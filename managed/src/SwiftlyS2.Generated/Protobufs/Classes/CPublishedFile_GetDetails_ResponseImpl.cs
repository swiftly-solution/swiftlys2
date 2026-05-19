using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_GetDetails_ResponseImpl : TypedProtobuf<CPublishedFile_GetDetails_Response>, CPublishedFile_GetDetails_Response
{
    public CPublishedFile_GetDetails_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails> Publishedfiledetails
    { get => new ProtobufRepeatedFieldSubMessageType<PublishedFileDetails>(Accessor, "publishedfiledetails"); }
}
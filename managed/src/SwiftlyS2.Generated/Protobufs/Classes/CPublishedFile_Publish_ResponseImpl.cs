using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_Publish_ResponseImpl : TypedProtobuf<CPublishedFile_Publish_Response>, CPublishedFile_Publish_Response
{
    public CPublishedFile_Publish_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Publishedfileid
    { get => Accessor.GetUInt64("publishedfileid"); set => Accessor.SetUInt64("publishedfileid", value); }
    public string RedirectUri
    { get => Accessor.GetString("redirect_uri"); set => Accessor.SetString("redirect_uri", value); }
}
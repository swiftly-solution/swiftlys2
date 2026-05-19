using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_GetUploadServerInfo_ResponseImpl : TypedProtobuf<CCloud_GetUploadServerInfo_Response>, CCloud_GetUploadServerInfo_Response
{
    public CCloud_GetUploadServerInfo_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string ServerUrl
    { get => Accessor.GetString("server_url"); set => Accessor.SetString("server_url", value); }
}
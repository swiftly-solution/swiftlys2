using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_GetUploadServerInfo_Response : ITypedProtobuf<CCloud_GetUploadServerInfo_Response>
{
    static CCloud_GetUploadServerInfo_Response ITypedProtobuf<CCloud_GetUploadServerInfo_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_GetUploadServerInfo_ResponseImpl(handle, isManuallyAllocated);

    public string ServerUrl { get; set; }
}
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_GetUploadServerInfo_Request : ITypedProtobuf<CCloud_GetUploadServerInfo_Request>
{
    static CCloud_GetUploadServerInfo_Request ITypedProtobuf<CCloud_GetUploadServerInfo_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_GetUploadServerInfo_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
}
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_GetFileDetails_Request : ITypedProtobuf<CCloud_GetFileDetails_Request>
{
    static CCloud_GetFileDetails_Request ITypedProtobuf<CCloud_GetFileDetails_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_GetFileDetails_RequestImpl(handle, isManuallyAllocated);

    public ulong Ugcid { get; set; }
    public uint Appid { get; set; }
}
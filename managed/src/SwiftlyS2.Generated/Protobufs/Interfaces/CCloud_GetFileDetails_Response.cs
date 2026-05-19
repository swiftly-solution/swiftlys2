using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_GetFileDetails_Response : ITypedProtobuf<CCloud_GetFileDetails_Response>
{
    static CCloud_GetFileDetails_Response ITypedProtobuf<CCloud_GetFileDetails_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_GetFileDetails_ResponseImpl(handle, isManuallyAllocated);

    public CCloud_UserFile Details { get; }
}
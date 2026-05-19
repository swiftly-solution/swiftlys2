using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_Delete_Response : ITypedProtobuf<CCloud_Delete_Response>
{
    static CCloud_Delete_Response ITypedProtobuf<CCloud_Delete_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_Delete_ResponseImpl(handle, isManuallyAllocated);

}
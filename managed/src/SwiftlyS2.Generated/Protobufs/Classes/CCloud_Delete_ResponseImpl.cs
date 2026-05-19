using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_Delete_ResponseImpl : TypedProtobuf<CCloud_Delete_Response>, CCloud_Delete_Response
{
    public CCloud_Delete_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}
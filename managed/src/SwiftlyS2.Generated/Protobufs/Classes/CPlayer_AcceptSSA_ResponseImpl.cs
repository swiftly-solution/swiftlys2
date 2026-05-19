using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_AcceptSSA_ResponseImpl : TypedProtobuf<CPlayer_AcceptSSA_Response>, CPlayer_AcceptSSA_Response
{
    public CPlayer_AcceptSSA_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}
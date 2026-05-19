using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_AcceptSSA_RequestImpl : TypedProtobuf<CPlayer_AcceptSSA_Request>, CPlayer_AcceptSSA_Request
{
    public CPlayer_AcceptSSA_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}
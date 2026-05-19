using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_AcceptSSA_Response : ITypedProtobuf<CPlayer_AcceptSSA_Response>
{
    static CPlayer_AcceptSSA_Response ITypedProtobuf<CPlayer_AcceptSSA_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_AcceptSSA_ResponseImpl(handle, isManuallyAllocated);

}
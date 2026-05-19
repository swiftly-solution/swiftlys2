using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_AcceptSSA_Request : ITypedProtobuf<CPlayer_AcceptSSA_Request>
{
    static CPlayer_AcceptSSA_Request ITypedProtobuf<CPlayer_AcceptSSA_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_AcceptSSA_RequestImpl(handle, isManuallyAllocated);

}
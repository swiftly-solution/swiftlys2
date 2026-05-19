using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_IgnoreFriend_Response : ITypedProtobuf<CPlayer_IgnoreFriend_Response>
{
    static CPlayer_IgnoreFriend_Response ITypedProtobuf<CPlayer_IgnoreFriend_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_IgnoreFriend_ResponseImpl(handle, isManuallyAllocated);

    public uint FriendRelationship { get; set; }
}
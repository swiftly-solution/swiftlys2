using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_RemoveFriend_Response : ITypedProtobuf<CPlayer_RemoveFriend_Response>
{
    static CPlayer_RemoveFriend_Response ITypedProtobuf<CPlayer_RemoveFriend_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_RemoveFriend_ResponseImpl(handle, isManuallyAllocated);

    public uint FriendRelationship { get; set; }
}
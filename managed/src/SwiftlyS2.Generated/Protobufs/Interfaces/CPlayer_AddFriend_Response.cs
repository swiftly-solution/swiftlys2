using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_AddFriend_Response : ITypedProtobuf<CPlayer_AddFriend_Response>
{
    static CPlayer_AddFriend_Response ITypedProtobuf<CPlayer_AddFriend_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_AddFriend_ResponseImpl(handle, isManuallyAllocated);

    public bool InviteSent { get; set; }
    public uint FriendRelationship { get; set; }
}
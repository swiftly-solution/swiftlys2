using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetMutualFriendsForIncomingInvites_Response : ITypedProtobuf<CPlayer_GetMutualFriendsForIncomingInvites_Response>
{
    static CPlayer_GetMutualFriendsForIncomingInvites_Response ITypedProtobuf<CPlayer_GetMutualFriendsForIncomingInvites_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetMutualFriendsForIncomingInvites_ResponseImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<CPlayer_IncomingInviteMutualFriendList> IncomingInviteMutualFriendsLists { get; }
}
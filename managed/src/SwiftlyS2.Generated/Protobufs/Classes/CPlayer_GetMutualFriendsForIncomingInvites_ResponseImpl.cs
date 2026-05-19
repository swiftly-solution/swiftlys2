using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetMutualFriendsForIncomingInvites_ResponseImpl : TypedProtobuf<CPlayer_GetMutualFriendsForIncomingInvites_Response>, CPlayer_GetMutualFriendsForIncomingInvites_Response
{
    public CPlayer_GetMutualFriendsForIncomingInvites_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldSubMessageType<CPlayer_IncomingInviteMutualFriendList> IncomingInviteMutualFriendsLists
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_IncomingInviteMutualFriendList>(Accessor, "incoming_invite_mutual_friends_lists"); }
}
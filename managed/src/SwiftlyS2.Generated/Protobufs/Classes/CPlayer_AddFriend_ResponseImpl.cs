using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_AddFriend_ResponseImpl : TypedProtobuf<CPlayer_AddFriend_Response>, CPlayer_AddFriend_Response
{
    public CPlayer_AddFriend_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public bool InviteSent
    { get => Accessor.GetBool("invite_sent"); set => Accessor.SetBool("invite_sent", value); }
    public uint FriendRelationship
    { get => Accessor.GetUInt32("friend_relationship"); set => Accessor.SetUInt32("friend_relationship", value); }
}
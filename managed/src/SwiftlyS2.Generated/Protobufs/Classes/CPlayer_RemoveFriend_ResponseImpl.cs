using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_RemoveFriend_ResponseImpl : TypedProtobuf<CPlayer_RemoveFriend_Response>, CPlayer_RemoveFriend_Response
{
    public CPlayer_RemoveFriend_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint FriendRelationship
    { get => Accessor.GetUInt32("friend_relationship"); set => Accessor.SetUInt32("friend_relationship", value); }
}
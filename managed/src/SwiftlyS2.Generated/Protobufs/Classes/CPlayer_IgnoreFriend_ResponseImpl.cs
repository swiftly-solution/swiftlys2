using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_IgnoreFriend_ResponseImpl : TypedProtobuf<CPlayer_IgnoreFriend_Response>, CPlayer_IgnoreFriend_Response
{
    public CPlayer_IgnoreFriend_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint FriendRelationship
    { get => Accessor.GetUInt32("friend_relationship"); set => Accessor.SetUInt32("friend_relationship", value); }
}
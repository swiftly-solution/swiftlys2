using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_IncomingInviteMutualFriendListImpl : TypedProtobuf<CPlayer_IncomingInviteMutualFriendList>, CPlayer_IncomingInviteMutualFriendList
{
    public CPlayer_IncomingInviteMutualFriendListImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Steamid
    { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }
    public IProtobufRepeatedFieldValueType<uint> MutualFriendAccountIds
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "mutual_friend_account_ids"); }
}
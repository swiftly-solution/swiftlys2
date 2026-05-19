using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_IncomingInviteMutualFriendList : ITypedProtobuf<CPlayer_IncomingInviteMutualFriendList>
{
    static CPlayer_IncomingInviteMutualFriendList ITypedProtobuf<CPlayer_IncomingInviteMutualFriendList>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_IncomingInviteMutualFriendListImpl(handle, isManuallyAllocated);

    public ulong Steamid { get; set; }
    public IProtobufRepeatedFieldValueType<uint> MutualFriendAccountIds { get; }
}
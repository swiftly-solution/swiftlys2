using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo : ITypedProtobuf<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo>
{
    static CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo ITypedProtobuf<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfoImpl(handle, isManuallyAllocated);

    public ulong Steamid { get; set; }
    public uint MinutesPlayed { get; set; }
    public uint MinutesPlayedForever { get; set; }
}
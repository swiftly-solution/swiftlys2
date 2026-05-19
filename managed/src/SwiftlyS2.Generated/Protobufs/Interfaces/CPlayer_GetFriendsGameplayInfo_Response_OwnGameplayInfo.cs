using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetFriendsGameplayInfo_Response_OwnGameplayInfo : ITypedProtobuf<CPlayer_GetFriendsGameplayInfo_Response_OwnGameplayInfo>
{
    static CPlayer_GetFriendsGameplayInfo_Response_OwnGameplayInfo ITypedProtobuf<CPlayer_GetFriendsGameplayInfo_Response_OwnGameplayInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetFriendsGameplayInfo_Response_OwnGameplayInfoImpl(handle, isManuallyAllocated);

    public ulong Steamid { get; set; }
    public uint MinutesPlayed { get; set; }
    public uint MinutesPlayedForever { get; set; }
    public bool InWishlist { get; set; }
    public bool Owned { get; set; }
}
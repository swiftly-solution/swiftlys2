using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfoImpl : TypedProtobuf<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo>, CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo
{
    public CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfoImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Steamid
    { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }
    public uint MinutesPlayed
    { get => Accessor.GetUInt32("minutes_played"); set => Accessor.SetUInt32("minutes_played", value); }
    public uint MinutesPlayedForever
    { get => Accessor.GetUInt32("minutes_played_forever"); set => Accessor.SetUInt32("minutes_played_forever", value); }
}
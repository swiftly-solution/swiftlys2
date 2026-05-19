using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetLastPlayedTimes_Response_GameImpl : TypedProtobuf<CPlayer_GetLastPlayedTimes_Response_Game>, CPlayer_GetLastPlayedTimes_Response_Game
{
    public CPlayer_GetLastPlayedTimes_Response_GameImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public int Appid
    { get => Accessor.GetInt32("appid"); set => Accessor.SetInt32("appid", value); }
    public uint LastPlaytime
    { get => Accessor.GetUInt32("last_playtime"); set => Accessor.SetUInt32("last_playtime", value); }
    public int Playtime2weeks
    { get => Accessor.GetInt32("playtime_2weeks"); set => Accessor.SetInt32("playtime_2weeks", value); }
    public int PlaytimeForever
    { get => Accessor.GetInt32("playtime_forever"); set => Accessor.SetInt32("playtime_forever", value); }
    public uint FirstPlaytime
    { get => Accessor.GetUInt32("first_playtime"); set => Accessor.SetUInt32("first_playtime", value); }
}
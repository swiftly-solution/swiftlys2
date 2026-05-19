using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetLastPlayedTimes_Response_Game : ITypedProtobuf<CPlayer_GetLastPlayedTimes_Response_Game>
{
    static CPlayer_GetLastPlayedTimes_Response_Game ITypedProtobuf<CPlayer_GetLastPlayedTimes_Response_Game>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetLastPlayedTimes_Response_GameImpl(handle, isManuallyAllocated);

    public int Appid { get; set; }
    public uint LastPlaytime { get; set; }
    public int Playtime2weeks { get; set; }
    public int PlaytimeForever { get; set; }
    public uint FirstPlaytime { get; set; }
}
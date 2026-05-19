using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetGameBadgeLevels_Request : ITypedProtobuf<CPlayer_GetGameBadgeLevels_Request>
{
    static CPlayer_GetGameBadgeLevels_Request ITypedProtobuf<CPlayer_GetGameBadgeLevels_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetGameBadgeLevels_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
}
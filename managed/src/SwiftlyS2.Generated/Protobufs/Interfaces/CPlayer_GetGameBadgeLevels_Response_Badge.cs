using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetGameBadgeLevels_Response_Badge : ITypedProtobuf<CPlayer_GetGameBadgeLevels_Response_Badge>
{
    static CPlayer_GetGameBadgeLevels_Response_Badge ITypedProtobuf<CPlayer_GetGameBadgeLevels_Response_Badge>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetGameBadgeLevels_Response_BadgeImpl(handle, isManuallyAllocated);

    public int Level { get; set; }
    public int Series { get; set; }
    public uint BorderColor { get; set; }
}
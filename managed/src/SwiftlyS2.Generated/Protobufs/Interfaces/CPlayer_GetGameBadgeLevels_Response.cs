using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetGameBadgeLevels_Response : ITypedProtobuf<CPlayer_GetGameBadgeLevels_Response>
{
    static CPlayer_GetGameBadgeLevels_Response ITypedProtobuf<CPlayer_GetGameBadgeLevels_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetGameBadgeLevels_ResponseImpl(handle, isManuallyAllocated);

    public uint PlayerLevel { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetGameBadgeLevels_Response_Badge> Badges { get; }
}
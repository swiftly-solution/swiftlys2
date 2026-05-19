using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetGameBadgeLevels_ResponseImpl : TypedProtobuf<CPlayer_GetGameBadgeLevels_Response>, CPlayer_GetGameBadgeLevels_Response
{
    public CPlayer_GetGameBadgeLevels_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint PlayerLevel
    { get => Accessor.GetUInt32("player_level"); set => Accessor.SetUInt32("player_level", value); }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetGameBadgeLevels_Response_Badge> Badges
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_GetGameBadgeLevels_Response_Badge>(Accessor, "badges"); }
}
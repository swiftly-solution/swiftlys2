using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetGameBadgeLevels_Response_BadgeImpl : TypedProtobuf<CPlayer_GetGameBadgeLevels_Response_Badge>, CPlayer_GetGameBadgeLevels_Response_Badge
{
    public CPlayer_GetGameBadgeLevels_Response_BadgeImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public int Level
    { get => Accessor.GetInt32("level"); set => Accessor.SetInt32("level", value); }
    public int Series
    { get => Accessor.GetInt32("series"); set => Accessor.SetInt32("series", value); }
    public uint BorderColor
    { get => Accessor.GetUInt32("border_color"); set => Accessor.SetUInt32("border_color", value); }
}
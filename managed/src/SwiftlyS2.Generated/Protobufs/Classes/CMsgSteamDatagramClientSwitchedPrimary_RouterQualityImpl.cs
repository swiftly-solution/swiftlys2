using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramClientSwitchedPrimary_RouterQualityImpl : TypedProtobuf<CMsgSteamDatagramClientSwitchedPrimary_RouterQuality>, CMsgSteamDatagramClientSwitchedPrimary_RouterQuality
{
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQualityImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Score
    { get => Accessor.GetUInt32("score"); set => Accessor.SetUInt32("score", value); }
    public uint FrontPing
    { get => Accessor.GetUInt32("front_ping"); set => Accessor.SetUInt32("front_ping", value); }
    public uint BackPing
    { get => Accessor.GetUInt32("back_ping"); set => Accessor.SetUInt32("back_ping", value); }
    public uint SecondsUntilDown
    { get => Accessor.GetUInt32("seconds_until_down"); set => Accessor.SetUInt32("seconds_until_down", value); }
}
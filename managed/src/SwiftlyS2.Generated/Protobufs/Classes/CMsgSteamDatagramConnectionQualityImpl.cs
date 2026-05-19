using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramConnectionQualityImpl : TypedProtobuf<CMsgSteamDatagramConnectionQuality>, CMsgSteamDatagramConnectionQuality
{
    public CMsgSteamDatagramConnectionQualityImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramLinkInstantaneousStats Instantaneous
    { get => new CMsgSteamDatagramLinkInstantaneousStatsImpl(NativeNetMessages.GetNestedMessage(Address, "instantaneous"), false); }
    public CMsgSteamDatagramLinkLifetimeStats Lifetime
    { get => new CMsgSteamDatagramLinkLifetimeStatsImpl(NativeNetMessages.GetNestedMessage(Address, "lifetime"), false); }
}
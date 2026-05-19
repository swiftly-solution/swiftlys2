using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectionQuality : ITypedProtobuf<CMsgSteamDatagramConnectionQuality>
{
    static CMsgSteamDatagramConnectionQuality ITypedProtobuf<CMsgSteamDatagramConnectionQuality>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectionQualityImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramLinkInstantaneousStats Instantaneous { get; }
    public CMsgSteamDatagramLinkLifetimeStats Lifetime { get; }
}
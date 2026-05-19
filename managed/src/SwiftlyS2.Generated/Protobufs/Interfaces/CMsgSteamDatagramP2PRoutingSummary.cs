using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PRoutingSummary : ITypedProtobuf<CMsgSteamDatagramP2PRoutingSummary>
{
    static CMsgSteamDatagramP2PRoutingSummary ITypedProtobuf<CMsgSteamDatagramP2PRoutingSummary>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PRoutingSummaryImpl(handle, isManuallyAllocated);

    public CMsgSteamNetworkingICESessionSummary Ice { get; }
    public CMsgSteamNetworkingP2PSDRRoutingSummary Sdr { get; }
}
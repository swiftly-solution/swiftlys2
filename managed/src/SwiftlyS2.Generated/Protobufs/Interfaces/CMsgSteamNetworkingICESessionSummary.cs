using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingICESessionSummary : ITypedProtobuf<CMsgSteamNetworkingICESessionSummary>
{
    static CMsgSteamNetworkingICESessionSummary ITypedProtobuf<CMsgSteamNetworkingICESessionSummary>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingICESessionSummaryImpl(handle, isManuallyAllocated);

    public uint FailureReasonCode { get; set; }
    public uint LocalCandidateTypes { get; set; }
    public uint RemoteCandidateTypes { get; set; }
    public uint InitialRouteKind { get; set; }
    public uint InitialPing { get; set; }
    public uint InitialScore { get; set; }
    public uint NegotiationMs { get; set; }
    public uint BestRouteKind { get; set; }
    public uint BestPing { get; set; }
    public uint BestScore { get; set; }
    public uint BestTime { get; set; }
    public uint SelectedSeconds { get; set; }
    public uint UserSettings { get; set; }
    public uint IceEnableVar { get; set; }
    public uint LocalCandidateTypesAllowed { get; set; }
}
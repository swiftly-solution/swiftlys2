using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingICESessionSummaryImpl : TypedProtobuf<CMsgSteamNetworkingICESessionSummary>, CMsgSteamNetworkingICESessionSummary
{
    public CMsgSteamNetworkingICESessionSummaryImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint FailureReasonCode
    { get => Accessor.GetUInt32("failure_reason_code"); set => Accessor.SetUInt32("failure_reason_code", value); }
    public uint LocalCandidateTypes
    { get => Accessor.GetUInt32("local_candidate_types"); set => Accessor.SetUInt32("local_candidate_types", value); }
    public uint RemoteCandidateTypes
    { get => Accessor.GetUInt32("remote_candidate_types"); set => Accessor.SetUInt32("remote_candidate_types", value); }
    public uint InitialRouteKind
    { get => Accessor.GetUInt32("initial_route_kind"); set => Accessor.SetUInt32("initial_route_kind", value); }
    public uint InitialPing
    { get => Accessor.GetUInt32("initial_ping"); set => Accessor.SetUInt32("initial_ping", value); }
    public uint InitialScore
    { get => Accessor.GetUInt32("initial_score"); set => Accessor.SetUInt32("initial_score", value); }
    public uint NegotiationMs
    { get => Accessor.GetUInt32("negotiation_ms"); set => Accessor.SetUInt32("negotiation_ms", value); }
    public uint BestRouteKind
    { get => Accessor.GetUInt32("best_route_kind"); set => Accessor.SetUInt32("best_route_kind", value); }
    public uint BestPing
    { get => Accessor.GetUInt32("best_ping"); set => Accessor.SetUInt32("best_ping", value); }
    public uint BestScore
    { get => Accessor.GetUInt32("best_score"); set => Accessor.SetUInt32("best_score", value); }
    public uint BestTime
    { get => Accessor.GetUInt32("best_time"); set => Accessor.SetUInt32("best_time", value); }
    public uint SelectedSeconds
    { get => Accessor.GetUInt32("selected_seconds"); set => Accessor.SetUInt32("selected_seconds", value); }
    public uint UserSettings
    { get => Accessor.GetUInt32("user_settings"); set => Accessor.SetUInt32("user_settings", value); }
    public uint IceEnableVar
    { get => Accessor.GetUInt32("ice_enable_var"); set => Accessor.SetUInt32("ice_enable_var", value); }
    public uint LocalCandidateTypesAllowed
    { get => Accessor.GetUInt32("local_candidate_types_allowed"); set => Accessor.SetUInt32("local_candidate_types_allowed", value); }
}
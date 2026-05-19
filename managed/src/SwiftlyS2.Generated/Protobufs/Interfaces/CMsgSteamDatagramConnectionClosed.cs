using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectionClosed : ITypedProtobuf<CMsgSteamDatagramConnectionClosed>
{
    static CMsgSteamDatagramConnectionClosed ITypedProtobuf<CMsgSteamDatagramConnectionClosed>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectionClosedImpl(handle, isManuallyAllocated);

    public uint ToConnectionId { get; set; }
    public uint FromConnectionId { get; set; }
    public string FromIdentityString { get; set; }
    public CMsgSteamNetworkingIdentityLegacyBinary LegacyFromIdentityBinary { get; }
    public ulong LegacyFromSteamId { get; set; }
    public uint LegacyGameserverRelaySessionId { get; set; }
    public uint ToRelaySessionId { get; set; }
    public uint FromRelaySessionId { get; set; }
    public byte[] ForwardTargetRelayRoutingToken { get; set; }
    public uint ForwardTargetRevision { get; set; }
    public CMsgSteamDatagramConnectionClosed_ERelayMode RelayMode { get; set; }
    public string Debug { get; set; }
    public uint ReasonCode { get; set; }
    public ulong RoutingSecret { get; set; }
    public bool NotPrimarySession { get; set; }
    public bool NotPrimaryTransport { get; set; }
    public bool RelayOverrideActive { get; set; }
    public CMsgSteamDatagramConnectionQuality QualityRelay { get; }
    public CMsgSteamDatagramConnectionQuality QualityE2e { get; }
    public CMsgSteamDatagramP2PRoutingSummary P2pRoutingSummary { get; }
}
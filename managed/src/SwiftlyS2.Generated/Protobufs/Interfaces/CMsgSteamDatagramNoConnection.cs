using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramNoConnection : ITypedProtobuf<CMsgSteamDatagramNoConnection>
{
    static CMsgSteamDatagramNoConnection ITypedProtobuf<CMsgSteamDatagramNoConnection>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramNoConnectionImpl(handle, isManuallyAllocated);

    public uint ToConnectionId { get; set; }
    public uint FromConnectionId { get; set; }
    public uint LegacyGameserverRelaySessionId { get; set; }
    public uint ToRelaySessionId { get; set; }
    public uint FromRelaySessionId { get; set; }
    public string FromIdentityString { get; set; }
    public ulong LegacyFromSteamId { get; set; }
    public bool EndToEnd { get; set; }
    public bool NotPrimarySession { get; set; }
    public bool NotPrimaryTransport { get; set; }
    public bool RelayOverrideActive { get; set; }
    public CMsgSteamDatagramConnectionQuality QualityRelay { get; }
    public CMsgSteamDatagramConnectionQuality QualityE2e { get; }
    public CMsgSteamDatagramP2PRoutingSummary P2pRoutingSummary { get; }
    public ulong RoutingSecret { get; set; }
    public uint DummyPad { get; set; }
}
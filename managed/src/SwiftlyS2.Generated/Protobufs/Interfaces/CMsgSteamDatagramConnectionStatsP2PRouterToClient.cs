using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectionStatsP2PRouterToClient : ITypedProtobuf<CMsgSteamDatagramConnectionStatsP2PRouterToClient>
{
    static CMsgSteamDatagramConnectionStatsP2PRouterToClient ITypedProtobuf<CMsgSteamDatagramConnectionStatsP2PRouterToClient>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectionStatsP2PRouterToClientImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramConnectionQuality QualityRelay { get; }
    public CMsgSteamDatagramConnectionQuality QualityE2e { get; }
    public uint SecondsUntilShutdown { get; set; }
    public uint MigrateRequestIp { get; set; }
    public uint MigrateRequestPort { get; set; }
    public uint ScoringPenaltyRelayCluster { get; set; }
    public IProtobufRepeatedFieldValueType<uint> AckRelay { get; }
    public IProtobufRepeatedFieldValueType<uint> LegacyAckE2e { get; }
    public uint Flags { get; set; }
    public uint AckForwardTargetRevision { get; set; }
    public byte[] Routes { get; set; }
    public uint AckPeerRoutesRevision { get; set; }
    public uint ConnectionId { get; set; }
    public uint SeqNumR2c { get; set; }
    public uint SeqNumE2e { get; set; }
}
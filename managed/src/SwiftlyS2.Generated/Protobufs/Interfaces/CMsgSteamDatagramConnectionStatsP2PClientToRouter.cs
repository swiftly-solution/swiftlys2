using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectionStatsP2PClientToRouter : ITypedProtobuf<CMsgSteamDatagramConnectionStatsP2PClientToRouter>
{
    static CMsgSteamDatagramConnectionStatsP2PClientToRouter ITypedProtobuf<CMsgSteamDatagramConnectionStatsP2PClientToRouter>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectionStatsP2PClientToRouterImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramConnectionQuality QualityRelay { get; }
    public CMsgSteamDatagramConnectionQuality QualityE2e { get; }
    public CMsgSteamDatagramP2PRoutingSummary P2pRoutingSummary { get; }
    public IProtobufRepeatedFieldValueType<uint> AckRelay { get; }
    public IProtobufRepeatedFieldValueType<uint> LegacyAckE2e { get; }
    public uint Flags { get; set; }
    public byte[] ForwardTargetRelayRoutingToken { get; set; }
    public uint ForwardTargetRevision { get; set; }
    public byte[] Routes { get; set; }
    public uint AckPeerRoutesRevision { get; set; }
    public uint ConnectionId { get; set; }
    public uint SeqNumC2r { get; set; }
    public uint SeqNumE2e { get; set; }
}
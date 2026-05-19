using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PRoutes_RelayCluster : ITypedProtobuf<CMsgSteamDatagramP2PRoutes_RelayCluster>
{
    static CMsgSteamDatagramP2PRoutes_RelayCluster ITypedProtobuf<CMsgSteamDatagramP2PRoutes_RelayCluster>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PRoutes_RelayClusterImpl(handle, isManuallyAllocated);

    public uint PopId { get; set; }
    public uint PingMs { get; set; }
    public uint ScorePenalty { get; set; }
    public byte[] SessionRelayRoutingToken { get; set; }
}
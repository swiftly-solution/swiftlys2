using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramP2PRoutes_RelayClusterImpl : TypedProtobuf<CMsgSteamDatagramP2PRoutes_RelayCluster>, CMsgSteamDatagramP2PRoutes_RelayCluster
{
    public CMsgSteamDatagramP2PRoutes_RelayClusterImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint PopId
    { get => Accessor.GetUInt32("pop_id"); set => Accessor.SetUInt32("pop_id", value); }
    public uint PingMs
    { get => Accessor.GetUInt32("ping_ms"); set => Accessor.SetUInt32("ping_ms", value); }
    public uint ScorePenalty
    { get => Accessor.GetUInt32("score_penalty"); set => Accessor.SetUInt32("score_penalty", value); }
    public byte[] SessionRelayRoutingToken
    { get => Accessor.GetBytes("session_relay_routing_token"); set => Accessor.SetBytes("session_relay_routing_token", value); }
}
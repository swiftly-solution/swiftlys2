using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramConnectionStatsP2PClientToRouterImpl : TypedProtobuf<CMsgSteamDatagramConnectionStatsP2PClientToRouter>, CMsgSteamDatagramConnectionStatsP2PClientToRouter
{
    public CMsgSteamDatagramConnectionStatsP2PClientToRouterImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramConnectionQuality QualityRelay
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "quality_relay"), false); }
    public CMsgSteamDatagramConnectionQuality QualityE2e
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "quality_e2e"), false); }
    public CMsgSteamDatagramP2PRoutingSummary P2pRoutingSummary
    { get => new CMsgSteamDatagramP2PRoutingSummaryImpl(NativeNetMessages.GetNestedMessage(Address, "p2p_routing_summary"), false); }
    public IProtobufRepeatedFieldValueType<uint> AckRelay
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "ack_relay"); }
    public IProtobufRepeatedFieldValueType<uint> LegacyAckE2e
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "legacy_ack_e2e"); }
    public uint Flags
    { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }
    public byte[] ForwardTargetRelayRoutingToken
    { get => Accessor.GetBytes("forward_target_relay_routing_token"); set => Accessor.SetBytes("forward_target_relay_routing_token", value); }
    public uint ForwardTargetRevision
    { get => Accessor.GetUInt32("forward_target_revision"); set => Accessor.SetUInt32("forward_target_revision", value); }
    public byte[] Routes
    { get => Accessor.GetBytes("routes"); set => Accessor.SetBytes("routes", value); }
    public uint AckPeerRoutesRevision
    { get => Accessor.GetUInt32("ack_peer_routes_revision"); set => Accessor.SetUInt32("ack_peer_routes_revision", value); }
    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public uint SeqNumC2r
    { get => Accessor.GetUInt32("seq_num_c2r"); set => Accessor.SetUInt32("seq_num_c2r", value); }
    public uint SeqNumE2e
    { get => Accessor.GetUInt32("seq_num_e2e"); set => Accessor.SetUInt32("seq_num_e2e", value); }
}
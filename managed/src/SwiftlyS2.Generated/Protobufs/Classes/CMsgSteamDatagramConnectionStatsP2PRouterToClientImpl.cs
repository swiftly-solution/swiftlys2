using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramConnectionStatsP2PRouterToClientImpl : TypedProtobuf<CMsgSteamDatagramConnectionStatsP2PRouterToClient>, CMsgSteamDatagramConnectionStatsP2PRouterToClient
{
    public CMsgSteamDatagramConnectionStatsP2PRouterToClientImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramConnectionQuality QualityRelay
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "quality_relay"), false); }
    public CMsgSteamDatagramConnectionQuality QualityE2e
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "quality_e2e"), false); }
    public uint SecondsUntilShutdown
    { get => Accessor.GetUInt32("seconds_until_shutdown"); set => Accessor.SetUInt32("seconds_until_shutdown", value); }
    public uint MigrateRequestIp
    { get => Accessor.GetUInt32("migrate_request_ip"); set => Accessor.SetUInt32("migrate_request_ip", value); }
    public uint MigrateRequestPort
    { get => Accessor.GetUInt32("migrate_request_port"); set => Accessor.SetUInt32("migrate_request_port", value); }
    public uint ScoringPenaltyRelayCluster
    { get => Accessor.GetUInt32("scoring_penalty_relay_cluster"); set => Accessor.SetUInt32("scoring_penalty_relay_cluster", value); }
    public IProtobufRepeatedFieldValueType<uint> AckRelay
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "ack_relay"); }
    public IProtobufRepeatedFieldValueType<uint> LegacyAckE2e
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "legacy_ack_e2e"); }
    public uint Flags
    { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }
    public uint AckForwardTargetRevision
    { get => Accessor.GetUInt32("ack_forward_target_revision"); set => Accessor.SetUInt32("ack_forward_target_revision", value); }
    public byte[] Routes
    { get => Accessor.GetBytes("routes"); set => Accessor.SetBytes("routes", value); }
    public uint AckPeerRoutesRevision
    { get => Accessor.GetUInt32("ack_peer_routes_revision"); set => Accessor.SetUInt32("ack_peer_routes_revision", value); }
    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public uint SeqNumR2c
    { get => Accessor.GetUInt32("seq_num_r2c"); set => Accessor.SetUInt32("seq_num_r2c", value); }
    public uint SeqNumE2e
    { get => Accessor.GetUInt32("seq_num_e2e"); set => Accessor.SetUInt32("seq_num_e2e", value); }
}
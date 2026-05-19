using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramClientPingSampleReply_POPImpl : TypedProtobuf<CMsgSteamDatagramClientPingSampleReply_POP>, CMsgSteamDatagramClientPingSampleReply_POP
{
    public CMsgSteamDatagramClientPingSampleReply_POPImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint PopId
    { get => Accessor.GetUInt32("pop_id"); set => Accessor.SetUInt32("pop_id", value); }
    public uint DefaultFrontPingMs
    { get => Accessor.GetUInt32("default_front_ping_ms"); set => Accessor.SetUInt32("default_front_ping_ms", value); }
    public uint ClusterPenalty
    { get => Accessor.GetUInt32("cluster_penalty"); set => Accessor.SetUInt32("cluster_penalty", value); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_POP_AltAddress> AltAddresses
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_POP_AltAddress>(Accessor, "alt_addresses"); }
    public uint DefaultE2ePingMs
    { get => Accessor.GetUInt32("default_e2e_ping_ms"); set => Accessor.SetUInt32("default_e2e_ping_ms", value); }
    public uint DefaultE2eScore
    { get => Accessor.GetUInt32("default_e2e_score"); set => Accessor.SetUInt32("default_e2e_score", value); }
    public uint P2pViaPeerRelayPopId
    { get => Accessor.GetUInt32("p2p_via_peer_relay_pop_id"); set => Accessor.SetUInt32("p2p_via_peer_relay_pop_id", value); }
    public uint BestDcPingMs
    { get => Accessor.GetUInt32("best_dc_ping_ms"); set => Accessor.SetUInt32("best_dc_ping_ms", value); }
    public uint BestDcScore
    { get => Accessor.GetUInt32("best_dc_score"); set => Accessor.SetUInt32("best_dc_score", value); }
    public uint BestDcViaRelayPopId
    { get => Accessor.GetUInt32("best_dc_via_relay_pop_id"); set => Accessor.SetUInt32("best_dc_via_relay_pop_id", value); }
    public uint DefaultDcPingMs
    { get => Accessor.GetUInt32("default_dc_ping_ms"); set => Accessor.SetUInt32("default_dc_ping_ms", value); }
    public uint DefaultDcScore
    { get => Accessor.GetUInt32("default_dc_score"); set => Accessor.SetUInt32("default_dc_score", value); }
    public uint DefaultDcViaRelayPopId
    { get => Accessor.GetUInt32("default_dc_via_relay_pop_id"); set => Accessor.SetUInt32("default_dc_via_relay_pop_id", value); }
    public uint TestDcPingMs
    { get => Accessor.GetUInt32("test_dc_ping_ms"); set => Accessor.SetUInt32("test_dc_ping_ms", value); }
    public uint TestDcScore
    { get => Accessor.GetUInt32("test_dc_score"); set => Accessor.SetUInt32("test_dc_score", value); }
    public uint TestDcViaRelayPopId
    { get => Accessor.GetUInt32("test_dc_via_relay_pop_id"); set => Accessor.SetUInt32("test_dc_via_relay_pop_id", value); }
}